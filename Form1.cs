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

        // Iterates recursively through comments
        // Only shows the first 5 levels of comments
        // Not to be used to print comments upon log-in
        void PrintChildComments(Comment currentComment)
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
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                // Runs file reader and stores error log
                List<string> fileReadErrors = RedditUtilities.ReadFiles();

                // Prints error log to system output
                foreach (string line in fileReadErrors)
                    systemOutput.AppendText(line + "\n");

                systemOutput.AppendText("Welcome! Select a user and enter a password to log in.");

                // Populates user, subreddit, and post boxes
                foreach (KeyValuePair<uint, User> user in Program.globalUsers.OrderBy(user => user.Value.Name)) { userSelection.Items.Add(user.Value); }
                foreach (KeyValuePair<uint, Subreddit> subreddit in Program.globalSubreddits.OrderBy(subreddit => subreddit.Value.Name)) { subredditSelection.Items.Add(subreddit.Value); }
            }
            catch (Exception exception)
            {
                systemOutput.Clear();
                systemOutput.AppendText(exception.Message);
            }
        }

        // this triggers when the user chooses a subreddit
        private void subredditSelection_SelectedValueChanged(object sender, EventArgs e)
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
        }

        private void postSelection_SelectedValueChanged(object sender, EventArgs e)
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
                    systemOutput.AppendText("You did not select a valid post");
                    return;
                }

                // Populates comment box
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
        }

        private void commentSelection_SelectedValueChanged(object sender, EventArgs e)
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
        }

        private void userSelection_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                // reset the text of the log-in button in case there was a previous failed attempt at entering the password
                loginButton.Text = "Log-in";
                // Safety check
                //if (userSelection.SelectedItem == null)
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
            }
            catch (Exception exception)
            {
                systemOutput.Clear();
                systemOutput.AppendText(exception.Message);
            }
        }

        private void loginButton_MouseClick(object sender, MouseEventArgs e)
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
                    }

                    // Iterates recursively through comments
                    void StoreChildComments(Comment currentComment)
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
        }

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
                if (chosenPost.Locked == true)
                {
                    systemOutput.Clear();
                    systemOutput.AppendText("Post is marked as \'Locked\' -- replies are disabled.");
                    return;
                }
                // case for when posting a reply to a post
                if (commentSelection.SelectedIndex == -1)
                {
                    Comment newComment = new Comment(replyInput.Text, Program.activeUser.Id, chosenPost.Id, 0);
                    chosenPost.postComments.Add(newComment.Id, newComment);
                }
                // this will trigger if a comment is selected (the commentSelection.SelectedIndex is not -1)
                else
                {
                    Comment chosenComment = commentSelection.SelectedItem as Comment;
                    if (chosenComment == null)
                    {
                        systemOutput.Clear();
                        systemOutput.AppendText("Your selection is not a comment\n");
                        return;
                    }
                    Comment newComment = new Comment(replyInput.Text, Program.activeUser.Id, chosenComment.Id, chosenComment.Indentation + 1);
                    chosenComment.commentReplies.Add(newComment.Id, newComment);
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

        private void deleteReplyButton_Click(object sender, EventArgs e)
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
                    systemOutput.AppendText("You have not selected a valid comment to reply to\n");
                    return;
                }

                Post selectedPost = postSelection.SelectedItem as Post;
                if (selectedPost == null)
                {
                    systemOutput.Clear();
                    systemOutput.AppendText("You have not selected a valid post to reply to\n");
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

                        // Refreshes post comments
                        // Displays abbreviated content if appropriate: commentSelection.DisplayMember -> Comment.AbbreviatedContent property -> ToString("ListBox") (extra step required by assignment specification)
                        commentSelection.Items.Clear();
                        foreach (Comment postComment in selectedPost.postComments.Values.OrderBy(postComment => postComment).ThenBy(postComment => postComment.TimeStamp))
                        {
                            commentSelection.Items.Add(postComment);
                            PrintChildComments(postComment);
                        }
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
        }

        private void deletePostButton_Click(object sender, EventArgs e)
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
        }
    }
}
