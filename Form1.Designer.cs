
namespace KozinskiAlamidiAssignment2
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
            this.label1 = new System.Windows.Forms.Label();
            this.userSelection = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.subredditSelection = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.postSelection = new System.Windows.Forms.ListBox();
            this.systemOutput = new System.Windows.Forms.RichTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.passwordInput = new System.Windows.Forms.TextBox();
            this.loginButton = new System.Windows.Forms.Button();
            this.deletePostButton = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.commentSelection = new System.Windows.Forms.ListBox();
            this.deleteCommentButton = new System.Windows.Forms.Button();
            this.addReplyButton = new System.Windows.Forms.Button();
            this.replyInput = new System.Windows.Forms.RichTextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.memberCount = new System.Windows.Forms.Label();
            this.activeCount = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.OrangeRed;
            this.label1.Location = new System.Drawing.Point(8, 11);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Users";
            // 
            // userSelection
            // 
            this.userSelection.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userSelection.FormattingEnabled = true;
            this.userSelection.ItemHeight = 17;
            this.userSelection.Location = new System.Drawing.Point(13, 39);
            this.userSelection.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.userSelection.Name = "userSelection";
            this.userSelection.Size = new System.Drawing.Size(435, 191);
            this.userSelection.TabIndex = 1;
            this.userSelection.SelectedValueChanged += new System.EventHandler(this.userSelection_SelectedValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.OrangeRed;
            this.label2.Location = new System.Drawing.Point(440, 11);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 25);
            this.label2.TabIndex = 2;
            this.label2.Text = "Subreddits";
            // 
            // subredditSelection
            // 
            this.subredditSelection.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.subredditSelection.FormattingEnabled = true;
            this.subredditSelection.ItemHeight = 17;
            this.subredditSelection.Location = new System.Drawing.Point(457, 39);
            this.subredditSelection.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.subredditSelection.Name = "subredditSelection";
            this.subredditSelection.Size = new System.Drawing.Size(169, 191);
            this.subredditSelection.TabIndex = 3;
            this.subredditSelection.SelectedValueChanged += new System.EventHandler(this.subredditSelection_SelectedValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.OrangeRed;
            this.label3.Location = new System.Drawing.Point(631, 11);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 25);
            this.label3.TabIndex = 4;
            this.label3.Text = "Posts";
            // 
            // postSelection
            // 
            this.postSelection.DisplayMember = "AbbreviateContent";
            this.postSelection.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.postSelection.FormattingEnabled = true;
            this.postSelection.ItemHeight = 17;
            this.postSelection.Location = new System.Drawing.Point(636, 39);
            this.postSelection.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.postSelection.Name = "postSelection";
            this.postSelection.Size = new System.Drawing.Size(811, 191);
            this.postSelection.TabIndex = 5;
            this.postSelection.SelectedValueChanged += new System.EventHandler(this.postSelection_SelectedValueChanged);
            // 
            // systemOutput
            // 
            this.systemOutput.Location = new System.Drawing.Point(19, 581);
            this.systemOutput.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.systemOutput.Name = "systemOutput";
            this.systemOutput.ReadOnly = true;
            this.systemOutput.Size = new System.Drawing.Size(1428, 80);
            this.systemOutput.TabIndex = 6;
            this.systemOutput.Text = "";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.OrangeRed;
            this.label4.Location = new System.Drawing.Point(453, 238);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 17);
            this.label4.TabIndex = 7;
            this.label4.Text = "Members";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.OrangeRed;
            this.label5.Location = new System.Drawing.Point(579, 238);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 17);
            this.label5.TabIndex = 8;
            this.label5.Text = "Active";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.OrangeRed;
            this.label6.Location = new System.Drawing.Point(8, 247);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(98, 25);
            this.label6.TabIndex = 9;
            this.label6.Text = "Password";
            // 
            // passwordInput
            // 
            this.passwordInput.Location = new System.Drawing.Point(13, 276);
            this.passwordInput.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.passwordInput.Name = "passwordInput";
            this.passwordInput.PasswordChar = '*';
            this.passwordInput.Size = new System.Drawing.Size(185, 22);
            this.passwordInput.TabIndex = 10;
            // 
            // loginButton
            // 
            this.loginButton.Location = new System.Drawing.Point(215, 276);
            this.loginButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.loginButton.Name = "loginButton";
            this.loginButton.Size = new System.Drawing.Size(100, 28);
            this.loginButton.TabIndex = 11;
            this.loginButton.Text = "Log-In";
            this.loginButton.UseVisualStyleBackColor = true;
            this.loginButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.loginButton_MouseClick);
            // 
            // deletePostButton
            // 
            this.deletePostButton.ForeColor = System.Drawing.Color.Red;
            this.deletePostButton.Location = new System.Drawing.Point(1312, 244);
            this.deletePostButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.deletePostButton.Name = "deletePostButton";
            this.deletePostButton.Size = new System.Drawing.Size(136, 28);
            this.deletePostButton.TabIndex = 12;
            this.deletePostButton.Text = "Delete Post";
            this.deletePostButton.UseVisualStyleBackColor = true;
            this.deletePostButton.Click += new System.EventHandler(this.deletePostButton_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.OrangeRed;
            this.label7.Location = new System.Drawing.Point(8, 553);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(142, 25);
            this.label7.TabIndex = 13;
            this.label7.Text = "System Output";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.OrangeRed;
            this.label8.Location = new System.Drawing.Point(8, 316);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(107, 25);
            this.label8.TabIndex = 14;
            this.label8.Text = "Comments";
            // 
            // commentSelection
            // 
            this.commentSelection.DisplayMember = "AbbreviatedContent";
            this.commentSelection.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.commentSelection.FormattingEnabled = true;
            this.commentSelection.ItemHeight = 17;
            this.commentSelection.Location = new System.Drawing.Point(19, 352);
            this.commentSelection.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.commentSelection.Name = "commentSelection";
            this.commentSelection.Size = new System.Drawing.Size(797, 174);
            this.commentSelection.TabIndex = 15;
            this.commentSelection.SelectedValueChanged += new System.EventHandler(this.commentSelection_SelectedValueChanged);
            // 
            // deleteCommentButton
            // 
            this.deleteCommentButton.ForeColor = System.Drawing.Color.Red;
            this.deleteCommentButton.Location = new System.Drawing.Point(825, 505);
            this.deleteCommentButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.deleteCommentButton.Name = "deleteCommentButton";
            this.deleteCommentButton.Size = new System.Drawing.Size(136, 28);
            this.deleteCommentButton.TabIndex = 16;
            this.deleteCommentButton.Text = "Delete Comment";
            this.deleteCommentButton.UseVisualStyleBackColor = true;
            this.deleteCommentButton.Click += new System.EventHandler(this.deleteReplyButton_Click);
            // 
            // addReplyButton
            // 
            this.addReplyButton.ForeColor = System.Drawing.Color.Lime;
            this.addReplyButton.Location = new System.Drawing.Point(1312, 505);
            this.addReplyButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.addReplyButton.Name = "addReplyButton";
            this.addReplyButton.Size = new System.Drawing.Size(136, 28);
            this.addReplyButton.TabIndex = 17;
            this.addReplyButton.Text = "Add Reply";
            this.addReplyButton.UseVisualStyleBackColor = true;
            this.addReplyButton.Click += new System.EventHandler(this.addReplyButton_Click);
            // 
            // replyInput
            // 
            this.replyInput.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.replyInput.Location = new System.Drawing.Point(825, 353);
            this.replyInput.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.replyInput.Name = "replyInput";
            this.replyInput.Size = new System.Drawing.Size(621, 143);
            this.replyInput.TabIndex = 18;
            this.replyInput.Text = "";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.OrangeRed;
            this.label9.Location = new System.Drawing.Point(820, 316);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(102, 25);
            this.label9.TabIndex = 19;
            this.label9.Text = "Add Reply";
            // 
            // memberCount
            // 
            this.memberCount.AutoSize = true;
            this.memberCount.ForeColor = System.Drawing.Color.Black;
            this.memberCount.Location = new System.Drawing.Point(528, 238);
            this.memberCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.memberCount.Name = "memberCount";
            this.memberCount.Size = new System.Drawing.Size(0, 17);
            this.memberCount.TabIndex = 20;
            // 
            // activeCount
            // 
            this.activeCount.AutoSize = true;
            this.activeCount.ForeColor = System.Drawing.Color.Black;
            this.activeCount.Location = new System.Drawing.Point(628, 238);
            this.activeCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.activeCount.Name = "activeCount";
            this.activeCount.Size = new System.Drawing.Size(0, 17);
            this.activeCount.TabIndex = 21;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1464, 677);
            this.Controls.Add(this.activeCount);
            this.Controls.Add(this.memberCount);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.replyInput);
            this.Controls.Add(this.addReplyButton);
            this.Controls.Add(this.deleteCommentButton);
            this.Controls.Add(this.commentSelection);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.deletePostButton);
            this.Controls.Add(this.loginButton);
            this.Controls.Add(this.passwordInput);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.systemOutput);
            this.Controls.Add(this.postSelection);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.subredditSelection);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.userSelection);
            this.Controls.Add(this.label1);
            this.ForeColor = System.Drawing.Color.Black;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.Text = "Discount Reddit";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox userSelection;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox subredditSelection;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox postSelection;
        private System.Windows.Forms.RichTextBox systemOutput;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox passwordInput;
        private System.Windows.Forms.Button loginButton;
        private System.Windows.Forms.Button deletePostButton;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ListBox commentSelection;
        private System.Windows.Forms.Button deleteCommentButton;
        private System.Windows.Forms.Button addReplyButton;
        private System.Windows.Forms.RichTextBox replyInput;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label memberCount;
        private System.Windows.Forms.Label activeCount;
    }
}

