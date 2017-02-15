namespace HomeCollector
{
    partial class MainForm
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
            this.cboCollectionType = new System.Windows.Forms.ComboBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnLoadCollection = new System.Windows.Forms.Button();
            this.btnCreateNewCollection = new System.Windows.Forms.Button();
            this.btnSaveCollection = new System.Windows.Forms.Button();
            this.lblCollectionType = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCollectionName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // cboCollectionType
            // 
            this.cboCollectionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCollectionType.FormattingEnabled = true;
            this.cboCollectionType.Location = new System.Drawing.Point(155, 23);
            this.cboCollectionType.Name = "cboCollectionType";
            this.cboCollectionType.Size = new System.Drawing.Size(117, 24);
            this.cboCollectionType.TabIndex = 0;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // btnLoadCollection
            // 
            this.btnLoadCollection.Location = new System.Drawing.Point(36, 167);
            this.btnLoadCollection.Name = "btnLoadCollection";
            this.btnLoadCollection.Size = new System.Drawing.Size(75, 23);
            this.btnLoadCollection.TabIndex = 1;
            this.btnLoadCollection.Text = "Load";
            this.btnLoadCollection.UseVisualStyleBackColor = true;
            this.btnLoadCollection.Click += new System.EventHandler(this.btnLoadCollection_Click);
            // 
            // btnCreateNewCollection
            // 
            this.btnCreateNewCollection.Location = new System.Drawing.Point(171, 167);
            this.btnCreateNewCollection.Name = "btnCreateNewCollection";
            this.btnCreateNewCollection.Size = new System.Drawing.Size(75, 23);
            this.btnCreateNewCollection.TabIndex = 2;
            this.btnCreateNewCollection.Text = "New";
            this.btnCreateNewCollection.UseVisualStyleBackColor = true;
            // 
            // btnSaveCollection
            // 
            this.btnSaveCollection.Location = new System.Drawing.Point(295, 167);
            this.btnSaveCollection.Name = "btnSaveCollection";
            this.btnSaveCollection.Size = new System.Drawing.Size(75, 23);
            this.btnSaveCollection.TabIndex = 3;
            this.btnSaveCollection.Text = "Save";
            this.btnSaveCollection.UseVisualStyleBackColor = true;
            // 
            // lblCollectionType
            // 
            this.lblCollectionType.AutoSize = true;
            this.lblCollectionType.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCollectionType.Location = new System.Drawing.Point(24, 26);
            this.lblCollectionType.Name = "lblCollectionType";
            this.lblCollectionType.Size = new System.Drawing.Size(120, 17);
            this.lblCollectionType.TabIndex = 4;
            this.lblCollectionType.Text = "Collection Type";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(24, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "Collection Name";
            // 
            // txtCollectionName
            // 
            this.txtCollectionName.Location = new System.Drawing.Point(155, 57);
            this.txtCollectionName.Name = "txtCollectionName";
            this.txtCollectionName.Size = new System.Drawing.Size(326, 22);
            this.txtCollectionName.TabIndex = 6;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1042, 545);
            this.Controls.Add(this.txtCollectionName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblCollectionType);
            this.Controls.Add(this.btnSaveCollection);
            this.Controls.Add(this.btnCreateNewCollection);
            this.Controls.Add(this.btnLoadCollection);
            this.Controls.Add(this.cboCollectionType);
            this.Name = "MainForm";
            this.Text = "Home Collector";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboCollectionType;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnLoadCollection;
        private System.Windows.Forms.Button btnCreateNewCollection;
        private System.Windows.Forms.Button btnSaveCollection;
        private System.Windows.Forms.Label lblCollectionType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCollectionName;
    }
}

