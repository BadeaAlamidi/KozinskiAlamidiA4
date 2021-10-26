
namespace KozinskiAlamidiAssignment4
{
    partial class Form1
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
            this.systemOutput = new System.Windows.Forms.RichTextBox();
            this.loginButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.subredditSelection = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.addPostButton = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // systemOutput
            // 
            this.systemOutput.Location = new System.Drawing.Point(12, 939);
            this.systemOutput.Name = "systemOutput";
            this.systemOutput.ReadOnly = true;
            this.systemOutput.Size = new System.Drawing.Size(1072, 66);
            this.systemOutput.TabIndex = 6;
            this.systemOutput.Text = "";
            // 
            // loginButton
            // 
            this.loginButton.BackColor = System.Drawing.Color.White;
            this.loginButton.Location = new System.Drawing.Point(1135, 16);
            this.loginButton.Name = "loginButton";
            this.loginButton.Size = new System.Drawing.Size(231, 28);
            this.loginButton.TabIndex = 11;
            this.loginButton.Text = "Log-In";
            this.loginButton.UseVisualStyleBackColor = false;
            this.loginButton.Click += new System.EventHandler(this.loginButton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::KozinskiAlamidiAssignment4.Properties.Resources.logo;
            this.pictureBox1.Location = new System.Drawing.Point(0, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(152, 60);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 22;
            this.pictureBox1.TabStop = false;
            // 
            // subredditSelection
            // 
            this.subredditSelection.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.subredditSelection.FormattingEnabled = true;
            this.subredditSelection.Location = new System.Drawing.Point(158, 21);
            this.subredditSelection.Name = "subredditSelection";
            this.subredditSelection.Size = new System.Drawing.Size(229, 28);
            this.subredditSelection.TabIndex = 25;
            this.subredditSelection.SelectedValueChanged += new System.EventHandler(this.subredditSelection_SelectedValueChanged);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Location = new System.Drawing.Point(12, 69);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1354, 852);
            this.panel1.TabIndex = 26;
            // 
            // addPostButton
            // 
            this.addPostButton.BackColor = System.Drawing.Color.White;
            this.addPostButton.Enabled = false;
            this.addPostButton.Location = new System.Drawing.Point(993, 19);
            this.addPostButton.Name = "addPostButton";
            this.addPostButton.Size = new System.Drawing.Size(136, 25);
            this.addPostButton.TabIndex = 27;
            this.addPostButton.Text = "Add Post";
            this.addPostButton.UseVisualStyleBackColor = false;
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(393, 24);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(594, 22);
            this.textBox1.TabIndex = 28;
            this.textBox1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyUp);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1378, 1017);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.addPostButton);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.subredditSelection);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.loginButton);
            this.Controls.Add(this.systemOutput);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "Form1";
            this.Text = "Discount Reddit";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.RichTextBox systemOutput;
        private System.Windows.Forms.Button loginButton;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ComboBox subredditSelection;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button addPostButton;
        private System.Windows.Forms.TextBox textBox1;
    }
}

