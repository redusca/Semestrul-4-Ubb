using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Lab1SGBD
{
    public partial class Form1 : Form
    {
        // Connection string
        string cs = ConfigurationManager.ConnectionStrings["dataBaseCS"].ConnectionString;
        // Data adapters for parent and child tables
        SqlDataAdapter parentAdapter;
        SqlDataAdapter childAdapter;

        // For other foreign keys on addition
        List<SqlDataAdapter> foreginKeyAdapters = new List<SqlDataAdapter>();

        DataSet ds = new DataSet();

        BindingSource parentBs = new BindingSource();
        BindingSource childBs = new BindingSource();

        DataColumn parentPk;
        DataColumn childPk;

        DataRelation relation;

        #region XMLVariables
        // Parent table variables
        String parentName;
        String selectParent;
        String parentId;
        String childRef;

        // Child table variables
        String childName;
        String selectChild;
        String childId;

        // Child Update command variables
        String updateCmd;
        String[] updateNames;
        String[] updateValues;
        String[] updateTypes;

        // Child Delete command variables
        String deleteCmd;
        String[] deleteNames;
        String[] deleteValues;
        String[] deleteTypes;

        // Child Insert command variables
        String insertCmd;
        String[] insertNames;
        String[] insertValues;
        String[] insertTypes;

        // Other tables variables
        String[] otherTablesNames;
        String[] selects;
        String[] ids;
        String[] idsInChild;
        String[] names;

        // Constants for UI layout
        const int maxNoFields = 9;
        const int distanceV = 60;
        const int distanceH = 160;
        const int sizeLabel = 150;
        const int heightLabel = 20;

        int x, y;

        // fields for UI
        List<Control> updateFields;
        List<ComboBox> extraTabels;
        #endregion

        public Form1()
        {
            InitializeComponent();
        }

        private void createLabels()
        {
            var addWitdh = distanceH * ((updateNames.Length - 1 + otherTablesNames.Length) / maxNoFields + ((updateNames.Length - 1 + otherTablesNames.Length) % maxNoFields != 0 ? 1 : 0));
            this.Width += addWitdh;
            Panel panel = new Panel();
            panel.Location = new System.Drawing.Point(ancor.Location.X, ancor.Location.Y);
            panel.Size = new System.Drawing.Size(addWitdh, maxNoFields * distanceH);

            this.Controls.Add(panel);

            x = 0;
            y = 0;

            updateFields = new List<Control>();
            extraTabels = new List<ComboBox>();
            int noFields = 0;
            //fields creation
            for (int i = 0; i < updateNames.Length - 1; i++)
            {
                noFields++;

                //Label  creation
                Label label = new Label();
                label.Text = updateNames[i].Replace('_', ' ');
                label.Location = new System.Drawing.Point(x, y);
                label.AutoSize = true;
                label.Size = new System.Drawing.Size(sizeLabel, heightLabel);
                label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                panel.Controls.Add(label);

                //Field creation
                SqlDbType type = (SqlDbType)Enum.Parse(typeof(SqlDbType), updateTypes[i]);
                Control control;
                switch (type)
                {
                    case SqlDbType.Int:
                    case SqlDbType.Money:
                        var numeric = new NumericUpDown();
                        numeric.Maximum = new decimal(new int[]
                        {
                             -727379968,
                                232,
                                0,
                                0
                        });
                        control = numeric;
                        break;
                    case SqlDbType.NVarChar:
                    case SqlDbType.VarChar:
                        control = new TextBox();
                        break;
                    case SqlDbType.Date:
                        var datapick = new DateTimePicker();
                        datapick.Format = DateTimePickerFormat.Custom;
                        datapick.CustomFormat = "dd/MM/yyyy";
                        datapick.MaxDate = DateTime.Now;
                        datapick.MinDate = new DateTime(1900, 1, 1);
                        control = datapick;
                        break;
                    default:
                        control = new Control();
                        MessageBox.Show("Tip de date necunoscut: " + type.ToString());
                        break;
                }
                control.Name = updateNames[i];
                control.Location = new System.Drawing.Point(x, y + heightLabel + 3);
                control.Size = new System.Drawing.Size(sizeLabel, heightLabel);

                updateFields.Add(control);
                panel.Controls.Add(control);

                if (noFields == maxNoFields)
                {
                    noFields = 0;
                    y = 0;
                    x += distanceH;
                }
                else
                {
                    y += distanceV;
                }
            }

            //comboBox creation for other tables
            for (int i = 0; i < otherTablesNames.Length; i++)
            {
                noFields++;
                //Label  creation
                Label label = new Label();
                label.Text = otherTablesNames[i];
                label.Location = new System.Drawing.Point(x, y);
                label.AutoSize = true;
                label.Size = new System.Drawing.Size(sizeLabel, heightLabel);
                label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                panel.Controls.Add(label);

                var comboBox = new ComboBox();
                comboBox.Name = idsInChild[i];
                comboBox.Location = new System.Drawing.Point(x, y + heightLabel + 3);
                comboBox.Size = new System.Drawing.Size(sizeLabel, heightLabel);
                panel.Controls.Add(comboBox);
                extraTabels.Add(comboBox);

                if (noFields == maxNoFields)
                {
                    noFields = 0;
                    y = 0;
                    x += distanceH;
                }
                else
                {
                    y += distanceV;
                }
            }

            this.Width += 20;
        }

        private void getXMLconfiguration()
        {
            try
            {
                //Parent
                parentName = ConfigurationManager.AppSettings["ParentTableName"];
                selectParent = ConfigurationManager.AppSettings["SelectParent"];
                parentId = ConfigurationManager.AppSettings["ParentId"];
                childRef = ConfigurationManager.AppSettings["ChildRef"];
                //Child
                childName = ConfigurationManager.AppSettings["ChildTableName"];
                selectChild = ConfigurationManager.AppSettings["SelectChild"];
                childId = ConfigurationManager.AppSettings["ChildId"];
                //Update child
                updateCmd = ConfigurationManager.AppSettings["Update"];
                updateNames = Regex.Split(ConfigurationManager.AppSettings["UpdateNames"], @"\|=\|");
                updateNames = Regex.Split(ConfigurationManager.AppSettings["UpdateNames"], @"\|=\|");
                updateValues = Regex.Split(ConfigurationManager.AppSettings["UpdateParams"], @"\|=\|");
                updateTypes = Regex.Split(ConfigurationManager.AppSettings["UpdateTypes"], @"\|=\|");
                //Delete child
                deleteCmd = ConfigurationManager.AppSettings["Delete"];
                deleteNames = Regex.Split(ConfigurationManager.AppSettings["DeleteNames"], @"\|=\|");
                deleteValues = Regex.Split(ConfigurationManager.AppSettings["DeleteParams"], @"\|=\|");
                deleteTypes = Regex.Split(ConfigurationManager.AppSettings["DeleteTypes"], @"\|=\|");
                //Insert child
                insertCmd = ConfigurationManager.AppSettings["Insert"];
                insertNames = Regex.Split(ConfigurationManager.AppSettings["InsertNames"], @"\|=\|");
                insertValues = Regex.Split(ConfigurationManager.AppSettings["InsertParams"], @"\|=\|");
                insertTypes = Regex.Split(ConfigurationManager.AppSettings["InsertTypes"], @"\|=\|");
                //Other tables
                otherTablesNames = Regex.Split(ConfigurationManager.AppSettings["OtherTables"], @"\|=\|");
                if(otherTablesNames[0] == "")
                {
                    otherTablesNames = new string[0];
                    ids = new string[0];
                    idsInChild = new string[0];
                    names = new string[0];
                    selects = new string[0];

                    return;
                }
                ids = Regex.Split(ConfigurationManager.AppSettings["ids"], @"\|=\|");
                idsInChild = Regex.Split(ConfigurationManager.AppSettings["idsInChild"], @"\|=\|");
                names = Regex.Split(ConfigurationManager.AppSettings["names"], @"\|=\|");
                selects = Regex.Split(ConfigurationManager.AppSettings["selects"], @"\|=\|");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la citirea fisierului XML! \n" + ex.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            getXMLconfiguration();
            createLabels();
            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    //Adapter
                    parentAdapter = new SqlDataAdapter(selectParent, con);
                    childAdapter = new SqlDataAdapter(selectChild, con);

                    parentAdapter.Fill(ds, parentName);
                    childAdapter.Fill(ds, childName);

                    parentBs.DataSource = ds.Tables[parentName];
                    parentView.DataSource = parentBs;

                    parentPk = ds.Tables[parentName].Columns[parentId];
                    childPk = ds.Tables[childName].Columns[childRef];
                    // Child commands
                    childAdapter.UpdateCommand = new SqlCommand(updateCmd, con);
                    for (int i = 0; i < updateNames.Length; i++)
                        childAdapter.UpdateCommand.Parameters.Add(updateValues[i],
                            (SqlDbType)Enum.Parse(typeof(SqlDbType), updateTypes[i]),
                            -1,
                            updateNames[i]);

                    childAdapter.DeleteCommand = new SqlCommand(deleteCmd, con);
                    for (int i = 0; i < deleteNames.Length; i++)
                        childAdapter.DeleteCommand.Parameters.Add(deleteValues[i],
                            (SqlDbType)Enum.Parse(typeof(SqlDbType), deleteTypes[i]),
                            -1,
                            deleteNames[i]);

                    childAdapter.InsertCommand = new SqlCommand(insertCmd, con);
                    for (int i = 0; i < insertNames.Length; i++)
                        childAdapter.InsertCommand.Parameters.Add(insertValues[i],
                            (SqlDbType)Enum.Parse(typeof(SqlDbType), insertTypes[i]),
                            -1,
                            insertNames[i]);

                    childAdapter.SelectCommand = new SqlCommand(@"Select IDENT_CURRENT(@table)", con);
                    childAdapter.SelectCommand.Parameters.Add("@table", SqlDbType.NVarChar, -1, childName);

                    relation = new DataRelation("fk_parent_child", parentPk, childPk);


                    for (int i = 0; i < otherTablesNames.Length; i++)
                    {
                        foreginKeyAdapters.Add(new SqlDataAdapter(selects[i], con));
                        foreginKeyAdapters[i].Fill(ds, otherTablesNames[i]);
                        extraTabels[i].DataSource = ds.Tables[otherTablesNames[i]];
                        extraTabels[i].DisplayMember = names[i];
                    }

                    ds.Relations.Add(relation);
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("2" + ex.Message);
            }

        }

        private void LineSelect(object sender, DataGridViewCellEventArgs e)
        {
            SaveButton.Enabled = true;
            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    if (e.RowIndex >= 0)
                    {
                        childBs.DataSource = parentBs;
                        childBs.DataMember = "fk_parent_child";
                        childView.DataSource = childBs;


                        update.Enabled = false;
                        delete.Enabled = false;

                        foreach (Control control in updateFields)
                        {
                            control.Text = "";
                        }

                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("1" + ex.Message);
            }
        }

        private void LineSelectInChild(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    if (e.RowIndex >= 0)
                    {
                        foreach (Control control in updateFields)
                        {
                            control.Text = childView.Rows[e.RowIndex].Cells[control.Name].Value.ToString();
                        }

                        update.Enabled = true;
                        delete.Enabled = true;
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Conexiunea la baza de date a esuat! \n" + ex.Message);
            }
        }

        private void update_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    foreach (Control control in updateFields)
                    {
                        if (control.Text == "")
                        {
                            MessageBox.Show("Toate campurile sunt obligatorii!");
                            return;
                        }
                    }

                    DataRow[] rows = ds.Tables[childName].Columns[childId]
                        .Table.Select(childId + "= " + childView.SelectedRows[0].Cells[childId].Value.ToString());

                    foreach (Control control in updateFields)
                    {
                        rows[0][control.Name] = control.Text;
                    }

                    childAdapter.UpdateCommand.Connection = con;

                    childAdapter.Update(ds, childName);

                    MessageBox.Show("Datele au fost actualizate cu succes!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("3" + ex.Message);
            }
        }

        private void delete_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();

                    DataRow[] rows = ds.Tables[childName].Columns[childId]
                        .Table.Select(childId + "= " + childView.SelectedRows[0].Cells[childId].Value.ToString());
                    rows[0].Delete();

                    childAdapter.DeleteCommand.Connection = con;
                    childAdapter.Update(ds, childName);

                    MessageBox.Show("S-a fost șters cu succes!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Conexiunea la baza de date a esuat! \n" + ex.Message);
            }
        }

        private void insert_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();

                    childAdapter.SelectCommand.Connection = con;
                    childAdapter.SelectCommand.Parameters["@table"].Value = childName;
                    int id = childAdapter.SelectCommand.ExecuteScalar() != null ? Convert.ToInt32(childAdapter.SelectCommand.ExecuteScalar()) + 1 : 1;

                    DataRow newRow = ds.Tables[childName].NewRow();

                    newRow[childId] = id;

                    foreach (Control control in updateFields)
                    {
                        if (control.Text == "")
                        {
                            MessageBox.Show("Toate campurile sunt obligatorii!");
                            return;
                        }
                    }

                    foreach (Control control in updateFields)
                    {
                        newRow[control.Name] = control.Text;
                    }

                    newRow[childRef] = ((DataRowView)parentBs.Current)[parentId];

                    int i = 0;
                    foreach (ComboBox combo in extraTabels)
                    {
                        newRow[combo.Name] = ((DataRowView)combo.SelectedItem)[ids[i++]];
                    }

                    ds.Tables[childName].Rows.Add(newRow);

                    childAdapter.InsertCommand.Connection = con;
                    childAdapter.Update(ds, childName);


                    MessageBox.Show("Succes adaugare!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("4" + ex.Message);
            }
        }
    }
}
