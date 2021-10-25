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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (usernameField.Text == "" || passwordField.Text == ""){ MessageBox.Show("Please fill both username and password fields"); return; }
            var resultingUser = from currentUser in Program.globalUsers.Values where currentUser.Id.ToString() == usernameField.Text select currentUser;
            if (!(resultingUser.First() is User user))
            {
                MessageBox.Show($"username {usernameField.Text} was not found in the database"); return;
            }
            else
            {
                if (user.PasswordHash == passwordField.Text.GetHashCode().ToString("X")) { Program.activeUser = user;  Close(); }
                else { MessageBox.Show("authentication failed. please re-type your password"); }
            }
        }

    }
}
