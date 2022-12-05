using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace practiceForExam
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                this.tbCountryTableAdapter.Fill(this.dbDataSet.tbCountry);
                this.tbTeacherTableAdapter.Fill(this.dbDataSet.tbTeacher);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }
        private void tbTeacherBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
           SaveData();

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        //in order to save automatically and notify them about changes
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.Validate())
            {
                this.tbTeacherBindingSource.EndEdit();
                if (dbDataSet.HasChanges())
                {
                    if (MessageBox.Show("Save?", "Exit", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        SaveData();
                }
            }
                else
                    e.Cancel = true;
        }

        // in solution explorer go to db.mdb and choose the "Copy to Output Directory" property
        //then change it to "copy if newer"
        private void SaveData()
        {
            if (this.Validate())
            {
                try
                {
                    this.tbTeacherBindingSource.EndEdit();
                    this.tableAdapterManager.UpdateAll(this.dbDataSet);
                    MessageBox.Show("Saved");
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
           
            }
            else
                MessageBox.Show("Fix errors first!");
        }


        //Move buttons option
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

        //enabling and disabling it
        private void EnableDisableButtons()
        {
            if (tbTeacherBindingSource.Position == 0)
            {
                btnFirst.Enabled = false;
                btnPrevious.Enabled = false;

            }
            else
            {
                btnFirst.Enabled =true;
                btnPrevious.Enabled = true;

            }

            if (tbTeacherBindingSource.Position == tbTeacherBindingSource.Count -1)
            {
                btnNext.Enabled = false;
                btnLast.Enabled = false;
            }
            else
            {
                btnNext.Enabled = true;
                btnLast.Enabled = true;
            }
        }

        //in order to turn on enable and disable buttons,
        //I have to go DESIGN and choose tbTeacherBindingSource and thunder icon then find CurrentChanged property then double click,
        //as well as write the code below
        private void tbTeacherBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            EnableDisableButtons();
        }


        //Delete button
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (tbTeacherBindingSource.Count == 0)
                MessageBox.Show("No more records");

            else
            {
            var userResponse = MessageBox.Show("Are you sure?", "Delete", MessageBoxButtons.YesNo);
                if (userResponse == DialogResult.Yes)
            tbTeacherBindingSource.RemoveCurrent();
            }
        }


        //add new record
        private void btnAdd_Click(object sender, EventArgs e)
        {
            var selectedCountry = ((DataRowView) cbxNewCountry.SelectedItem).Row;
            dbDataSet.tbTeacher.AddtbTeacherRow(
                tbxNewFirstName.Text,
                tbxNewLastName.Text,
                dtpNewDob.Value,
                tbxNewPhone.Text,
                (int)nudGradeLevel.Value,
                cbNewIsActive.Checked,
                (dbDataSet.tbCountryRow) selectedCountry);
            tbxNewFirstName.Text = " ";
            tbxNewLastName.Text = " ";
            dtpNewDob.Value = DateTime.Now;
            tbxNewPhone.Text = " ";
            nudGradeLevel.Value = 0;
            cbNewIsActive.CheckState = 0;
            cbxNewCountry.Text = " ";
            MessageBox.Show("Your record is added!");
        }


        //Filter stuff

        private void tbxFilter_TextChanged(object sender, EventArgs e)
        {
            tbTeacherBindingSource.Filter = $"firstName LIKE '{tbxFilter.Text}%'";
        }


        //fistname validating
        private void firstNameTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(firstNameTextBox.Text))
            {
                MessageBox.Show("Your first name cannot be empty");
                e.Cancel = true;
            }    
        }
    }
}
