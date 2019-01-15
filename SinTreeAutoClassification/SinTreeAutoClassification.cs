using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using DC_LasReader;

namespace SinTreeAutoClassification
{
  //--- This structure and its counterpart in the Win32 DLL must have identical layouts and member alignments
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public struct LASPointData
  {
    public double x, y, z;
    public ushort intensity;
    public int isTrunk;
  }

  public class SinTreeAutoClassification : IDisposable
  {
    //Turn on 'Allow unsafe code' (Project properties/Build tab)
    [System.Runtime.InteropServices.DllImport("Win32Project1.dll", CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
    public static extern unsafe int Classify(int itemNr, float intensityEstimation, float neighbourhoodEstimation, LASPointData* lasPointArray, byte* classArray);

    private bool disposedValue = false; // To detect redundant calls
    private readonly float intensityEstimation;
    private readonly float neighbourhoodEstimation;
    private readonly List<byte> sourceClasses;
    // private PCS.Vector.TRIConnection groundModel; // currently not used
    private ShapeFile.ShapeCollection treeLocations;
    private decimal approxTreeDiameter = 0.0M;

    public SinTreeAutoClassification(float intensityEstimation, float neighbourhoodEstimation, List<byte> sourceClasses, ShapeFile.ShapeCollection treeLocations)
    {
      this.intensityEstimation = intensityEstimation;
      this.neighbourhoodEstimation = neighbourhoodEstimation;
      this.sourceClasses = sourceClasses;
      // this.groundModel = groundModel;  // currently not used
      this.treeLocations = treeLocations;

      ////Initialize license server connection
      //PCS.Program.CreateSKL();
      //PCS.Program.CreateLicenseServiceConnection0();
      //DC_LasReader.Globals.LicenseService = PCS.Program.LicenseService;
      // TODO: initialize unmamanged parts, reusable resources
    }

    public void Classify(string SingleTreeLasFilePath)
    {
      System.Diagnostics.Trace.WriteLine(String.Format("Classification started of tree: {0}", SingleTreeLasFilePath));

      if (disposedValue) throw new ObjectDisposedException("Object disposed: SinTreeAutoClassification");

      DC_LasReader.DC_LasReader lasFileReaderWriter = new DC_LasReader.DC_LasReader(); //Reader and writer!!!!!

      lasFileReaderWriter.Open(SingleTreeLasFilePath, false);

      List<DC_LasReader.DC_LasPoint> lasPoints = new List<DC_LasReader.DC_LasPoint>(10000); // 10000 is an approximation 

      var guid = Path.GetFileNameWithoutExtension(SingleTreeLasFilePath);
      var treeLoc = GetTreeLocationFromShape(guid);

      #region filter by Voxel model
      var v_size_xy = 1.0;
      var v_size_z = .25;

      // init voxel model
      MathLib.SparseGrid.Sparse3DGridEx<DC_LasReader.DC_LasPoint> plane_voxels = new MathLib.SparseGrid.Sparse3DGridEx<DC_LasPoint>(v_size_xy, v_size_xy, v_size_z, p => p.x, p => p.y, p => p.z);

      if (lasFileReaderWriter.Open(SingleTreeLasFilePath, false))
      {
        foreach (var lasPoint in lasFileReaderWriter.GetPointEnumerator())
        {
          if (sourceClasses.Contains(lasPoint.classification))
          {
            // lasPoint is a PCS point definition, contains many information ... 
            plane_voxels.AddItem(lasPoint, false);
          }
        }
      }
      int numOf = 0;


      //plane_voxels.SaveToPly(SingleTreeLasFilePath.Substring(0, SingleTreeLasFilePath.Length - 4) + "_raw_planes.ply", 0, 0, 0);

      List<Tuple<int, int, int>> exlcudeList = new List<Tuple<int, int, int>>();
      List<Tuple<int, int, int>> bufferList = new List<Tuple<int, int, int>>();

      foreach (var rowCol in plane_voxels.GetEnumerator_RowColIxs())
      {
        int countGaps = 0;
        bool prevExists = false;
        var minPlaneKey = plane_voxels.GetMinPlane();
        var maxPlaneKey = plane_voxels.GetMaxPlane();
        var offset = (double)approxTreeDiameter * 2.5;
        for (int i = minPlaneKey; i <= (minPlaneKey + (maxPlaneKey - minPlaneKey)); i++)
        {
          if (i > minPlaneKey + (maxPlaneKey - minPlaneKey) * 0.20)
          {
            offset += v_size_xy * 0.7;
          }
          if (countGaps <= 3)
          {
            if (plane_voxels.CellExist(rowCol.Item1, rowCol.Item2, i))
            {
              var pts = plane_voxels.GetCellItems(rowCol.Item1, rowCol.Item2, i);

              if (MathLib.GeoMath.GetDist2D(treeLoc[0], treeLoc[1], pts[0].x, pts[0].y) < offset)
              {
                continue;
              }
              bufferList.Add(new Tuple<int, int, int>(rowCol.Item1, rowCol.Item2, i));
              prevExists = true;
            }
            else
            {
              if (prevExists)
              {
                ++countGaps;
              }
            }
          }
          else
          {
            countGaps = 0;
            prevExists = false;
            foreach (var item in bufferList)
            {
              exlcudeList.Add(item);
            }
            bufferList.Clear();
          }
        }
      }

      foreach (var cell in exlcudeList)
      {
        plane_voxels.RemoveCell(cell.Item1, cell.Item2, cell.Item3);
      }

      // remove isolated blocks and cells
      plane_voxels.RemoveIsolatedCells(out numOf);
      plane_voxels.RemoveIsolatedBlocks(3, out numOf);
      plane_voxels.RemoveIsolatedBlocks(5, out numOf);

      //plane_voxels.SaveToPly(SingleTreeLasFilePath.Substring(0, SingleTreeLasFilePath.Length - 4) + "_rem_noise.ply", 0, 0, 0);

      #endregion filter by Voxel model
      #region process

      // Fill the rest into the LasPoint list
      foreach (var lasPoint in plane_voxels.GetEnumerator())
      {
        // lasPoint is a PCS point definition, contains many information ... 
        lasPoints.Add(lasPoint);
      }

      //Prepare input and output arrays for native function
      LASPointData[] lasPointDataArray = new LASPointData[lasPoints.Count]; //input array , this is what we define for a point ... 


      int cntr = 0;

      //Copy into native struct
      for (int ix = 0; ix < lasPoints.Count; ix++)
      {
        // world coordinates, the location xyz 
        lasPointDataArray[ix].x = lasPoints[ix].x;
        lasPointDataArray[ix].y = lasPoints[ix].y;
        lasPointDataArray[ix].z = lasPoints[ix].z;

        // this relative location is not centralized yet, just within itself.. , this is an integer
        // relative coordinates
        // modify the LASPointData struct members and the used references (double -> int)
        // lasPointDataArray[ix].x_rel = lasPoints[ix].x_rel;  
        // lasPointDataArray[ix].y_rel = lasPoints[ix].y_rel;  
        // lasPointDataArray[ix].z_rel = lasPoints[ix].z_rel;


        lasPointDataArray[ix].intensity = lasPoints[ix].intensity;
        // a small subsection of the points is then designated as part of a trunk.. 
        if (IsTrunkPoint(lasPoints[ix], treeLoc) == 1)
        {
          lasPointDataArray[ix].isTrunk = 1;
          cntr++;
        }
        else
        {
          lasPointDataArray[ix].isTrunk = 0;
        }
      }

      byte[] classes = new byte[lasPoints.Count];  //output array
      int itemNr = classes.Length;

      if (cntr > 0)
      {
        System.Diagnostics.Trace.WriteLine(String.Format("Classification in unsafe"));
        unsafe
        {

          fixed (LASPointData* lasPointArray = lasPointDataArray)
          {
            fixed (byte* classArray = classes)
            {
              System.Diagnostics.Trace.WriteLine(String.Format("Classification Classify step in"));
              var rv = Classify(itemNr, intensityEstimation, neighbourhoodEstimation, lasPointArray, classArray);
              System.Diagnostics.Trace.WriteLine(String.Format("Classification Classify step out"));
            }
          }
        }
        System.Diagnostics.Trace.WriteLine(String.Format("Classification out unsafe"));

        //Update classification byte for every las point
        for (int ix = 0; ix < lasPoints.Count; ix++)
        {
          lasPoints[ix].classification = classes[ix];
        }
        System.Diagnostics.Trace.WriteLine(String.Format("Classification classes updated"));

        string errorMsg;
        //Write a new las file
        var outDir = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(SingleTreeLasFilePath), "OUT");
        if (!System.IO.Directory.Exists(outDir))
        {
          System.IO.Directory.CreateDirectory(outDir);
        }
        var classifiedLasPath = System.IO.Path.Combine(outDir, System.IO.Path.GetFileName(SingleTreeLasFilePath) + ".out.las");
        if (lasFileReaderWriter.Save_Binary(classifiedLasPath, lasPoints,
            lasFileReaderWriter.Header.x_offset, lasFileReaderWriter.Header.y_offset,
            lasFileReaderWriter.Header.z_offset, lasFileReaderWriter.Header.x_scale_factor,
            lasFileReaderWriter.Header.y_scale_factor, lasFileReaderWriter.Header.z_scale_factor, out errorMsg))
        {
          // optimize LAS files for PCS
          DC_LasReader.DC_LasReader rdr = new DC_LasReader.DC_LasReader();
          if (rdr.OpenEx(classifiedLasPath, true, true))
          {
            File.Delete(classifiedLasPath + ".bak");
          }
          //todo make it switchable
          rdr.Close();
        }
        System.Diagnostics.Trace.WriteLine(String.Format("Classification ended of tree: {0}", SingleTreeLasFilePath));
      }
      else
      {
        System.Diagnostics.Trace.WriteLine(String.Format("Couldn't identify trunk at tree: {0}", SingleTreeLasFilePath));
        // TODO handle 0 trunk points
      }
      #endregion process
      lasFileReaderWriter.Close();
    }

    private int IsTrunkPoint(DC_LasPoint dC_LasPoint, double[] treeLoc)
    {
      if (Math.Abs(dC_LasPoint.z - treeLoc[2]) > .9 && Math.Abs(dC_LasPoint.z - treeLoc[2]) < 1.4)
      {
        var distSqr = Math.Pow((dC_LasPoint.x - treeLoc[0]), 2) + Math.Pow((dC_LasPoint.y - treeLoc[1]), 2);
        if (approxTreeDiameter > 0)
        {
          if (distSqr <= (Convert.ToDouble(approxTreeDiameter) / 2) * 1.4)
          {
            return 1;
          }
        }
        else
        {
          if (distSqr < 1.0)
          {
            return 1;
          }
        }
      }
      return 0;
    }

    private double[] GetTreeLocationFromShape(string guid)
    {
      double[] treeLoc = { 0, 0, 0 };
      ShapeFile.Point treeLocPt;
      foreach (DataRow row in treeLocations.TheGeoDataTable.TheTable.Rows)
      {
        if (row.RowState != DataRowState.Deleted && (string)row["GUID"] == guid)
        {
          if (row["TRUNK_D"] != DBNull.Value && (decimal)row["TRUNK_D"] > 0)
          {
            approxTreeDiameter = (decimal)row["TRUNK_D"];
          }
          else
          {
            approxTreeDiameter = 2.0M;
          }
          treeLocPt = treeLocations.TheGeoDataTable.GetGeometry(row) as ShapeFile.Point;
          treeLoc[0] = treeLocPt.X;
          treeLoc[1] = treeLocPt.Y;
          treeLoc[2] = treeLocPt.Z;
          break;
        }
      }
      return treeLoc;
    }

    public void Dispose()
    {
      if (!disposedValue)
      {
        // TODO: dispose managed state (managed objects).
        // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
        // TODO: set large fields to null.

        disposedValue = true;
      }
    }

    public bool IsDisposed()
    {
      return disposedValue;
    }
  }
}
