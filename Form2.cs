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
    public partial class Form2 : Form
    {
        private uint postID;

        private int offsetXComment = 0;
        private int offsetYComment = 0;
        private int offsetYCommentContainer = 0;

        public uint PostID
        {
            get { return postID; }
            set { postID = value; }
        }

        public int OffsetXComment
        {
            get { return offsetXComment; }
            set { offsetXComment = value; }
        }
        public int OffsetYComment
        {
            get { return offsetYComment; }
            set { offsetYComment = value; }
        }
        public int OffsetYCommentContainer
        {
            get { return offsetYCommentContainer; }
            set { offsetYCommentContainer = value; }
        }

        // Default constructor
        public Form2()
        {
            InitializeComponent();
        }

        // Constructor for a particular post
        public Form2(uint newPostID)
        {
            InitializeComponent(newPostID);
        }

        public void Form3_Load(object sender, EventArgs e)
        {
            PrintComments(PostID);
        }

        #region InitializeComponent(PostID): Programmer generated layout code

        /// <summary>
        /// Programmer-defined method that supports a particular post ID.
        /// </summary>
        private void InitializeComponent(uint postID)
        {
            // VARIABLES
            PostID = postID;
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
            this.DisplayPostUpvoteButton = new System.Windows.Forms.PictureBox();
            this.DisplayPostScore = new System.Windows.Forms.Label();
            this.DisplayPostDownvoteButton = new System.Windows.Forms.PictureBox();
            this.DisplayPostContext = new System.Windows.Forms.Label();
            this.DisplayPostTitle = new System.Windows.Forms.Label();
            this.DisplayPostContent = new System.Windows.Forms.RichTextBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.DisplayPostCommentCount = new System.Windows.Forms.Label();
            this.DisplayCommentContainer = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.DisplayPostUpvoteButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DisplayPostDownvoteButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // DisplayUpvoteButton
            // 
            this.DisplayPostUpvoteButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.DisplayPostUpvoteButton.Image = global::KozinskiAlamidiAssignment4.Properties.Resources.upVote_grey;
            this.DisplayPostUpvoteButton.Location = new System.Drawing.Point(11, 12);
            this.DisplayPostUpvoteButton.Name = "DisplayUpvoteButton";
            this.DisplayPostUpvoteButton.Size = new System.Drawing.Size(42, 23);
            this.DisplayPostUpvoteButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.DisplayPostUpvoteButton.TabIndex = 0;
            this.DisplayPostUpvoteButton.TabStop = false;
            // 
            // DisplayPostScore
            // 
            this.DisplayPostScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DisplayPostScore.ForeColor = System.Drawing.Color.White;
            this.DisplayPostScore.Location = new System.Drawing.Point(12, 43);
            this.DisplayPostScore.Name = "DisplayPostScore";
            this.DisplayPostScore.Size = new System.Drawing.Size(41, 17);
            this.DisplayPostScore.TabIndex = 1;
            this.DisplayPostScore.Text = $"{post.Score}";
            this.DisplayPostScore.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DisplayDownvoteButton
            // 
            this.DisplayPostDownvoteButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.DisplayPostDownvoteButton.Image = global::KozinskiAlamidiAssignment4.Properties.Resources.downVote_grey;
            this.DisplayPostDownvoteButton.Location = new System.Drawing.Point(11, 68);
            this.DisplayPostDownvoteButton.Name = "DisplayDownvoteButton";
            this.DisplayPostDownvoteButton.Size = new System.Drawing.Size(42, 24);
            this.DisplayPostDownvoteButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.DisplayPostDownvoteButton.TabIndex = 2;
            this.DisplayPostDownvoteButton.TabStop = false;
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
            this.DisplayPostContent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.DisplayPostContent.BorderStyle = BorderStyle.None;
            this.DisplayPostContent.ForeColor = System.Drawing.Color.White;
            this.DisplayPostContent.Location = new System.Drawing.Point(60, 80);
            this.DisplayPostContent.Name = "DisplayPostContent";
            this.DisplayPostContent.ReadOnly = true;
            this.DisplayPostContent.Size = new System.Drawing.Size(665, 100);
            this.DisplayPostContent.TabIndex = 6;
            this.DisplayPostContent.Text = $"{postContentText}";
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::KozinskiAlamidiAssignment4.Properties.Resources.comment_icon;
            this.pictureBox3.Location = new System.Drawing.Point(60, 195);
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
            this.DisplayPostCommentCount.Location = new System.Drawing.Point(90, 200);
            this.DisplayPostCommentCount.Name = "DisplayPostCommentCount";
            this.DisplayPostCommentCount.Size = new System.Drawing.Size(205, 17);
            this.DisplayPostCommentCount.TabIndex = 8;
            this.DisplayPostCommentCount.Text = $"{commentCountText} Comments";
            //
            // DisplayCommentBox
            //
            if (Program.activeUser != null)
            {

            }
            // 
            // DisplayCommentContainer
            // 
            this.DisplayCommentContainer.AutoScroll = true;
            this.DisplayCommentContainer.Location = new System.Drawing.Point(60, 235);
            this.DisplayCommentContainer.Name = "DisplayCommentContainer";
            this.DisplayCommentContainer.Size = new System.Drawing.Size(665, 300);
            this.DisplayCommentContainer.TabIndex = 9;
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.DisplayCommentContainer);
            this.Controls.Add(this.DisplayPostCommentCount);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.DisplayPostContent);
            this.Controls.Add(this.DisplayPostTitle);
            this.Controls.Add(this.DisplayPostContext);
            this.Controls.Add(this.DisplayPostDownvoteButton);
            this.Controls.Add(this.DisplayPostScore);
            this.Controls.Add(this.DisplayPostUpvoteButton);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "Form3";
            this.Text = "Post View";
            this.Load += new System.EventHandler(this.Form3_Load);
            this.DisplayPostUpvoteButton.MouseEnter += new System.EventHandler(this.PostUpvote_MouseEnter);
            this.DisplayPostUpvoteButton.MouseLeave += new System.EventHandler(this.PostUpvote_MouseLeave);
            this.DisplayPostUpvoteButton.Click += new System.EventHandler(this.PostUpvote_Click);
            this.DisplayPostDownvoteButton.MouseEnter += new System.EventHandler(this.PostDownvote_MouseEnter);
            this.DisplayPostDownvoteButton.MouseLeave += new System.EventHandler(this.PostDownvote_MouseLeave);
            this.DisplayPostDownvoteButton.Click += new System.EventHandler(this.PostDownvote_Click);
            ((System.ComponentModel.ISupportInitialize)(this.DisplayPostUpvoteButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DisplayPostDownvoteButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        #region Upvote/downvote button event handlers

        public void PostUpvote_MouseEnter(object sender, EventArgs e)
        {
            this.DisplayPostUpvoteButton.Image = global::KozinskiAlamidiAssignment4.Properties.Resources.upVote_red;
        }

        public void PostUpvote_MouseLeave(object sender, EventArgs e)
        {
            this.DisplayPostUpvoteButton.Image = global::KozinskiAlamidiAssignment4.Properties.Resources.upVote_grey;
        }

        public void PostUpvote_Click(object sender, EventArgs e)
        {
            if (Program.activeUser == null)
            {
                MessageBox.Show("You must be logged in to vote on posts.");
                return;
            }

            bool hasVoted = Program.activeUser.PostVoteStatuses.ContainsKey(postID);

            if (!hasVoted)
            {
                Program.activeUser.PostVoteStatuses.Add(postID, 1);
                this.DisplayPostUpvoteButton.Image = global::KozinskiAlamidiAssignment4.Properties.Resources.upVote_red;
            }
            else
            {
                if (Program.activeUser.PostVoteStatuses[postID] < 1)
                {
                    Program.activeUser.PostVoteStatuses[postID] = 1;
                    this.DisplayPostUpvoteButton.Image = global::KozinskiAlamidiAssignment4.Properties.Resources.upVote_red;
                    this.DisplayPostDownvoteButton.Image = global::KozinskiAlamidiAssignment4.Properties.Resources.upVote_grey;
                }
                else
                {
                    Program.activeUser.PostVoteStatuses[postID] = 0;
                    this.DisplayPostUpvoteButton.Image = global::KozinskiAlamidiAssignment4.Properties.Resources.upVote_grey;
                }
            }
        }
        
        public void PostDownvote_MouseEnter(object sender, EventArgs e)
        {
            this.DisplayPostDownvoteButton.Image = global::KozinskiAlamidiAssignment4.Properties.Resources.downVote_blue;
        }

        public void PostDownvote_MouseLeave(object sender, EventArgs e)
        {
            this.DisplayPostDownvoteButton.Image = global::KozinskiAlamidiAssignment4.Properties.Resources.downVote_grey;
        }

        public void PostDownvote_Click(object sender, EventArgs e)
        {
            if (Program.activeUser == null)
            {
                MessageBox.Show("You must be logged in to vote on posts.");
                return;
            }

            bool hasVoted = Program.activeUser.PostVoteStatuses.ContainsKey(postID);

            if (!hasVoted)
            {
                Program.activeUser.PostVoteStatuses.Add(postID, -1);
                this.DisplayPostDownvoteButton.Image = global::KozinskiAlamidiAssignment4.Properties.Resources.downVote_blue;
            }
            else
            {
                if (Program.activeUser.PostVoteStatuses[postID] > -1)
                {
                    Program.activeUser.PostVoteStatuses[postID] = -1;
                    this.DisplayPostDownvoteButton.Image = global::KozinskiAlamidiAssignment4.Properties.Resources.downVote_blue;
                    this.DisplayPostUpvoteButton.Image = global::KozinskiAlamidiAssignment4.Properties.Resources.upVote_grey;
                }
                else
                {
                    Program.activeUser.PostVoteStatuses[postID] = 0;
                    this.DisplayPostDownvoteButton.Image = global::KozinskiAlamidiAssignment4.Properties.Resources.downVote_grey;
                }
            }
        }

        #endregion

        #region addReply
        /**
         * Method Name: addReplyButton_Click
         * 
         * The following triggers when the user clicks on this component( represented by a button: Add Reply)
         * 
         * This event includes the following responsibilities
         *  - inform the user about whether their reply attempt was appropriate: disallow empty replies, disallow replying without
         *    logging in, disallow replies with no chosen post, disallow average users to comment on locked posts, disallow users from
         *    using foul language
         *  - replying to posts when only a post is chosen
         *  - replying to a comment when both a post and a comment are chosen
         *  - displaying the newly added comment by refreshing the comment listbox
         * 
         * Returns: void
         * 
         * Notes: NONE
         */
        /*
        private void addReplyButton_Click(object sender, EventArgs e)
        {

            try
            {
                if (replyInput.Text == "")
                {
                    systemOutput.Clear();
                    systemOutput.AppendText("You may not post an empty comment\n");
                    return;
                }
                if (Program.activeUser == null)
                {
                    systemOutput.Clear();
                    systemOutput.AppendText("Log-in is required to add posts and comments\n");
                    return;
                }
                // the following check is for when neither a post nor a comment is chosen
                if (postSelection.SelectedIndex == -1)
                {
                    systemOutput.Clear();
                    systemOutput.AppendText("Choose a post to reply to a post, or choose a post + comment to reply to a comment.\n");
                    return;
                }
                Post chosenPost = postSelection.SelectedItem as Post;
                if (chosenPost == null)
                {
                    systemOutput.Clear();
                    systemOutput.AppendText("You have not selected a valid post\n");
                    return;
                }
                if (chosenPost.Locked == true)
                {
                    systemOutput.Clear();
                    systemOutput.AppendText("Post is marked as \'Locked\' -- replies are disabled.\n");
                    return;
                }
                // case for when posting a reply to a post
                if (commentSelection.SelectedIndex == -1)
                {   
                    try
                    {
                        Comment newComment = new Comment(replyInput.Text, Program.activeUser.Id, chosenPost.Id, 0);
                        chosenPost.postComments.Add(newComment.Id, newComment);
                    }
                    catch (FoulLanguageException) {
                        systemOutput.Clear();
                        systemOutput.AppendText("Please refrain from using foul language and try again\n");
                        MessageBox.Show("Please refrain from using foul language and try again\n");
                        return;
                    }
                }
                // this will trigger if a comment is selected (the commentSelection.SelectedIndex is not -1)
                else
                {
                    Comment chosenComment = commentSelection.SelectedItem as Comment;
                    if (chosenComment == null)
                    {
                        systemOutput.Clear();
                        systemOutput.AppendText("You have not selected a valid comment\n");
                        return;
                    }
                    try
                    {
                        Comment newComment = new Comment(replyInput.Text, Program.activeUser.Id, chosenComment.Id, chosenComment.Indentation + 1);
                        chosenPost.postComments.Add(newComment.Id, newComment);
                    }
                    catch (FoulLanguageException)
                    {
                        MessageBox.Show("Please refrain from using foul language and try again\n");
                        systemOutput.AppendText("Please refrain from using foul language and try again\n");
                        return;
                    }
                }
                // refreshes comments printed to comment ListBox
                commentSelection.Items.Clear();
                foreach (Comment comment in chosenPost.postComments.Values.OrderBy(postComment => postComment).ThenBy(postComment => postComment.TimeStamp))
                {
                    commentSelection.Items.Add(comment);
                    PrintChildComments(comment);
                }
                replyInput.Clear();
                systemOutput.Clear();
                systemOutput.AppendText("Comment successfully added\n");
            }
            catch (Exception exception)
            {
                systemOutput.Clear();
                systemOutput.AppendText(exception.Message);
            }
        }
        */
        #endregion

        #region PrintComments(uint postID)
        /**
         * Method Name: PrintComments
         * 
         * Iterates recursively through all comments to print them
         * 
         * Returns: void
         * 
         * Notes:
         * Caps the number of comment levels printed
         */
        void PrintComments(uint postID)
        {
            Post chosenPost = Program.globalPosts[postID];

            //try
            {
                // Populates comment box (comments are sorted by score, then by date/time posted)
                // Starts recursion at the post level
                // Displays abbreviated content if appropriate: commentSelection.DisplayMember -> Comment.AbbreviatedContent property -> ToString("ListBox") (extra step required by assignment specification)
                foreach (Comment postComment in chosenPost.postComments.Values.OrderBy(postComment => postComment).ThenBy(postComment => postComment.TimeStamp))
                {
                    DisplayComment newComment = new DisplayComment(postComment);
                    DisplayCommentContainer.Controls.Add(newComment);
                    
                    PrintChildComments(postComment);
                }

                /**
                 * Method Name: PrintChildComments
                 * 
                 * Iterates recursively through comment replies to print them
                 * 
                 * Returns: void
                 * 
                 * Notes:
                 * Caps the number of comment levels printed
                 */
                void PrintChildComments(Comment currentComment)
                {
                    foreach (Comment commentReply in currentComment.commentReplies.Values.OrderBy(comment => comment).ThenBy(postComment => postComment.TimeStamp))
                    {
                        OffsetXComment = Convert.ToInt32(commentReply.Indentation * 40);

                        if (commentReply.Indentation < 5)
                        {
                            //MessageBox.Show(commentReply.Content);
                            DisplayComment newComment = new DisplayComment(commentReply);
                            DisplayCommentContainer.Controls.Add(newComment);
                        }
                        else
                        {
                            //MessageBox.Show(commentReply.AuthorID.ToString());

                            DisplayNoComment noComment = new DisplayNoComment();
                            DisplayCommentContainer.Controls.Add(noComment);
                        }

                        // Recursive call
                        PrintChildComments(commentReply);
                    }
                }
            }
            //catch (Exception exception)
            //{
            //    MessageBox.Show(exception.Message);
            //}
        }
        #endregion

        class DisplayComment : Panel
        {
            private Form2 form3Instance;
            private int commentWidth;
            private int commentHeight;

            public DisplayComment(Comment newComment)
            {
                form3Instance = (Form2)Application.OpenForms["Form3"];
                commentWidth = form3Instance.DisplayCommentContainer.Width
                               - form3Instance.OffsetXComment
                               - (Margin.Horizontal * 4);
                commentHeight = 125;

                Location = new Point(form3Instance.OffsetXComment, form3Instance.OffsetYComment);

                InitializeComponent(newComment);

                Width = commentWidth;
                Height = commentHeight;

                form3Instance.OffsetYComment += Height;
            }

            #region InitializeComponent(Comment newComment): Programmer generated layout code

            /// <summary>
            /// Programmer-defined method that supports a particular comment.
            /// </summary>
            private void InitializeComponent(Comment comment)
            {
                // VARIABLES
                int column2 = 60;
                string commentContentText = comment.Content;
                string authorText = Program.globalUsers[comment.AuthorID].Name;
                string timespanText = "";
                TimeSpan timeSincePost = DateTime.Now - comment.TimeStamp;
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
                this.DisplayCommentUpvoteButton = new System.Windows.Forms.PictureBox();
                this.DisplayCommentScore = new System.Windows.Forms.Label();
                this.DisplayCommentDownvoteButton = new System.Windows.Forms.PictureBox();
                this.DisplayCommentContext = new System.Windows.Forms.Label();
                this.DisplayCommentContent = new System.Windows.Forms.RichTextBox();
                this.DisplayReplyIcon = new System.Windows.Forms.PictureBox();
                ((System.ComponentModel.ISupportInitialize)(this.DisplayCommentUpvoteButton)).BeginInit();
                ((System.ComponentModel.ISupportInitialize)(this.DisplayCommentDownvoteButton)).BeginInit();
                ((System.ComponentModel.ISupportInitialize)(this.DisplayReplyIcon)).BeginInit();
                this.SuspendLayout();
                // 
                // DisplayUpvoteButton
                // 
                this.DisplayCommentUpvoteButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
                this.DisplayCommentUpvoteButton.Image = global::KozinskiAlamidiAssignment4.Properties.Resources.upVote_grey;
                this.DisplayCommentUpvoteButton.Location = new System.Drawing.Point(11, 12);
                this.DisplayCommentUpvoteButton.Name = "DisplayCommentUpvoteButton";
                this.DisplayCommentUpvoteButton.Size = new System.Drawing.Size(42, 23);
                this.DisplayCommentUpvoteButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
                this.DisplayCommentUpvoteButton.TabIndex = 10;
                this.DisplayCommentUpvoteButton.TabStop = false;
                // 
                // DisplayCommentScore
                // 
                this.DisplayCommentScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.DisplayCommentScore.ForeColor = System.Drawing.Color.White;
                this.DisplayCommentScore.Location = new System.Drawing.Point(12, 43);
                this.DisplayCommentScore.Name = "DisplayCommentScore";
                this.DisplayCommentScore.Size = new System.Drawing.Size(41, 17);
                this.DisplayCommentScore.TabIndex = 11;
                this.DisplayCommentScore.Text = $"{comment.Score}";
                this.DisplayCommentScore.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                // 
                // DisplayDownvoteButton
                // 
                this.DisplayCommentDownvoteButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
                this.DisplayCommentDownvoteButton.Image = global::KozinskiAlamidiAssignment4.Properties.Resources.downVote_grey;
                this.DisplayCommentDownvoteButton.Location = new System.Drawing.Point(11, 68);
                this.DisplayCommentDownvoteButton.Name = "DisplayCommentDownvoteButton";
                this.DisplayCommentDownvoteButton.Size = new System.Drawing.Size(42, 24);
                this.DisplayCommentDownvoteButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
                this.DisplayCommentDownvoteButton.TabIndex = 12;
                this.DisplayCommentDownvoteButton.TabStop = false;
                // 
                // DisplayPostContext
                // 
                this.DisplayCommentContext.AutoSize = true;
                this.DisplayCommentContext.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.DisplayCommentContext.ForeColor = System.Drawing.Color.White;
                this.DisplayCommentContext.Location = new System.Drawing.Point(column2, 12);
                this.DisplayCommentContext.Name = "DisplayCommentContext";
                this.DisplayCommentContext.Size = new System.Drawing.Size(commentWidth - column2, 17);
                this.DisplayCommentContext.TabIndex = 13;
                this.DisplayCommentContext.Text = $"{authorText} | {timespanText}";
                // 
                // DisplayCommentContent
                // 
                this.DisplayCommentContent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
                this.DisplayCommentContent.BorderStyle = BorderStyle.None;
                this.DisplayCommentContent.ForeColor = System.Drawing.Color.White;
                this.DisplayCommentContent.Location = new System.Drawing.Point(column2, 40);
                this.DisplayCommentContent.Name = "DisplayCommentContent";
                this.DisplayCommentContent.ReadOnly = true;
                this.DisplayCommentContent.Size = new System.Drawing.Size(commentWidth - column2, 40);
                this.DisplayCommentContent.TabIndex = 14;
                this.DisplayCommentContent.Text = $"{commentContentText}";
                // 
                // DisplayReplyIcon
                // 
                this.DisplayReplyIcon.Image = global::KozinskiAlamidiAssignment4.Properties.Resources.reply_icon;
                this.DisplayReplyIcon.Location = new System.Drawing.Point(column2, 40 + 40 + 6);
                this.DisplayReplyIcon.Name = "DisplayReplyIcon";
                this.DisplayReplyIcon.Size = new System.Drawing.Size(23, 21);
                this.DisplayReplyIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
                this.DisplayReplyIcon.TabIndex = 15;
                this.DisplayReplyIcon.TabStop = false;
                // 
                // Form4
                // 
                //this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
                //this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
                this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
                //this.ClientSize = new System.Drawing.Size(800, 450);
                this.Controls.Add(this.DisplayReplyIcon);
                this.Controls.Add(this.DisplayCommentContent);
                this.Controls.Add(this.DisplayCommentContext);
                this.Controls.Add(this.DisplayCommentDownvoteButton);
                this.Controls.Add(this.DisplayCommentScore);
                this.Controls.Add(this.DisplayCommentUpvoteButton);
                this.Name = "Form4";
                this.Text = "Form4";
                ((System.ComponentModel.ISupportInitialize)(this.DisplayCommentUpvoteButton)).EndInit();
                ((System.ComponentModel.ISupportInitialize)(this.DisplayCommentDownvoteButton)).EndInit();
                ((System.ComponentModel.ISupportInitialize)(this.DisplayReplyIcon)).EndInit();
                this.ResumeLayout(false);
                this.PerformLayout();

            }

            #endregion


            private System.Windows.Forms.PictureBox DisplayCommentUpvoteButton;
            private System.Windows.Forms.Label DisplayCommentScore;
            private System.Windows.Forms.PictureBox DisplayCommentDownvoteButton;
            private System.Windows.Forms.Label DisplayCommentContext;
            private System.Windows.Forms.RichTextBox DisplayCommentContent;
            private System.Windows.Forms.PictureBox DisplayReplyIcon;
        }

        class DisplayNoComment : Panel
        {
            private Form2 form3Instance;
            private int commentWidth;
            private int commentHeight;

            public DisplayNoComment()
            {
                form3Instance = (Form2)Application.OpenForms["Form3"];
                commentWidth = form3Instance.DisplayCommentContainer.Width
                               - form3Instance.OffsetXComment
                               - (Margin.Horizontal * 4);
                commentHeight = 125;

                Location = new Point(form3Instance.OffsetXComment, form3Instance.OffsetYComment);

                InitializeComponent();

                Width = commentWidth;
                Height = commentHeight;

                form3Instance.OffsetYComment += Height;
            }

            #region InitializeComponent(): Programmer generated layout code

            /// <summary>
            /// Programmer-defined method that supports a particular comment.
            /// </summary>
            private void InitializeComponent()
            {
                // VARIABLES
                string commentContentText = "...";
                //
                // BEGIN FORM INITIALIZATION
                //
                this.DisplayCommentContent = new System.Windows.Forms.RichTextBox();
                this.SuspendLayout();
                // 
                // DisplayCommentContent
                // 
                this.DisplayCommentContent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
                this.DisplayCommentContent.BorderStyle = BorderStyle.None;
                this.DisplayCommentContent.ForeColor = System.Drawing.Color.White;
                this.DisplayCommentContent.Location = new System.Drawing.Point(0, 40);
                this.DisplayCommentContent.Name = "DisplayCommentContent";
                this.DisplayCommentContent.ReadOnly = true;
                this.DisplayCommentContent.Size = new System.Drawing.Size(commentWidth, 40);
                this.DisplayCommentContent.TabIndex = 14;
                this.DisplayCommentContent.Text = $"{commentContentText}";
                // 
                // Form5
                // 
                //this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
                //this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
                this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
                //this.ClientSize = new System.Drawing.Size(800, 450);
                this.Controls.Add(this.DisplayCommentContent);
                this.Name = "Form5";
                this.Text = "Form5";
                this.ResumeLayout(false);
                this.PerformLayout();

            }

            #endregion


            private System.Windows.Forms.RichTextBox DisplayCommentContent;
        }
    }
}
