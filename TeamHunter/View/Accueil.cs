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
    public partial class Accueil : Form
    {

        public Accueil()
        {
            InitializeComponent();
        }

        private void client_Load(object sender, EventArgs e)
        {
            updatestate();
            updatestateco();
            refresh();
            refresh2();
            
        }

        [Obsolete]

   

        //refresh datagridview
        public void refresh()
        {
            dataGridView1.DataSource = null;
            using (SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-96FHGM6;Initial Catalog=TeamHunter;User ID=sa;Password=Khalil17;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Clients ORDER BY State", con))
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
        public void refresh2()
        {
            dataGridView2.DataSource = null;
            using (SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-96FHGM6;Initial Catalog=TeamHunter;User ID=sa;Password=Khalil17;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Coachs ORDER BY State", con))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            dataGridView2.DataSource = dt;
                        }
                    }
                }
            }


        }
        public DataTable  state()
        {

            using (SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-96FHGM6;Initial Catalog=TeamHunter;User ID=sa;Password=Khalil17;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT IdCl,Prix,PrixAssurance,DateEnt from Clients join Prix on Prix.IdCat=Clients.IdCat", con))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);

                            dt.Columns.Add("Sum", typeof(System.String));

                            foreach (DataRow row in dt.Rows)
                            {

                                using (SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-96FHGM6;Initial Catalog=TeamHunter;User ID=sa;Password=Khalil17;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
                                {
                                    using (SqlCommand cmmd = new SqlCommand("SELECT Sum(Montant) FROM Entrant where IdCl='"+ row["IdCl"].ToString() + "' group by IdCl", conn))
                                    {
                                        cmmd.CommandType = CommandType.Text;
                                        using (SqlDataAdapter sdda = new SqlDataAdapter(cmmd))
                                        {
                                            using (DataTable ddt = new DataTable())
                                            {
                                                sdda.Fill(ddt);
                                                try
                                                {
                                                    row["Sum"] = ddt.Rows[0].ItemArray[0].ToString();
                                                }
                                                catch (IndexOutOfRangeException e)  // CS0168
                                                {

                                                    row["Sum"] = "0";
                                                }
                                                // dataGridView2.DataSource = dt;
                                            }
                                        }
                                    }
                                }


                               
                            }


                            return dt;
                        }
                    }
                }
            }

        }

        public void updatestate()
        {
            foreach (DataRow row in state().Rows)
            {


                DateTime oDate = Convert.ToDateTime(row["DateEnt"].ToString());
                DateTime dateTime = DateTime.UtcNow.Date;
                TimeSpan difference = dateTime - oDate;
                int days = Math.Abs(difference.Days);
                float count = (days / 30)+1;
                float count2 = (days / 365) + 1;
                float totalprix= Convert.ToInt32(row["Prix"].ToString()) * count;
                float totalprixass = Convert.ToInt32(row["PrixAssurance"].ToString()) * count2;
               float total = totalprix + totalprixass;
                if (Convert.ToInt32(row["Sum"].ToString())>=total)
                {

                    using (SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-96FHGM6;Initial Catalog=TeamHunter;User ID=sa;Password=Khalil17;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
                    {
                        try
                        {

                            using (var cmd = new SqlCommand("UPDATE  Clients SET State='True' WHERE IdCl='"+row["IdCl"].ToString()+"'"))
                            {
                                cmd.Connection = con;
                                con.Open();
                                if (cmd.ExecuteNonQuery() > 0){ }
                                else { }
                            }
                        }

                        catch (Exception c)
                        {
                            MessageBox.Show("Error during update: " + c.Message);
                        }
                    }

                }
                else
                {
                    using (SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-96FHGM6;Initial Catalog=TeamHunter;User ID=sa;Password=Khalil17;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
                    {
                        try
                        {

                            using (var cmd = new SqlCommand("UPDATE  Clients SET State='False' WHERE IdCl='" + row["IdCl"].ToString() + "'"))
                            {
                                cmd.Connection = con;
                                con.Open();
                                if (cmd.ExecuteNonQuery() > 0) { }
                                else { }
                            }
                        }

                        catch (Exception c)
                        {
                            MessageBox.Show("Error during update: " + c.Message);
                        }
                    }

                }

            }
        }
          


        public DataTable stateco()
        {

            using (SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-96FHGM6;Initial Catalog=TeamHunter;User ID=sa;Password=Khalil17;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT IdCo,Salaire,DateEnt,PrixAssurance from Coachs ", con))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);

                            dt.Columns.Add("Sum", typeof(System.String));

                            foreach (DataRow row in dt.Rows)
                            {

                                using (SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-96FHGM6;Initial Catalog=TeamHunter;User ID=sa;Password=Khalil17;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
                                {
                                    using (SqlCommand cmmd = new SqlCommand("SELECT Sum(Montant) FROM Sortant where IdCo='" + row["IdCo"].ToString() + "' group by IdCo", conn))
                                    {
                                        cmmd.CommandType = CommandType.Text;
                                        using (SqlDataAdapter sdda = new SqlDataAdapter(cmmd))
                                        {
                                            using (DataTable ddt = new DataTable())
                                            {
                                                sdda.Fill(ddt);
                                                try
                                                {
                                                    row["Sum"] = ddt.Rows[0].ItemArray[0].ToString();
                                                }
                                                catch (IndexOutOfRangeException e)  // CS0168
                                                {

                                                    row["Sum"] = "0";
                                                }
                                                // dataGridView2.DataSource = dt;
                                            }
                                        }
                                    }
                                }



                            }


                            return dt;
                        }
                    }
                }
            }

        }


        public void updatestateco()
        {
            foreach (DataRow row in stateco().Rows)
            {


                DateTime oDate = Convert.ToDateTime(row["DateEnt"].ToString());
                DateTime dateTime = DateTime.UtcNow.Date;
                TimeSpan difference = dateTime - oDate;
                int days = Math.Abs(difference.Days);
                float count = (days / 30) + 1;
                float count2 = (days / 365) + 1;
                float totalprix = Convert.ToInt32(row["Salaire"].ToString()) * count;
                float totalprixass = Convert.ToInt32(row["PrixAssurance"].ToString()) * count2;
                float total = totalprix + totalprixass;
                if (Convert.ToInt32(row["Sum"].ToString()) >= total)
                {

                    using (SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-96FHGM6;Initial Catalog=TeamHunter;User ID=sa;Password=Khalil17;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
                    {
                        try
                        {

                            using (var cmd = new SqlCommand("UPDATE  Coachs SET State='True' WHERE IdCo='" + row["IdCo"].ToString() + "'"))
                            {
                                cmd.Connection = con;
                                con.Open();
                                if (cmd.ExecuteNonQuery() > 0) { }
                                else { }
                            }
                        }

                        catch (Exception c)
                        {
                            MessageBox.Show("Error during update: " + c.Message);
                        }
                    }

                }
                else
                {
                    using (SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-96FHGM6;Initial Catalog=TeamHunter;User ID=sa;Password=Khalil17;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
                    {
                        try
                        {

                            using (var cmd = new SqlCommand("UPDATE  Coachs SET State='False' WHERE IdCo='" + row["IdCo"].ToString() + "'"))
                            {
                                cmd.Connection = con;
                                con.Open();
                                if (cmd.ExecuteNonQuery() > 0) { }
                                else { }
                            }
                        }

                        catch (Exception c)
                        {
                            MessageBox.Show("Error during update: " + c.Message);
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

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 7 && Convert.ToString(e.Value.ToString()) == "True")
            {
                e.PaintBackground(e.ClipBounds, false);
                dataGridView1[e.ColumnIndex, e.RowIndex].ToolTipText = e.Value.ToString();
                PointF p = e.CellBounds.Location;
                // p.X += imageList1.ImageSize.Width;
                p.X += 24;
                // string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"EasySMPP\App\Images\sms.ico");
                string path = "C:\\Users\\khali\\Desktop\\checkmark_18px.png";
                e.Graphics.DrawImage(Image.FromFile(path), e.CellBounds.X, e.CellBounds.Y, 25, 16);
                // e.Graphics.DrawString(e.Value.ToString(), e.CellStyle.Font, Brushes.Black, p);
                e.Handled = true;
            }
            else if (e.RowIndex >= 0 && e.ColumnIndex == 7 && Convert.ToString(e.Value.ToString()) == "False")
            {
                e.PaintBackground(e.ClipBounds, false);
                dataGridView1[e.ColumnIndex, e.RowIndex].ToolTipText = e.Value.ToString();
                PointF p = e.CellBounds.Location;
                // p.X += imageList1.ImageSize.Width;
                p.X += 24;
                // string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"EasySMPP\App\Images\sms.ico");
                string path = "C:\\Users\\khali\\Desktop\\delete_100px.png";
                e.Graphics.DrawImage(Image.FromFile(path), e.CellBounds.X, e.CellBounds.Y, 25, 16);

                // e.Graphics.DrawString(e.Value.ToString(), e.CellStyle.Font, Brushes.Black, p);
                e.Handled = true;
            }
        }

        private void dataGridView2_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 8 && Convert.ToString(e.Value.ToString()) == "True")
            {
                e.PaintBackground(e.ClipBounds, false);
                dataGridView2[e.ColumnIndex, e.RowIndex].ToolTipText = e.Value.ToString();
                PointF p = e.CellBounds.Location;
                // p.X += imageList1.ImageSize.Width;
                p.X += 24;
                // string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"EasySMPP\App\Images\sms.ico");
                string path = "C:\\Users\\khali\\Desktop\\checkmark_18px.png";
                e.Graphics.DrawImage(Image.FromFile(path), e.CellBounds.X, e.CellBounds.Y, 25, 16);
                // e.Graphics.DrawString(e.Value.ToString(), e.CellStyle.Font, Brushes.Black, p);
                e.Handled = true;
            }
            else if (e.RowIndex >= 0 && e.ColumnIndex == 8 && Convert.ToString(e.Value.ToString()) == "False")
            {
                e.PaintBackground(e.ClipBounds, false);
                dataGridView2[e.ColumnIndex, e.RowIndex].ToolTipText = e.Value.ToString();
                PointF p = e.CellBounds.Location;
                // p.X += imageList1.ImageSize.Width;
                p.X += 24;
                // string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"EasySMPP\App\Images\sms.ico");
                string path = "C:\\Users\\khali\\Desktop\\delete_100px.png";
                e.Graphics.DrawImage(Image.FromFile(path), e.CellBounds.X, e.CellBounds.Y, 25, 16);

                // e.Graphics.DrawString(e.Value.ToString(), e.CellStyle.Font, Brushes.Black, p);
                e.Handled = true;
            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            updatestate();
            updatestateco();
            refresh();
            refresh2();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            Recherche();
            RechercheCo();
        }
        public void Recherche()
        {
            dataGridView1.DataSource = null;
            using (SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-96FHGM6;Initial Catalog=TeamHunter;User ID=sa;Password=Khalil17;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT IdCl,Nom,Prenom,DateNaiss,DateEnt,Tel,IdCat,State FROM Clients where Nom like @res or Prenom like @res or Tel like @res  order by State", con))
                {

                    cmd.Parameters.AddWithValue("@res", "%"+textBox2.Text+"%");
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
        public void RechercheCo()
        {
            dataGridView2.DataSource = null;
            using (SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-96FHGM6;Initial Catalog=TeamHunter;User ID=sa;Password=Khalil17;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Coachs where Nom like @res or Prenom like @res or Tel like @res  order by State", con))
                {

                    cmd.Parameters.AddWithValue("@res", "%" + textBox2.Text + "%");
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            dataGridView2.DataSource = dt;
                        }
                    }
                }
            }

        }
    }
}
