using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TeamHunter
{

    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }

        private void Home_Load(object sender, EventArgs e)
        {
            panel3.Controls.Clear();
            Accueil frm = new Accueil() { TopLevel = false, TopMost = true };
            frm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            frm.AutoSize = true;
            frm.AutoScroll = true;
            frm.Dock = DockStyle.Fill;
            this.panel3.Controls.Add(frm);
            frm.Show();
        }
    

        private void prixToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void btncoach_Click(object sender, EventArgs e)
        {
            panel3.Controls.Clear();
            coach frm = new coach() { TopLevel = false, TopMost = true };
            frm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;

            frm.AutoSize = true;
            frm.AutoScroll = true;
            frm.Dock = DockStyle.Fill;
            this.panel3.Controls.Add(frm);
            frm.Show();
        }

        private void btnclient_Click(object sender, EventArgs e)
        {

           
            panel3.Controls.Clear();
            client frm = new client() { TopLevel = false, TopMost = true };
            frm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
           
            frm.AutoSize = true;
            frm.AutoScroll = true;
            frm.Dock = DockStyle.Fill;
            this.panel3.Controls.Add(frm);
            frm.Show();

        }

        private void btnconf_Click(object sender, EventArgs e)
        {
            panel3.Controls.Clear();
            Prix frm = new Prix() { TopLevel = false, TopMost = true };
            frm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            frm.AutoSize = true;
            frm.AutoScroll = true;
            frm.Dock = DockStyle.Fill;
            this.panel3.Controls.Add(frm);
            frm.Show();
        }

        private void btnpayment_Click(object sender, EventArgs e)
        {
            panel3.Controls.Clear();
            PaymentClient frm = new PaymentClient() { TopLevel = false, TopMost = true };
            frm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            frm.AutoSize = true;
            frm.AutoScroll = true;
            frm.Dock = DockStyle.Fill;
            this.panel3.Controls.Add(frm);
            frm.Show();
        }

        private void btnaccueil_Click(object sender, EventArgs e)
        {
            panel3.Controls.Clear();
            Accueil frm = new Accueil() { TopLevel = false, TopMost = true };
            frm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            frm.AutoSize = true;
            frm.AutoScroll = true;
            frm.Dock = DockStyle.Fill;
            this.panel3.Controls.Add(frm);
            frm.Show();
        }

        private void btnexit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnsort_Click(object sender, EventArgs e)
        {
            panel3.Controls.Clear();
            Sortant frm = new Sortant() { TopLevel = false, TopMost = true };
            frm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            frm.AutoSize = true;
            frm.AutoScroll = true;
            frm.Dock = DockStyle.Fill;
            this.panel3.Controls.Add(frm);
            frm.Show();
        }

        private void btnent_Click(object sender, EventArgs e)
        {
            panel3.Controls.Clear();
            Entrant frm = new Entrant() { TopLevel = false, TopMost = true };
            frm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            frm.AutoSize = true;
            frm.AutoScroll = true;
            frm.Dock = DockStyle.Fill;
            this.panel3.Controls.Add(frm);
            frm.Show();
        }

        private void btncat_Click(object sender, EventArgs e)
        {
            panel3.Controls.Clear();
            category frm = new category() { TopLevel = false, TopMost = true };
            frm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            frm.AutoSize = true;
            frm.AutoScroll = true;
            frm.Dock = DockStyle.Fill;
            this.panel3.Controls.Add(frm);
            frm.Show();
        }

        private void Home_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel3.Controls.Clear();
            PaymentCoach frm = new PaymentCoach() { TopLevel = false, TopMost = true };
            frm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            frm.AutoSize = true;
            frm.AutoScroll = true;
            frm.Dock = DockStyle.Fill;
            this.panel3.Controls.Add(frm);
            frm.Show();
        }
    }
}
