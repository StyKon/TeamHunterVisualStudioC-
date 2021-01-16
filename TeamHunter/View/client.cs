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

namespace TeamHunter
{
    public partial class client : Form
    {
        public client()
        {
            InitializeComponent();
        }
      
        private void client_Load(object sender, EventArgs e)
        {

            refresh();
        }

        [Obsolete]
        private void button1_Click(object sender, EventArgs e)
        {
            if (!CheckValiditée())
            {
                using (SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-96FHGM6;Initial Catalog=TeamHunter;User ID=sa;Password=Khalil17;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
            {
                try
                {

                    using (var cmd = new SqlCommand("INSERT INTO Clients(Nom,Prenom,DateNaiss,DateEnt,Tel,IdCat,State)values (@Nom,@Prenom,@DateNaiss,@DateEnt,@Tel,@IdCat,'False')"))
                    {

                        cmd.Connection = con;
                        cmd.Parameters.Add("@Nom", textBox1.Text);
                        cmd.Parameters.Add("@Prenom", textBox2.Text);
                        cmd.Parameters.Add("@DateNaiss", dateTimePicker2.Text);
                        cmd.Parameters.Add("@DateEnt", dateTimePicker1.Text);
                        cmd.Parameters.Add("@Tel", textBox4.Text);
                        cmd.Parameters.Add("@IdCat", comboBox1.SelectedValue);


                        con.Open();
                        if (cmd.ExecuteNonQuery() > 0)
                        {
                            MessageBox.Show("Client inserted");
                            dataGridView1.Update();
                            dataGridView1.Refresh();
                        }
                        else
                        {
                            MessageBox.Show("failed");
                        }
                    }
                }

                catch (Exception c)
                {
                    MessageBox.Show("Error during insert: " + c.Message);
                }
                refresh();
               
            }

            }
            else { MessageBox.Show("verifier que tout les champ sont bient saisie Nb: ne saisie pas le combobox manuallement ,il faut bien saisie la date de naissance"); }





        }

        private void fillByToolStripButton_Click(object sender, EventArgs e)
        {
         
        }
       //refresh datagridview
        public void refresh()
        {
            dataGridView1.DataSource = null;
            using (SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-96FHGM6;Initial Catalog=TeamHunter;User ID=sa;Password=Khalil17;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT IdCl,Nom,Prenom,DateNaiss,DateEnt,Tel,NomCat FROM Clients LEFT JOIN Category ON Category.IdCat = Clients.IdCat", con))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            dataGridView1.DataSource = dt;
                        }
                    }
                }
            }
            refreshcb();
            textBox1.Text = "";
            textBox2.Text = "";
            textBox4.Text = "";
            dateTimePicker2.Text = "";
            dateTimePicker1.Text = "";
        }
        public void refreshcb()
        {
            using (SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-96FHGM6;Initial Catalog=TeamHunter;User ID=sa;Password=Khalil17;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Category", con))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        using (DataSet ds = new DataSet())
                        {
                            sda.Fill(ds, "Category");
                            comboBox1.DisplayMember = "NomCat";
                            comboBox1.ValueMember = "IdCat";
                            comboBox1.DataSource = ds.Tables["Category"];
                        }
                    }
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
            try
            {
                if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    textBox1.Text = dataGridView1.SelectedCells[1].Value.ToString();
                    textBox2.Text = dataGridView1.SelectedCells[2].Value.ToString();
                    dateTimePicker2.Text = dataGridView1.SelectedCells[3].Value.ToString();
                    dateTimePicker1.Text = dataGridView1.SelectedCells[4].Value.ToString();
                    textBox4.Text = dataGridView1.SelectedCells[5].Value.ToString();
                    comboBox1.Text = dataGridView1.SelectedCells[6].Value.ToString();
                }
            }
            catch (ArgumentOutOfRangeException)
            {

                MessageBox.Show("Please Select valid row");
            }
        }
       

        private void button3_Click(object sender, EventArgs e)
        {
            if (!CheckValiditée())
            {
                using (SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-96FHGM6;Initial Catalog=TeamHunter;User ID=sa;Password=Khalil17;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
            {
                try
                {

                    using (var cmd = new SqlCommand("UPDATE  Clients SET Nom=@Nom,Prenom=@Prenom,DateNaiss=@DateNaiss,DateEnt=@DateEnt,Tel=@Tel,IdCat=@IdCat WHERE IdCl=@IdCl"))
                    {

                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@Nom",textBox1.Text);
                        cmd.Parameters.AddWithValue("@Prenom", textBox2.Text);
                        cmd.Parameters.AddWithValue("@DateNaiss",dateTimePicker2.Text);
                        cmd.Parameters.AddWithValue("@DateEnt",dateTimePicker1.Text);
                        cmd.Parameters.AddWithValue("@Tel",textBox4.Text);
                        cmd.Parameters.AddWithValue("@IdCat", comboBox1.SelectedValue);
                        cmd.Parameters.AddWithValue("@IdCl",dataGridView1.SelectedCells[0].Value);


                        con.Open();
                        if (cmd.ExecuteNonQuery() > 0)
                        {
                            MessageBox.Show("Client Updated");
                            dataGridView1.Update();
                            dataGridView1.Refresh();
                        }
                        else
                        {
                            MessageBox.Show("failed");
                        }
                    }
                }

                catch (Exception c)
                {
                    MessageBox.Show("Error during insert: " + c.Message);
                }
                refresh();

            }
            }
            else { MessageBox.Show("verifier que tout les champ sont bient saisie Nb: ne saisie pas le combobox manuallement ,il faut bien saisie la date de naissance"); }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Voulez-vous supprime ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
            {
                using (SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-96FHGM6;Initial Catalog=TeamHunter;User ID=sa;Password=Khalil17;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
                {
                    try
                    {

                        using (var cmd = new SqlCommand("Delete from Clients where IdCl=" + dataGridView1.SelectedCells[0].Value.ToString()))
                        {

                            cmd.Connection = con;


                            con.Open();
                            if (cmd.ExecuteNonQuery() > 0)
                            {
                                MessageBox.Show("Client Deleted");
                                dataGridView1.Update();
                                dataGridView1.Refresh();
                            }
                            else
                            {
                                MessageBox.Show("failed");
                            }
                        }
                    }

                    catch (Exception c)
                    {
                        MessageBox.Show("Error during delete: " + c.Message);
                    }
                    refresh();
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            refresh();
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '+'))
            {
                e.Handled = true;
            }


            if ((e.KeyChar == '+') && ((sender as TextBox).Text.IndexOf('+') > -1))
            {
                e.Handled = true;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (comboBox2.Text == "Categorie")
            {
               
                    RechercheParCat();
                
            }
            else if (comboBox2.Text == "Nom")
            {
                RechercheParNom();
            }
            else if (comboBox2.Text == "Prenom")
            {
                RechercheParPrenom();
            }
            else if (comboBox2.Text == "N° Telephone")
            {
                RechercheParTelephone();
            }
            else if (comboBox2.Text == "Date Naissance")
            {
                DateTime temp;

                if (DateTime.TryParse(textBox3.Text, out temp))
                {
                    RechercheParDateNaiss();
                }
                else { MessageBox.Show("Check Date sous Forme mm/jj/yyyy"); }
            }
            else if (comboBox2.Text == "Date Entree")
            {
                DateTime temp;

                if (DateTime.TryParse(textBox3.Text, out temp))
                {
                    RechercheParDateEnt();
                }
                else { MessageBox.Show("Check Date sous Forme mm/jj/yyyy"); }
            }
            else
            {
                MessageBox.Show("CHOISIR TYPE De RECHERCHE");
            }
        }
        public void RechercheParNom()
        {
            dataGridView1.DataSource = null;
            using (SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-96FHGM6;Initial Catalog=TeamHunter;User ID=sa;Password=Khalil17;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT IdCl,Nom,Prenom,DateNaiss,DateEnt,Tel,NomCat FROM Clients LEFT JOIN Category ON Category.IdCat = Clients.IdCat where Nom=@Nom", con))
                {

                    cmd.Parameters.AddWithValue("@Nom", textBox3.Text);
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            dataGridView1.DataSource = dt;
                        }
                    }
                }
            }

        }
        public void RechercheParPrenom()
        {
            dataGridView1.DataSource = null;
            using (SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-96FHGM6;Initial Catalog=TeamHunter;User ID=sa;Password=Khalil17;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT IdCl,Nom,Prenom,DateNaiss,DateEnt,Tel,NomCat FROM Clients LEFT JOIN Category ON Category.IdCat = Clients.IdCat where Prenom=@Prenom", con))
                {

                    cmd.Parameters.AddWithValue("@Prenom", textBox3.Text);
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            dataGridView1.DataSource = dt;
                        }
                    }
                }
            }

        }
        public void RechercheParTelephone()
        {
            dataGridView1.DataSource = null;
            using (SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-96FHGM6;Initial Catalog=TeamHunter;User ID=sa;Password=Khalil17;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT IdCl,Nom,Prenom,DateNaiss,DateEnt,Tel,NomCat FROM Clients LEFT JOIN Category ON Category.IdCat = Clients.IdCat where Tel=@Tel", con))
                {

                    cmd.Parameters.AddWithValue("@Tel", textBox3.Text);
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            dataGridView1.DataSource = dt;
                        }
                    }
                }
            }

        }

        public void RechercheParDateNaiss()
        {
            dataGridView1.DataSource = null;
            using (SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-96FHGM6;Initial Catalog=TeamHunter;User ID=sa;Password=Khalil17;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT IdCl,Nom,Prenom,DateNaiss,DateEnt,Tel,NomCat FROM Clients LEFT JOIN Category ON Category.IdCat = Clients.IdCat where DateNaiss=@DateNaiss", con))
                {

                    cmd.Parameters.AddWithValue("@DateNaiss", textBox3.Text);
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            dataGridView1.DataSource = dt;
                        }
                    }
                }
            }

        }
        public void RechercheParDateEnt()
        {
            dataGridView1.DataSource = null;
            using (SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-96FHGM6;Initial Catalog=TeamHunter;User ID=sa;Password=Khalil17;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT IdCl,Nom,Prenom,DateNaiss,DateEnt,Tel,NomCat FROM Clients LEFT JOIN Category ON Category.IdCat = Clients.IdCat where DateEnt=@DateEnt", con))
                {

                    cmd.Parameters.AddWithValue("@DateEnt", textBox3.Text);
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            dataGridView1.DataSource = dt;
                        }
                    }
                }
            }

        }
        public void RechercheParCat()
        {
            dataGridView1.DataSource = null;
            using (SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-96FHGM6;Initial Catalog=TeamHunter;User ID=sa;Password=Khalil17;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT IdCl,Nom,Prenom,DateNaiss,DateEnt,Tel,NomCat FROM Clients LEFT JOIN Category ON Category.IdCat = Clients.IdCat where NomCat=@NomCat", con))
                {

                    cmd.Parameters.AddWithValue("@NomCat", textBox3.Text);
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            dataGridView1.DataSource = dt;
                        }
                    }
                }
            }

        }
        public bool CheckValiditée()
        {

            DateTime DateDeb = DateTime.Now - TimeSpan.FromDays(700);
            DateTime datefin = Convert.ToDateTime(dateTimePicker2.Value);
           
            if ((textBox2.Text == "") || (textBox1.Text == "") || (textBox4.Text == "") || (comboBox1.SelectedIndex == -1)|| (DateDeb < datefin))
            {
                return true;
            }
            else
            {
                return false;
            }


        }

       
    }
}
