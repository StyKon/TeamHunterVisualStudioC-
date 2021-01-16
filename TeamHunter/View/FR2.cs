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
    public partial class FR2 : Form
    {
        private PaymentCoach pc;
        public FR2(PaymentCoach pc1)
        {
            InitializeComponent();
            pc = pc1;
        }
      
        private void FR2_Load(object sender, EventArgs e)
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
        }

        

        private void button1_Click_1(object sender, EventArgs e)
        {
           
            pc.comboBox1.SelectedValue = dataGridView1.SelectedCells[0].Value;
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            using (SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-96FHGM6;Initial Catalog=TeamHunter;User ID=sa;Password=Khalil17;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT IdCo,Nom,Prenom,Salaire,PrixAssurance,DateNaiss,DateEnt,Tel FROM Coachs where Nom like @res or Prenom like @res or Tel like @res  order by State", con))
                {

                    cmd.Parameters.AddWithValue("@res", "%" + textBox2.Text + "%");
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
    }
}
