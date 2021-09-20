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

        // this triggers when the user chooses a subreddit
        private void subredditSelection_SelectedValueChanged(object sender, EventArgs e)
        {
            postSelection.Items.Clear();
            systemOutput.Clear();

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
                foreach (KeyValuePair<uint, Subreddit> subredditTuple in Program.globalSubreddits)
                {
                    if (subredditTuple.Value.Name == subredditSelection.SelectedItem.ToString())
                    {
                        // Displays abbreviated title if appropriate: postSelection.DisplayMember -> Post.AbbreviatedTitle property -> ToString("ListBox") (extra step required by assignment specification)
                        foreach (KeyValuePair<uint, Post> postTuple in Program.globalPosts.Where(globalPostTuple => subredditTuple.Value.subPostIDs.Contains(globalPostTuple.Key)).OrderBy(globalPostTuple => globalPostTuple.Value))
                            postSelection.Items.Add(postTuple.Value);

                        // there can only be one match for this check, which warrants ending this loop once a single match was found
                        break;
                    }
                }
            }
        }

        private void postSelection_SelectedValueChanged(object sender, EventArgs e)
        {
            // Prevents exception from being thrown when post is deleted
            if (postSelection.SelectedItem == null)
                return;

            systemOutput.Clear();
            systemOutput.AppendText(postSelection.SelectedItem.ToString());

            commentSelection.Items.Clear();

            Post chosenPost = postSelection.SelectedItem as Post;
            if (chosenPost == null)
                throw new Exception("Casting from postSelection.SelectedItem to a Post was unsuccessful");

            // Populates comment box

            // Sorts comment level by post rating
            Comment[] sortedPostComments = chosenPost.postComments.Values.ToArray();
            Array.Sort(sortedPostComments);

            // Starts recursion at the post level
            // Displays abbreviated content if appropriate: commentSelection.DisplayMember -> Comment.AbbreviatedContent property -> ToString("ListBox") (extra step required by assignment specification)
            foreach (Comment postComment in sortedPostComments)
            {
                commentSelection.Items.Add(postComment);
                PrintChildComments(postComment);
            }
        }

        private void commentSelection_SelectedValueChanged(object sender, EventArgs e)
        {
            // Prevents exception from being thrown when comment is deleted
            if (postSelection.SelectedItem == null)
                return;

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
            if (replyInput.Text == "")
            {
                systemOutput.AppendText("You may not post an empty comment\n");
                return;
            }
            if (Program.activeUser == null)
            {
                systemOutput.AppendText("Log-in is required to add posts and comments\n");
                return;
            }
            // the following check is for when neither a post nor a comment is chosen
            if (postSelection.SelectedIndex == -1)
            {
                systemOutput.AppendText("Choose a post to reply to a post or a comment to reply to a comment.\n");
                return;
            }
            Post chosenPost = postSelection.SelectedItem as Post;
            if (chosenPost.Locked == true)
            {
                systemOutput.AppendText("Post is marked as \'Locked\' -- replies are disabled.");
                return;
            }
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

        private void deleteReplyButton_Click(object sender, EventArgs e)
        {
            try
            {
                Comment selectedComment = commentSelection.SelectedItem as Comment;
                if (selectedComment == null)
                    throw new ArgumentNullException("Casting from commentSelection.SelectedItem to a Comment was unsuccessful\n");

                Post selectedPost = postSelection.SelectedItem as Post;
                if (selectedPost == null)
                    throw new ArgumentNullException("Casting from postSelection.SelectedItem to a Post was unsuccessful\n");

                if (Program.activeUser == null)
                    throw new ArgumentNullException("Log-in is required to delete posts and comments\n");

                if (postSelection.SelectedItem == null || commentSelection.SelectedItem == null)
                    throw new ArgumentNullException("You must select a post and one of its comments first\n");

                // Removes or modifies comment (see notes below for reasoning)
                if (selectedComment.AuthorID == Program.activeUser.Id)
                {
                    // Removes comment from comment ListBox if comment has no children
                    if (selectedComment.commentReplies.Count() == 0)
                        postSelection.Items.Remove(commentSelection.SelectedItem);

                    // Replaces comment content and author if comment has children;
                    // deviates from the assignment specification a little bit, but
                    // makes sense if we also want to preserve the requirement that
                    // users cannot delete others' comments
                    else
                    {
                        selectedComment.Content = "<This comment has been removed>";
                        selectedComment.AuthorID = 10000; // Outside normal range of user IDs to avoid conflicts
                    }
                }
                else
                    throw new Exception("You cannot delete other users' comments\n");

                // Removes object from post ListBox
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
        }

        private void deletePostButton_Click(object sender, EventArgs e)
        {
            try
            {
                Post selectedPost = postSelection.SelectedItem as Post;
                if (selectedPost == null)
                    throw new ArgumentNullException("Casting from postSelection.SelectedItem to a Post was unsuccessful\n");

                if (Program.activeUser == null)
                    throw new ArgumentNullException("Log-in is required to delete posts and comments\n");

                if (postSelection.SelectedItem == null)
                    throw new ArgumentNullException("You must select a post first\n");

                if (selectedPost.AuthorId == Program.activeUser.Id)
                {
                    if (!Program.globalPosts.ContainsKey(selectedPost.Id))
                        throw new ArgumentException("Post with id " + selectedPost.Id + " was not found\n");

                    // Removes post from home subreddit
                    Program.globalSubreddits[selectedPost.subHomeId].subPostIDs.Remove(selectedPost.Id);

                    // Removes post from global post collection
                    Program.globalPosts.Remove(selectedPost.Id);

                    // Replaces post content/author
                    selectedPost.PostContent = "<This post has been removed>";
                    selectedPost.AuthorId = 10000; // Outside normal range of user IDs to avoid conflicts
                }
                else
                    throw new Exception("You cannot delete other users' posts\n");

                // Removes object from post ListBox
                postSelection.Items.Remove(postSelection.SelectedItem);

                // Prints success message
                commentSelection.Items.Clear();
                systemOutput.Clear();
                systemOutput.AppendText("Post successfully deleted\n");
            }
            catch (Exception exception)
            {
                systemOutput.Clear();
                systemOutput.AppendText(exception.Message);
            }
        }
    }
}
