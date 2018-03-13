namespace MediCare
{
    partial class CheminScanner
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
            this.parcourir = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // parcourir
            // 
            this.parcourir.Location = new System.Drawing.Point(90, 102);
            this.parcourir.Name = "parcourir";
            this.parcourir.Size = new System.Drawing.Size(112, 41);
            this.parcourir.TabIndex = 0;
            this.parcourir.Text = "Parcourir";
            this.parcourir.UseVisualStyleBackColor = true;
            this.parcourir.Click += new System.EventHandler(this.parcourir_Click);
            // 
            // CheminScanner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.parcourir);
            this.Name = "CheminScanner";
            this.Text = "CheminScanner";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button parcourir;
    }
}