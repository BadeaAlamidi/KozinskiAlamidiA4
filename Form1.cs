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

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try { RedditUtilities.ReadFiles(); } catch (ArgumentException exception){ systemOutput.AppendText($"the error message is: {exception.Message}");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
