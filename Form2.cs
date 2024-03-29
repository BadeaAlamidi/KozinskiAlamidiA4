﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace KozinskiAlamidiAssignment4
{
    /**
     * Partial Class: form2
     * this class is an extension to the full declaration of form2 that 
     * contains aspect specific to this assignment such as keeping a list for comments to write
     * and many more. Form 2 is the form that displays the full details of the post, including each
     * comment related to that post.
     * 
     * Attributes:
     * postID                               the id of the post that this form is representing
     * totalComments                        a collection representing comments newly added to memory and to be
     *                                      - written back to comments.txt
     * offsetXComment                       index used for placement of comment replies' visual indentation (also is the xLocation)
     * offsetYComment                       // // // // // // // y location
     * offsetYCommentContainer              constant of 0 (unused?)
     * totalComments                        represents the number of comments of the post that is being represented by this form2 instance
     *  Properties
     *  postID                              getter and setter for postID
     *  TotalComments                       // // // // totalComments
     *  OffsetYComment                      // // // // fsetYComment
     *  OffsetXComment                      // // // // offsetXcomment
     *  OffsetYCommentContainer             // // // // offsetYCommentContainer
     *  
     *  Controls: 
     *  DisplayCommentBox                   panel that is only visible when there's currently an active user
     *  DisplayPostUpvoteButton             picturebox with the correct upvote button that is interactive to increase/decrease the number
     *                                      - of upvotes of the post being represented in form
     * DisplayPostDownvoteButtonn           same as UpvoteButton but increases/decreases the number of downvotes
     * DisplayPostScore                     label that shows the score of the represented post
     * DisplayPostContext                   lable that displays the author of the represented post and how long ago the post was 
     *                                      - created
     * DisplayPostTitle                     label that displays the represented post's title
     * DisplayPostContent                   a rich textbox field that displays the represented post's content property
     * pictureBox3                          a picturebox with the comment icon
     * DisplaPostCommentCount               a label that posts the represented post's comment count. gets updated when a new comment is made
     * DisplayCommentContainer              a panel that holds DisplayComment instances to show all of the represented post's comments
     * 
     * Methods:
     * PrintComments                        prints the comments by populating them in the DisplayCommentContainer as DisplayComment instances
     *  Parameters: uint postId
     * 
     * Event Methods:
     * Form2_Load                           triggers when an instance of this form is shown to the user. invokes the printChildren function
     * SubmitButton_Click                   tirggers when this the submit button is clicked. assumes all of the responsibilties that come with adding a comment
     *                                      - see corresponding documentation box for details
     * RichTextBoxField_TextChanged         triggers when the user is typing on the richTextBoxField (created dynamically). responsible for showing and hiding
     *                                      the gostLabel that is dynamically created inside of the DisplayCommentReplyBox
     * postUpvote_mouseEnter                triggers when the mouse hovers over this picturebox. changes the upvote image 
     * postdownvote_mouseEnter              triggers when the mouse hovers over this picturebox. changes the Downvote image 
     * postUpVote_Leave                     triggers when the mouse hovers out of this picturebox. condiationally changes the upvote image
     *                                      - depending on whether the user has already interacted with this kind of vote on this kind of post
     * postDownVote_Leave                   // // // // / // // // // // // conditionally changes the downvote image //
     * postUpvote_Click                     adds/decreases to the represented posts upvotes. assumes all of the changes needed to be made to reflect
     *                                      - the change of the post's score with all relevant parts of the program such as the posts global variable
     * postDownVote_Click                   // // // // // Downvotes. //                                     
     * DisplayComment_CommentReplyAdded     adds to the displayCommentReplies label 1. triggered when the submit button click event is triggered
     * Form2_FormClosed                     triggers when this form2 closes and handles the writing of new comments to comments.txt
     */
    public partial class Form2 : Form
    {
        private uint postID;
        private uint totalComments = 0;

        private int offsetXComment = 0;
        private int offsetYComment = 0;
        private int offsetYCommentContainer = 0;

        //private SortedList<uint, uint> PostsToWrite;
        private List<Comment> CommentsToWrite;

        public uint PostID
        {
            get { return postID; }
            set { postID = value; }
        }

        public uint TotalComments
        {
            get { return totalComments; }
            set { totalComments = value; }
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
            PostID = newPostID;
            //PostsToWrite = new SortedList<uint, uint>();
            CommentsToWrite = new List<Comment>();
            // Counts total comments
            TotalComments = (uint)Program.globalPosts[PostID].postComments.Count();
            Action<Comment> traverse = null;
            traverse = (Comment currentComment) => 
            {
                TotalComments += (uint)currentComment.commentReplies.Count();
                foreach (Comment reply in currentComment.commentReplies.Values)
                    traverse(reply);
            };
            foreach (Comment comment in Program.globalPosts[PostID].postComments.Values)
                traverse(comment);

            // Builds form
            InitializeComponent(newPostID);
        }
        /**
         * Event tirggered when this form is created on the user's screen
         * responsible for populating the DisplayCommentContainer with DisplayComment instances
         */
        public void Form2_Load(object sender, EventArgs e)
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
            Post post = Program.globalPosts[postID];
            string postTitleText = post.Title;
            string postContentText = post.PostContent;
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
            if (Program.activeUser != null && Program.activeUser.PostVoteStatuses.ContainsKey(PostID))
            {
                this.DisplayPostUpvoteButton.Image = Program.activeUser.PostVoteStatuses[postID] == 1 ? Properties.Resources.upVote_red : Properties.Resources.upVote_grey;
            }
            else
                this.DisplayPostUpvoteButton.Image = global::KozinskiAlamidiAssignment4.Properties.Resources.upVote_grey;
            this.DisplayPostUpvoteButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
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
            this.DisplayPostScore.Location = new System.Drawing.Point(8, 43);
            this.DisplayPostScore.Name = "DisplayPostScore";
            this.DisplayPostScore.Size = new System.Drawing.Size(49, 17);
            this.DisplayPostScore.TabIndex = 1;
            this.DisplayPostScore.Text = $"{post.Score}";
            this.DisplayPostScore.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DisplayDownvoteButton
            // 
            this.DisplayPostDownvoteButton.Image = global::KozinskiAlamidiAssignment4.Properties.Resources.downVote_grey;
            if (Program.activeUser != null && Program.activeUser.PostVoteStatuses.ContainsKey(PostID))
            {
                this.DisplayPostDownvoteButton.Image = Program.activeUser.PostVoteStatuses[postID] == -1 ? Properties.Resources.downVote_blue : Properties.Resources.downVote_grey;
            }
            this.DisplayPostDownvoteButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
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
            this.DisplayPostTitle.AutoSize = true; ;
            this.DisplayPostTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DisplayPostTitle.ForeColor = System.Drawing.Color.White;
            this.DisplayPostTitle.Location = new System.Drawing.Point(60, 38);
            this.DisplayPostTitle.Name = "DisplayPostTitle";
            this.DisplayPostTitle.Size = new System.Drawing.Size(160, 29);
            this.DisplayPostTitle.TabIndex = 5;
            this.DisplayPostTitle.Text = $"{postTitleText}";
            //
            // DisplayCommentBox( the following describes the contents of the displayCommentBox control )
            // this panel is not added to this instance of form2 if activeUser is null in Program
            if (Program.activeUser != null)
            {

                DisplayCommentBox = new Panel() {
                    Location = new Point(60, 235),
                    Size = new Size(665,300),
                };
                var textBoxContainer = new Panel() {
                    Size = new Size(650,245),
                    Padding = new Padding(10),
                    Name = "DisplayCommentBoxRichTextFieldContainer",

                };
                var submitButton = new Button() {
                    Text = "Submit",
                    Location = new Point(455, 250),
                    Size = new Size(190, 40),
                    ForeColor = Color.White,
                    BackColor = Color.Black,
                };
                var RichTextBoxField = new RichTextBox() { 
                    Name = "DisplayCommentBoxRichTextField",
                    Location = new Point(5,5),
                    ForeColor = Color.White,
                    BackColor = Color.Black,
                    Dock = DockStyle.Fill,
                };
                var GhostLabel = new Label() {
                    ForeColor = Color.Silver,
                    BackColor = Color.Transparent,
                    AutoSize = true,
                    Text = "What are your thoughts?",
                    Location = new Point(2,0),
                };
                //EVENTS:
                RichTextBoxField.TextChanged += RichTextBoxField_TextChanged;
                submitButton.Click += SubmitButton_Click;

                RichTextBoxField.Controls.Add(GhostLabel);
                textBoxContainer.Controls.Add(RichTextBoxField);
                DisplayCommentBox.Controls.Add(textBoxContainer);
                DisplayCommentBox.Controls.Add(submitButton);
            }
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
            this.DisplayPostCommentCount.Text = $"{TotalComments} Comments";
            // 
            // DisplayCommentContainer
            // 
            this.DisplayCommentContainer.AutoScroll = true;
            this.DisplayCommentContainer.Location = 
                Program.activeUser == null ? new System.Drawing.Point(60, 235) : new Point (60,535);
            this.DisplayCommentContainer.Name = "DisplayCommentContainer";
            this.DisplayCommentContainer.Size = new System.Drawing.Size(665, 300);
            this.DisplayCommentContainer.TabIndex = 9;
            // 
            // View Post
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.ClientSize = 
                Program.activeUser == null? new System.Drawing.Size(800, 600) : new Size(800,900);
            this.Controls.Add(this.DisplayCommentContainer);
            this.Controls.Add(this.DisplayPostCommentCount);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.DisplayPostContent);
            this.Controls.Add(this.DisplayPostTitle);
            this.Controls.Add(this.DisplayPostContext);
            this.Controls.Add(this.DisplayPostDownvoteButton);
            this.Controls.Add(this.DisplayPostScore);
            this.Controls.Add(this.DisplayPostUpvoteButton);
            if (Program.activeUser != null)this.Controls.Add(this.DisplayCommentBox);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "View Post";
            this.Text = "Post View";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.DisplayPostUpvoteButton.MouseEnter += new System.EventHandler(this.PostUpvote_MouseEnter);
            this.DisplayPostUpvoteButton.MouseLeave += new System.EventHandler(this.PostUpvote_MouseLeave);
            this.DisplayPostUpvoteButton.Click += new System.EventHandler(this.PostUpvote_Click);
            this.DisplayPostDownvoteButton.MouseEnter += new System.EventHandler(this.PostDownvote_MouseEnter);
            this.DisplayPostDownvoteButton.MouseLeave += new System.EventHandler(this.PostDownvote_MouseLeave);
            this.DisplayPostDownvoteButton.Click += new System.EventHandler(this.PostDownvote_Click);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form2_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.DisplayPostUpvoteButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DisplayPostDownvoteButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        /**
         * triggered when the SubmitButton is clicked
         * this event contains the following responsibilites
         * - retaining the dynamically created richTextBoxField 
         * - creating a new 1st level comment based on the active user, current time, and the richTextBoxField content
         * - adding the new comment to the commentsToWrite collection
         * - creating a DisplayComment instance to insert into the DisplayCommentContainer
         * - inserting the new DisplayComment instance in the right position
         * - adding the new comment to the corresponding post
         */
        private void SubmitButton_Click(object sender, EventArgs e)
        {
            RichTextBox DisplayCommentBoxControlRichTextBox = null;
            for (int i = 0; i < DisplayCommentBox.Controls.Count; ++i)
            {
                if (DisplayCommentBox.Controls[i].Name == "DisplayCommentBoxRichTextFieldContainer")
                    DisplayCommentBoxControlRichTextBox = DisplayCommentBox.Controls[i].Controls[0] as RichTextBox;
            }
            if (DisplayCommentBoxControlRichTextBox != null)
            {
                if (DisplayCommentBoxControlRichTextBox.Text.Trim() == "")
                    MessageBox.Show("Your Content is empty.");
                else
                {
                    var new1stLevelComment = new Comment(
                        DisplayCommentBoxControlRichTextBox.Text, 
                        Program.activeUser.Id,
                        Program.globalPosts[PostID].Id,
                        0);

                    CommentsToWrite.Add(new1stLevelComment);
                    Program.globalPosts[postID].postComments.Add(new1stLevelComment.Id, new1stLevelComment);

                    var newDisplayComment = new DisplayComment(new1stLevelComment);
                    newDisplayComment.Height = 125;
                    newDisplayComment.Location = new Point(0,0);

                    DisplayCommentContainer.Controls.Add(newDisplayComment);
                    DisplayCommentContainer.Controls.SetChildIndex(newDisplayComment, 0);

                    for (int i = 1; i < DisplayCommentContainer.Controls.Count; ++i)
                    {
                        DisplayCommentContainer.Controls[i].Location =
                            new Point(DisplayCommentContainer.Controls[i].Location.X,
                            DisplayCommentContainer.Controls[i].Location.Y + 125);
                    }

                    DisplayComment_CommentReplyAdded();
                    DisplayCommentBoxControlRichTextBox.Text = "";
                }
            }
            else MessageBox.Show("Comment Box rich text field was not found.");
        }
        /**
         * this event is triggered for every key the user presses while typing into the RichTextBoxField
         * this event is responsible for changing the label text of DisplayCommentBox to simulate a ghost message
         * when this rich text field is empty
         */
        private void RichTextBoxField_TextChanged(object sender, EventArgs e)
        {
            var richTextBox = sender as RichTextBox;
            if (richTextBox.Text == "")
                richTextBox.Controls[0].Text = "What are your thoughts?";
            else richTextBox.Controls[0].Text = "";
        }

        #endregion

        #region Post upvote/downvote button event handlers
        /**
         * this event triggers when the mouse hovers over the upvote picturebox
         * responsible for chaning the picturebox's image to red upvote
         */
        public void PostUpvote_MouseEnter(object sender, EventArgs e)
        {
            this.DisplayPostUpvoteButton.Image = global::KozinskiAlamidiAssignment4.Properties.Resources.upVote_red;
        }
        /**
         * this event triggers when the mouse hovers out of the upvote picturebox
         * responsible for changing the picturebox image back to grey if the user did not previously upvote this comment
         */
        public void PostUpvote_MouseLeave(object sender, EventArgs e)
        {
            if (Program.activeUser != null && Program.activeUser.PostVoteStatuses.ContainsKey(postID))
                if (Program.activeUser.PostVoteStatuses[postID] == 1) return;
            
            // Else
            this.DisplayPostUpvoteButton.Image = global::KozinskiAlamidiAssignment4.Properties.Resources.upVote_grey;
        }
        /**
         * this event trigger when the user clicks on the upvote picture box
         * responsible for changing the upvote picturebox's picture 
         * - if the active user is null or has not previously upvoted this post, the image will be a red upvote
         * - if the user is not null and has already clicked on this picturebox, the picture will be grey upvote
         */
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
                Program.globalPosts[postID].UpVotes += 1;
                this.DisplayPostUpvoteButton.Image = global::KozinskiAlamidiAssignment4.Properties.Resources.upVote_red;
            }
            else
            {
                if (Program.activeUser.PostVoteStatuses[postID] < 1)
                {
                    Program.activeUser.PostVoteStatuses[postID] = 1;
                    Program.globalPosts[postID].DownVotes -= 1;
                    Program.globalPosts[postID].UpVotes += 1;
                    this.DisplayPostUpvoteButton.Image = global::KozinskiAlamidiAssignment4.Properties.Resources.upVote_red;
                    this.DisplayPostDownvoteButton.Image = global::KozinskiAlamidiAssignment4.Properties.Resources.downVote_grey;
                }
                else
                {
                    Program.globalPosts[postID].UpVotes -= 1;
                    Program.activeUser.PostVoteStatuses.Remove(postID);
                    this.DisplayPostUpvoteButton.Image = global::KozinskiAlamidiAssignment4.Properties.Resources.upVote_grey;
                }
            }
            DisplayPostScore.Text = $"{Program.globalPosts[postID].Score}";
        }
        /**
         * this event triggers when the mouse hovers over the downvote picturebox
         * responsible for chaning the picturebox's image to blue downvote 
         */
        public void PostDownvote_MouseEnter(object sender, EventArgs e)
        {
            this.DisplayPostDownvoteButton.Image = global::KozinskiAlamidiAssignment4.Properties.Resources.downVote_blue;
        }
        /**
         * this event triggers when the mouse hovers out of the downvote picturebox
         * responsible for changing the picturebox image back to grey if the user did not previously upvote this comment
         */
        public void PostDownvote_MouseLeave(object sender, EventArgs e)
        {
            if (Program.activeUser != null && Program.activeUser.PostVoteStatuses.ContainsKey(postID))
                if (Program.activeUser.PostVoteStatuses[postID] == -1) return;

            // Else
            this.DisplayPostDownvoteButton.Image = global::KozinskiAlamidiAssignment4.Properties.Resources.downVote_grey;
        }
        /**
          * this event trigger when the user clicks on the downvote picture box
          * responsible for changing the downvote picturebox's picture 
          * - if the active user is null or has not previously upvoted this post, the image will be a blue downvote
          * - if the user is not null and has already clicked on this picturebox, the picture will be grey downvote
          */
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
                Program.globalPosts[postID].DownVotes += 1;
                Program.activeUser.PostVoteStatuses.Add(postID, -1);
                this.DisplayPostDownvoteButton.Image = global::KozinskiAlamidiAssignment4.Properties.Resources.downVote_blue;
            }
            else
            {
                if (Program.activeUser.PostVoteStatuses[postID] > -1)
                {
                    Program.globalPosts[postID].UpVotes -= 1;
                    Program.globalPosts[postID].DownVotes += 1;
                    Program.activeUser.PostVoteStatuses[postID] = -1;
                    this.DisplayPostDownvoteButton.Image = global::KozinskiAlamidiAssignment4.Properties.Resources.downVote_blue;
                    this.DisplayPostUpvoteButton.Image = global::KozinskiAlamidiAssignment4.Properties.Resources.upVote_grey;
                }
                else
                {
                    Program.globalPosts[postID].DownVotes -= 1;
                    Program.activeUser.PostVoteStatuses.Remove(postID);
                    this.DisplayPostDownvoteButton.Image = global::KozinskiAlamidiAssignment4.Properties.Resources.downVote_grey;
                }
            }
            DisplayPostScore.Text = $"{Program.globalPosts[postID].Score}";
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
        /**
         * simple function for increasing the DisplayPostCommentCount label text by one
         */
        public void DisplayComment_CommentReplyAdded()
        {
            var temp = Convert.ToInt32(DisplayPostCommentCount.Text.Split()[0]);
            DisplayPostCommentCount.Text = $"{temp + 1} comments";
        }

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
                    // THIS WAS ADDED TO FURTHER DEBUG WHY 1ST LEVEL COMMENTS CREATED FROM A PREVIOUS RUN TIME ARE INDENTED
                    newComment.Location = new Point(0, newComment.Location.Y);
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
        /**
         * Class: DisplayComment
         * this class represents an instance that will be contained in the DisplayCommentContainer
         * of form2. in contrast to form2, it represents a comment rather than a post but holds the same kind of informations
         * 
         * Attributes:
         * commentID                                the id of the represented comment
         * comment                                  comment instance of the represented comment
         * form2Instance                            instance of the currently open form2
         * commentWidth                             a constant number representing the width of this panel
         * commentHeight                            // // // // // height of this panel
         * 
         * Properties:
         * CommentView                              a getter for the comment attribute
         * 
         * Constructors:            
         * DisplayComment(Comment)                  performs the necessary assignments to the properties; calls initializeComponent
         *                                          sets own location (based off form2), and size
         * Methods:
         * NONE
         * 
         * Event methods:
         * CommentUpvote_MouseEnter                 responsible for changing image of upvote picturebox when mouse hovers over DisplayCommentUpvoteButton
         * CommentUpvote_MouseLeave                 // // // // // when mouse hovers out of DisplayCommentUpvoteButton picturebox
         * CommentUpvote_Click                      responsible for performing necessary changes of changing the comments score and upvote picturebox image
         * CommentDownvote_MouseEnter               responsible for changing image of Downvote picturebox when mouse hovers over DisplayCommentDownvoteButton
         * CommentDownvote_MouseLeave               // // // // // when mouse hovers out of DisplayCommentDownvoteButton picturebox
         * CommentDownvote_Click                    responsible for performing necessary changes of changing the comments score and downvote picturebox image
         * ReplyIcon_Click                          responsible for performing necessary changes of adding a comment the program, to the comment container, and to
         *                                          - the comments to write collection
         * 
         */
        class DisplayComment : Panel
        {
            private uint commentID;
            private Comment comment;

            private Form2 form2Instance;
            private int commentWidth;
            private int commentHeight;

            public Comment CommentView => comment;
            public DisplayComment(Comment newComment)
            {
                commentID = newComment.Id;
                comment = newComment;

                form2Instance = (Form2)Application.OpenForms["View Post"];
                commentWidth = form2Instance.DisplayCommentContainer.Width
                               - form2Instance.OffsetXComment
                               - (Margin.Horizontal * 4);
                commentHeight = 125;

                Location = new Point(form2Instance.OffsetXComment, form2Instance.OffsetYComment);

                InitializeComponent(newComment);

                Width = commentWidth;
                Height = commentHeight;

                form2Instance.OffsetYComment += Height;
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
                if (Program.activeUser != null && Program.activeUser.CommentVoteStatuses.ContainsKey(comment))
                    this.DisplayCommentUpvoteButton.Image = Program.activeUser.CommentVoteStatuses[comment] == 1?
                        global::KozinskiAlamidiAssignment4.Properties.Resources.upVote_red:
                        global::KozinskiAlamidiAssignment4.Properties.Resources.upVote_grey;
                else
                    this.DisplayCommentUpvoteButton.Image = global::KozinskiAlamidiAssignment4.Properties.Resources.upVote_grey;
                this.DisplayCommentUpvoteButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
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
                if (Program.activeUser != null && Program.activeUser.CommentVoteStatuses.ContainsKey(comment))
                    this.DisplayCommentDownvoteButton.Image = Program.activeUser.CommentVoteStatuses[comment] == -1 ?
                        global::KozinskiAlamidiAssignment4.Properties.Resources.downVote_blue :
                        global::KozinskiAlamidiAssignment4.Properties.Resources.downVote_grey;
                else
                    this.DisplayCommentDownvoteButton.Image = global::KozinskiAlamidiAssignment4.Properties.Resources.downVote_grey;
                this.DisplayCommentDownvoteButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
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
                this.DisplayCommentUpvoteButton.MouseEnter += new System.EventHandler(this.CommentUpvote_MouseEnter);
                this.DisplayCommentUpvoteButton.MouseLeave += new System.EventHandler(this.CommentUpvote_MouseLeave);
                this.DisplayCommentUpvoteButton.Click += new System.EventHandler(this.CommentUpvote_Click);
                this.DisplayCommentDownvoteButton.MouseEnter += new System.EventHandler(this.CommentDownvote_MouseEnter);
                this.DisplayCommentDownvoteButton.MouseLeave += new System.EventHandler(this.CommentDownvote_MouseLeave);
                this.DisplayCommentDownvoteButton.Click += new System.EventHandler(this.CommentDownvote_Click);
                this.DisplayReplyIcon.Click += new System.EventHandler(this.ReplyIcon_Click);

                ((System.ComponentModel.ISupportInitialize)(this.DisplayCommentUpvoteButton)).EndInit();
                ((System.ComponentModel.ISupportInitialize)(this.DisplayCommentDownvoteButton)).EndInit();
                ((System.ComponentModel.ISupportInitialize)(this.DisplayReplyIcon)).EndInit();
                this.ResumeLayout(false);
                this.PerformLayout();

            }

            #endregion

            #region Comment upvote/downvote button event handlers
            /**
             * triggered when the mouse hover over the commentUpvote picture box
             * responsible for changing the image of upvote picturebox to the red upvote
             */
            public void CommentUpvote_MouseEnter(object sender, EventArgs e)
            {
                this.DisplayCommentUpvoteButton.Image = global::KozinskiAlamidiAssignment4.Properties.Resources.upVote_red;
            }
            /**
             * triggered when the mouse leaves the commentUpvote picturebox
             * responsible for changing the upvotepicturebox's image back to grey if the user is either 
             * not logged, or has not upvoted this comment before
             * 
             */
            public void CommentUpvote_MouseLeave(object sender, EventArgs e)
            {
                if (Program.activeUser != null && Program.activeUser.CommentVoteStatuses.ContainsKey(comment))
                    if (Program.activeUser.CommentVoteStatuses[comment] == 1) return;

                // Else
                this.DisplayCommentUpvoteButton.Image = global::KozinskiAlamidiAssignment4.Properties.Resources.upVote_grey;
            }
            /**
             * triggers when the CommentUpvote button is clicked
             * responsible for adding an upvote to the represented comment
             * - changing the image of the commentupvote picturebox to red upvote
             * - decreasing the number of downvotes by 1 and increasing the number of upvotes
             *    by 1 if the user is logged in but has previously downvoted the represented comment
             * - decreasing the number of upvotes by one if the user is logged in and has previously upvoted
             *   the represented comment
             * - changing the DisplayCommentScore lebel text to reflect the new comment score
             */
            public void CommentUpvote_Click(object sender, EventArgs e)
            {
                if (Program.activeUser == null)
                {
                    MessageBox.Show("You must be logged in to vote on comments.");
                    return;
                }

                bool hasVoted = Program.activeUser.CommentVoteStatuses.ContainsKey(comment);

                if (!hasVoted)
                {
                    Program.activeUser.CommentVoteStatuses.Add(comment, 1);
                    comment.UpVotes += 1;
                    this.DisplayCommentUpvoteButton.Image = global::KozinskiAlamidiAssignment4.Properties.Resources.upVote_red;
                }
                else
                {
                    if (Program.activeUser.CommentVoteStatuses[comment] < 1)
                    {
                        Program.activeUser.CommentVoteStatuses[comment] = 1;
                        comment.DownVotes -= 1;
                        comment.UpVotes += 1;
                        this.DisplayCommentUpvoteButton.Image = global::KozinskiAlamidiAssignment4.Properties.Resources.upVote_red;
                        this.DisplayCommentDownvoteButton.Image = global::KozinskiAlamidiAssignment4.Properties.Resources.downVote_grey;
                    }
                    else
                    {
                        Program.activeUser.CommentVoteStatuses.Remove(comment);
                        comment.UpVotes -= 1;
                        this.DisplayCommentUpvoteButton.Image = global::KozinskiAlamidiAssignment4.Properties.Resources.upVote_grey;
                    }
                }
                DisplayCommentScore.Text = $"{comment.Score}";
            }
            /**
             * triggered when the mouse hover over the commentDownvote picture box
             * responsible for changing the image of downvote picturebox to the blue downvote
             */
            public void CommentDownvote_MouseEnter(object sender, EventArgs e)
            {
                this.DisplayCommentDownvoteButton.Image = global::KozinskiAlamidiAssignment4.Properties.Resources.downVote_blue;
            }
            /**
             * triggered when the mouse leaves the commentDownvote picturebox
             * responsible for changing the downvotepicturebox's image back to grey if the user is either 
             * not logged, or has not downvoted this comment before
             * 
             */
            public void CommentDownvote_MouseLeave(object sender, EventArgs e)
            {
                if (Program.activeUser != null && Program.activeUser.CommentVoteStatuses.ContainsKey(comment))
                    if (Program.activeUser.CommentVoteStatuses[comment] == -1) return;

                // Else
                this.DisplayCommentDownvoteButton.Image = global::KozinskiAlamidiAssignment4.Properties.Resources.downVote_grey;
            }
            /**
             * triggers when the CommentDownvote button is clicked
             * responsible for adding an downvote to the represented comment
             * - changing the image of the commentdownvote picturebox to blue downvote
             * - decreasing the number of upvotes by 1 and increasing the number of downvotes
             *    by 1 if the user is logged in but has previously upvoted the represented comment
             * - decreasing the number of downvotes by one if the user is logged in and has previously downvoted
             *   the represented comment
             * - changing the DisplayCommentScore lebel text to reflect the new comment score
             */
            public void CommentDownvote_Click(object sender, EventArgs e)
            {
                if (Program.activeUser == null)
                {
                    MessageBox.Show("You must be logged in to vote on comments.");
                    return;
                }

                bool hasVoted = Program.activeUser.CommentVoteStatuses.ContainsKey(comment);

                if (!hasVoted)
                {
                    Program.activeUser.CommentVoteStatuses.Add(comment, -1);
                    comment.DownVotes += 1;
                    this.DisplayCommentDownvoteButton.Image = global::KozinskiAlamidiAssignment4.Properties.Resources.downVote_blue;
                }
                else
                {
                    if (Program.activeUser.CommentVoteStatuses[comment] > -1)
                    {
                        Program.activeUser.CommentVoteStatuses[comment] = -1;
                        comment.UpVotes -= 1;
                        comment.DownVotes += 1;
                        this.DisplayCommentDownvoteButton.Image = global::KozinskiAlamidiAssignment4.Properties.Resources.downVote_blue;
                        this.DisplayCommentUpvoteButton.Image = global::KozinskiAlamidiAssignment4.Properties.Resources.upVote_grey;
                    }
                    else
                    {
                        comment.DownVotes -= 1;
                        Program.activeUser.CommentVoteStatuses.Remove(comment);
                        this.DisplayCommentDownvoteButton.Image = global::KozinskiAlamidiAssignment4.Properties.Resources.downVote_grey;
                    }
                }
                DisplayCommentScore.Text = $"{comment.Score}";
            }

            #endregion
            /**
             * this event triggers when the user has clicked the reply icon
             * 
             * this event is responsible creating a new DisplayReplyBox instance to the form2Instance Comment container
             * at the right location underneath this instance of DisplayComment. other responsibilities include:
             * - displacing all subsequent DisplayComment instances in teh container to allocate space for the new 
             *   DisplayReply instance
             */
            public void ReplyIcon_Click(object sender, EventArgs e)
            {
                if (Program.activeUser == null) { MessageBox.Show("You are not logged in."); return; }
                form2Instance = (Form2)Application.OpenForms["View Post"];
                ControlCollection ctrls = form2Instance.DisplayCommentContainer.Controls;
                if (DisplayReplyBox.Active == false)
                {
                    DisplayReplyBox.Active = true;
                    DisplayReplyBox newReplyBox = new DisplayReplyBox(comment);
                    newReplyBox.Location = new Point(Location.X, Location.Y + 125);
                    newReplyBox.Height = 125;

                    ctrls.Add(newReplyBox);
                    ctrls.SetChildIndex(newReplyBox, ctrls.IndexOf(this) + 1);

                    int index = ctrls.IndexOf(this);
                    for (int i = index + 1; i < ctrls.Count; i++)
                    {
                        if (ctrls[i] is DisplayComment)
                        {
                            ctrls[i].Location = new Point(ctrls[i].Location.X, ctrls[i].Location.Y + 125);
                        }
                    }
                }
                else
                    MessageBox.Show("Finish writing your comment, or press 'cancel' to continue.");

            }


            private System.Windows.Forms.PictureBox DisplayCommentUpvoteButton;
            private System.Windows.Forms.Label DisplayCommentScore;
            private System.Windows.Forms.PictureBox DisplayCommentDownvoteButton;
            private System.Windows.Forms.Label DisplayCommentContext;
            private System.Windows.Forms.RichTextBox DisplayCommentContent;
            private System.Windows.Forms.PictureBox DisplayReplyIcon;
        }
        /**
         * Class : DisplayNoComment (unused?)
         * this class is a panel that is inserted to the DisplayCommentsContainer of form2 as means
         * of indicating that the represented comment is too nested to be visible
         * 
         * Attributes: 
         * commentWidth             used as parameter for determining the instance's width, which is based on the width of
         *                          the form2 container
         * commentHeight            // // // // // height, which is a fixed value of 125
         * form2Instance            an instance of the currently open form2 
         * 
         * Properties: NONE
         * Controls: 
         * DisplayCommentContent    contains the text of commentContent (. . .) as means to indicate that the 
         *                          comment is too nested
         * 
         */
        class DisplayNoComment : Panel
        {
            private Form2 form2Instance;
            private int commentWidth;
            private int commentHeight;

            public DisplayNoComment()
            {
                form2Instance = (Form2)Application.OpenForms["View Post"];
                commentWidth = form2Instance.DisplayCommentContainer.Width
                               - form2Instance.OffsetXComment
                               - (Margin.Horizontal * 4);
                commentHeight = 125;

                Location = new Point(form2Instance.OffsetXComment, form2Instance.OffsetYComment);

                InitializeComponent();

                Width = commentWidth;
                Height = commentHeight;

                form2Instance.OffsetYComment += Height;
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
        /**
         * Class: DisplayReplyBox
         * forms panel that is decorated to give the user interface for adding a new comment to a comment represented by
         * the DisplayComment instance whose reply icon was clicked
         * 
         * Attributes:
         * active                               static boolean created to disallow the user from replying to two comments
         * RTFGainedFocus                       boolean created to disallow the user from submitting ghost text as comment content
         * CommentID                            the id of the parent that is being replied to
         * comment                              Comment instance that represents the comment of the DisplayComment that is being replied to 
         * form2Instance                        instance of the currently open form2 window
         * commentWidth                         used as reference to define the promer width of this instance. based on the width
         *                                      - of the commentContainer in form2
         * commentHeight                        fixed integer value used for sizing this instance of the replybox
         * 
         * Properties:
         * Acitve                               getter for active
         * 
         * Controls:
         * DisplayReplyContent                  rich text field for the user to type their reply
         * DisplayReplyButton                   picturebox with fixed image and has the ability to add the typed comment in its corresponding click event
         * DisplayCancelButton                  picturebox with fixed image and has the ability to cancel the comment reply in its corresponding click event
         * 
         * Constructor:
         * DisplayReplyBox                      assigns to attributes and determines the location and size of this instance
         *                                      - calls initialize components for the controls of this class
         * 
         * Methods: NONE
         * Event Methods:
         * ReplyButton_Click                    triggers when replybutton picturebox is clicked. assumes all responsiblities that come with adding 
         *                                      a new comment reply in the program, including postioning of displaycomment panels in the comments container
         * CancelButton_Click                   Triggers when the cancelbutton picturebox is clicked, assumes all responsibilities associated re-positioning
         *                                      - DisplayComment panels
         */
        class DisplayReplyBox : Panel
        {
            private static bool active = false;
            public static bool Active
            {
                get { return active; }
                set { active = value; }
            }

            private bool RTFGainedFocus;
            private uint commentID;
            private Comment comment;

            private Form2 form2Instance;
            private int commentWidth;
            private int commentHeight;

            public DisplayReplyBox(Comment newComment)
            {
                
                commentID = newComment.Id;
                comment = newComment;

                RTFGainedFocus = false;

                form2Instance = (Form2)Application.OpenForms["View Post"];
                commentWidth = form2Instance.DisplayCommentContainer.Width
                               - (Margin.Horizontal * 4);
                commentHeight = 125;

                Location = new Point(form2Instance.OffsetXComment, commentHeight);

                InitializeComponent(newComment);

                Width = commentWidth;
                Height = commentHeight;
            }

            #region InitializeComponent(Comment newComment): Programmer generated layout code

            /// <summary>
            /// Programmer-defined method that supports a particular comment.
            /// </summary>
            private void InitializeComponent(Comment comment)
            {
                // VARIABLES
                int column2 = 60;
                string commentContentText = "What are your thoughts?";
                //
                // BEGIN FORM INITIALIZATION
                //
                this.DisplayReplyContent = new System.Windows.Forms.RichTextBox();
                this.DisplayReplyButton = new System.Windows.Forms.PictureBox();
                this.DisplayCancelButton = new System.Windows.Forms.PictureBox();
                ((System.ComponentModel.ISupportInitialize)(this.DisplayReplyButton)).BeginInit();
                ((System.ComponentModel.ISupportInitialize)(this.DisplayCancelButton)).BeginInit();
                this.SuspendLayout();
                // 
                // DisplayReplyContent
                // 
                this.DisplayReplyContent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
                this.DisplayReplyContent.BorderStyle = BorderStyle.None;
                this.DisplayReplyContent.ForeColor = System.Drawing.Color.DarkGray;
                this.DisplayReplyContent.Location = new System.Drawing.Point(0, 0);
                this.DisplayReplyContent.Name = "DisplayReplyContent";
                this.DisplayReplyContent.Size = new System.Drawing.Size(commentWidth - column2, 40);
                this.DisplayReplyContent.TabIndex = 20;
                this.DisplayReplyContent.Text = $"{commentContentText}";
                // 
                // DisplayReplyButton
                // 
                this.DisplayReplyButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
                this.DisplayReplyButton.Image = global::KozinskiAlamidiAssignment4.Properties.Resources.reply_button;
                this.DisplayReplyButton.Location = new System.Drawing.Point(Width - 5, 50);
                this.DisplayReplyButton.Name = "DisplayReplyButton";
                this.DisplayReplyButton.Size = new System.Drawing.Size(104, 33);
                this.DisplayReplyButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
                this.DisplayReplyButton.TabIndex = 21;
                this.DisplayReplyButton.TabStop = false;
                // 
                // DisplayCancelButton
                // 
                this.DisplayCancelButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
                this.DisplayCancelButton.Image = global::KozinskiAlamidiAssignment4.Properties.Resources.cancel_button;
                this.DisplayCancelButton.Location = new System.Drawing.Point(Width - 110 - column2, 50);
                this.DisplayCancelButton.Name = "DisplayCancelButton";
                this.DisplayCancelButton.Size = new System.Drawing.Size(68, 33);
                this.DisplayCancelButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
                this.DisplayCancelButton.TabIndex = 22;
                this.DisplayCancelButton.TabStop = false;
                // 
                // Reply to Comment Panel
                // 
                this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
                this.Controls.Add(this.DisplayCancelButton);
                this.Controls.Add(this.DisplayReplyButton);
                this.Controls.Add(this.DisplayReplyContent);
                this.Name = "Reply to Comment";
                this.DisplayReplyButton.Click += new System.EventHandler(this.ReplyButton_Click);
                this.DisplayCancelButton.Click += new System.EventHandler(this.CancelButton_Click);
                this.DisplayReplyContent.GotFocus += new System.EventHandler(this.ReplyContent_GotFocus);
                this.DisplayReplyContent.LostFocus += new System.EventHandler(this.ReplyContent_LostFocus);
                ((System.ComponentModel.ISupportInitialize)(this.DisplayReplyButton)).EndInit();
                ((System.ComponentModel.ISupportInitialize)(this.DisplayCancelButton)).EndInit();
                this.ResumeLayout(false);
                this.PerformLayout();

            }
            /**
             * triggered when the replybutton picture box is clicked
             * this event has the following responsibilites:
             * - checking if the reply is empty, in which case the function will return
             * - creating a new comment and adding it to its parent and the commentstowrite collection in form2
             * - creating a new DisplayComment instance with the appropriate index, location, and height to be displayed in the container
             * - removing this instance from the commentContainer
             * - setting the new index of the new CommentDisplay to the index of this instance for appropriate displaying
             */
            public void ReplyButton_Click(object sender, EventArgs e)
            {
                Form2 form2Instance = (Form2)Application.OpenForms["View Post"];
                ControlCollection ctrls = form2Instance.DisplayCommentContainer.Controls;

                if (DisplayReplyContent.Text == "" || RTFGainedFocus == false) { MessageBox.Show("Your comment must have content.");  return; }

                var replyBoxResult = new Comment(DisplayReplyContent.Text, Program.activeUser.Id, comment.Id, this.comment.Indentation + 1);

                form2Instance.CommentsToWrite.Add(replyBoxResult);
                this.comment.commentReplies.Add(replyBoxResult.Id, replyBoxResult);

                DisplayComment newDisplayComment = new DisplayComment(replyBoxResult);
                newDisplayComment.Location = new Point(Convert.ToInt32(replyBoxResult.Indentation * 40), Location.Y);
                newDisplayComment.Height = 125;

                ctrls.Add(newDisplayComment);
                ctrls.SetChildIndex(newDisplayComment, ctrls.IndexOf(this));
                form2Instance.DisplayComment_CommentReplyAdded();
                ctrls.Remove(this);

                Active = false;
            }
            /**
             * this event triggers when the user clicks the cancelbutton picturebox
             * this event has the following responsibilities:
             * 
             * - removing this instance from the DisplayCommentCollection
             * - re-spacing all of the already displaced comments back to their original y location
             */
            public void CancelButton_Click(object sender,EventArgs e)
            {
                ControlCollection ctrls = ((Form2)Application.OpenForms["View Post"]).DisplayCommentContainer.Controls;

                int index = ctrls.IndexOf(this);
                for (int i = index + 1; i < ctrls.Count; i++)
                {
                    if (ctrls[i] is DisplayComment)
                        ctrls[i].Location = new Point(ctrls[i].Location.X, ctrls[i].Location.Y - 125);
                }

                this.Parent.Controls.Remove(this);

                Active = false;
            }
            /**
             * this event triggers when the rich text field in this instance (Reply_Content) gains focus
             * this event is responsible for setting RTFGainedFocus to true and removing the ghost text in the
             * rich textfield
             */
            public void ReplyContent_GotFocus(object sender, EventArgs e)
            {
                RichTextBox replyBox = this.DisplayReplyContent;

                RTFGainedFocus = true;

                if (replyBox.Text.CompareTo("What are your thoughts?") == 0)
                {
                    replyBox.Text = "";
                    replyBox.ForeColor = System.Drawing.Color.Black;
                }
            }
            /**
             * this event triggers when the rich text field ReplyContent loses focus
             * this event is responsible for adding back the ghost text of the rich textfield, 
             * - should it contain no text
             */
            public void ReplyContent_LostFocus(object sender, EventArgs e)
            {
                RichTextBox replyBox = this.DisplayReplyContent;


                if (replyBox.Text.CompareTo("") == 0)
                {
                    RTFGainedFocus = false;
                    replyBox.ForeColor = System.Drawing.Color.DarkGray;
                    replyBox.Text = "What are your thoughts?";
                }
            }

            #endregion

            private System.Windows.Forms.RichTextBox DisplayReplyContent;
            private System.Windows.Forms.PictureBox DisplayReplyButton;
            private System.Windows.Forms.PictureBox DisplayCancelButton;
        }
        /**
         * event triggered when this form2 instance is closed
         * responsible for traversing the commentsToWrite collection and adding each comment to comment.txt 
         * by wrting asynchronously the current comment expressed appropriately as a string.
         * 
         * */
        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1.RefreshPanel1(sender, e);
            DisplayReplyBox.Active = false;

            //WRITE BACK COMMENTS TO COMMENTS FILE
            Action<Comment> asyncWrite = async (Comment newTuple) => {
                
                using (StreamWriter file = new StreamWriter("..\\..\\comments.txt", append: true)) {
                    await file.WriteLineAsync($"{newTuple.Id}\t{newTuple.AuthorID}\t{newTuple.Content}\t{newTuple.ParentID}\t"
                        + $"{newTuple.UpVotes}\t{newTuple.DownVotes}\t{newTuple.TimeStamp.Year}\t"
                        + $"{newTuple.TimeStamp.Month}\t{newTuple.TimeStamp.Day}\t{newTuple.TimeStamp.Hour}\t"
                        + $"{newTuple.TimeStamp.Minute}\t{newTuple.TimeStamp.Second}\t{newTuple[0]}\t{newTuple[1]}\t{newTuple[2]}");
                };
            };

            foreach (Comment comment in CommentsToWrite) asyncWrite(comment);

        }
    }
}
