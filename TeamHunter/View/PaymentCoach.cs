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
    public partial class PaymentCoach : Form
    {
        public PaymentCoach()
        {
            InitializeComponent();
        }
      
        private void PaymentCoach_Load(object sender, EventArgs e)
        {
         
            refresh();
            if (checkBox1.Checked)
            {
                textBox2.Enabled = true;
                textBox2.Text = "0";
            }
            else
            {
                textBox2.Enabled = false;
                textBox2.Text = "0";
            }
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

                    using (var cmd = new SqlCommand("INSERT INTO PaymentCoach(Montant,MontantAssurance,DatePayment,IdCo)values (@Montant,@MontantAssurance,@DatePayment,@IdCo)"))
                    {

                        cmd.Connection = con;
                        cmd.Parameters.Add("@Montant", textBox1.Text);
                        cmd.Parameters.Add("@MontantAssurance", textBox2.Text);
                        cmd.Parameters.Add("@DatePayment", dateTimePicker2.Text);
                  
                        cmd.Parameters.Add("@IdCo", comboBox1.SelectedValue);


                        con.Open();
                        if (cmd.ExecuteNonQuery() > 0)
                        {
                            MessageBox.Show("Payment inserted");
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
                using (SqlCommand cmd = new SqlCommand("SELECT IdPayCo,Montant,MontantAssurance,DatePayment,Nom,Prenom,Tel FROM PaymentCoach LEFT JOIN Coachs ON Coachs.IdCo = PaymentCoach.IdCo", con))
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
            checkBox1.Checked = false;
            dateTimePicker2.Text = "";
        }
        public void refreshcb()
        {
            using (SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-96FHGM6;Initial Catalog=TeamHunter;User ID=sa;Password=Khalil17;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Coachs", con))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        using (DataSet ds = new DataSet())
                        {
                            sda.Fill(ds, "Coachs");
                            comboBox1.DisplayMember = "Nom";
                            comboBox1.ValueMember = "IdCo";
                            comboBox1.DataSource = ds.Tables["Coachs"];
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
                    comboBox1.Text = dataGridView1.SelectedCells[4].Value.ToString();
                }

            }
            catch (ArgumentOutOfRangeException )
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

                    using (var cmd = new SqlCommand("UPDATE  PaymentCoach SET Montant=@Montant,MontantAssurance=@MontantAssurance,DatePayment=@DatePayment,IdCo=@IdCo WHERE IdPayCo=@IdPayCo"))
                    {

                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@Montant", textBox1.Text);
                        cmd.Parameters.AddWithValue("@MontantAssurance", textBox2.Text);
                        cmd.Parameters.AddWithValue("@DatePayment",dateTimePicker2.Text);
                        cmd.Parameters.AddWithValue("@IdCo", comboBox1.SelectedValue);
                        cmd.Parameters.AddWithValue("@IdPayCo", dataGridView1.SelectedCells[0].Value);


                        con.Open();
                        if (cmd.ExecuteNonQuery() > 0)
                        {
                            MessageBox.Show("Payment Updated");
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

                        using (var cmd = new SqlCommand("Delete from PaymentCoach where IdPayCo=" + dataGridView1.SelectedCells[0].Value.ToString()))
                        {

                            cmd.Connection = con;


                            con.Open();
                            if (cmd.ExecuteNonQuery() > 0)
                            {
                                MessageBox.Show("Payment Deleted");
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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox2.Enabled = true;
                textBox2.Text = "0";
            }
            else
            {
                textBox2.Enabled = false;
                textBox2.Text = "0";
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

        private void button5_Click(object sender, EventArgs e)
        {
            if (comboBox2.Text == "Montant")
            {
                float f;
                if (float.TryParse(textBox3.Text, out f))
                {
                    RechercheParMontant();
                }
                else { MessageBox.Show("Not a number"); }
            }
            else if (comboBox2.Text == "Montant Assurance")
            {
                float f;
                if (float.TryParse(textBox3.Text, out f))
                {
                    RechercheParMontantAssurance();
                }
                else { MessageBox.Show("Not a number"); }
            }
            else if (comboBox2.Text == "Coach")
            {
                RechercheParCoach();
            }
            else if (comboBox2.Text == "Date Payment")
            {
                DateTime temp;

                if (DateTime.TryParse(textBox3.Text, out temp))
                {
                    RechercheParDatePayment();
                }
                else { MessageBox.Show("Check Date sous Forme mm/jj/yyyy"); }
            }
            else
            {
                MessageBox.Show("CHOISIR TYPE De RECHERCHE");
            }
        }
        public void RechercheParMontant()
        {
            dataGridView1.DataSource = null;
            using (SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-96FHGM6;Initial Catalog=TeamHunter;User ID=sa;Password=Khalil17;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT IdPayCo,Montant,MontantAssurance,DatePayment,Nom,Prenom,Tel FROM PaymentCoach LEFT JOIN Coachs ON Coachs.IdCo = PaymentCoach.IdCo where Montant=@Montant", con))
                {

                    cmd.Parameters.AddWithValue("@Montant", textBox3.Text);
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
        public void RechercheParMontantAssurance()
        {
            dataGridView1.DataSource = null;
            using (SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-96FHGM6;Initial Catalog=TeamHunter;User ID=sa;Password=Khalil17;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT IdPayCo,Montant,MontantAssurance,DatePayment,Nom,Prenom,Tel FROM PaymentCoach LEFT JOIN Coachs ON Coachs.IdCo = PaymentCoach.IdCo where MontantAssurance=@MontantAssurance", con))
                {

                    cmd.Parameters.AddWithValue("@MontantAssurance", textBox3.Text);
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
        public void RechercheParCoach()
        {
            dataGridView1.DataSource = null;
            using (SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-96FHGM6;Initial Catalog=TeamHunter;User ID=sa;Password=Khalil17;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT IdPayCo,Montant,MontantAssurance,DatePayment,Nom,Prenom,Tel FROM PaymentCoach LEFT JOIN Coachs ON Coachs.IdCo = PaymentCoach.IdCo where Nom=@Nom", con))
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
        public void RechercheParDatePayment()
        {
            dataGridView1.DataSource = null;
            using (SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-96FHGM6;Initial Catalog=TeamHunter;User ID=sa;Password=Khalil17;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT IdPayCo,Montant,MontantAssurance,DatePayment,Nom,Prenom,Tel FROM PaymentCoach LEFT JOIN Coachs ON Coachs.IdCo = PaymentCoach.IdCo where DatePayment=@DatePayment", con))
                {

                    cmd.Parameters.AddWithValue("@DatePayment", textBox3.Text);
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
            if ((textBox2.Text == "") || (textBox1.Text == "") || (comboBox1.SelectedIndex == -1))
            {
                return true;
            }
            else
            {
                return false;
            }


        }

        private void button6_Click(object sender, EventArgs e)
        {
            FR2 fr = new FR2(this);
            fr.Show();
        }
    }
}
