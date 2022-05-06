namespace ShoppingApp
{
    partial class Seen
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
            this.dgvseen = new System.Windows.Forms.DataGridView();
            this.dgv1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvseen)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvseen
            // 
            this.dgvseen.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvseen.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvseen.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvseen.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvseen.Location = new System.Drawing.Point(12, 76);
            this.dgvseen.Name = "dgvseen";
            this.dgvseen.RowHeadersWidth = 51;
            this.dgvseen.RowTemplate.Height = 24;
            this.dgvseen.Size = new System.Drawing.Size(765, 207);
            this.dgvseen.TabIndex = 0;
            // 
            // dgv1
            // 
            this.dgv1.AutoSize = true;
            this.dgv1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgv1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.dgv1.Location = new System.Drawing.Point(270, 24);
            this.dgv1.Name = "dgv1";
            this.dgv1.Size = new System.Drawing.Size(257, 32);
            this.dgv1.TabIndex = 2;
            this.dgv1.Text = "Sản phẩm đã xem";
            // 
            // Seen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dgv1);
            this.Controls.Add(this.dgvseen);
            this.Name = "Seen";
            this.Text = "Seen";
            this.Load += new System.EventHandler(this.Seen_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvseen)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvseen;
        private System.Windows.Forms.Label dgv1;
    }
}