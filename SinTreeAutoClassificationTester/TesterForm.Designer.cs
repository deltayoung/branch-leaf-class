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
            this.StartButton = new System.Windows.Forms.Button();
            this.SingleTreeLasPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // StartButton
            // 
            this.StartButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.StartButton.Location = new System.Drawing.Point(353, 73);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(79, 26);
            this.StartButton.TabIndex = 0;
            this.StartButton.Text = "Start";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // SingleTreeLasPath
            // 
            this.SingleTreeLasPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SingleTreeLasPath.Location = new System.Drawing.Point(101, 12);
            this.SingleTreeLasPath.Name = "SingleTreeLasPath";
            this.SingleTreeLasPath.Size = new System.Drawing.Size(331, 20);
            this.SingleTreeLasPath.TabIndex = 1;
            this.SingleTreeLasPath.Text = "D:\\Data\\1a5a3dd6-9d1f-4548-b4a4-69b4f08bb8ff.las";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "LAS file / folder:";
            // 
            // TesterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 112);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SingleTreeLasPath);
            this.Controls.Add(this.StartButton);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(300, 150);
            this.Name = "TesterForm";
            this.ShowIcon = false;
            this.Text = "Test";
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button StartButton;
    private System.Windows.Forms.TextBox SingleTreeLasPath;
    private System.Windows.Forms.Label label1;
  }
}

