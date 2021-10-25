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
    /** Class name: Form1
     * this partial class contains the different events that can be invoked while the user
     * interacts with the different components in the windows Form
     * 
     * Attributes: NONE
     * Properties: NONE
     * 
     * Constructors: 
     * Form1()                      Generic default constructor
     * 
     * Methods:
     * PrintChildComments           used to populate the comment listbox with nested comments
     * Event Methods:
     * Form1_Load                   triggers upon the windows form startup. performs necessary
     *                               - initializtion steps such as populating Program.global*
     *                               - dictionaries with data
     *                               
     * subredditSelection_SelectedValueChanged  triggers when the user chooses/changes a selectable item
     *                                           - in the list subreddit listbox
     * postSelection_SelectedValueChanged       same as above
     * commentSelection_SelectedValueChanged    same as above
     * userSelection_SelectedValueChanged       same as above
     * 
     * loginButton_MouseClick                   triggers when the user clicks on this component
     * addReplyButton_Click                     same as above
     * deleteReplyButton_Click                  same as above
     * deletePostButton_Click                   same as above
     * 
     * Notes: NONE
     * 
     * 
     * */
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /**
         * 
         * Method Name: PrintChildComments
         * 
         * Iterates recursively through comments
         * Only shows the first 5 levels of comments
         * 
         * Returns: void
         * 
         * Notes:
         * Not to be used to print comments upon log-in
         * Caps the number of comment levels printed
         * 
         */
     /*   void PrintChildComments(Comment currentComment)
        {
            try
            {
                foreach (Comment commentReply in currentComment.commentReplies.Values.OrderBy(comment => comment).ThenBy(postComment => postComment.TimeStamp))
                {
                    if (commentReply.Indentation < 5)
                        commentSelection.Items.Add(commentReply);
                    else
                        commentSelection.Items.Add(RedditUtilities.FinalIndentation(5) + "...\n");

                    // Recursive call
                    PrintChildComments(commentReply);
                }
            }
            catch (Exception exception)
            {
                systemOutput.Clear();
                systemOutput.AppendText(exception.Message);
            } 
        }*/
        /**
         * 
         * Method Name: Form1_Load
         * 
         * Triggers when the application starts. This event is responsble for
         * invoking the ReadFiles function in Program.cs and populating
         * the global subreddits with the corresponding data retained from
         * ReadFiles. After this, the corresponding listboxes for posts,
         * subreddits, and users are populated with the global dictionaries'
         * data in the right order
         * 
         * Returns: void
         * 
         * Notes: Errors encounterd during the ReadFiles function
         * are stored in an array and displayed at the end of the 
         * function in systemOutput component.
         * 
         */
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                // Runs file reader and stores error log
                List<string> fileReadErrors = RedditUtilities.ReadFiles();

                // Prints error log to system output
                foreach (string line in fileReadErrors)
                    systemOutput.AppendText(line + "\n");

                systemOutput.AppendText("Welcome! Select a user and enter a password to log in.\n");

                // Populate subreddit ComboBox
                foreach (KeyValuePair<uint, Subreddit> subreddit in Program.globalSubreddits.OrderBy(subreddit => subreddit.Value.Name)) { subredditSelection.Items.Add(subreddit.Value); }
                subredditSelection.SelectedIndex = 0;

                // Populates main panel with posts
                foreach (KeyValuePair<uint, Post> post in Program.globalPosts.OrderBy(post => post.Value.PostRating))
                {
                    // Creates display object
                    DisplayPost newPost = new DisplayPost(post.Key);
                    newPost.Click += new EventHandler(DisplayPost_Click);

                    // Adds display object to main panel
                    panel1.Controls.Add(newPost);
                }
            }
            catch (Exception exception)
            {
                systemOutput.Clear();
                systemOutput.AppendText(exception.Message);
            }
        }
        /**
         * 
         * Method Name: subredditSelection_SelectedValueChanged
         * 
         * the following triggers when 1. the highlighted item in the subreddit
         * selection changes. and 2. when the currently selected item gets unselected
         * the responsibilities of this event include:
         *  - setting the member count and active member count labels
         *    to the appropriate number depending on the chosen post
         *  - listing the posts of the currently selected subreddit item
         *    in the post listbox (abbreviated version)
         * 
         * Returns: void
         * 
         * Notes: NONE
         * 
         */
        // this triggers when the user chooses a subreddit
       /* private void subredditSelection_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                memberCount.Text = "";
                activeCount.Text = "";
                if (subredditSelection.SelectedItem == null)
                    return;

                postSelection.Items.Clear();
                commentSelection.Items.Clear();
                systemOutput.Clear();
                replyInput.Clear();

                if ("all" == subredditSelection.SelectedItem.ToString())
                {
                    foreach (KeyValuePair<uint, Subreddit> subredditTuple in Program.globalSubreddits.OrderBy(subredditTuple => subredditTuple.Value))
                    {
                        // Displays abbreviated title if appropriate: postSelection.DisplayMember -> Post.AbbreviatedTitle property -> ToString("ListBox") (extra step required by assignment specification)
                        foreach (KeyValuePair<uint, Post> postTuple in Program.globalPosts.Where(globalPostTuple => subredditTuple.Value.subPostIDs.Contains(globalPostTuple.Key)).OrderBy(globalPostTuple => globalPostTuple.Value))
                            postSelection.Items.Add(postTuple.Value);
                    }
                }
                else
                {
                    if (!(subredditSelection.SelectedItem is Subreddit chosenSubreddit))
                        return;

                    // Displays abbreviated title if appropriate: postSelection.DisplayMember -> Post.AbbreviatedTitle property -> ToString("ListBox") (extra step required by assignment specification)
                    foreach (KeyValuePair<uint, Post> postTuple in Program.globalPosts.Where(globalPostTuple => chosenSubreddit.subPostIDs.Contains(globalPostTuple.Key)).OrderBy(globalPostTuple => globalPostTuple.Value))
                        postSelection.Items.Add(postTuple.Value);

                    memberCount.Text = chosenSubreddit.Members.ToString();
                    activeCount.Text = chosenSubreddit.Active.ToString();
                }
            }
            catch (Exception exception)
            {
                systemOutput.Clear();
                systemOutput.AppendText(exception.Message);
            }
        } */
        /**
         * 
         * Method Name: postSelection_SelectedValueChanged
         * 
         * the following triggers when 1. the highlighted item in the post
         * selection changes. and 2. when the currently selected item gets unselected
         * the responsibilities of this event include:
         *  
         *  - displaying the full contents of the chosen item in systemOutput
         *  - showing the comments (abbreviated) of the selected item in the comment listbox
         *    with appropriate indentation (up to 5 levels deep lest the comments won't be visible).
         * 
         * Returns: void
         * 
         * Notes: NONE
         * 
         */
        /*private void postSelection_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (postSelection.SelectedItem == null)
                    return;

                systemOutput.Clear();
                systemOutput.AppendText(postSelection.SelectedItem.ToString());

                commentSelection.Items.Clear();
                replyInput.Clear();

                Post chosenPost = postSelection.SelectedItem as Post;
                if (chosenPost == null)
                {
                    systemOutput.Clear();
                    systemOutput.AppendText("You did not select a valid post\n");
                    return;
                }

                // Shows default message if post has no comments
                if (chosenPost.postComments.Count == 0)
                {
                    commentSelection.Items.Add("\n---no comments associated with this post---\n");
                    return;
                }

                // Populates comment box (comments are sorted by score, then by date/time posted)
                // Starts recursion at the post level
                // Displays abbreviated content if appropriate: commentSelection.DisplayMember -> Comment.AbbreviatedContent property -> ToString("ListBox") (extra step required by assignment specification)
                foreach (Comment postComment in chosenPost.postComments.Values.OrderBy(postComment => postComment).ThenBy(postComment => postComment.TimeStamp))
                {
                    commentSelection.Items.Add(postComment);
                    PrintChildComments(postComment);
                }
            }
            catch (Exception exception)
            {
                systemOutput.Clear();
                systemOutput.AppendText(exception.Message);
            }
        }*/
        /**
         * 
         * Method Name: commentSelection_SelectedValueChanged
         * 
         * the following triggers when 1. the highlighted item in the comment
         * selection changes. and 2. when the currently selected item gets unselected
         * the responsibilities of this event include:
         *  
         *  - displaying the full contents of the chosen comment in systemOutput
         * 
         * Returns: void
         * 
         * Notes: NONE
         * 
         */
       /* private void commentSelection_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (commentSelection.SelectedItem == null)
                    return;

                systemOutput.Clear();
                systemOutput.AppendText(commentSelection.SelectedItem.ToString());

                replyInput.Clear();
            }
            catch (Exception exception)
            {
                systemOutput.Clear();
                systemOutput.AppendText(exception.Message);
            }
        }*/
        /**
         * 
         * Method Name: userSelection_SelectedValueChanged
         * 
         * the following triggers when 1. the highlighted item in the comment
         * selection changes. and 2. when the currently selected item gets unselected
         * the responsibilities of this event include:
         *  
         *  - switching the text of the log-in button back to Log-in should the user
         *    decide to log into another account after failing to log-in to their original choice
         *  - clearing all of the listboxes except for itself as a subtle message to the user to
         *    indicate that a new log-in under a differnt account could have different privilages
         *  - prompt the user to enter a password for the chosen username in systemOutput
         *  
         * 
         * Returns: void
         * 
         * Notes: NONE
         * 
         */
     /*   private void userSelection_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                // reset the text of the log-in button in case there was a previous failed attempt at entering the password
                loginButton.Text = "Log-in";

                // Safety check
                if (!(userSelection.SelectedItem is User chosenUser))
                    return;

                // Clears all form fields

                subredditSelection.SelectedItem = null;
                postSelection.SelectedItem = null;
                commentSelection.SelectedItem = null;

                postSelection.Items.Clear();
                commentSelection.Items.Clear();

                systemOutput.Clear();
                replyInput.Clear();

                if (Program.activeUser == null || Program.activeUser != chosenUser)
                    systemOutput.AppendText($"Enter a password to log in as the username: {chosenUser.Name}\n");

                if (Program.activeUser == chosenUser)
                    systemOutput.AppendText($"You are logged in as: {chosenUser.Name}\n");
            }
            catch (Exception exception)
            {
                systemOutput.Clear();
                systemOutput.AppendText(exception.Message);
            }
        }*/
        /**
         * 
         * Method Name: loginButton_MouseClick
         * 
         * The following triggers when the user clicks on this component( represented by a button: Log-in/Retry Password)
         * 
         * This event includes the following responsibilities
         *  - inform the user about whether their log-in attempt was done correctly/successful/unsuccessful
         *  - authenticate the user by turning their string input into a hashcode that gets further translated into hex,
         *    which is compared against the highlighted user's hex'd hashcode string to test if the two values are the same
         *  - upon successful log-in, displaying the authenticated user's posts and comments (without indentation)
         * 
         * Returns: void
         * 
         * Notes: NONE
         * 
         */

     /*   private void loginButton_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (userSelection.SelectedIndex == -1) { systemOutput.AppendText("You haven't specified a username!\n"); return; }
                if (passwordInput.Text == "") { systemOutput.AppendText("Please enter a password. Be sure to choose your intended username\n"); return; }

                // authentication happens here:
                User chosenUser = userSelection.SelectedItem as User;
                if (passwordInput.Text.GetHashCode().ToString("X") == chosenUser.PasswordHash)
                {
                    // reset the text of the log-in button in case there was a previous failed attempt at entering the password
                    loginButton.Text = "Log-in";

                    systemOutput.Clear();
                    systemOutput.AppendText($"Authentication successful! Welcome, {chosenUser.Name}\n");
                    Program.activeUser = chosenUser;

                    subredditSelection.SelectedItem = null;
                    postSelection.Items.Clear();
                    commentSelection.Items.Clear();

                    List<Comment> comments = new List<Comment>();

                    // Adds user's own posts and comments to respective ListBoxes
                    // Sorts posts by subreddit name, then by post rating
                    foreach (Post post in Program.globalPosts.Values.OrderBy(post => Program.globalSubreddits[post.subHomeId]).ThenBy(post => post))
                    {
                        // Adds posts to ListBox
                        // Displays abbreviated content if appropriate: commentSelection.DisplayMember -> Comment.AbbreviatedContent property -> ToString("ListBox") (extra step required by assignment specification)
                        if (post.AuthorId == Program.activeUser.Id)
                            postSelection.Items.Add(post);

                        // Adds comments to list (to be sorted and printed later)
                        // Displays abbreviated content if appropriate: commentSelection.DisplayMember -> Comment.AbbreviatedContent property -> ToString("ListBox") (extra step required by assignment specification)
                        // Starts recursion at the post level
                        foreach (Comment userComment in post.postComments.Values)
                        {
                            if (userComment.AuthorID == Program.activeUser.Id)
                                comments.Add(userComment);

                            // Recursive call
                            StoreChildComments(userComment);
                        }
                    }*/
                    /**
                     * Method name: StoreChildComments
                     * 
                     * this method recursively searches comment trees, with posts serving as the root, in order to
                     * find the comments that belong to the currently authenticated user
                     * 
                     * returns void
                     * 
                     *
                     */
                    // Iterates recursively through comments
        /*            void StoreChildComments(Comment currentComment)
                    {
                        foreach (Comment userComment in currentComment.commentReplies.Values)
                        {
                            if (userComment.AuthorID == Program.activeUser.Id)
                                comments.Add(userComment);

                            // Recursive call
                            StoreChildComments(userComment);
                        }
                    }

                    // Sorts and prints comments (without indentation) to ListBox
                    comments.Sort();
                    foreach (Comment c in comments)
                    {
                        uint originalLevel = c.Indentation;
                        c.Indentation = 0;
                        commentSelection.Items.Add(c);
                        c.Indentation = originalLevel;
                    }

                    systemOutput.AppendText($"Displaying all posts and comments made by user \'{chosenUser.Name}\'\n");
                }
                else
                {
                    systemOutput.AppendText("Authentication failed\n");
                    loginButton.Text = "Retry Password";
                }
            }
            catch (Exception exception)
            {
                systemOutput.Clear();
                systemOutput.AppendText(exception.Message);
            }
        }*/
        /**
         * 
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
         * 
         */
    /*    private void addReplyButton_Click(object sender, EventArgs e)
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
        }*/
        /**
         * 
         * Method Name: deleteReplyButton_Click
         * 
         * The following triggers when the user clicks on this component( represented by a button: Delete Comment)
         * 
         * This event includes the following responsibilities
         *  - inform the user about whether their delete  attempt was appropriate: disallow deletion without authentication,
         *    disallow deletions when the user is not an administrator or if said comment does not belong to the user
         *  - removing the selected comment if the conditions above permit so
         *  - displaying the removal by refreshing the comment listbox
         * Returns: void
         * 
         * Notes: the assignemnt spec does not indicate whether the deleted content is to be deleted completely
         *        or "cleared out". this assignment assumes the former approach
         * 
         */
    /*    private void deleteReplyButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (Program.activeUser == null)
                {
                    systemOutput.Clear();
                    systemOutput.AppendText("Log-in is required to delete posts and comments\n");
                    return;
                }

                if (postSelection.SelectedItem == null || commentSelection.SelectedItem == null)
                {
                    systemOutput.Clear();
                    systemOutput.AppendText("You must select a post and one of its comments first\n");
                    return;
                }

                Comment selectedComment = commentSelection.SelectedItem as Comment;
                if (selectedComment == null)
                {
                    systemOutput.Clear();
                    systemOutput.AppendText("You have not selected a valid comment to delete\n");
                    return;
                }

                Post selectedPost = postSelection.SelectedItem as Post;
                if (selectedPost == null)
                {
                    systemOutput.Clear();
                    systemOutput.AppendText("You have not selected a valid post\n");
                    return;
                }

                // Removes comment
                if (selectedComment.AuthorID == Program.activeUser.Id || Program.activeUser.Type == User.UserType.Admin)
                {
                    // Removes comment from parent's comment collection
                    if (Program.globalPosts.ContainsKey(selectedComment.ParentID))
                        Program.globalPosts[selectedComment.ParentID].postComments.Remove(selectedComment.Id);
                    else
                    {
                        Comment parentComment = RedditUtilities.CommentReplyAdderExtension(selectedComment.ParentID);
                        parentComment.commentReplies.Remove(selectedComment.Id);
                    }

                    // Refreshes post comments
                    // Displays abbreviated content if appropriate: commentSelection.DisplayMember -> Comment.AbbreviatedContent property -> ToString("ListBox") (extra step required by assignment specification)
                    commentSelection.Items.Clear();
                    foreach (Comment postComment in selectedPost.postComments.Values.OrderBy(postComment => postComment).ThenBy(postComment => postComment.TimeStamp))
                    {
                        commentSelection.Items.Add(postComment);
                        PrintChildComments(postComment);
                    }
                }
                else
                {
                    systemOutput.Clear();
                    systemOutput.AppendText("You cannot delete other users' comments\n");
                    return;
                }

                // Removes object from comment ListBox
                commentSelection.Items.Remove(commentSelection.SelectedItem);

                // Prints success message
                systemOutput.Clear();
                systemOutput.AppendText("Comment successfully deleted\n");
            }
            catch (Exception exception)
            {
                systemOutput.Clear();
                systemOutput.AppendText(exception.Message);
            }
        }*/
        /**
         * 
         * Method Name: deletePostButton_Click
         * 
         * The following triggers when the user clicks on this component( represented by a button: Delete Post)
         * 
         * This event includes the following responsibilities
         *  - inform the user about whether their delete attempt was appropriate: disallow deletion without authentication,
         *    disallow deletions when the user is not an administrator or if said post does not belong to the user
         *  - removing the selected post if the conditions above permit so
         *  - displaying the removal by clearing the comment box
         *  - printing feedback to the user on system output
         * Returns: void
         * 
         * Notes: like comment deletion, the functionality of this button completely deletes the chosen post
         * 
         */
    /*    private void deletePostButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (Program.activeUser == null)
                {
                    systemOutput.Clear();
                    systemOutput.AppendText("Log-in is required to delete posts and comments\n");
                    return;
                }

                if (postSelection.SelectedItem == null)
                {
                    systemOutput.Clear();
                    systemOutput.AppendText("You must select a post first\n");
                    return;
                }

                Post selectedPost = postSelection.SelectedItem as Post;
                if (selectedPost == null)
                {
                    systemOutput.Clear();
                    systemOutput.AppendText("You did not select a valid post\n");
                    return;
                }

                if (selectedPost.AuthorId == Program.activeUser.Id || Program.activeUser.Type == User.UserType.Admin)
                {
                    if (!Program.globalPosts.ContainsKey(selectedPost.Id))
                    {
                        systemOutput.Clear();
                        systemOutput.AppendText("Post with id " + selectedPost.Id + " was not found\n");
                        return;
                    }

                    // Clears comment ListBox
                    commentSelection.Items.Clear();

                    // Removes post from home subreddit
                    Program.globalSubreddits[selectedPost.subHomeId].subPostIDs.Remove(selectedPost.Id);

                    // Removes post from global post collection
                    Program.globalPosts.Remove(selectedPost.Id);
                }
                else
                {
                    systemOutput.Clear();
                    systemOutput.AppendText("You cannot delete other users' posts\n");
                    return;
                }

                // Removes object from post ListBox
                postSelection.Items.Remove(postSelection.SelectedItem);

                // Prints success message
                systemOutput.Clear();
                systemOutput.AppendText("Post successfully deleted\n");
            }
            catch (Exception exception)
            {
                systemOutput.Clear();
                systemOutput.AppendText(exception.Message);
            }
        }*/
        private class DisplayPost : Panel
        {
            public static int y_offset = 0;
            private Form1 form1Instance;

            private uint postId;
            public uint PostId => postId;

            public DisplayPost(uint postID)
            {
                
                BackColor = Color.LightGray;
                form1Instance = (Form1)Application.OpenForms["Form1"];
                Location = new Point(0, y_offset );
                y_offset += 180;
                postId = postID;
                InitializeComponent();
                Width = form1Instance.Width - 70;
                Height = 180;
                
            }

            #region Windows Form Designer generated code
            // LAYOUT:

            /// <summary>
            /// Required method for Designer support - do not modify
            /// the contents of this method with the code editor.
            /// </summary>
            private void InitializeComponent()
            {
                var post = Program.globalPosts[postId];

                this.label1 = new System.Windows.Forms.Label();
                this.label2 = new System.Windows.Forms.Label();
                this.label3 = new System.Windows.Forms.Label();
                this.label4 = new System.Windows.Forms.Label();
                this.label5 = new System.Windows.Forms.Label();
                this.pictureBox3 = new System.Windows.Forms.PictureBox();
                this.pictureBox2 = new System.Windows.Forms.PictureBox();
                this.pictureBox1 = new System.Windows.Forms.PictureBox();
                this.label6 = new System.Windows.Forms.Label();
                ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
                ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
                ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
                this.SuspendLayout();
                // 
                // label1
                // 
                this.label1.AutoSize = true;
                this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.label1.Location = new System.Drawing.Point(17, 68);
                this.label1.Name = "label1";
                this.label1.Size = new System.Drawing.Size(45, 16);
                this.label1.TabIndex = 2;
                this.label1.Text = $"{post.Score}";
                // 
                // label2
                // 
                this.label2.AutoSize = true;
                this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.label2.Location = new System.Drawing.Point(538, 12);
                this.label2.Name = "label2";
                this.label2.Size = new System.Drawing.Size(169, 24);
                this.label2.TabIndex = 3;
                // TIME MEASUREMENT:
                var timeSincePost = DateTime.Now - post.DateString;
                if (timeSincePost < new TimeSpan(1,0,0))
                    this.label2.Text = $"Less than an hour old from {DateTime.Now:G}";
                else if (timeSincePost < new TimeSpan(1,0,0,0))
                    this.label2.Text = $"Less than a day old from {DateTime.Now:G}";
                else if (timeSincePost < new TimeSpan(30,0,0,0))
                    this.label2.Text = $"Less than a month old from {DateTime.Now:G}";
                else if (timeSincePost < new TimeSpan(365,0,0,0))
                    this.label2.Text = $"Less than a year old from {DateTime.Now:G}";
                else
                    this.label2.Text = $"More than a year old from {DateTime.Now:G}";

                // 
                // label3
                // 
                this.label3.AutoSize = true;
                this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.label3.Location = new System.Drawing.Point(268, 12);
                this.label3.Name = "label3";
                this.label3.Size = new System.Drawing.Size(264, 24);
                this.label3.TabIndex = 4;
                this.label3.Text = $"| Posted by r/{Program.globalUsers[post.AuthorId].Name}";
                // 
                // label4
                // 
                this.label4.AutoSize = true;
                this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.label4.Location = new System.Drawing.Point(68, 12);
                this.label4.Name = "label4";
                this.label4.Size = new System.Drawing.Size(194, 24);
                this.label4.TabIndex = 5;
                this.label4.Text = $"r/{Program.globalSubreddits[post.subHomeId].Name}";
                // 
                // label5
                // 
                this.label5.AutoSize = true;
                this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.label5.Location = new System.Drawing.Point(79, 59);
                this.label5.Name = "label5";
                this.label5.Size = new System.Drawing.Size(138, 25);
                this.label5.TabIndex = 6;
                this.label5.Text = $"{post.Title}";
                // 
                // pictureBox3
                // 
                this.pictureBox3.Image = global::KozinskiAlamidiAssignment4.Properties.Resources.comment;
                this.pictureBox3.Location = new System.Drawing.Point(552, 110);
                this.pictureBox3.Name = "pictureBox3";
                this.pictureBox3.Size = new System.Drawing.Size(34, 30);
                this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.pictureBox3.TabIndex = 7;
                this.pictureBox3.TabStop = false;
                // 
                // pictureBox2
                // 
                this.pictureBox2.Image = global::KozinskiAlamidiAssignment4.Properties.Resources.downvote;
                this.pictureBox2.Location = new System.Drawing.Point(12, 87);
                this.pictureBox2.Name = "pictureBox2";
                this.pictureBox2.Size = new System.Drawing.Size(50, 53);
                this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.pictureBox2.TabIndex = 1;
                this.pictureBox2.TabStop = false;
                // 
                // pictureBox1
                // 
                this.pictureBox1.Image = global::KozinskiAlamidiAssignment4.Properties.Resources.upvote;
                this.pictureBox1.Location = new System.Drawing.Point(12, 12);
                this.pictureBox1.Name = "pictureBox1";
                this.pictureBox1.Size = new System.Drawing.Size(50, 53);
                this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.pictureBox1.TabIndex = 0;
                this.pictureBox1.TabStop = false;
                // 
                // label6
                // 
                this.label6.AutoSize = true;
                this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.label6.Location = new System.Drawing.Point(592, 124);
                this.label6.Name = "label6";
                this.label6.Size = new System.Drawing.Size(199, 16);
                this.label6.TabIndex = 8;
                this.label6.Text = $"{post.postComments.Count} Comments";
                // 
                // Form2
                // 
                this.ClientSize = new System.Drawing.Size(800, 141);
                this.Controls.Add(this.label6);
                this.Controls.Add(this.pictureBox3);
                this.Controls.Add(this.label5);
                this.Controls.Add(this.label4);
                this.Controls.Add(this.label3);
                this.Controls.Add(this.label2);
                this.Controls.Add(this.label1);
                this.Controls.Add(this.pictureBox2);
                this.Controls.Add(this.pictureBox1);
                this.Name = $"Form2";
                this.Text = "Form2";
                ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
                ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
                ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
                this.ResumeLayout(false);
                this.PerformLayout();

            }

            #endregion
            

            private System.Windows.Forms.PictureBox pictureBox1;
            private System.Windows.Forms.PictureBox pictureBox2;
            private System.Windows.Forms.Label label1;
            private System.Windows.Forms.Label label2;
            private System.Windows.Forms.Label label3;
            private System.Windows.Forms.Label label4;
            private System.Windows.Forms.Label label5;
            private System.Windows.Forms.PictureBox pictureBox3;
            private System.Windows.Forms.Label label6;
        }

        public void DisplayPost_Click(Object sender, EventArgs e)
        {
            DisplayPost newPost = sender as DisplayPost;
            Form3 viewPost = new Form3(newPost.PostId);
            viewPost.Show();
            
        }

        private void subredditSelection_SelectedValueChanged(object sender, EventArgs e)
        {
            Subreddit selectedSubreddit = subredditSelection.SelectedItem as Subreddit;
            if (selectedSubreddit == null) { MessageBox.Show("failed to cast subreddit.selecteditem to a subreddit in function subredditSelection_selectedValueChanged"); return; }
            if (selectedSubreddit.Name == "all") 
            {
                DisplayPost.y_offset = 0;
                panel1.Controls.Clear();
                foreach (KeyValuePair<uint, Post> postTuple in Program.globalPosts)
                {
                    panel1.Controls.Add(new DisplayPost(postTuple.Value.Id));
                }
            }
            else
            {
                DisplayPost.y_offset = 0;
                panel1.Controls.Clear();
                foreach (KeyValuePair<uint, Post> postTuple in Program.globalPosts.Where(postTuple => postTuple.Value.subHomeId == selectedSubreddit.Id))
                {
                    panel1.Controls.Add(new DisplayPost(postTuple.Value.Id));
                }
            }
        }
    }
}
