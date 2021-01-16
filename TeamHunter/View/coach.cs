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
    public partial class coach : Form
    {
        public coach()
        {
            InitializeComponent();
        }
      
        private void coach_Load(object sender, EventArgs e)
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

                    using (var cmd = new SqlCommand("INSERT INTO Coachs(Nom,Prenom,Salaire,PrixAssurance,DateNaiss,DateEnt,Tel,State)values (@Nom,@Prenom,@Salaire,@PrixAssurance,@DateNaiss,@DateEnt,@Tel,'False')"))
                    {

                        cmd.Connection = con;
                        cmd.Parameters.Add("@Nom", textBox1.Text);
                        cmd.Parameters.Add("@Prenom", textBox2.Text);
                        cmd.Parameters.Add("@Salaire", textBox3.Text);
                            cmd.Parameters.Add("@PrixAssurance", textBox6.Text);
                            cmd.Parameters.Add("@DateNaiss", dateTimePicker2.Text);
                        cmd.Parameters.Add("@DateEnt", dateTimePicker1.Text);
                        cmd.Parameters.Add("@Tel", textBox4.Text);


                        con.Open();
                        if (cmd.ExecuteNonQuery() > 0)
                        {
                            MessageBox.Show("Coach inserted");
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
            else { MessageBox.Show("verifier que tout les champ sont bient saisie Nb: il faut bien saisie la date de naissance"); }





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
                using (SqlCommand cmd = new SqlCommand("SELECT IdCo,Nom,Prenom,Salaire,PrixAssurance,DateNaiss,DateEnt,Tel FROM Coachs", con))
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
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox6.Text = "";
            dateTimePicker2.Text = "";
            dateTimePicker1.Text = "";
        }
        
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
          
            try
            {
                if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    textBox1.Text = dataGridView1.SelectedCells[1].Value.ToString();
                    textBox2.Text = dataGridView1.SelectedCells[2].Value.ToString();
                    textBox3.Text = dataGridView1.SelectedCells[3].Value.ToString();
                    textBox6.Text = dataGridView1.SelectedCells[4].Value.ToString();
                    dateTimePicker2.Text = dataGridView1.SelectedCells[5].Value.ToString();
                    dateTimePicker1.Text = dataGridView1.SelectedCells[6].Value.ToString();
                    textBox4.Text = dataGridView1.SelectedCells[7].Value.ToString();
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

                    using (var cmd = new SqlCommand("UPDATE  Coachs SET Nom=@Nom,Prenom=@Prenom,Salaire=@Salaire,PrixAssurance=@PrixAssurance,DateNaiss=@DateNaiss,DateEnt=@DateEnt,Tel=@Tel WHERE IdCo=@IdCo"))
                    {

                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@Nom",textBox1.Text);
                        cmd.Parameters.AddWithValue("@Prenom", textBox2.Text);
                        cmd.Parameters.AddWithValue("@Salaire", textBox3.Text);
                        cmd.Parameters.AddWithValue("@PrixAssurance", textBox6.Text);
                        cmd.Parameters.AddWithValue("@DateNaiss",dateTimePicker2.Text);
                        cmd.Parameters.AddWithValue("@DateEnt",dateTimePicker1.Text);
                        cmd.Parameters.AddWithValue("@Tel",textBox4.Text);
                        cmd.Parameters.AddWithValue("@IdCo",dataGridView1.SelectedCells[0].Value);


                        con.Open();
                        if (cmd.ExecuteNonQuery() > 0)
                        {
                            MessageBox.Show("Coach Updated");
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
            else { MessageBox.Show("verifier que tout les champ sont bient saisie Nb: il faut bien saisie la date de naissance"); }

}

private void button2_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Voulez-vous supprime ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
            {
                using (SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-96FHGM6;Initial Catalog=TeamHunter;User ID=sa;Password=Khalil17;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
                {
                    try
                    {

                        using (var cmd = new SqlCommand("Delete from Coachs where IdCo=" + dataGridView1.SelectedCells[0].Value.ToString()))
                        {

                            cmd.Connection = con;


                            con.Open();
                            if (cmd.ExecuteNonQuery() > 0)
                            {
                                MessageBox.Show("Coach Deleted");
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

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
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
           
            if (comboBox1.Text == "Salaire")
            {
                float f;
                if (float.TryParse(textBox5.Text, out f))
                {
                    RechercheParSalaire();
                }
                else { MessageBox.Show("Not a number"); }
            }
            else if (comboBox1.Text == "Nom")
            {
                RechercheParNom();
            }
            else if (comboBox1.Text == "Prenom")
            {
                RechercheParPrenom();
            }
            else if (comboBox1.Text == "N° Telephone")
            {
                RechercheParTelephone();
            }
            else if (comboBox1.Text == "Date Naissance")
            {
                DateTime temp;

                if (DateTime.TryParse(textBox5.Text, out temp))
                {
                    RechercheParDateNaiss();
                }
                else { MessageBox.Show("Check Date sous Forme mm/jj/yyyy"); }
            }
            else if (comboBox1.Text == "Date Entree")
            {
                DateTime temp;

                if (DateTime.TryParse(textBox5.Text, out temp))
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
        public void RechercheParSalaire()
        {
            dataGridView1.DataSource = null;
            using (SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-96FHGM6;Initial Catalog=TeamHunter;User ID=sa;Password=Khalil17;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT IdCo,Nom,Prenom,Salaire,PrixAssurance,DateNaiss,DateEnt,Tel FROM Coachs where Salaire=@Salaire", con))
                {

                    cmd.Parameters.AddWithValue("@Salaire", textBox5.Text);
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
        public void RechercheParNom()
        {
            dataGridView1.DataSource = null;
            using (SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-96FHGM6;Initial Catalog=TeamHunter;User ID=sa;Password=Khalil17;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT IdCo,Nom,Prenom,Salaire,PrixAssurance,DateNaiss,DateEnt,Tel FROM Coachs where Nom=@Nom", con))
                {

                    cmd.Parameters.AddWithValue("@Nom", textBox5.Text);
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
                using (SqlCommand cmd = new SqlCommand("SELECT IdCo,Nom,Prenom,Salaire,PrixAssurance,DateNaiss,DateEnt,Tel FROM Coachs where Prenom=@Prenom", con))
                {

                    cmd.Parameters.AddWithValue("@Prenom", textBox5.Text);
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
                using (SqlCommand cmd = new SqlCommand("SELECT IdCo,Nom,Prenom,Salaire,PrixAssurance,DateNaiss,DateEnt,Tel FROM Coachs where Tel=@Tel", con))
                {

                    cmd.Parameters.AddWithValue("@Tel", textBox5.Text);
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
                using (SqlCommand cmd = new SqlCommand("SELECT IdCo,Nom,Prenom,Salaire,PrixAssurance,DateNaiss,DateEnt,Tel FROM Coachs where DateNaiss=@DateNaiss", con))
                {

                    cmd.Parameters.AddWithValue("@DateNaiss", textBox5.Text);
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
                using (SqlCommand cmd = new SqlCommand("SELECT IdCo,Nom,Prenom,Salaire,PrixAssurance,DateNaiss,DateEnt,Tel FROM Coachs where DateEnt=@DateEnt", con))
                {

                    cmd.Parameters.AddWithValue("@DateEnt", textBox5.Text);
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
            DateTime DateDeb = DateTime.Now - TimeSpan.FromDays(5840);
            DateTime datefin = Convert.ToDateTime(dateTimePicker2.Value);

            if ((textBox2.Text == "") || (textBox1.Text == "") || (textBox4.Text == "") || (textBox3.Text == "") || (textBox6.Text == "") || (DateDeb < datefin))
            {
                return true;
            }
            else
            {
                return false;
            }


        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
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
    }
}
