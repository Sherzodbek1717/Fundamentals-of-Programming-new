using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace week10new
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void tbCountryBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.tbCountryBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dbDataSet2);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dbDataSet2.tbTeacher' table. You can move, or remove it, as needed.
            this.tbTeacherTableAdapter.Fill(this.dbDataSet2.tbTeacher);
            // TODO: This line of code loads data into the 'dbDataSet2.tbCountry' table. You can move, or remove it, as needed.
            this.tbCountryTableAdapter.Fill(this.dbDataSet2.tbCountry);

        }

        private void isActiveCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            tbTeacherBindingSource.MoveFirst();
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            tbTeacherBindingSource.MovePrevious();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            tbTeacherBindingSource.MoveNext();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            tbTeacherBindingSource.MoveLast();
        }

        private void btnRetrieve_Click(object sender, EventArgs e)
        {
            try
            {
                var sql = "select * from tb_teacher";
                var cmd = new OleDbCommand(sql, conn);
                conn.Open();
                var rdr = cmd.ExecuteReader();
                var result = "";
                while (rdr.Read())
                {
                    result += $"Id: {rdr.GetInt32(0)}; First name: {rdr.GetString(1)}; Last name: {rdr.GetString(2)}; Address: {rdr.GetString(3)}; Phone: {rdr.GetString(4)}";
                    result += "\n";
                }

                MessageBox.Show(result);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if ((conn.State != ConnectionState.Closed))
                {
                    conn.Close();
                }
            }

        }
    }
}
