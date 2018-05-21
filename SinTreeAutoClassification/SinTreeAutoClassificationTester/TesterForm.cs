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
    public TesterForm()
    {
      InitializeComponent();
    }

    private void StartButton_Click(object sender, EventArgs e)
    {
            // this is where the start button comes to .
      StartButton.Enabled = false;
      SinTreeAutoClassification.SinTreeAutoClassification autoClassification = new SinTreeAutoClassification.SinTreeAutoClassification();
      var path = SingleTreeLasPath.Text;
      System.IO.FileAttributes attr = System.IO.File.GetAttributes(path);
      // dir
      if (attr.HasFlag(System.IO.FileAttributes.Directory))
      {
        var files = System.IO.Directory.GetFiles(path, "*.las");
        foreach (string lasFile in files)
        {
                    // for each file, run classify
          autoClassification.Classify(lasFile);
        }
      }
      //file
      else
      {
        if (System.IO.File.Exists(path))
        {
          autoClassification.Classify(path);
        }
        else
        {
          MessageBox.Show(String.Format("{0}{1}File not exists!", path, Environment.NewLine), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
      }
      autoClassification.Dispose();
      StartButton.Enabled = true;
    }
  }
}
