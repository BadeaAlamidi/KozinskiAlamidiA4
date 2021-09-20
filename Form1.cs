﻿using System;
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
        void PrintChildComments(Comment currentComment)
        {
            foreach (Comment commentReply in currentComment.commentReplies.Values.OrderBy(comment => comment).ThenBy(postComment => postComment.TimeStamp))
            {
                commentSelection.Items.Add(commentReply);

                // Recursive call
                PrintChildComments(commentReply);
            }
        }

        // Iterates recursively through comments
        void UnprintChildComments(Comment currentComment)
        {
            foreach (Comment commentReply in currentComment.commentReplies.Values)
            {
                commentSelection.Items.Remove(commentReply);

                // Recursive call
                UnprintChildComments(commentReply);
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
            if (postSelection.SelectedItem == null)
                return;

            systemOutput.Clear();
            systemOutput.AppendText(postSelection.SelectedItem.ToString());

            commentSelection.Items.Clear();
            replyInput.Clear();

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
            if (commentSelection.SelectedItem == null)
                return;

            systemOutput.Clear();
            systemOutput.AppendText(commentSelection.SelectedItem.ToString());

            replyInput.Clear();
        }

        private void userSelection_SelectedValueChanged(object sender, EventArgs e)
        {
            // Safety check
            if (userSelection.SelectedItem == null)
                return;

            // Clears all form fields

            subredditSelection.SelectedItem = null;
            postSelection.SelectedItem = null;
            commentSelection.SelectedItem = null;

            postSelection.Items.Clear();
            commentSelection.Items.Clear();

            systemOutput.Clear();
            replyInput.Clear();
        }

        private void loginButton_MouseClick(object sender, MouseEventArgs e)
        {
            if (userSelection.SelectedIndex == -1) { systemOutput.AppendText("You haven't specified a username!\n"); return; }
            if (passwordInput.Text == "") { systemOutput.AppendText("please enter a password. Be sure to choose the your intended username\n"); return; }
            
            // authentication happens here:
            User chosenUser = userSelection.SelectedItem as User;
            if (passwordInput.Text.GetHashCode().ToString("X") == chosenUser.PasswordHash)
            {
                systemOutput.Clear();
                systemOutput.AppendText("authentication successful\n");
                Program.activeUser = chosenUser;

                subredditSelection.SelectedItem = null;
                postSelection.Items.Clear();
                commentSelection.Items.Clear();

                // Adds user's own posts and comments to respective ListBoxes
                // Sorts posts by subreddit name, then by post rating
                foreach (Post userPost in Program.globalPosts.Values.OrderBy(userPost => Program.globalSubreddits[userPost.subHomeId]).ThenBy(userPost => userPost))
                {
                    // Adds posts
                    // Displays abbreviated content if appropriate: commentSelection.DisplayMember -> Comment.AbbreviatedContent property -> ToString("ListBox") (extra step required by assignment specification)
                    if (userPost.AuthorId == Program.activeUser.Id)
                        postSelection.Items.Add(userPost);

                    // Adds comments
                    // Displays abbreviated content if appropriate: commentSelection.DisplayMember -> Comment.AbbreviatedContent property -> ToString("ListBox") (extra step required by assignment specification)
                    // Starts recursion at the post level
                    foreach (Comment userComment in userPost.postComments.Values)
                    {
                        if (userComment.AuthorID == Program.activeUser.Id)
                        {
                            uint originalLevel = userComment.Indentation;
                            userComment.Indentation = 0;
                            commentSelection.Items.Add(userComment);
                            userComment.Indentation = originalLevel;
                        }

                        // Recursive call
                        noIndentChildComments(userComment);
                    }
                }

                // Iterates recursively through comments
                void noIndentChildComments(Comment currentComment)
                {
                    foreach (Comment userComment in currentComment.commentReplies.Values)
                    {
                        if (userComment.AuthorID == Program.activeUser.Id)
                        {
                            uint originalLevel = userComment.Indentation;
                            userComment.Indentation = 0;
                            commentSelection.Items.Add(userComment);
                            userComment.Indentation = originalLevel;
                        }

                        // Recursive call
                        noIndentChildComments(userComment);
                    }
                }
            }
            else systemOutput.AppendText("authentication failed\n");
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
                systemOutput.AppendText("Choose a post to reply to a post, or choose a post + comment to reply to a comment.\n");
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
            // this will trigger if a comment is selected (the commentSelection.SelectedIndex is not -1)
            else
            {
                Comment chosenComment = commentSelection.SelectedItem as Comment;
                Comment newComment = new Comment(replyInput.Text, Program.activeUser.Id, chosenComment.Id, chosenComment.Indentation + 1);
                chosenComment.commentReplies.Add(newComment.Id, newComment);
            }
            // refreshes comments printed to comment ListBox
            commentSelection.Items.Clear();
            foreach(Comment comment in chosenPost.postComments.Values.OrderBy(postComment => postComment).ThenBy(postComment => postComment.TimeStamp))
            {
                commentSelection.Items.Add(comment);
                PrintChildComments(comment);
            }
            replyInput.Clear();
            systemOutput.Clear();
            systemOutput.AppendText("Comment successfully added\n");
        }

        private void deleteReplyButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (Program.activeUser == null)
                    throw new ArgumentNullException("Log-in is required to delete posts and comments\n");

                if (postSelection.SelectedItem == null || commentSelection.SelectedItem == null)
                    throw new ArgumentNullException("You must select a post and one of its comments first\n");

                Comment selectedComment = commentSelection.SelectedItem as Comment;
                if (selectedComment == null)
                    throw new ArgumentNullException("Casting from commentSelection.SelectedItem to a Comment was unsuccessful\n");

                Post selectedPost = postSelection.SelectedItem as Post;
                if (selectedPost == null)
                    throw new ArgumentNullException("Casting from postSelection.SelectedItem to a Post was unsuccessful\n");

                // Removes comment
                if (selectedComment.AuthorID == Program.activeUser.Id)
                {
                    // Removes comment from parent's comment collection
                    if (Program.globalPosts.ContainsKey(selectedComment.ParentID))
                        Program.globalPosts[selectedComment.ParentID].postComments.Remove(selectedComment.Id);
                    else
                    {
                        Comment parentComment = RedditUtilities.CommentReplyAdderExtension(selectedComment.ParentID);
                        parentComment.commentReplies.Remove(selectedComment.Id);

                        UnprintChildComments(selectedComment);
                    }
                }
                else
                    throw new Exception("You cannot delete other users' comments\n");

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
                    throw new ArgumentNullException("Log-in is required to delete posts and comments\n");

                if (postSelection.SelectedItem == null)
                    throw new ArgumentNullException("You must select a post first\n");

                Post selectedPost = postSelection.SelectedItem as Post;
                if (selectedPost == null)
                    throw new ArgumentNullException("Casting from postSelection.SelectedItem to a Post was unsuccessful\n");

                if (selectedPost.AuthorId == Program.activeUser.Id)
                {
                    if (!Program.globalPosts.ContainsKey(selectedPost.Id))
                        throw new ArgumentException("Post with id " + selectedPost.Id + " was not found\n");

                    // Clears comment ListBox
                    commentSelection.Items.Clear();

                    // Removes post from home subreddit
                    Program.globalSubreddits[selectedPost.subHomeId].subPostIDs.Remove(selectedPost.Id);

                    // Removes post from global post collection
                    Program.globalPosts.Remove(selectedPost.Id);
                }
                else
                    throw new Exception("You cannot delete other users' posts\n");

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
