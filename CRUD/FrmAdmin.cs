using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace CRUD
{
    public partial class FrmAdmin : Form
    {
        private MySqlConnection koneksi;
        private MySqlDataAdapter adapter;
        private MySqlCommand perintah;

        private DataSet ds = new DataSet();
        private string alamat, query;
        public FrmAdmin()
        {
            alamat = "server=localhost; database=db_vispro; username=root; password=;";
            koneksi = new MySqlConnection(alamat);

            InitializeComponent();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                FrmAdmin_Load(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtIdPengguna.Text != "")
                {
                    query = string.Format("select * from tbl_pengguna where id_pengguna = '{0}'", txtIdPengguna.Text);
                    ds.Clear();
                    koneksi.Open();
                    perintah = new MySqlCommand(query, koneksi);
                    adapter = new MySqlDataAdapter(perintah);
                    perintah.ExecuteNonQuery();
                    adapter.Fill(ds);
                    koneksi.Close();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow kolom in ds.Tables[0].Rows)
                        {
                            txtUsername.Text = kolom["user_name"].ToString();
                            txtPassword.Text = kolom["password"].ToString();
                            txtNamaPengguna.Text = kolom["nama_pengguna"].ToString();
                            if (kolom["level"].ToString() == "1")
                            {
                                CbLevel.Text = "Administrator";
                            }
                            else
                            {
                                CbLevel.Text = "Pengguna";
                            }


                            CbLevel.Enabled = true;

                            btnSave.Enabled = false;
                            btnDelete.Enabled = true;
                            btnUpdate.Enabled = true;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("ID Pengguna masih kosong");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                query = string.Format("DELETE FROM `tbl_pengguna` where id_pengguna = '{0}'", txtIdPengguna.Text);
                ds.Clear();
                koneksi.Open();
                perintah = new MySqlCommand(query, koneksi);
                adapter = new MySqlDataAdapter(perintah);
                perintah.ExecuteNonQuery();
                adapter.Fill(ds);
                koneksi.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (CbLevel.Text == "Administrator")
                {
                    CbLevel.Text = "1";
                }
                else
                {
                    CbLevel.Text = "2";
                }
                query = string.Format("UPDATE `tbl_pengguna` SET `user_name`='{0}',`password`='{1}',`nama_pengguna`='{2}',`level`='{3}' where id_pengguna = '{4}'", txtUsername.Text, txtPassword.Text, txtNamaPengguna.Text, CbLevel.Text, txtIdPengguna.Text);
                ds.Clear();
                koneksi.Open();
                perintah = new MySqlCommand(query, koneksi);
                adapter = new MySqlDataAdapter(perintah);
                perintah.ExecuteNonQuery();
                adapter.Fill(ds);
                koneksi.Close();

                FrmAdmin_Load(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (CbLevel.Text == "Administrator")
                {
                    CbLevel.Text = "1";
                }
                else
                {
                    CbLevel.Text = "2";
                }
                query = string.Format("insert into `tbl_pengguna` (`user_name`, `password`, `nama_pengguna`, `level`) VALUES ('{0}','{1}', '{2}', '{3}')", txtUsername.Text, txtPassword.Text, txtNamaPengguna.Text, CbLevel.Text);

                koneksi.Open();
                perintah = new MySqlCommand(query, koneksi);
                adapter = new MySqlDataAdapter(perintah);
                int res = perintah.ExecuteNonQuery();

                koneksi.Close();
                if (res == 1)
                {
                    MessageBox.Show("Insert data success");
                    FrmAdmin_Load(null, null);
                }
                else
                {
                    MessageBox.Show("Insert data Error");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void FrmAdmin_Load(object sender, EventArgs e)
        {
            try
            {
                koneksi.Open();
                query = string.Format("select * from tbl_pengguna");
                perintah = new MySqlCommand(query, koneksi);
                adapter = new MySqlDataAdapter(perintah);
                perintah.ExecuteNonQuery();
                ds.Clear();
                adapter.Fill(ds);
                koneksi.Close();

                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Columns[0].Width = 30;
                dataGridView1.Columns[0].HeaderText = "No";
                dataGridView1.Columns[1].Width = 70;
                dataGridView1.Columns[1].HeaderText = "Username";
                dataGridView1.Columns[2].Width = 70;
                dataGridView1.Columns[2].HeaderText = "Password";
                dataGridView1.Columns[3].Width = 70;
                dataGridView1.Columns[3].HeaderText = "Nama Pengguna";
                dataGridView1.Columns[4].Width = 50;
                dataGridView1.Columns[4].HeaderText = "Level";

                txtIdPengguna.Clear();
                txtUsername.Clear();
                txtPassword.Clear();


                txtIdPengguna.Focus();

                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;
                btnRead.Enabled = false;
                btnSave.Enabled = true;
                btnRead.Enabled = true;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
