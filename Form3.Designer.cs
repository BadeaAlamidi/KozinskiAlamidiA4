
namespace KozinskiAlamidiAssignment4
{
    partial class Form3
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.DisplayPostScore = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.DisplayPostContext = new System.Windows.Forms.Label();
            this.DisplayPostTitle = new System.Windows.Forms.Label();
            this.DisplayPostContent = new System.Windows.Forms.RichTextBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.DisplayPostCommentCount = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::KozinskiAlamidiAssignment4.Properties.Resources.upVote_grey;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(22, 23);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // DisplayPostScore
            // 
            this.DisplayPostScore.AutoSize = true;
            this.DisplayPostScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DisplayPostScore.ForeColor = System.Drawing.Color.White;
            this.DisplayPostScore.Location = new System.Drawing.Point(23, 38);
            this.DisplayPostScore.Name = "DisplayPostScore";
            this.DisplayPostScore.Size = new System.Drawing.Size(0, 20);
            this.DisplayPostScore.TabIndex = 1;
            this.DisplayPostScore.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::KozinskiAlamidiAssignment4.Properties.Resources.downVote_grey;
            this.pictureBox2.Location = new System.Drawing.Point(11, 61);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(26, 24);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 2;
            this.pictureBox2.TabStop = false;
            // 
            // DisplayPostContext
            // 
            this.DisplayPostContext.AutoSize = true;
            this.DisplayPostContext.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DisplayPostContext.ForeColor = System.Drawing.Color.White;
            this.DisplayPostContext.Location = new System.Drawing.Point(60, 12);
            this.DisplayPostContext.Name = "DisplayPostContext";
            this.DisplayPostContext.Size = new System.Drawing.Size(476, 17);
            this.DisplayPostContext.TabIndex = 3;
            this.DisplayPostContext.Text = "r/SUBREDDIT_HOME   |   Posted by u/AUTHOR_NAME TIME_FRAME ago";
            this.DisplayPostContext.Click += new System.EventHandler(this.DisplaySubHome_Click);
            // 
            // DisplayPostTitle
            // 
            this.DisplayPostTitle.AutoSize = true;
            this.DisplayPostTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DisplayPostTitle.ForeColor = System.Drawing.Color.White;
            this.DisplayPostTitle.Location = new System.Drawing.Point(60, 38);
            this.DisplayPostTitle.Name = "DisplayPostTitle";
            this.DisplayPostTitle.Size = new System.Drawing.Size(79, 29);
            this.DisplayPostTitle.TabIndex = 5;
            this.DisplayPostTitle.Text = "label1";
            // 
            // DisplayPostContent
            // 
            this.DisplayPostContent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.DisplayPostContent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DisplayPostContent.Enabled = false;
            this.DisplayPostContent.ForeColor = System.Drawing.Color.White;
            this.DisplayPostContent.Location = new System.Drawing.Point(60, 89);
            this.DisplayPostContent.Name = "DisplayPostContent";
            this.DisplayPostContent.Size = new System.Drawing.Size(664, 202);
            this.DisplayPostContent.TabIndex = 6;
            this.DisplayPostContent.Text = "";
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::KozinskiAlamidiAssignment4.Properties.Resources.comment_icon;
            this.pictureBox3.Location = new System.Drawing.Point(60, 308);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(23, 21);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox3.TabIndex = 7;
            this.pictureBox3.TabStop = false;
            // 
            // DisplayPostCommentCount
            // 
            this.DisplayPostCommentCount.AutoSize = true;
            this.DisplayPostCommentCount.ForeColor = System.Drawing.Color.White;
            this.DisplayPostCommentCount.Location = new System.Drawing.Point(90, 310);
            this.DisplayPostCommentCount.Name = "DisplayPostCommentCount";
            this.DisplayPostCommentCount.Size = new System.Drawing.Size(205, 17);
            this.DisplayPostCommentCount.TabIndex = 8;
            this.DisplayPostCommentCount.Text = "COMMENT_COUNT Comments";
            this.DisplayPostCommentCount.Click += new System.EventHandler(this.DisplayPostCommentCount_Click);
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.DisplayPostCommentCount);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.DisplayPostContent);
            this.Controls.Add(this.DisplayPostTitle);
            this.Controls.Add(this.DisplayPostContext);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.DisplayPostScore);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form3";
            this.Text = "Form3";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label DisplayPostScore;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label DisplayPostContext;
        private System.Windows.Forms.Label DisplayPostTitle;
        private System.Windows.Forms.RichTextBox DisplayPostContent;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label DisplayPostCommentCount;
    }
}