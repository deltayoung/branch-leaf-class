namespace SinTreeAutoClassificationTester
{
  partial class TesterForm
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      this.StartButton = new System.Windows.Forms.Button();
      this.SingleTreeLasPath = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.TriConnection = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.TriConBrowse = new System.Windows.Forms.Button();
      this.IntensityEstimation = new System.Windows.Forms.NumericUpDown();
      this.NeighbourhoodEstimation = new System.Windows.Forms.NumericUpDown();
      this.label3 = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.BrowseFolder = new System.Windows.Forms.Button();
      this.BrowseFile = new System.Windows.Forms.Button();
      this.TreeLocations = new System.Windows.Forms.TextBox();
      this.TreeLocationsBrowse = new System.Windows.Forms.Button();
      this.TreeLocationsLabel = new System.Windows.Forms.Label();
      this.SourceClassesGroup = new System.Windows.Forms.GroupBox();
      this.SourceClasses = new System.Windows.Forms.CheckedListBox();
      this.SourceClassesContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.checkAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.uncheckAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.invertToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      ((System.ComponentModel.ISupportInitialize)(this.IntensityEstimation)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.NeighbourhoodEstimation)).BeginInit();
      this.SourceClassesGroup.SuspendLayout();
      this.SourceClassesContextMenuStrip.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.SuspendLayout();
      // 
      // StartButton
      // 
      this.StartButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.StartButton.Location = new System.Drawing.Point(610, 175);
      this.StartButton.Name = "StartButton";
      this.StartButton.Size = new System.Drawing.Size(95, 30);
      this.StartButton.TabIndex = 0;
      this.StartButton.Text = "Start";
      this.StartButton.UseVisualStyleBackColor = true;
      this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
      // 
      // SingleTreeLasPath
      // 
      this.SingleTreeLasPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.SingleTreeLasPath.Location = new System.Drawing.Point(140, 16);
      this.SingleTreeLasPath.Name = "SingleTreeLasPath";
      this.SingleTreeLasPath.Size = new System.Drawing.Size(314, 20);
      this.SingleTreeLasPath.TabIndex = 1;
      this.SingleTreeLasPath.TextChanged += new System.EventHandler(this.SingleTreeLasPath_TextChanged);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(54, 19);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(80, 13);
      this.label1.TabIndex = 2;
      this.label1.Text = "LAS file / folder";
      // 
      // TriConnection
      // 
      this.TriConnection.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.TriConnection.Enabled = false;
      this.TriConnection.Location = new System.Drawing.Point(140, 43);
      this.TriConnection.Name = "TriConnection";
      this.TriConnection.Size = new System.Drawing.Size(314, 20);
      this.TriConnection.TabIndex = 3;
      //this.TriConnection.TextChanged += new System.EventHandler(this.TriCon_TextChanged);
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Enabled = false;
      this.label2.Location = new System.Drawing.Point(61, 46);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(73, 13);
      this.label2.TabIndex = 4;
      this.label2.Text = "Ground model";
      // 
      // TriConBrowse
      // 
      this.TriConBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.TriConBrowse.Enabled = false;
      this.TriConBrowse.Location = new System.Drawing.Point(460, 42);
      this.TriConBrowse.Name = "TriConBrowse";
      this.TriConBrowse.Size = new System.Drawing.Size(64, 21);
      this.TriConBrowse.TabIndex = 5;
      this.TriConBrowse.Text = "Browse";
      this.TriConBrowse.UseVisualStyleBackColor = true;
      //this.TriConBrowse.Click += new System.EventHandler(this.TriConBrowse_Click);
      // 
      // IntensityEstimation
      // 
      this.IntensityEstimation.Location = new System.Drawing.Point(140, 96);
      this.IntensityEstimation.Name = "IntensityEstimation";
      this.IntensityEstimation.Size = new System.Drawing.Size(81, 20);
      this.IntensityEstimation.TabIndex = 6;
      this.IntensityEstimation.ValueChanged += new System.EventHandler(this.IntensityEstimation_ValueChanged);
      // 
      // NeighbourhoodEstimation
      // 
      this.NeighbourhoodEstimation.Location = new System.Drawing.Point(140, 122);
      this.NeighbourhoodEstimation.Name = "NeighbourhoodEstimation";
      this.NeighbourhoodEstimation.Size = new System.Drawing.Size(81, 20);
      this.NeighbourhoodEstimation.TabIndex = 7;
      this.NeighbourhoodEstimation.ValueChanged += new System.EventHandler(this.NeighbourhoodEstimation_ValueChanged);
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(40, 98);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(94, 13);
      this.label3.TabIndex = 8;
      this.label3.Text = "IntensityEstimation";
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(6, 124);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(128, 13);
      this.label4.TabIndex = 9;
      this.label4.Text = "NeighbourhoodEstimation";
      // 
      // BrowseFolder
      // 
      this.BrowseFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.BrowseFolder.Location = new System.Drawing.Point(530, 16);
      this.BrowseFolder.Name = "BrowseFolder";
      this.BrowseFolder.Size = new System.Drawing.Size(53, 20);
      this.BrowseFolder.TabIndex = 10;
      this.BrowseFolder.Text = "Folder";
      this.BrowseFolder.UseVisualStyleBackColor = true;
      this.BrowseFolder.Click += new System.EventHandler(this.BrowseFolder_Click);
      // 
      // BrowseFile
      // 
      this.BrowseFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.BrowseFile.Location = new System.Drawing.Point(460, 16);
      this.BrowseFile.Name = "BrowseFile";
      this.BrowseFile.Size = new System.Drawing.Size(64, 20);
      this.BrowseFile.TabIndex = 11;
      this.BrowseFile.Text = "File";
      this.BrowseFile.UseVisualStyleBackColor = true;
      this.BrowseFile.Click += new System.EventHandler(this.BrowseFile_Click);
      // 
      // TreeLocations
      // 
      this.TreeLocations.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.TreeLocations.Location = new System.Drawing.Point(140, 70);
      this.TreeLocations.Name = "TreeLocations";
      this.TreeLocations.Size = new System.Drawing.Size(314, 20);
      this.TreeLocations.TabIndex = 12;
      this.TreeLocations.TextChanged += new System.EventHandler(this.TreeLocations_TextChanged);
      // 
      // TreeLocationsBrowse
      // 
      this.TreeLocationsBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.TreeLocationsBrowse.Location = new System.Drawing.Point(460, 70);
      this.TreeLocationsBrowse.Name = "TreeLocationsBrowse";
      this.TreeLocationsBrowse.Size = new System.Drawing.Size(64, 20);
      this.TreeLocationsBrowse.TabIndex = 13;
      this.TreeLocationsBrowse.Text = "Browse";
      this.TreeLocationsBrowse.UseVisualStyleBackColor = true;
      this.TreeLocationsBrowse.Click += new System.EventHandler(this.TreeLocationsBrowse_Click);
      // 
      // TreeLocationsLabel
      // 
      this.TreeLocationsLabel.AutoSize = true;
      this.TreeLocationsLabel.Location = new System.Drawing.Point(20, 74);
      this.TreeLocationsLabel.Name = "TreeLocationsLabel";
      this.TreeLocationsLabel.Size = new System.Drawing.Size(114, 13);
      this.TreeLocationsLabel.TabIndex = 14;
      this.TreeLocationsLabel.Text = "Tree locations (Shape)";
      // 
      // SourceClassesGroup
      // 
      this.SourceClassesGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
      this.SourceClassesGroup.Controls.Add(this.SourceClasses);
      this.SourceClassesGroup.Location = new System.Drawing.Point(12, 12);
      this.SourceClassesGroup.Name = "SourceClassesGroup";
      this.SourceClassesGroup.Size = new System.Drawing.Size(98, 193);
      this.SourceClassesGroup.TabIndex = 15;
      this.SourceClassesGroup.TabStop = false;
      this.SourceClassesGroup.Text = "Source Classes";
      // 
      // SourceClasses
      // 
      this.SourceClasses.CheckOnClick = true;
      this.SourceClasses.ContextMenuStrip = this.SourceClassesContextMenuStrip;
      this.SourceClasses.Dock = System.Windows.Forms.DockStyle.Fill;
      this.SourceClasses.FormattingEnabled = true;
      this.SourceClasses.Location = new System.Drawing.Point(3, 16);
      this.SourceClasses.Name = "SourceClasses";
      this.SourceClasses.Size = new System.Drawing.Size(92, 174);
      this.SourceClasses.TabIndex = 0;
      this.SourceClasses.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.SourceClasses_ItemCheck);
      // 
      // SourceClassesContextMenuStrip
      // 
      this.SourceClassesContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.checkAllToolStripMenuItem,
            this.uncheckAllToolStripMenuItem,
            this.toolStripSeparator1,
            this.invertToolStripMenuItem});
      this.SourceClassesContextMenuStrip.Name = "SourceClassesContextMenuStrip";
      this.SourceClassesContextMenuStrip.Size = new System.Drawing.Size(138, 76);
      // 
      // checkAllToolStripMenuItem
      // 
      this.checkAllToolStripMenuItem.Name = "checkAllToolStripMenuItem";
      this.checkAllToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
      this.checkAllToolStripMenuItem.Text = "Check All";
      this.checkAllToolStripMenuItem.Click += new System.EventHandler(this.CheckAllToolStripMenuItem_Click);
      // 
      // uncheckAllToolStripMenuItem
      // 
      this.uncheckAllToolStripMenuItem.Name = "uncheckAllToolStripMenuItem";
      this.uncheckAllToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
      this.uncheckAllToolStripMenuItem.Text = "Uncheck All";
      this.uncheckAllToolStripMenuItem.Click += new System.EventHandler(this.UncheckAllToolStripMenuItem_Click);
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(134, 6);
      // 
      // invertToolStripMenuItem
      // 
      this.invertToolStripMenuItem.Name = "invertToolStripMenuItem";
      this.invertToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
      this.invertToolStripMenuItem.Text = "Invert";
      this.invertToolStripMenuItem.Click += new System.EventHandler(this.InvertToolStripMenuItem_Click);
      // 
      // groupBox1
      // 
      this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.groupBox1.Controls.Add(this.BrowseFolder);
      this.groupBox1.Controls.Add(this.TreeLocationsLabel);
      this.groupBox1.Controls.Add(this.TriConBrowse);
      this.groupBox1.Controls.Add(this.TreeLocations);
      this.groupBox1.Controls.Add(this.TreeLocationsBrowse);
      this.groupBox1.Controls.Add(this.label4);
      this.groupBox1.Controls.Add(this.BrowseFile);
      this.groupBox1.Controls.Add(this.label3);
      this.groupBox1.Controls.Add(this.SingleTreeLasPath);
      this.groupBox1.Controls.Add(this.NeighbourhoodEstimation);
      this.groupBox1.Controls.Add(this.label1);
      this.groupBox1.Controls.Add(this.IntensityEstimation);
      this.groupBox1.Controls.Add(this.TriConnection);
      this.groupBox1.Controls.Add(this.label2);
      this.groupBox1.Location = new System.Drawing.Point(116, 12);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(589, 157);
      this.groupBox1.TabIndex = 16;
      this.groupBox1.TabStop = false;
      // 
      // TesterForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(717, 217);
      this.Controls.Add(this.SourceClassesGroup);
      this.Controls.Add(this.StartButton);
      this.Controls.Add(this.groupBox1);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.MinimumSize = new System.Drawing.Size(300, 150);
      this.Name = "TesterForm";
      this.ShowIcon = false;
      this.Text = "Tester Form";
      ((System.ComponentModel.ISupportInitialize)(this.IntensityEstimation)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.NeighbourhoodEstimation)).EndInit();
      this.SourceClassesGroup.ResumeLayout(false);
      this.SourceClassesContextMenuStrip.ResumeLayout(false);
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button StartButton;
    private System.Windows.Forms.TextBox SingleTreeLasPath;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox TriConnection;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Button TriConBrowse;
    private System.Windows.Forms.NumericUpDown IntensityEstimation;
    private System.Windows.Forms.NumericUpDown NeighbourhoodEstimation;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Button BrowseFolder;
    private System.Windows.Forms.Button BrowseFile;
    private System.Windows.Forms.TextBox TreeLocations;
    private System.Windows.Forms.Button TreeLocationsBrowse;
    private System.Windows.Forms.Label TreeLocationsLabel;
    private System.Windows.Forms.GroupBox SourceClassesGroup;
    private System.Windows.Forms.CheckedListBox SourceClasses;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.ContextMenuStrip SourceClassesContextMenuStrip;
    private System.Windows.Forms.ToolStripMenuItem checkAllToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem uncheckAllToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripMenuItem invertToolStripMenuItem;
  }
}

