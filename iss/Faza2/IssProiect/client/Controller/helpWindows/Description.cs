using model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace client.Controller.helpWindows
{
    public partial class Description : Form
    {
        public Description(Bug bug)
        {
            InitializeComponent();

            Title.Text = bug.Description.Split('\n')[0] + " " + bug.BugNo;
            Time.Text = bug.Description.Split('\n')[1];

            foreach (var line in bug.Description.Split('\n').Skip(2))
            {
                richTextBox1.AppendText(line + "\n");
            }
        }

        private void Description_Load(object sender, EventArgs e)
        {

        }
    }
}
