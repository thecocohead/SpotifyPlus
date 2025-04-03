namespace SpotifyPlus
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            button1 = new Button();
            label3 = new Label();
            label4 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 48F);
            label1.Location = new Point(14, 12);
            label1.Name = "label1";
            label1.Size = new Size(457, 106);
            label1.TabIndex = 0;
            label1.Text = "Spotify Plus";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(14, 127);
            label2.Name = "label2";
            label2.Size = new Size(371, 20);
            label2.TabIndex = 1;
            label2.Text = "we live in a nightmare nightmare nightmare nightmare";
            // 
            // button1
            // 
            button1.Font = new Font("Segoe UI", 48F);
            button1.Location = new Point(14, 151);
            button1.Margin = new Padding(3, 4, 3, 4);
            button1.Name = "button1";
            button1.Size = new Size(353, 147);
            button1.TabIndex = 2;
            button1.Text = "Connect";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(609, 12);
            label3.Name = "label3";
            label3.Size = new Size(82, 20);
            label3.TabIndex = 3;
            label3.Text = "Username: ";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(595, 242);
            label4.Name = "label4";
            label4.Size = new Size(50, 20);
            label4.TabIndex = 4;
            label4.Text = "label4";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(914, 600);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(button1);
            Controls.Add(label2);
            Controls.Add(label1);
            Margin = new Padding(3, 4, 3, 4);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Button button1;
        private Label label3;
        private Label label4;
    }
}
