using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KozinskiAlamidiAssignment2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            RedditUtilities.ReadFiles();
            //try { RedditUtilities.ReadFiles(); } /*catch (ArgumentException exception)*/catch (Exception exception){ systemOutput.AppendText(exception.Message);}
            foreach (KeyValuePair<uint, User> user in Program.globalUsers.OrderBy(user => user.Value.Name)) { userSelection.Items.Add(user.Value); }
            foreach (KeyValuePair<uint,Subreddit>subreddit in Program.globalSubreddits.OrderBy(subreddit => subreddit.Value.Name)) { subredditSelection.Items.Add(subreddit.Value.Name); }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
        // this triggers when the user chooses a subreddit
        private void subredditSelection_SelectedValueChanged(object sender, EventArgs e)
        {
            postSelection.Items.Clear();
            foreach (KeyValuePair<uint, Subreddit> subredditTuple in Program.globalSubreddits) {
                if (subredditTuple.Value.Name == subredditSelection.SelectedItem.ToString()) {
                    foreach (KeyValuePair<uint, Post> postTuple in Program.globalPosts.Where(globalPostTuple => subredditTuple.Value.subPostIDs.Contains(globalPostTuple.Key)).OrderBy(globalPostTuple => globalPostTuple.Value))
                        postSelection.Items.Add(postTuple.Value);
                    //there can only be one match for this check, which warrants ending this loop once a single match was found
                    break;
                }
            }
        }

        private void postSelection_SelectedValueChanged(object sender, EventArgs e)
        {
            commentSelection.Items.Clear();
            Post chosenPost = postSelection.SelectedItem as Post;
            if (chosenPost == null) throw new Exception("casting from postSelection.SelectedItem to a Post was unsuccessful");

            // Sorts comment level by post rating
            Comment[] sortedPostComments = chosenPost.postComments.Values.ToArray();
            Array.Sort(sortedPostComments);

            // Starts at the post level
            foreach (Comment postComment in sortedPostComments)
            {
                commentSelection.Items.Add(postComment);
                PrintChildComment(postComment);
            }

            // Iterates recursively through comments (function is called in loop above)
            void PrintChildComment(Comment currentComment)
            {
                // Sorts comment level by post rating
                Comment[] sortedCommentReplies = currentComment.commentReplies.Values.ToArray();
                Array.Sort(sortedCommentReplies);

                foreach (Comment commentReply in sortedCommentReplies)
                {

                    commentSelection.Items.Add(commentReply);
                    // Recursive call
                    PrintChildComment(commentReply);
                }
            }
        }

        private void loginButton_MouseClick(object sender, MouseEventArgs e)
        {
            if (userSelection.SelectedIndex == -1) { systemOutput.AppendText("You haven't specified a username!"); return; }
            if (passwordInput.Text == "") { systemOutput.AppendText("\nplease enter a password. Be sure to choose the your intended username\n"); return; }
            //authentication happens here:
            User chosenUser = userSelection.SelectedItem as User;
            if (passwordInput.Text.GetHashCode().ToString("X") == chosenUser.PasswordHash) { systemOutput.AppendText("authentication successful"); Program.activeUser = chosenUser; }
            else systemOutput.AppendText("authentication failed");
        }
        
    }
}
