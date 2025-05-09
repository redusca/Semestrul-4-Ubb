using model;
using model.Enum;
using services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace client.Controller
{
    public partial class TesterWindow : UserControl
    {
        IService _service;
        Form1 _form;
        public TesterWindow(IService service, Form1 form)
        {
            InitializeComponent();
            _service = service;
            _form = form;
        }

        internal void Logout()
        {
            _form.SwitchLogin(false);
            _service.Logout(_form.getUser());
        }

        private void TesterWindow_Load(object sender, EventArgs e)
        {
            bugList.View = View.Details;
            bugList.Columns.Add("Bug Names", 300);
            bugList.FullRowSelect = true;
        }

        private void logoutButton_Click(object sender, EventArgs e)
        {
            _form.setController(ControllerType.LOGIN);

            bugTBox.Text = "";
            descriptionBox.Text = "";

            if (bugList.Items.Count > 0)
                bugList.Items.Clear();

            Logout();
        }

        private void AddBugToList_Click(object sender, EventArgs e)
        {
            try
            {
                if (descriptionBox.Text == "" || bugTBox.Text == "")
                {
                    MessageBox.Show("Please fill in all fields", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var desc = bugTBox.Text + "\n" + DateTime.Now + "\n" + descriptionBox.Text;
                Bug bug = new(desc, BugStatus.UnSolved);

                var listViewItem = new ListViewItem(bug.ToString())
                {
                    Tag = bug
                };

                foreach(ListViewItem item in bugList.Items)
                {
                    Bug existingBug = (Bug)item.Tag;
                    if (existingBug.Equals(bug))
                    {
                        MessageBox.Show("Bug already exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                bugList.Items.Add(listViewItem);
            }
            catch (Exception er)
            {
                Console.WriteLine(er.StackTrace);
                MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddBugs_Click(object sender, EventArgs e)
        {
            try
            {
                if (bugList.Items.Count == 0)
                {
                    MessageBox.Show("Please add bugs to the list", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                List<Bug> bugs = [];
                foreach (ListViewItem item in bugList.Items)
                {
                    Bug bug = (Bug)item.Tag;
                    bugs.Add(bug);
                }
                _service.AddBugs(bugs);
                bugList.Items.Clear();
                MessageBox.Show("Bugs added successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception er)
            {
                Console.WriteLine(er.StackTrace);
                MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void elimBug_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedItem = bugList.SelectedItems[0];
                if (selectedItem != null)
                {
                    bugList.Items.Remove(selectedItem);
                }
                else
                {
                    MessageBox.Show("Please select a bug to delete", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch(Exception er)
            {
                Console.WriteLine(er.StackTrace);
                MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
