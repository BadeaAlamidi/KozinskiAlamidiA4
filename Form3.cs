using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KozinskiAlamidiAssignment4
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        public Form3(uint postID)
        {
            InitializeComponent(postID);
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        #region Programmer generated code

        /// <summary>
        /// Programmer-defined method that supports a particular post.
        /// </summary>
        private void InitializeComponent(uint postID)
        {
            // VARIABLES
            Post post = Program.globalPosts[postID];
            string postTitleText = post.Title;
            string postContentText = post.PostContent;
            string commentCountText = post.postComments.Count.ToString();
            string subText = Program.globalSubreddits[post.subHomeId].Name;
            string authorText = Program.globalUsers[post.AuthorId].Name;
            string timespanText = "";
            TimeSpan timeSincePost = DateTime.Now - post.DateString;
            //
            // TIME MEASUREMENT CALCULATION
            // Month calculation is approximate
            //
            if (timeSincePost < new TimeSpan(1, 0, 0))
                timespanText = $"{timeSincePost.Minutes} minutes ago";
            else if (timeSincePost < new TimeSpan(1, 0, 0, 0))
                timespanText = $"{timeSincePost.Hours} hours ago";
            else if (timeSincePost < new TimeSpan(30, 0, 0, 0))
                timespanText = $"{timeSincePost.Days} days ago";
            else if (timeSincePost < new TimeSpan(365, 0, 0, 0))
                timespanText = $"{timeSincePost.Days / 30} months ago";
            else
                timespanText = $"{timeSincePost.Days / 365} years ago";
            //
            // BEGIN FORM INITIALIZATION
            //
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
            this.DisplayPostScore.Text = $"{post.Score}";
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
            this.DisplayPostContext.Text = $"r/{subText}   |   Posted by u/{authorText} {timespanText}";
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
            this.DisplayPostTitle.Text = $"{postTitleText}";
            // 
            // DisplayPostContent
            // 
            this.DisplayPostContent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20))))); ;
            this.DisplayPostContent.BorderStyle = BorderStyle.None;
            this.DisplayPostContent.ForeColor = System.Drawing.Color.White;
            this.DisplayPostContent.Location = new System.Drawing.Point(60, 89);
            this.DisplayPostContent.Name = "DisplayPostContent";
            this.DisplayPostContent.ReadOnly = true;
            this.DisplayPostContent.Size = new System.Drawing.Size(664, 202);
            this.DisplayPostContent.TabIndex = 6;
            this.DisplayPostContent.Text = $"{postContentText}";
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
            this.DisplayPostCommentCount.Text = $"{commentCountText} Comments";
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
    }
}
