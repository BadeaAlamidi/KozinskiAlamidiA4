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
    /**
     * Class Name : Form3
     * this class represents a form that is shown by the log in button in form1
     * the purpose of this class is to allow users to login as one of ther users in 
     * the globalUsers container in the Program class
     * 
     * Constructors:
     * Form3()          basic forms constructor to initialize the controls of this form
     * 
     * Control properties:
     * button 1         log-in button that can be interacted with by the click event
     * label1           label with "Username" as text
     * label2           /// "Password" // //
     * passwordField    textbox for inputting the password
     * usernameField    textbox for inputting the username
     * 
     * Events:
     * passwordField_keyDown    event that is triggered by both username and password text fields 
     *                          - its behavior is identical to button1 click only when the keystroke was "Enter"
     * button1_click            event triggered when button 1 is clicked. it is responsible for authenticating the user and 
     *                          - password fields against the users in main memory to set the activeUser variable
     * 
     * 
     */
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        /**
         * event triggered when a keystroke is detected while typing in either username or password field
         * this event is responsible for triggering the button1_click event if the keystroke was "Enter"
         */
        private void passwordField_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                button1_Click(sender, e);
        }
        /**
         * event triggered when the user clicks on the log-in button in from3
         * this event is responsible for making sure that the passwrodField.text and usernameField.text are not empty
         * -retaining the user specified in the username field if it exists (otherwise err with a message)
         * -authenticating the provided password with the password hash in the retained user, and sending an error message if the
         *      authentication had failed
         * 
         */
        private void button1_Click(object sender, EventArgs e)
        {
        
            if (usernameField.Text == "" || passwordField.Text == "") 
            {
                MessageBox.Show("Please fill both username and password fields");
                return;
            }
            var resultingUser = from currentUser in Program.globalUsers.Values where currentUser.Name == usernameField.Text select currentUser;
            if (resultingUser.Count() == 0 || !(resultingUser.First() is User user))
            {
                MessageBox.Show($"username {usernameField.Text} was not found in the database"); return;
            }
            else
            {
                if (user.PasswordHash == passwordField.Text.GetHashCode().ToString("X")) { Program.activeUser = user; Close(); }
                else { MessageBox.Show("authentication failed. please re-type your password"); }
            }
        
        }
    }
}
