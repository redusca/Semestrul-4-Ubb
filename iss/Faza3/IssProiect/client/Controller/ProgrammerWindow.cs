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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace client.Controller
{
    public partial class ProgrammerWindow : UserControl
    {
        IService _service;
        bool _solved = true;
        Form1 _form;
        List<ListViewItem> _listBugs = new List<ListViewItem>();
        public ProgrammerWindow(IService service, Form1 form)
        {
            InitializeComponent();
            _service = service;
            _form = form;

            bugList.View = View.Details;
            bugList.Columns.Add("Bug Names", 430);
            bugList.Columns.Add("Status", 100);
            bugList.Sorting = SortOrder.Ascending;

            bugList.FullRowSelect = true;
        }

        public void populate()
        {
            bugList.Items.Clear();
            var bugs = _service.GetBugs();

            addItemsToList(bugs);
        }

        private void addItemsToList(IEnumerable<Bug> bugs)
        {
            foreach (var bug in bugs)
            {
                var listViewItem = new ListViewItem(bug.ToString())
                {
                    Tag = bug
                };
                listViewItem.SubItems.Add(bug.Status.ToString());
                if (bug.Status == BugStatus.Solved)
                {
                    listViewItem.BackColor = Color.Green;
                }
                else if (bug.Status == BugStatus.UnSolved)
                {
                    listViewItem.BackColor = Color.LightGray;
                }
                else
                {
                    listViewItem.BackColor = Color.LightYellow;
                }
                bugList.Items.Add(listViewItem);
            }
        }

        internal void Logout()
        {
            _form.SwitchLogin(false);
            _service.Logout(_form.getUser());
        }

        private void ProgrammerWindow_Load(object sender, EventArgs e)
        {
        }

        private void logoutButton_Click(object sender, EventArgs e)
        {
            _form.setController(ControllerType.LOGIN);
            UnsolvedRadio.Checked = false;
            SolvedRadio.Checked = false;
            workingRadio.Checked = false;

            Logout();
        }

        private void solvedCheck_Click(object sender, EventArgs e)
        {
            try
            {
                if (_solved)
                {
                    foreach (ListViewItem item in bugList.Items)
                    {
                        var b = (Bug)item.Tag;
                        if (b.Status == BugStatus.Solved)
                        {
                            _listBugs.Add(item);
                        }
                    }
                    foreach (var item in _listBugs)
                    {
                        bugList.Items.Remove(item);
                    }
                    solvedCheck.Text = "Show Solved Bugs";
                }
                else
                {
                    bugList.Items.AddRange(_listBugs.ToArray());
                    _listBugs.Clear();
                    solvedCheck.Text = "Hide Solved Bugs";
                }
                _solved = !_solved;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void changeStatusBugButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (bugList.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Please select a bug", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                Bug bug = (Bug)bugList.SelectedItems[0].Tag;

                var status = getStatus();

                if (status == bug.Status)
                {
                    MessageBox.Show("Bug already has this status", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                bug.Status = status;
                _service.ChangeBugStatus(bug);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private BugStatus getStatus()
        {
            if (SolvedRadio.Checked)
            {
                return BugStatus.Solved;
            }
            else if (UnsolvedRadio.Checked)
            {
                return BugStatus.UnSolved;
            }
            else if (workingRadio.Checked)
            {
                return BugStatus.WorkingOn;
            }
            else
            {
                throw new Exception("Please select a status");
            }
        }

        public void bugStatusChanged(Bug bug)
        {
            Console.WriteLine("Bug status changed");
            foreach (ListViewItem item in bugList.Items)
            {
                var b = (Bug)item.Tag;
                if (b.BugNo == bug.BugNo)
                {
                    item.SubItems[1].Text = bug.Status.ToString();
                    if (bug.Status == BugStatus.Solved)
                    {
                        item.BackColor = Color.Green;
                    }
                    else if (bug.Status == BugStatus.UnSolved)
                    {
                        item.BackColor = Color.LightGray;
                    }
                    else
                    {
                        item.BackColor = Color.LightYellow;
                    }
                    bugList.Refresh();
                    return;
                }
            }
        }

        public void newBugs(IEnumerable<Bug> bugs)
        {
            Console.WriteLine("New bugs added");
            addItemsToList(bugs);
        }

        private void bugList_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            bugList.ItemSelectionChanged -= bugList_ItemSelectionChanged;

            if (e.IsSelected)
            {
                var bug = (Bug)e.Item.Tag;
                if (bug.Status == BugStatus.Solved)
                {
                    SolvedRadio.Checked = true;
                }
                else if (bug.Status == BugStatus.UnSolved)
                {
                    UnsolvedRadio.Checked = true;
                }
                else
                {
                    workingRadio.Checked = true;
                }
            }

            bugList.ItemSelectionChanged += bugList_ItemSelectionChanged;
        }

        private void descriptionButton_Click(object sender, EventArgs e)
        {
            if (bugList.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a bug", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Bug bug = (Bug)bugList.SelectedItems[0].Tag;
            var description = new helpWindows.Description(bug);
            description.ShowDialog();
        }
    }
}
