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
    public partial class Prix : Form
    {
        public Prix()
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

                        using (var cmd = new SqlCommand("INSERT INTO Prix(Prix,PrixAssurance,IdCat)values (@Prix,@PrixAssurance,@IdCat)"))
                        {

                            cmd.Connection = con;
                            cmd.Parameters.Add("@Prix", textBox1.Text);
                            cmd.Parameters.Add("@PrixAssurance", textBox2.Text);
                            cmd.Parameters.Add("@IdCat", comboBox1.SelectedValue.ToString());


                            con.Open();
                            if (cmd.ExecuteNonQuery() > 0)
                            {
                                MessageBox.Show("Prix inserted");
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
            else { MessageBox.Show("verifier que tout les champ sont bient saisie Nb: ne saisie pas le combobox manuallement"); }

         

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
                using (SqlCommand cmd = new SqlCommand("SELECT IdPr,Prix,PrixAssurance,NomCat,Category.IdCat FROM Prix LEFT JOIN Category ON Prix.IdCat = Category.IdCat", con))
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
                    comboBox1.Text = dataGridView1.SelectedCells[3].Value.ToString();
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

                    using (var cmd = new SqlCommand("UPDATE  Prix SET Prix=@Prix,PrixAssurance=@PrixAssurance,IdCat=@IdCat WHERE IdPr=@IdPr"))
                    {

                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@Prix", textBox1.Text);
                        cmd.Parameters.AddWithValue("@PrixAssurance", textBox2.Text);
                        cmd.Parameters.AddWithValue("@IdCat", comboBox1.SelectedValue.ToString());
                        cmd.Parameters.AddWithValue("@IdPr", dataGridView1.SelectedCells[0].Value);


                        con.Open();
                        if (cmd.ExecuteNonQuery() > 0)
                        {
                            MessageBox.Show("Prix Updated");
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
            else { MessageBox.Show("verifier que tout les champ sont bient saisie Nb: ne saisie pas le combobox manuallement"); }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Voulez-vous supprime ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
            {
                using (SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-96FHGM6;Initial Catalog=TeamHunter;User ID=sa;Password=Khalil17;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
                {
                    try
                    {

                        using (var cmd = new SqlCommand("Delete from Prix where IdPr=" + dataGridView1.SelectedCells[0].Value.ToString()))
                        {

                            cmd.Connection = con;


                            con.Open();
                            if (cmd.ExecuteNonQuery() > 0)
                            {
                                MessageBox.Show("Prix Deleted");
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

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (comboBox2.Text == "Prix")
            {
                float f;
                if (float.TryParse(textBox3.Text, out f))
                {
                    RechercheParPrix();
                }
                else { MessageBox.Show("Not a number"); }
            }
            else if (comboBox2.Text == "Prix Assurance")
            {
                float f;
                if (float.TryParse(textBox3.Text, out f))
                {
                    RechercheParPrixAss();
                }
                else { MessageBox.Show("Not a number"); }
            }
            else if (comboBox2.Text == "Categorie")
            {
                RechercheParCategorie();
            }
            else
            {
                MessageBox.Show("CHOISIR TYPE De RECHERCHE");
            }
        }

        public void RechercheParPrix()
        {
            dataGridView1.DataSource = null;
            using (SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-96FHGM6;Initial Catalog=TeamHunter;User ID=sa;Password=Khalil17;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT IdPr,Prix,PrixAssurance,NomCat,Category.IdCat  FROM Prix LEFT JOIN Category ON Prix.IdCat = Category.IdCat where Prix=@Prix", con))
                {

                    cmd.Parameters.AddWithValue("@Prix", textBox3.Text);
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
        public void RechercheParPrixAss()
        {
            dataGridView1.DataSource = null;
            using (SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-96FHGM6;Initial Catalog=TeamHunter;User ID=sa;Password=Khalil17;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT IdPr,Prix,PrixAssurance,NomCat,Category.IdCat  FROM Prix LEFT JOIN Category ON Prix.IdCat = Category.IdCat where PrixAssurance=@PrixAssurance", con))
                {

                    cmd.Parameters.AddWithValue("@PrixAssurance", textBox3.Text);
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
        public void RechercheParCategorie()
        {
            dataGridView1.DataSource = null;
            using (SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-96FHGM6;Initial Catalog=TeamHunter;User ID=sa;Password=Khalil17;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT IdPr,Prix,PrixAssurance,NomCat,Category.IdCat  FROM Prix LEFT JOIN Category ON Prix.IdCat = Category.IdCat where NomCat=@NomCat", con))
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
            if ((textBox2.Text == "") || (textBox1.Text == "")|| (comboBox1.SelectedIndex == -1))
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
