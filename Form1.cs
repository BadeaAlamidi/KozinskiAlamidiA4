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
            try { RedditUtilities.ReadFiles(); } catch (ArgumentException exception){ systemOutput.AppendText(exception.Message);}
            foreach (KeyValuePair<uint, User> user in Program.globalUsers.OrderBy(user => user.Value.Name)) { userSelection.Items.Add(user.Value.Name); }
            foreach (KeyValuePair<uint,Subreddit>subreddit in Program.globalSubreddits.OrderBy(subreddit => subreddit.Value.Name)) { subredditSelection.Items.Add(subreddit.Value.Name); }

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
        // this triggers when the user chooses a subreddit
        private void subredditSelection_SelectedValueChanged(object sender, EventArgs e)
        {
            postSelection.Items.Clear();
            ListBox selectedSubreddit = sender as ListBox;
            foreach (KeyValuePair<uint, Subreddit> subredditTuple in Program.globalSubreddits) {
                if (subredditTuple.Value.Name == selectedSubreddit.SelectedItem.ToString()) {
                    foreach (KeyValuePair<uint, Post> postTuple in Program.globalPosts)
                    {//.Where(globalPostTuple => subredditTuple.Value.subPostIDs.Contains(globalPostTuple.Key)).OrderBy(globalPostTuple => globalPostTuple.Value.Score)) 
                        postSelection.Items.Add(">");
                    }
                    //foreach (uint postId in subredditTuple.Value.subPostIDs)
                      //  foreach (KeyValuePair<uint, Post> postTuple in Program.globalPosts.Where(postTuple => postTuple.Key == postId).OrderBy(postTuple => postTuple.Value.Score))
                    //there can only be one match for this check, which warrants ending this loop once a single match was found
                    break;
                }
            }
            //foreach (uint id in subredditSelection.)
        }
    }
}
