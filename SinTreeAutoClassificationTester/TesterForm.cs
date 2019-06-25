using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SinTreeAutoClassificationTester
{
  public partial class TesterForm : Form
  {
    private float intensityEstimation;
    private float neighbourhoodEstimation;
    // private TRIConnection groundModel; // currently not used
    private ShapeFile.ShapeCollection treeLocations;
    private List<byte> sourceClasses;

    private string lasFilePath;

    public TesterForm()
    {
      InitializeComponent();
      InitializeControls();
      //Initialize license server connection
      PCS.Program.CreateSKL();
      PCS.Program.CreateLicenseServiceConnection0();
      DC_LasLib.Globals.LicenseService = PCS.Program.LicenseService;
    }

    private void InitializeControls()
    {
      IntensityEstimation.Minimum = 0.01M;
      IntensityEstimation.Maximum = 1.0M;
      IntensityEstimation.Increment = 0.01M;
      IntensityEstimation.DecimalPlaces = 2;
      IntensityEstimation.TextAlign = HorizontalAlignment.Right;
      IntensityEstimation.Value = 0.49M;

      NeighbourhoodEstimation.Minimum = 0.01M;
      NeighbourhoodEstimation.Maximum = 0.25M;
      NeighbourhoodEstimation.Increment = 0.01M;
      NeighbourhoodEstimation.DecimalPlaces = 2;
      NeighbourhoodEstimation.TextAlign = HorizontalAlignment.Right;
      NeighbourhoodEstimation.Value = 0.25M;

      var range = Enumerable.Range(0, 256);
      foreach (var item in range.OrderBy(el => el))
      {
        if (new int[] { 0, 1 }.Contains(item))
        {
          SourceClasses.Items.Add(item, true);
        }
        else
        {
          SourceClasses.Items.Add(item, false);
        }
      }
    }

    private void StartButton_Click(object sender, EventArgs e)
    {
      // this is where the start button comes to .
      StartButton.Enabled = false;
      SinTreeAutoClassification.SinTreeAutoClassification autoClassification =
        new SinTreeAutoClassification.SinTreeAutoClassification(intensityEstimation, neighbourhoodEstimation, sourceClasses, treeLocations);

      System.IO.FileAttributes attr = System.IO.File.GetAttributes(lasFilePath);
      // dir
      if (attr.HasFlag(System.IO.FileAttributes.Directory))
      {
        string[] files = System.IO.Directory.GetFiles(lasFilePath, "*.las");
        foreach (string lasFile in files)
        {
          // for each file, run classify
          autoClassification.Classify(lasFile);
        }
      }
      //file
      else
      {
        if (System.IO.File.Exists(lasFilePath))
        {
          autoClassification.Classify(lasFilePath);
        }
        else
        {
          MessageBox.Show(String.Format("{0}{1}File not exists!", lasFilePath, Environment.NewLine), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
      }
      autoClassification.Dispose();
      MessageBox.Show("Classification Done!", "Ready", MessageBoxButtons.OK, MessageBoxIcon.Information);
      StartButton.Enabled = true;
    }

    // currently not used
    //private void TriConBrowse_Click(object sender, EventArgs e)
    //{
    //  var ofd = new OpenFileDialog();
    //  ofd.Filter = "Ground model | *.txt";
    //  ofd.Title = "Open Ground model";
    //  if (ofd.ShowDialog() == DialogResult.OK)
    //  {
    //    TriConnection.Text = ofd.FileName;
    //  }
    //  ValidateData();
    //}

    //private void TriCon_TextChanged(object sender, EventArgs e)
    //{
    //  if (System.IO.File.Exists((sender as TextBox).Text.Trim(' ').Trim('"')))
    //  {
    //    groundModel = new PCS.Vector.TRIConnection();
    //    groundModel.FilePath = TriConnection.Text;
    //    groundModel.Load(TriConnection.Text.Trim(' ').Trim('"'));
    //  }
    //  ValidateData();
    //}

    private void TreeLocations_TextChanged(object sender, EventArgs e)
    {
      var path = (sender as TextBox).Text.Trim(' ').Trim('"');
      if (System.IO.File.Exists(path))
      {
        treeLocations = new ShapeFile.ShapeCollection(ShapeFile.ShapeType.ShapeTypePointZ, false);
        treeLocations.LoadSHP(path);
      }
      ValidateData();
    }

    private void SingleTreeLasPath_TextChanged(object sender, EventArgs e)
    {
      lasFilePath = (sender as TextBox).Text.Trim(' ').Trim('"');
      ValidateData();
    }

    private void IntensityEstimation_ValueChanged(object sender, EventArgs e)
    {
      intensityEstimation = (float)(sender as NumericUpDown).Value;
      ValidateData();
    }

    private void NeighbourhoodEstimation_ValueChanged(object sender, EventArgs e)
    {
      neighbourhoodEstimation = (float)(sender as NumericUpDown).Value;
    }

    private void BrowseFile_Click(object sender, EventArgs e)
    {
      var ofd = new OpenFileDialog();
      ofd.Filter = "Point Clouds (LAS) | *.las";
      ofd.Title = "Open Point Cloud file";
      if (ofd.ShowDialog() == DialogResult.OK)
      {
        SingleTreeLasPath.Text = ofd.FileName;
      }
      ValidateData();
    }

    private void BrowseFolder_Click(object sender, EventArgs e)
    {
      var fbd = new FolderBrowserDialog();
      if (fbd.ShowDialog() == DialogResult.OK)
      {
        SingleTreeLasPath.Text = fbd.SelectedPath;
      }
    }

    private void TreeLocationsBrowse_Click(object sender, EventArgs e)
    {
      var ofd = new OpenFileDialog();
      ofd.Filter = "Shape file (shp) | *.shp";
      ofd.Title = "Open Tree Location Shape file";
      if (ofd.ShowDialog() == DialogResult.OK)
      {
        TreeLocations.Text = ofd.FileName;
      }
      ValidateData();
    }

    private void SourceClasses_ItemCheck(object sender, ItemCheckEventArgs e)
    {
      sourceClasses = new List<byte>();
      for (int i = 0; i < (sender as CheckedListBox).CheckedItems.Count; i++)
      {
        sourceClasses.Add(Convert.ToByte((sender as CheckedListBox).CheckedItems[i]));
      }
      if (e.NewValue == CheckState.Checked)
      {
        sourceClasses.Add(Convert.ToByte(e.Index));
      }
      if (e.NewValue == CheckState.Unchecked)
      {
        sourceClasses.Remove(Convert.ToByte(e.Index));
      }
    }
    private void CheckAllToolStripMenuItem_Click(object sender, EventArgs e)
    {
      for (int i = 0; i < SourceClasses.Items.Count; i++)
      {
        SourceClasses.SetItemCheckState(i, CheckState.Checked);
      }
    }
    private void UncheckAllToolStripMenuItem_Click(object sender, EventArgs e)
    {
      for (int i = 0; i < SourceClasses.Items.Count; i++)
      {
        SourceClasses.SetItemCheckState(i, CheckState.Unchecked);
      }
    }
    private void InvertToolStripMenuItem_Click(object sender, EventArgs e)
    {
      for (int i = 0; i < SourceClasses.Items.Count; i++)
      {
        var stateChecked = SourceClasses.GetItemCheckState(i) == CheckState.Checked;
        SourceClasses.SetItemCheckState(i, (stateChecked ? CheckState.Unchecked : CheckState.Checked));
      }
    }

    private void ValidateData()
    {
      StartButton.Enabled = (intensityEstimation != default(float))
        && (intensityEstimation > 0f)
        && (intensityEstimation <= 1f)
        && (neighbourhoodEstimation != default(float))
        && (neighbourhoodEstimation > 0f)
        && (neighbourhoodEstimation <= 0.5f)
        && (sourceClasses.Count > 0)
        && (lasFilePath != null)
        && (lasFilePath != String.Empty)
        //&& (groundModel != null) // currently not used
        && (treeLocations != null);
    }
  }
}
