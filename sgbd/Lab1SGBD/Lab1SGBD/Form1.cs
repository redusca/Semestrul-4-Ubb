using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab1SGBD
{
    public partial class Form1 : Form
    {
        private string cs = "Data Source=DESKTOP-0PG9QNP\\SQLEXPRESS;Initial Catalog=StatisticiJocuriVideo;Integrated Security=True";
        SqlDataAdapter gamePublisherAdapter;
        SqlDataAdapter jocuriVideoAdapater;
        SqlDataAdapter gameDeveloper;
        SqlDataAdapter platforma;

        DataSet ds = new DataSet();

        BindingSource parentBs = new BindingSource();
        BindingSource childBs = new BindingSource();

        DataColumn parentPk;
        DataColumn childPk;

        DataRelation relation;

        public Form1()
        {
            InitializeComponent();
            
        }

        private void GamePublishers_CellContentClick(object sender, DataGridViewCellEventArgs e)
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
                        GameView.DataSource = childBs;

                        update.Enabled = false;
                        delete.Enabled = false;
                        Nume.Text = "";
                        gen.Text = "";
                        launchDate.Text = "";
                        nrCopii.Text = "";
                        playerAnuali.Text = "";
                        price.Text = "";

                    }
                    con.Close();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Conexiunea la baza de date a esuat! \n" + ex.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GamePublishers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            GameView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            GamePublishers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            GameView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            GamePublishers.AllowUserToAddRows = false;
            GameView.AllowUserToAddRows = false;

            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();

                    gamePublisherAdapter = new SqlDataAdapter("SELECT * FROM GamePublishers", con);
                    jocuriVideoAdapater = new SqlDataAdapter("SELECT * FROM JocuriVideo", con);
                    gameDeveloper = new SqlDataAdapter("SELECT id,Nume FROM GameDeveloperi", con);
                    platforma = new SqlDataAdapter("SELECT id, Nume_Platforma FROM Platforma", con);

                    gamePublisherAdapter.Fill(ds, "GamePublishers");
                    jocuriVideoAdapater.Fill(ds, "JocuriVideo");
                    gameDeveloper.Fill(ds, "GameDevelopers");
                    platforma.Fill(ds, "Platforma");

                    parentBs.DataSource = ds.Tables["GamePublishers"];
                    GamePublishers.DataSource = parentBs;

                    parentPk = ds.Tables["GamePublishers"].Columns["id"];
                    childPk = ds.Tables["JocuriVideo"].Columns["id_p"];

                    #region UpdateCommand
                    jocuriVideoAdapater.UpdateCommand = new SqlCommand("UPDATE JocuriVideo " +
                     "SET Nume = @Nume, " +
                     "Gen = @Gen, " +
                     "Data_Lansare = @Data_Lansare, " +
                     "Numar_de_copi = @Numar_de_copi, " +
                     "Playeri_anuali = @Playeri_anuali, " +
                     "Price = @Price " +
                     "WHERE id = @id", con);
                    jocuriVideoAdapater.UpdateCommand.Parameters.Add("@Nume",SqlDbType.NVarChar, 50, "Nume");
                    jocuriVideoAdapater.UpdateCommand.Parameters.Add("@Gen",SqlDbType.NVarChar, 60, "Gen");
                    jocuriVideoAdapater.UpdateCommand.Parameters.Add("@Data_Lansare", SqlDbType.Date, 0, "Data_Lansare");
                    jocuriVideoAdapater.UpdateCommand.Parameters.Add("@Numar_de_copi", SqlDbType.Int, 0, "Numar_de_copi");
                    jocuriVideoAdapater.UpdateCommand.Parameters.Add("@Playeri_anuali", SqlDbType.Int, 0, "Playeri_anuali");
                    jocuriVideoAdapater.UpdateCommand.Parameters.Add("@Price", SqlDbType.Money, 0, "Price");
                    jocuriVideoAdapater.UpdateCommand.Parameters.Add("@id", SqlDbType.Int, 0, "id");
                    #endregion

                    jocuriVideoAdapater.DeleteCommand = new SqlCommand("Delete from JocuriVideo where id = @id",con);
                    jocuriVideoAdapater.DeleteCommand.Parameters.Add("@id", SqlDbType.Int, 0, "id");

                    #region InsertCommand
                    jocuriVideoAdapater.InsertCommand = new SqlCommand
                    {
                        Connection = con,
                        CommandText = "INSERT INTO JocuriVideo(Nume, Gen, Data_Lansare, Numar_de_copi, Playeri_anuali, Price, id_p, id_d, Main_Platform_id) " +
                        "values (@Nume, @Gen, @Data_Lansare, @Numar_de_copi, @Playeri_anuali, @Price, @id_p, @id_d,@Main_Platform_id)"
                    };
                    jocuriVideoAdapater.InsertCommand.Parameters.Add("@Nume", SqlDbType.NVarChar, 50, "Nume");
                    jocuriVideoAdapater.InsertCommand.Parameters.Add("@Gen", SqlDbType.NVarChar, 60, "Gen");
                    jocuriVideoAdapater.InsertCommand.Parameters.Add("@Data_Lansare", SqlDbType.Date, 0, "Data_Lansare");
                    jocuriVideoAdapater.InsertCommand.Parameters.Add("@Numar_de_copi", SqlDbType.Int, 0, "Numar_de_copi");
                    jocuriVideoAdapater.InsertCommand.Parameters.Add("@Playeri_anuali", SqlDbType.Int, 0, "Playeri_anuali");
                    jocuriVideoAdapater.InsertCommand.Parameters.Add("@Price", SqlDbType.Money, 0, "Price");
                    jocuriVideoAdapater.InsertCommand.Parameters.Add("@id_p", SqlDbType.Int, 0, "id_p");
                    jocuriVideoAdapater.InsertCommand.Parameters.Add("@id_d", SqlDbType.Int, 0, "id_d");
                    jocuriVideoAdapater.InsertCommand.Parameters.Add("@Main_Platform_id", SqlDbType.Int, 0, "Main_Platform_id");
                    #endregion

                    relation = new DataRelation("fk_parent_child", parentPk, childPk);

                    devTeam.DataSource = ds.Tables["GameDevelopers"];
                    devTeam.DisplayMember = "Nume";

                    platformaBox.DataSource = ds.Tables["Platforma"];
                    platformaBox.DisplayMember = "Nume_Platforma";

                    ds.Relations.Add(relation);
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Conexiunea la baza de date a esuat! \n" + ex.Message);
            }

        }

        private void GameView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    if (e.RowIndex >= 0)
                    {
                        Nume.Text = GameView.Rows[e.RowIndex].Cells["Nume"].Value.ToString();
                        gen.Text = GameView.Rows[e.RowIndex].Cells["Gen"].Value.ToString();
                        launchDate.Text = GameView.Rows[e.RowIndex].Cells["Data_Lansare"].Value.ToString();
                        nrCopii.Text = GameView.Rows[e.RowIndex].Cells["Numar_de_copi"].Value.ToString();
                        playerAnuali.Text = GameView.Rows[e.RowIndex].Cells["Playeri_anuali"].Value.ToString();
                        price.Text = GameView.Rows[e.RowIndex].Cells["Price"].Value.ToString();

                        update.Enabled = true;
                        delete.Enabled = true;
                    }
                    con.Close();
                }
            }
            catch(Exception ex)
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

                    DataRow[] rows = ds.Tables["JocuriVideo"].Columns["id"]
                        .Table.Select("id = " + GameView.SelectedRows[0].Cells["id"].Value.ToString());
                    rows[0]["Nume"] = Nume.Text;
                    rows[0]["Gen"] = gen.Text;
                    rows[0]["Data_Lansare"] = launchDate.Text;
                    rows[0]["Numar_de_copi"] = nrCopii.Text;
                    rows[0]["Playeri_anuali"] = playerAnuali.Text;

                    jocuriVideoAdapater.UpdateCommand.Connection = con;

                    jocuriVideoAdapater.Update(ds, "JocuriVideo");

                    MessageBox.Show("Datele au fost actualizate cu succes!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Conexiunea la baza de date a esuat! \n" + ex.Message);
            }
        }

        private void delete_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();

                    DataRow[] rows = ds.Tables["JocuriVideo"].Columns["id"]
                        .Table.Select("id = " + GameView.SelectedRows[0].Cells["id"].Value.ToString());
                    rows[0].Delete();

                    jocuriVideoAdapater.DeleteCommand.Connection = con;
                    jocuriVideoAdapater.Update(ds, "JocuriVideo");

                    MessageBox.Show("Jocul a fost șters cu succes!");

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Conexiunea la baza de date a esuat! \n" + ex.Message);
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();

                    DataRow newRow = ds.Tables["JocuriVideo"].NewRow();
                    newRow["Nume"] = Nume.Text;
                    newRow["Gen"] = gen.Text;
                    newRow["Data_Lansare"] = launchDate.Text;
                    newRow["Numar_de_copi"] = nrCopii.Text;
                    newRow["Playeri_anuali"] = playerAnuali.Text;
                    newRow["Price"] = price.Text;
                    newRow["id_p"] = ((DataRowView)parentBs.Current)["id"];
                    newRow["id_d"] = ((DataRowView)devTeam.SelectedItem)["id"];
                    newRow["Main_Platform_id"] = ((DataRowView)platformaBox.SelectedItem)["id"];

                    ds.Tables["JocuriVideo"].Rows.Add(newRow);

                    jocuriVideoAdapater.InsertCommand.Connection = con;
                    jocuriVideoAdapater.Update(ds, "JocuriVideo");

                    MessageBox.Show("Jocul a fost adăugat cu succes!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Conexiunea la baza de date a esuat! \n" + ex.Message);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
