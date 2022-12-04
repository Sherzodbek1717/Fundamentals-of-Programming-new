# Fundamentals-of-Programming-new

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace week11
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void tbTeacherBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            SaveData();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveData();
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.Validate())
            {
                this.tbTeacherBindingSource.EndEdit();
                if (dbDataSet.HasChanges())
                {
                    if (MessageBox.Show("Save?", "Exit",
                        MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        SaveData();
                    }
                }    
            }
            else
                e.Cancel = true;
        }
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
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }
            else
                MessageBox.Show("Fix it!");
           
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

        private void EnableDisableButtons()
        {
            if (tbTeacherBindingSource.Position == 0)
            {
                btnFirst.Enabled = false;
                btnPrevious.Enabled = false;

            }

            else
            {
                btnFirst.Enabled = true;
                btnPrevious.Enabled = true;
            }

            if (tbTeacherBindingSource.Position == tbTeacherBindingSource.Count - 1)
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

        private void tbTeacherBindingSource_CurrentItemChanged(object sender, EventArgs e)
        {
            EnableDisableButtons();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (tbTeacherBindingSource.Count == 0)
                MessageBox.Show("Nothing to Delete");
            else
            {
                var userResponse = MessageBox.Show("Are you Sure?", "Delete", MessageBoxButtons.YesNo);
                if (userResponse == DialogResult.Yes)

                tbTeacherBindingSource.RemoveCurrent();
            }
            
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            var selectedCountry = ((DataRowView) cbxNewCountry.SelectedItem).Row;
            dbDataSet.tbTeacher.AddtbTeacherRow(
                tbxNewFirstName.Text,
                tbxNewLastName.Text,
                dtpNewDob.Value,
                tbxNewPhone.Text,
                (int)nudNewGrade.Value,
                chbNewIsActive.Checked,
                (dbDataSet.tbCountryRow)selectedCountry);


        }

        private void tbxFilter_TextChanged(object sender, EventArgs e)
        {
            tbTeacherBindingSource.Filter = $"firstName LIKE ('{tbxFilter.Text}%')";
        }

        private void firstNameTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(firstNameTextBox.Text))
            {
                MessageBox.Show("First Name cannot be empty!");
                    e.Cancel = true;
            }    
        }
    }
}



