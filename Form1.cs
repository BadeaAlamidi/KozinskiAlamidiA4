using System;
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
     * RefreshPanel1                triggers the subredditValue_changed event 
     * 
     * Event Methods:
     * Form1_Load                   triggers upon the windows form startup. performs necessary
     *                               - initializtion steps such as populating Program.global*
     *                               - dictionaries with data
     * subredditSelection_ValueChanged  triggers when the user chooses a value from the subreddit combo box.
     *                                  - performs a reset of panel1, which holds DisplayPost panels in order
     *                                  - to filter the display posts in panel1 to only reflect the posts that 
     *                                  - belong to that subreddit
     * 
     * loginButton_Click              instantiates an instance of form3 for prompting the user to set the
     *                               - activeUser variable in the program
     *                               
     * textBox1_keyUp               triggers upon every keystroke while typing into the textbox textBox1
     *                              - calls reset panel1 whenever triggered to show DisplayPosts in panel1 that
     *                              - whose title contents contain a substring of the text entered into textBox1
     *                              
     * Form1_FormClosed             triggers when this form is closed to update the votes of all posts that have been
     *                              voted at run time
     * 
     *                               
     * subredditSelection_SelectedValueChanged  triggers when the user chooses/changes a selectable item
     *                                           - in the list subreddit listbox
     *
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
         * Method Name: Form1_Load
         * 
         * Triggers when the application starts. This event is responsble for
         * invoking the ReadFiles function in A3Program.cs and populating
         * the global subreddits with the corresponding data retained from
         * ReadFiles. After this, the panel 1 is populated with instances of
         * displayposts showing the posts that fall under the value of this subreddit
         * value. by extension, the subreddit value combo vbox is also populated
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
                WindowState = FormWindowState.Maximized;
                // Prints error log to system output
                foreach (string line in fileReadErrors)
                    systemOutput.AppendText(line + "\n");

                // Populate subreddit ComboBox
                foreach (KeyValuePair<uint, Subreddit> subreddit in Program.globalSubreddits.OrderBy(subreddit => subreddit.Value.Name)) { subredditSelection.Items.Add(subreddit.Value); }
                subredditSelection.SelectedIndex = 0;

            }
            catch (Exception exception)
            {
                systemOutput.Clear();
                systemOutput.AppendText(exception.Message);
            }
        }


        /**
         * Class : DisplayPost
         * this class is a panel with a certain design meant to reflect the information of a post
         * that is associated with it. instances of this class are meant to be added to the controls of 
         * panel1 of the containing class Form1.
         * 
         * Attributes:
         * y_offset         an offset used to determine the ylocation of the next displayPost that will get added
         * form1Instance    an instance of the containing class used to determine the size of this DisplayPost
         * postId           the id of the post that this DisplayPost represents. used to retain the post's information 
         *                  - such as the score and content
         * totalComments    represents the number of comments under the post that this DisplayPost is representing
         *                  - determined in the constructor
         * Properties
         * PostId           getter for postId
         * 
         * Controls properties:
         * pictureBox1      contains the image of an upvote
         * pictureBox2      contains the image of a downvote
         * pictureBox3      contians the image of a comment
         * label1           displays the Score of the post
         * label3           // the poster of the post and the duration since the post was made
         * label4           // the subreddit this post is under
         * label5           // post title
         * lebel6           // the number of comments under that post
         * 
         * Constructors:
         * DisplayPost(postId)  refer to documentation box where this is declared
         * 
         * Events:
         * DisplayPost_Click    shows an instance of form2 that gets instantiated with the id of the
         *                      - post of that is repreented by this displayPost
         * postUpvote_mouseEnter    changes the picture that is shown in DisplayPost to redOrange
         * postUpvote_mouseLeave    // // back to the right color (dependent on whether the user has already voted on 
         *                          - this DisplayPost or not
         * postUpvote_Click         adjusts the value of this DisplayPost's score and changes the color of the upvote
         *                          -both are dependent on whether the activeUser has already voted
         * postDownvote_mouseEnter  similar to postUpvote_mouseEnter
         * postUpvote_mouseLeave    similar to postUpvote_mouseLeave
         * postDownvote_Click       similar to postUpvote_Click
         * 
         * 
         * 
         * 
         */
        private class DisplayPost : Panel
        {
            public static int y_offset = 0;

            private Form1 form1Instance;

            private uint postId = 0;
            private uint totalComments;

            public uint PostId => postId;

            public DisplayPost(uint postID)
            {
                
                BackColor = Color.LightGray;
                form1Instance = (Form1)Application.OpenForms["Form1"];
                Location = new Point(0, y_offset );
                y_offset += 180;
                postId = postID;
                totalComments = (uint)Program.globalPosts[postId].postComments.Count();

                // COUNTING TOTAL COMMENTS:
                Action<Comment> traverse = null;
                traverse = (Comment currentComment) => {
                    totalComments += (uint)currentComment.commentReplies.Count();
                    foreach (Comment reply in currentComment.commentReplies.Values)
                        traverse(reply);
                };

                foreach(Comment comment in Program.globalPosts[postId].postComments.Values)
                {
                    traverse(comment);
                }

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
                // click event for this panel
                //
                Click += new System.EventHandler(this.DisplayPost_Click);
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
                // label3
                // 
                this.label3.AutoSize = true;
                this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.label3.Location = new System.Drawing.Point(268, 12);
                this.label3.Name = "label3";
                this.label3.Size = new System.Drawing.Size(264, 24);
                this.label3.TabIndex = 4;
                this.label3.Text = $"| Posted by r/{Program.globalUsers[post.AuthorId].Name}";
                // TIME MEASUREMENT:
                var timeSincePost = DateTime.Now - post.DateString;
                if (timeSincePost < new TimeSpan(1, 0, 0))
                    label3.Text += $" {timeSincePost.Minutes} minutes ago";
                else if (timeSincePost < new TimeSpan(1, 0, 0, 0))
                    label3.Text += $" {timeSincePost.Hours} hours ago";
                else if (timeSincePost < new TimeSpan(30, 0, 0, 0))
                    label3.Text += $" {timeSincePost.Days} days ago";
                else if (timeSincePost < new TimeSpan(365, 0, 0, 0))
                    label3.Text += $" {timeSincePost.Days / 30} months ago";
                else
                    label3.Text += $" {timeSincePost.Days / 365} years ago";
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
                this.pictureBox2.Image = global::KozinskiAlamidiAssignment4.Properties.Resources.greyDownvote;
                if (Program.activeUser != null && Program.activeUser.PostVoteStatuses.ContainsKey(PostId))
                {
                    this.pictureBox1.Image = Program.activeUser.PostVoteStatuses[postId] == -1 ? Properties.Resources.downvote : Properties.Resources.greyDownvote;
                }
                this.pictureBox2.Location = new System.Drawing.Point(12, 87);
                this.pictureBox2.Name = "pictureBox2";
                this.pictureBox2.Size = new System.Drawing.Size(50, 53);
                this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.pictureBox2.TabIndex = 1;
                this.pictureBox2.TabStop = false;
                // 
                // pictureBox1
                // 
                if (Program.activeUser != null && Program.activeUser.PostVoteStatuses.ContainsKey(PostId))
                {
                    this.pictureBox1.Image = Program.activeUser.PostVoteStatuses[postId] == 1? Properties.Resources.upvote : Properties.Resources.greyUpvote;
                }
                else
                    this.pictureBox1.Image = global::KozinskiAlamidiAssignment4.Properties.Resources.greyUpvote;
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
                this.label6.Text = $"{totalComments} Comments";
                // 
                // Form2
                // 
                this.ClientSize = new System.Drawing.Size(800, 141);
                this.Controls.Add(this.label6);
                this.Controls.Add(this.pictureBox3);
                this.Controls.Add(this.label5);
                this.Controls.Add(this.label4);
                this.Controls.Add(this.label3);
                this.Controls.Add(this.label1);
                this.Controls.Add(this.pictureBox2);
                this.Controls.Add(this.pictureBox1);
                this.Name = $"postID{post.Id}";
                this.pictureBox1.MouseEnter += new System.EventHandler(this.PostUpvote_MouseEnter);
                this.pictureBox1.MouseLeave += new System.EventHandler(this.PostUpvote_MouseLeave);
                this.pictureBox1.Click += new System.EventHandler(this.PostUpvote_Click);
                this.pictureBox2.MouseEnter += new System.EventHandler(this.PostDownvote_MouseEnter);
                this.pictureBox2.MouseLeave += new System.EventHandler(this.PostDownvote_MouseLeave);
                this.pictureBox2.Click += new System.EventHandler(this.PostDownvote_Click);
                ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
                ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
                ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
                this.ResumeLayout(false);
                this.PerformLayout();

            }


            #endregion

            //
            // Events
            //

            /**
             * this event is triggered when a displayPost panel is clicked
             * the responsiblity of this event is to instantiate an instance of form2
             * with the corresponding id of this DisplayPost
             * 
             * 
             */
            public void DisplayPost_Click(Object sender, EventArgs e)
            {
                DisplayPost newPost = sender as DisplayPost;
                Form2 viewPost = new Form2(newPost.PostId);
                viewPost.ShowDialog();
            }

            #region Post upvote/downvote button event handlers
            /**
             * triggered when mouse enters the upvote button
             * responsible for changing the upvote picture to red
             * 
             */
            public void PostUpvote_MouseEnter(object sender, EventArgs e)
            {
                this.pictureBox1.Image = global::KozinskiAlamidiAssignment4.Properties.Resources.upvote;
            }
            /**
             * triggered when mouse no longer hovers over the upvote button
             * responsible for changing the picture of the upvote button, depending on whether the user
             * is logged in:
             * if the user is logged in, the image does not change, otherwise the image will turn back to
             * the grey upvote counterpart
             */
            public void PostUpvote_MouseLeave(object sender, EventArgs e)
            {
                if (Program.activeUser != null && Program.activeUser.PostVoteStatuses.ContainsKey(postId))
                    if (Program.activeUser.PostVoteStatuses[postId] == 1) return;

                // Else
                this.pictureBox1.Image = global::KozinskiAlamidiAssignment4.Properties.Resources.greyUpvote;
            }
            /**
             * 
             * triggered when the user clicks on the upvote button
             * -responsible for changing the amount of upvotes on the associated post with this displaylist
             * -responsible for setting the upvote to be permenantly colored to red, unless clicked again, in which
             *      case the upvote will be taken away from the post and the picture of the upvote will turn back to
             *      grey
             * -responsible for changing label1 score to reflect the new score of the post
             */
            public void PostUpvote_Click(object sender, EventArgs e)
            {
                if (Program.activeUser == null)
                {
                    MessageBox.Show("You must be logged in to vote on posts.");
                    return;
                }

                bool hasVoted = Program.activeUser.PostVoteStatuses.ContainsKey(postId);

                if (!hasVoted)
                {
                    Program.activeUser.PostVoteStatuses.Add(postId, 1);
                    Program.globalPosts[postId].UpVotes += 1;
                    this.pictureBox1.Image = global::KozinskiAlamidiAssignment4.Properties.Resources.upvote;
                }
                else
                {
                    if (Program.activeUser.PostVoteStatuses[postId] < 1)
                    {
                        Program.globalPosts[postId].DownVotes -= 1;
                        Program.globalPosts[postId].UpVotes += 1;
                        Program.activeUser.PostVoteStatuses[postId] = 1;
                        this.pictureBox1.Image = global::KozinskiAlamidiAssignment4.Properties.Resources.upvote;
                        this.pictureBox2.Image = global::KozinskiAlamidiAssignment4.Properties.Resources.greyDownvote;
                    }
                    else
                    {
                        Program.activeUser.PostVoteStatuses.Remove(postId);
                        Program.globalPosts[postId].UpVotes -= 1;
                        this.pictureBox1.Image = global::KozinskiAlamidiAssignment4.Properties.Resources.greyUpvote;
                    }
                }
                label1.Text = $"{Program.globalPosts[postId].Score}";
            }
            /**
             * triggered when mouse enters the downvote button
             * responsible for changing the downvote picture to blue
             * 
             */
            public void PostDownvote_MouseEnter(object sender, EventArgs e)
            {
                this.pictureBox2.Image = global::KozinskiAlamidiAssignment4.Properties.Resources.downvote;
            }
            /**
             * triggered when mouse no longer hovers over the downvote button
             * responsible for changing the picture of the downvote button, depending on whether the user
             * is logged in:
             * if the user is logged in, the image does not change, otherwise the image will turn back to
             * the grey upvote counterpart
             */
            public void PostDownvote_MouseLeave(object sender, EventArgs e)
            {
                if (Program.activeUser != null && Program.activeUser.PostVoteStatuses.ContainsKey(postId))
                    if (Program.activeUser.PostVoteStatuses[postId] == -1) return;

                // Else
                this.pictureBox2.Image = global::KozinskiAlamidiAssignment4.Properties.Resources.greyDownvote;
            }
            /**
             * 
             * triggered when the user clicks on the downvote button
             * -responsible for changing the amount of upvotes on the associated post with this displaylist
             * -responsible for setting the downvote to be permenantly colored to blue, unless clicked again, in which
             *      case the downvote will be taken away from the post and the picture of the upvote will turn back to
             *      grey
             * -responsible for changing label1 score to reflect the new score of the post
             */
            public void PostDownvote_Click(object sender, EventArgs e)
            {
                if (Program.activeUser == null)
                {
                    MessageBox.Show("You must be logged in to vote on posts.");
                    return;
                }

                bool hasVoted = Program.activeUser.PostVoteStatuses.ContainsKey(postId);

                if (!hasVoted)
                {
                    Program.activeUser.PostVoteStatuses.Add(postId, -1);
                    Program.globalPosts[PostId].DownVotes += 1;
                    this.pictureBox2.Image = global::KozinskiAlamidiAssignment4.Properties.Resources.downvote;
                }
                else
                {
                    if (Program.activeUser.PostVoteStatuses[postId] > -1)
                    {
                        Program.activeUser.PostVoteStatuses[postId] = -1;
                        Program.globalPosts[PostId].UpVotes -= 1;
                        Program.globalPosts[PostId].DownVotes += 1;
                        this.pictureBox2.Image = global::KozinskiAlamidiAssignment4.Properties.Resources.downvote;
                        this.pictureBox1.Image = global::KozinskiAlamidiAssignment4.Properties.Resources.greyUpvote;
                    }
                    else
                    {
                        Program.globalPosts[PostId].DownVotes -= 1;
                        Program.activeUser.PostVoteStatuses.Remove(postId);
                        this.pictureBox2.Image = global::KozinskiAlamidiAssignment4.Properties.Resources.greyDownvote;
                    }
                }
                label1.Text = $"{Program.globalPosts[PostId].Score}";
            }

            #endregion


            //
            // Controls
            //
            private System.Windows.Forms.PictureBox pictureBox1;
            private System.Windows.Forms.PictureBox pictureBox2;
            private System.Windows.Forms.Label label1;
            private System.Windows.Forms.Label label3;
            private System.Windows.Forms.Label label4;
            private System.Windows.Forms.Label label5;
            private System.Windows.Forms.PictureBox pictureBox3;
            private System.Windows.Forms.Label label6;
        }

        /**
         * 
         * Method Name: subredditSelection_SelectedValueChanged
         * 
         * the following triggers when the highlighted item in the subreddit
         * selection changes
         * the responsibilities of this event include:
         *  - changing the content of panel1 of form1 to show the DisplayPosts that 
         *    fall under the subreddit value denoted by the current value of subredditSelection
         *  - listing the posts of the currently selected subreddit item
         *    in the post listbox (abbreviated version)
         *  - resetting the static value of DisplayPost to 0
         *  - if all subreddits are chosen, every subreddit in represented in panel1 with a displaypost
         *    instance
         * 
         * Returns: void
         * 
         * Notes: NONE
         * 
         */
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
        /**
         * 
         * Method Name: loginButton_Click
         * 
         * The following triggers when the user clicks on the login button in form1
         * 
         * This event includes the following responsibilities
         *  - instantiate an instance of form3 if the activeUser is null. otherwise set the active user to 3
         *  - refreshes the contents of panel 1
         * 
         * Returns: void
         * 
         * Notes: NONE
         * 
         */

        private void loginButton_Click(object sender, EventArgs e)
        {
            if (Program.activeUser != null)
            {
                Program.activeUser = null;
                loginButton.BackColor = Color.White;
                loginButton.Text = "Log in";
            }
            else
            {
                var form3 = new Form3();
                form3.ShowDialog();
                // check for successful authentication
                if (Program.activeUser is User) {
                    loginButton.BackColor = Color.RoyalBlue;
                    loginButton.Text = $"{Program.activeUser.Name} [{Program.activeUser.PostScore + Program.activeUser.CommentScore}] ( Log out )";
                }

            }
            RefreshPanel1(sender, e);
        }

        /**
         * the follwing event triggers when a keystroke is on the keyboard is done
         * 
         * the responsibilites of this event include updating the displayposts in panel1 to 
         * one's whose title contains the textbox1's contents
         * 
         * 
         */
        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (textBox1.Text == "") subredditSelection_SelectedValueChanged(sender, e);
            else
            {
                DisplayPost.y_offset = 0;
                panel1.Controls.Clear();
                foreach (Post post in Program.globalPosts.Values.OrderBy(post => post.PostRating).Where(post => post.Title.Contains(textBox1.Text))){
                    panel1.Controls.Add(new DisplayPost(post.Id));
                }
            }
        }
        /**
         * 
         * simple function as shorthand for invoking the subredditSelection_selectedValueChanged event
         * with organization
         */
        public static void RefreshPanel1(object sender, EventArgs e)
        {
            (Application.OpenForms["Form1"] as Form1).subredditSelection_SelectedValueChanged(sender, e);

        }

        /**
         * 
         * this event is invoked when the program is terminated
         * 
         * this event is responsible for updating all of the posts and comments with 
         * the new values of upvotes and downvotes in the their corresponding values
         * 
         */
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            /*var totalDictionary = new Dictionary<uint, int>();
            var votedPostsDictionaries = from user in Program.globalUsers.Values
                                         where user.PostVoteStatuses.Any()
                                         from key in user.PostVoteStatuses.Keys
                                         group user.PostVoteStatuses[key] by key into postVoteList
                                         select new { key = postVoteList.Key, voteChange = postVoteList.Sum()};*/
            // writing back to posts.txt
            var votedPostsCollection = (from user in Program.globalUsers.Values
                                          where user.PostVoteStatuses.Any()
                                          from key in user.PostVoteStatuses.Keys
                                          select Program.globalPosts[key]).ToDictionary(post => post.Id);
            
            string[] AllLines = File.ReadAllLines("..\\..\\posts.txt");
            for (int i = 0; i < AllLines.Count(); ++i)
            {
                string[] lineToken = AllLines[i].Split('\t');
                if (votedPostsCollection.ContainsKey(Convert.ToUInt32(lineToken[1])))
                {
                    lineToken[6] = Program.globalPosts[Convert.ToUInt32(lineToken[1])].UpVotes.ToString();
                    lineToken[7] = Program.globalPosts[Convert.ToUInt32(lineToken[1])].DownVotes.ToString();
                    AllLines[i] = string.Join("\t", lineToken);
                }
            
            }
            File.WriteAllLines("..\\..\\posts.txt", AllLines);

            // writing back to comments.txt
            var votedCommentsCollection = (from user in Program.globalUsers.Values
                                           where user.CommentVoteStatuses.Any()
                                           select user.CommentVoteStatuses.Keys into dictionaryCollection
                                           from voteStatus in dictionaryCollection
                                           select voteStatus).ToDictionary(comment=>comment.Id);
            AllLines = File.ReadAllLines("..\\..\\comments.txt");
            for (int i = 0; i < AllLines.Count(); ++i)
            {
                if (AllLines[i].Trim() == "") continue;
                string[] lineToken = AllLines[i].Split('\t');
                if (votedCommentsCollection.ContainsKey(Convert.ToUInt32(lineToken[0])))
                {
                    lineToken[4] = votedCommentsCollection[Convert.ToUInt32(lineToken[0])].UpVotes.ToString();
                    lineToken[5] = votedCommentsCollection[Convert.ToUInt32(lineToken[0])].DownVotes.ToString();
                    AllLines[i] = string.Join("\t", lineToken);
                }
            }
            File.WriteAllLines("..\\..\\comments.txt", AllLines);
        }
    }
}