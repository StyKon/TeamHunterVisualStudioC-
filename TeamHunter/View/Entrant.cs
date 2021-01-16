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
    public partial class Entrant : Form
    {
        public Entrant()
        {
            InitializeComponent();
        }
      
        private void Entrant_Load(object sender, EventArgs e)
        {

            DateTime DateDeb = DateTime.Now - TimeSpan.FromDays(30);
            dateTimePicker1.Text = DateDeb.ToString();
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

                    using (var cmd = new SqlCommand("INSERT INTO Entrant(NomEnt,Montant,DatePayment)values (@NomEnt,@Montant,@DatePayment)"))
                    {

                        cmd.Connection = con;
                        cmd.Parameters.Add("@NomEnt", textBox3.Text);
                        cmd.Parameters.Add("@Montant", textBox1.Text);
                        cmd.Parameters.Add("@DatePayment", dateTimePicker2.Text);
                  


                        con.Open();
                        if (cmd.ExecuteNonQuery() > 0)
                        {
                            MessageBox.Show("Entrant inserted");
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
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Entrant ", con))
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
            
            textBox3.Text = "";
            textBox1.Text = "";
            dateTimePicker2.Text = "";

            SumEntrant();
        }
       

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    textBox3.Text = dataGridView1.SelectedCells[1].Value.ToString();
                    textBox1.Text = dataGridView1.SelectedCells[2].Value.ToString();
                    dateTimePicker2.Text = dataGridView1.SelectedCells[3].Value.ToString();
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

                    using (var cmd = new SqlCommand("UPDATE  Entrant SET NomEnt=@NomEnt,Montant=@Montant,DatePayment=@DatePayment WHERE IdEnt=@IdEnt"))
                    {

                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@NomEnt", textBox3.Text);
                        cmd.Parameters.AddWithValue("@Montant", textBox1.Text);
                        cmd.Parameters.AddWithValue("@DatePayment", dateTimePicker2.Text);
                        cmd.Parameters.AddWithValue("@IdEnt", dataGridView1.SelectedCells[0].Value);


                        con.Open();
                        if (cmd.ExecuteNonQuery() > 0)
                        {
                            MessageBox.Show("Entrant Updated");
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

                        using (var cmd = new SqlCommand("Delete from Entrant where IdEnt=" + dataGridView1.SelectedCells[0].Value.ToString()))
                        {

                            cmd.Connection = con;


                            con.Open();
                            if (cmd.ExecuteNonQuery() > 0)
                            {
                                MessageBox.Show("Entrant Deleted");
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
            dateTimePicker3.Text = "";
            DateTime DateDeb = DateTime.Now - TimeSpan.FromDays(30);
            dateTimePicker1.Text = DateDeb.ToString();

           

        }
        public void SumEntrant()
        {
            using (SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-96FHGM6;Initial Catalog=TeamHunter;User ID=sa;Password=Khalil17;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT SUM(Montant) As Total FROM Entrant where DatePayment between @DateDebut And @DateFin", con))
                {

                    cmd.Parameters.AddWithValue("@DateDebut", dateTimePicker1.Text);
                    cmd.Parameters.AddWithValue("@DateFin", dateTimePicker3.Text);
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            label8.Text=(dt.Rows[0].ItemArray[0].ToString());
                        }
                    }
                }
            }
        }

        private void dateTimePicker1_CloseUp(object sender, EventArgs e)
        {
            DateTime datedebut = Convert.ToDateTime(dateTimePicker1.Value);
            DateTime datefin = Convert.ToDateTime(dateTimePicker3.Value);
            if (datedebut > datefin)
            {
                MessageBox.Show("Verifier que la date de debut et inferieur ou date fin");
            }
            
        }

        private void dateTimePicker3_CloseUp(object sender, EventArgs e)
        {
            DateTime datedebut = Convert.ToDateTime(dateTimePicker1.Value);
            DateTime datefin = Convert.ToDateTime(dateTimePicker3.Value);
            if (datedebut > datefin)
            {
                MessageBox.Show("Verifier que la date de debut et inferieur ou date fin");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DateTime datedebut = Convert.ToDateTime(dateTimePicker1.Value);
            DateTime datefin = Convert.ToDateTime(dateTimePicker3.Value);
            if (datedebut > datefin)
            {
                MessageBox.Show("Verifier que la date de debut et inferieur ou date fin");
            }
            else {
                SumEntrant();
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

        private void button6_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Description")
            {
                RechercheParDescription();
            }
            else if (comboBox1.Text == "Montant")
            {
                float f;
                if (float.TryParse(textBox2.Text, out f))
                {
                    RechercheParMontant();
                }
                else { MessageBox.Show("Not a number"); }
            }
            else if (comboBox1.Text == "Date Payment")
            {
                DateTime temp;

                if (DateTime.TryParse(textBox2.Text, out temp))
                {
                    RechercheParDate();
                }
                else { MessageBox.Show("Check Date sous Forme mm/jj/yyyy"); }
            }
            else
            {
                MessageBox.Show("CHOISIR TYPE De RECHERCHE");
            }
        }
        public void RechercheParDescription()
        {
            dataGridView1.DataSource = null;
            using (SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-96FHGM6;Initial Catalog=TeamHunter;User ID=sa;Password=Khalil17;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Entrant where NomEnt=@NomEnt", con))
                {

                    cmd.Parameters.AddWithValue("@NomEnt", textBox2.Text);
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
        public void RechercheParMontant()
        {
            dataGridView1.DataSource = null;
            using (SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-96FHGM6;Initial Catalog=TeamHunter;User ID=sa;Password=Khalil17;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Entrant where Montant=@Montant", con))
                {

                    cmd.Parameters.AddWithValue("@Montant", textBox2.Text);
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

        public void RechercheParDate()
        {
            dataGridView1.DataSource = null;
            using (SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-96FHGM6;Initial Catalog=TeamHunter;User ID=sa;Password=Khalil17;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Entrant where DatePayment=@DatePayment", con))
                {

                    cmd.Parameters.AddWithValue("@DatePayment", textBox2.Text);
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
            if ((textBox3.Text == "") || (textBox1.Text == "") )
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
