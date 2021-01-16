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
    public partial class LoginPage : Form
    {
        public LoginPage()
        {
            InitializeComponent();
        }
      
        private void client_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ((textBox1.Text == "khalil") && (textBox2.Text == "Khalil17")){         
            this.Hide();
            Home frm = new Home();
            frm.Show();
            }
            else
            {
                MessageBox.Show("Incorrect Mote de Passe");
            }

        }
    }
}
