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
            // Runs file reader and stores error log
            List<string> fileReadErrors = RedditUtilities.ReadFiles();

            // Prints error log to system output
            foreach (string line in fileReadErrors)
                systemOutput.AppendText(line + "\n");

            // Populates user, subreddit, and post boxes
            foreach (User user in Program.globalUsers.Values) { userSelection.Items.Add(user.Name); }
            foreach (Subreddit subreddit in Program.globalSubreddits.Values) { subredditSelection.Items.Add(subreddit.Name); }

            // Need to abbreviate post length
            foreach (Post post in Program.globalPosts.Values) { postSelection.Items.Add(post.Title); }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
        // this triggers when the user chooses a subreddit
        private void subredditSelection_SelectedValueChanged(object sender, EventArgs e)
        {
            postSelection.Items.Clear();
            //foreach (uint id in subredditSelection.)
        }
    }
}
