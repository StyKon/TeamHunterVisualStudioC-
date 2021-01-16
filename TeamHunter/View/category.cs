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
    public partial class category : Form
    {
        public category()
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

                    using (var cmd = new SqlCommand("INSERT INTO Category(NomCat)values (@NomCat)"))
                    {

                        cmd.Connection = con;
                        cmd.Parameters.Add("@NomCat", textBox1.Text);
             


                        con.Open();
                        if (cmd.ExecuteNonQuery() > 0)
                        {
                            MessageBox.Show("Categorie inserted");
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
            else { MessageBox.Show("verifier que tout les champ sont bient saisie"); }






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
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Category", con))
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
            textBox1.Text = "";
        }
        
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
            try
            {
                if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    textBox1.Text = dataGridView1.SelectedCells[1].Value.ToString();
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

                    using (var cmd = new SqlCommand("UPDATE  Category SET NomCat=@NomCat WHERE IdCat=@IdCat"))
                    {

                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@NomCat", textBox1.Text);
          
                        cmd.Parameters.AddWithValue("@IdCat",dataGridView1.SelectedCells[0].Value);


                        con.Open();
                        if (cmd.ExecuteNonQuery() > 0)
                        {
                            MessageBox.Show("Categorie Updated");
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
            else { MessageBox.Show("verifier que tout les champ sont bient saisie"); }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Voulez-vous supprime ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
            {
                using (SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-96FHGM6;Initial Catalog=TeamHunter;User ID=sa;Password=Khalil17;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
                {
                    try
                    {

                        using (var cmd = new SqlCommand("Delete from Category where IdCat=" + dataGridView1.SelectedCells[0].Value.ToString()))
                        {

                            cmd.Connection = con;


                            con.Open();
                            if (cmd.ExecuteNonQuery() > 0)
                            {
                                MessageBox.Show("Categorie Deleted");
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

        private void button5_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Nom Categorie")
            {
              
                    RechercheParCategorie();
                
            }
            else
            {
                MessageBox.Show("CHOISIR TYPE De RECHERCHE");
            }
        }
        public void RechercheParCategorie()
        {
            dataGridView1.DataSource = null;
            using (SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-96FHGM6;Initial Catalog=TeamHunter;User ID=sa;Password=Khalil17;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Category where NomCat=@NomCat", con))
                {

                    cmd.Parameters.AddWithValue("@NomCat", textBox2.Text);
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
            if ((textBox1.Text == ""))
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
