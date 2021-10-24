
using System;

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
            this.DisplayUpvoteButton = new System.Windows.Forms.PictureBox();
            this.DisplayPostScore = new System.Windows.Forms.Label();
            this.DisplayDownvoteButton = new System.Windows.Forms.PictureBox();
            this.DisplayPostContext = new System.Windows.Forms.Label();
            this.DisplayPostTitle = new System.Windows.Forms.Label();
            this.DisplayPostContent = new System.Windows.Forms.RichTextBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.DisplayPostCommentCount = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.DisplayUpvoteButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DisplayDownvoteButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // DisplayUpvoteButton
            // 
            this.DisplayUpvoteButton.Image = global::KozinskiAlamidiAssignment4.Properties.Resources.upVote_grey;
            this.DisplayUpvoteButton.Location = new System.Drawing.Point(12, 12);
            this.DisplayUpvoteButton.Name = "DisplayUpvoteButton";
            this.DisplayUpvoteButton.Size = new System.Drawing.Size(22, 23);
            this.DisplayUpvoteButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.DisplayUpvoteButton.TabIndex = 0;
            this.DisplayUpvoteButton.TabStop = false;
            // 
            // DisplayPostScore
            // 
            this.DisplayPostScore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.DisplayPostScore.AutoSize = true;
            this.DisplayPostScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DisplayPostScore.ForeColor = System.Drawing.Color.White;
            this.DisplayPostScore.Location = new System.Drawing.Point(11, 39);
            this.DisplayPostScore.Name = "DisplayPostScore";
            this.DisplayPostScore.Size = new System.Drawing.Size(26, 17);
            this.DisplayPostScore.TabIndex = 1;
            this.DisplayPostScore.Text = "SC";
            this.DisplayPostScore.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DisplayDownvoteButton
            // 
            this.DisplayDownvoteButton.Image = global::KozinskiAlamidiAssignment4.Properties.Resources.downVote_grey;
            this.DisplayDownvoteButton.Location = new System.Drawing.Point(11, 61);
            this.DisplayDownvoteButton.Name = "DisplayDownvoteButton";
            this.DisplayDownvoteButton.Size = new System.Drawing.Size(26, 24);
            this.DisplayDownvoteButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.DisplayDownvoteButton.TabIndex = 2;
            this.DisplayDownvoteButton.TabStop = false;
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
            // 
            // DisplayPostTitle
            // 
            this.DisplayPostTitle.AutoSize = true;
            this.DisplayPostTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DisplayPostTitle.ForeColor = System.Drawing.Color.White;
            this.DisplayPostTitle.Location = new System.Drawing.Point(60, 38);
            this.DisplayPostTitle.Name = "DisplayPostTitle";
            this.DisplayPostTitle.Size = new System.Drawing.Size(160, 29);
            this.DisplayPostTitle.TabIndex = 5;
            this.DisplayPostTitle.Text = "POST_TITLE";
            // 
            // DisplayPostContent
            // 
            this.DisplayPostContent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.DisplayPostContent.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DisplayPostContent.ForeColor = System.Drawing.Color.White;
            this.DisplayPostContent.Location = new System.Drawing.Point(60, 89);
            this.DisplayPostContent.Name = "DisplayPostContent";
            this.DisplayPostContent.ReadOnly = true;
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
            this.Controls.Add(this.DisplayDownvoteButton);
            this.Controls.Add(this.DisplayPostScore);
            this.Controls.Add(this.DisplayUpvoteButton);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "Form3";
            this.Text = "Form3";
            this.Load += new System.EventHandler(this.Form3_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DisplayUpvoteButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DisplayDownvoteButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox DisplayUpvoteButton;
        private System.Windows.Forms.Label DisplayPostScore;
        private System.Windows.Forms.PictureBox DisplayDownvoteButton;
        private System.Windows.Forms.Label DisplayPostContext;
        private System.Windows.Forms.Label DisplayPostTitle;
        private System.Windows.Forms.RichTextBox DisplayPostContent;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label DisplayPostCommentCount;
    }
}