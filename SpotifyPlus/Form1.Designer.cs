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
            headerLabel = new Label();
            connectButton = new Button();
            shortTermButton = new Button();
            mediumTermButton = new Button();
            longTermButton = new Button();
            SuspendLayout();
            // 
            // headerLabel
            // 
            headerLabel.AutoSize = true;
            headerLabel.Font = new Font("Segoe UI", 48F);
            headerLabel.Location = new Point(12, 9);
            headerLabel.Name = "headerLabel";
            headerLabel.Size = new Size(369, 86);
            headerLabel.TabIndex = 0;
            headerLabel.Text = "Spotify Plus";
            // 
            // connectButton
            // 
            connectButton.Font = new Font("Segoe UI", 48F);
            connectButton.Location = new Point(48, 120);
            connectButton.Name = "connectButton";
            connectButton.Size = new Size(309, 111);
            connectButton.TabIndex = 2;
            connectButton.Text = "Connect";
            connectButton.UseVisualStyleBackColor = true;
            connectButton.Click += button1_Click;
            // 
            // shortTermButton
            // 
            shortTermButton.Enabled = false;
            shortTermButton.Font = new Font("Segoe UI", 12F);
            shortTermButton.Location = new Point(48, 257);
            shortTermButton.Name = "shortTermButton";
            shortTermButton.Size = new Size(309, 37);
            shortTermButton.TabIndex = 3;
            shortTermButton.Text = "Last 4 weeks";
            shortTermButton.UseVisualStyleBackColor = true;
            shortTermButton.Click += shortTermButton_Click;
            // 
            // mediumTermButton
            // 
            mediumTermButton.Enabled = false;
            mediumTermButton.Font = new Font("Segoe UI", 12F);
            mediumTermButton.Location = new Point(48, 300);
            mediumTermButton.Name = "mediumTermButton";
            mediumTermButton.Size = new Size(309, 37);
            mediumTermButton.TabIndex = 4;
            mediumTermButton.Text = "Last 6 Months";
            mediumTermButton.UseVisualStyleBackColor = true;
            mediumTermButton.Click += mediumTermButton_Click;
            // 
            // longTermButton
            // 
            longTermButton.Enabled = false;
            longTermButton.Font = new Font("Segoe UI", 12F);
            longTermButton.Location = new Point(48, 343);
            longTermButton.Name = "longTermButton";
            longTermButton.Size = new Size(309, 37);
            longTermButton.TabIndex = 5;
            longTermButton.Text = "Last Year";
            longTermButton.UseVisualStyleBackColor = true;
            longTermButton.Click += longTermButton_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(409, 414);
            Controls.Add(longTermButton);
            Controls.Add(mediumTermButton);
            Controls.Add(shortTermButton);
            Controls.Add(connectButton);
            Controls.Add(headerLabel);
            Name = "Form1";
            Text = "Spotify Plus";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label headerLabel;
        private Button connectButton;
        private Button shortTermButton;
        private Button mediumTermButton;
        private Button longTermButton;
    }
}
