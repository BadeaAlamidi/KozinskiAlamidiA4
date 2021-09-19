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

        // Iterates recursively through comments (function is called in loop above)
        void PrintChildComments(Comment currentComment)
        {
            // Sorts comment level by post rating
            Comment[] sortedCommentReplies = currentComment.commentReplies.Values.ToArray();
            Array.Sort(sortedCommentReplies);

            foreach (Comment commentReply in currentComment.commentReplies.Values.OrderBy(comment => comment))
            {
                commentSelection.Items.Add(commentReply);

                // Recursive call
                PrintChildComments(commentReply);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            // Runs file reader and stores error log
            List<string> fileReadErrors = RedditUtilities.ReadFiles();

            // Prints error log to system output
            foreach (string line in fileReadErrors)
                systemOutput.AppendText(line + "\n");

            // Populates user, subreddit, and post boxes
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
                    // there can only be one match for this check, which warrants ending this loop once a single match was found
                    // displays abbreviated title if appropriate: postSelection.DisplayMember -> Post.AbbreviatedTitle property -> ToString("ListBox") (extra step required by assignment specification)
                    break;
                }
            }
        }

        private void postSelection_SelectedValueChanged(object sender, EventArgs e)
        {
            systemOutput.Clear();
            systemOutput.AppendText(postSelection.SelectedItem.ToString());

            commentSelection.Items.Clear();
            Post chosenPost = postSelection.SelectedItem as Post;
            if (chosenPost == null) throw new Exception("casting from postSelection.SelectedItem to a Post was unsuccessful");

            // Sorts comment level by post rating
            Comment[] sortedPostComments = chosenPost.postComments.Values.ToArray();
            Array.Sort(sortedPostComments);

            // Starts at the post level
            // Displays abbreviated title if appropriate: commentSelection.DisplayMember -> Comment.AbbreviatedTitle property -> ToString("ListBox") (extra step required by assignment specification)
            foreach (Comment postComment in sortedPostComments)
            {
                commentSelection.Items.Add(postComment);
                PrintChildComments(postComment);
            }

        }

        private void commentSelection_SelectedValueChanged(object sender, EventArgs e)
        {
            systemOutput.Clear();
            systemOutput.AppendText(commentSelection.SelectedItem.ToString());
        }

        private void loginButton_MouseClick(object sender, MouseEventArgs e)
        {
            if (userSelection.SelectedIndex == -1) { systemOutput.AppendText("You haven't specified a username!"); return; }
            if (passwordInput.Text == "") { systemOutput.AppendText("\nplease enter a password. Be sure to choose the your intended username\n"); return; }
            //authentication happens here:
            User chosenUser = userSelection.SelectedItem as User;
            if (passwordInput.Text.GetHashCode().ToString("X") == chosenUser.PasswordHash) { systemOutput.AppendText("authentication successful\n"); Program.activeUser = chosenUser; }
            else systemOutput.AppendText("authentication failed");
        }

        private void addReplyButton_Click(object sender, EventArgs e)
        {
            if (replyInput.Text == "") { systemOutput.AppendText("You may not post an empty comment\n"); return; }
            if (Program.activeUser == null) { systemOutput.AppendText("Log-in is required to add posts and comments\n"); return; }
            // the following check is for when neither a post nor a comment is chosen
            if (postSelection.SelectedIndex == -1) { systemOutput.AppendText("Choose a post to reply to a post or a comment to reply to a comment.\n"); return; }
            Post chosenPost = postSelection.SelectedItem as Post;
            // case for when posting a reply to a post
            if (commentSelection.SelectedIndex == -1)
            {
                Comment newComment = new Comment(replyInput.Text, Program.activeUser.Id, chosenPost.Id, 0);
                chosenPost.postComments.Add(newComment.Id, newComment);
            }
            // this will trigger if the commentSelection.SelectedIndex is not -1.
            else 
            {
                Comment chosenComment = commentSelection.SelectedItem as Comment;
                Comment newComment = new Comment(replyInput.Text, Program.activeUser.Id, chosenComment.Id, chosenComment.Indentation + 1);
                chosenComment.commentReplies.Add(newComment.Id, newComment);
            }
            commentSelection.Items.Clear();
            foreach(Comment comment in chosenPost.postComments.Values.OrderBy(postComment => postComment))
            {
                commentSelection.Items.Add(comment);
                PrintChildComments(comment);
            }
        }
    }
}
