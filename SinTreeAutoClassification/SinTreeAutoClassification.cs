using System;
using System.Collections.Generic;
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
    unsafe public static extern int Classify(int itemNr, LASPointData* lasPointArray, byte* classArray);

    private bool disposedValue = false; // To detect redundant calls

    public SinTreeAutoClassification()
    {
      //Initialize license server connection
      PCS.Program.CreateSKL();
      PCS.Program.CreateLicenseServiceConnection0();
      DC_LasReader.Globals.LicenseService = PCS.Program.LicenseService;
      // TODO: initialize unmamanged parts, reusable resources
    }

    public void Classify(string SingleTreeLasFilePath)
    {
      if (disposedValue) throw new ObjectDisposedException("Object disposed: SinTreeAutoClassification");

      DC_LasReader.DC_LasReader lasFileReaderWriter = new DC_LasReader.DC_LasReader(); //Reader and writer!!!!!

      lasFileReaderWriter.Open(SingleTreeLasFilePath, false);
      var xmlFilePath = SingleTreeLasFilePath.Substring(0, SingleTreeLasFilePath.Length - 3) + "xml";

      // xml contains the location only.. that is the only information that is read. 

      List<DC_LasReader.DC_LasPoint> lasPoints = new List<DC_LasReader.DC_LasPoint>(10000); // 10000 is an approximation 
      foreach (DC_LasReader.DC_LasPoint pt in lasFileReaderWriter.GetPointEnumerator())
      {
        // pt is a PCS point definition, contains many information ... 
        lasPoints.Add(pt);
      }

      //Prepare input and output arrays for native function
      LASPointData[] lasPointDataArray = new LASPointData[lasPoints.Count]; //input array , this is what we define for a point ... 
      var treeLoc = GetTreeLocation(xmlFilePath);

      int cntr = 0;

      //Copy into native struct
      for (int ix = 0; ix < lasPoints.Count; ix++)
      {
        // world coordinates , the location xyz 
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
        if (IsTrunkPoint(lasPoints[ix], treeLoc) == 1) // a small subsection of the points is then designated as part of a trunk.. 
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

        unsafe
        {
          fixed (LASPointData* lasPointArray = lasPointDataArray)
          {
            fixed (byte* classArray = classes)
            {
              var rv = Classify(itemNr, lasPointArray, classArray);
            }
          }
        }

        //Update classification byte for every las point
        for (int ix = 0; ix < lasPoints.Count; ix++)
        {
          lasPoints[ix].classification = classes[ix];
        }

        string errorMsg;
        //Write a new las file
        var outDir = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(SingleTreeLasFilePath), "OUT");
        if (!System.IO.Directory.Exists(outDir))
        {
          System.IO.Directory.CreateDirectory(outDir);
        }
        lasFileReaderWriter.Save_Binary(System.IO.Path.Combine(outDir, System.IO.Path.GetFileName(SingleTreeLasFilePath) + ".out.las"), lasPoints,
            lasFileReaderWriter.Header.x_offset, lasFileReaderWriter.Header.y_offset,
            lasFileReaderWriter.Header.z_offset, lasFileReaderWriter.Header.x_scale_factor,
            lasFileReaderWriter.Header.y_scale_factor, lasFileReaderWriter.Header.z_scale_factor, out errorMsg);
      }
      else
      {
        // TODO handle 0 trunk points
      }
      lasFileReaderWriter.Close();
    }

    private int IsTrunkPoint(DC_LasPoint dC_LasPoint, double[] treeLoc)
    {
      if (Math.Abs(dC_LasPoint.z - treeLoc[2]) > 2 && Math.Abs(dC_LasPoint.z - treeLoc[2]) < 2.5)
      {
        var distSqr = Math.Pow((dC_LasPoint.x - treeLoc[0]), 2) + Math.Pow((dC_LasPoint.y - treeLoc[1]), 2);
        if (distSqr < 0.25)
        {
          return 1;
        }
      }
      return 0;
    }

    private double[] GetTreeLocation(string xmlPath)
    {
      double[] treeLoc = { 0, 0, 0 };
      using (System.Xml.XmlReader reader = System.Xml.XmlReader.Create(xmlPath))
      {
        while (reader.Read())
        {
          if (reader.IsStartElement())
          {
            // Get element name and switch on it.
            switch (reader.Name)
            {
              case "BASE_COORD":
              case "CORD":  // obsolate
                if (reader.Read())
                {
                  var value = reader.Value.Trim().Split(", ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                  if (value.Length == 3)
                  {
                    treeLoc[0] = Double.Parse(value[0]);
                    treeLoc[1] = Double.Parse(value[1]);
                    treeLoc[2] = Double.Parse(value[2]);
                  }
                }
                break;
            }
          }
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
