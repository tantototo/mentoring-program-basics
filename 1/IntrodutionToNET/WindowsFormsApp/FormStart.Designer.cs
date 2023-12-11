using System.ComponentModel;

namespace WindowsFormsApp
{
    partial class FormStart
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            this.startOk = new System.Windows.Forms.Button();
            this.name = new System.Windows.Forms.TextBox();
            this.labelinputName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // startOk
            // 
            this.startOk.Location = new System.Drawing.Point(18, 47);
            this.startOk.Name = "startOk";
            this.startOk.Size = new System.Drawing.Size(112, 37);
            this.startOk.TabIndex = 0;
            this.startOk.Text = "Ok";
            this.startOk.UseVisualStyleBackColor = true;
            this.startOk.Click += new System.EventHandler(this.startOk_Click);
            // 
            // name
            // 
            this.name.Location = new System.Drawing.Point(130, 12);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(277, 26);
            this.name.TabIndex = 1;
            // 
            // labelinputName
            // 
            this.labelinputName.Location = new System.Drawing.Point(18, 15);
            this.labelinputName.Name = "labelinputName";
            this.labelinputName.Size = new System.Drawing.Size(106, 29);
            this.labelinputName.TabIndex = 2;
            this.labelinputName.Text = "Input name: ";
            // 
            // FormStart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.labelinputName);
            this.Controls.Add(this.name);
            this.Controls.Add(this.startOk);
            this.Name = "FormStart";
            this.Text = "FormStart";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.TextBox name;
        private System.Windows.Forms.Label labelinputName;

        private System.Windows.Forms.Button startOk;

        #endregion
    }
}