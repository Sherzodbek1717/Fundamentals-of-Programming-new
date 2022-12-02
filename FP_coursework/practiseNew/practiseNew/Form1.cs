using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace practiseNew
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void tbTeacherBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            //this.Validate();
            //this.tbTeacherBindingSource.EndEdit();
            //this.tableAdapterManager.UpdateAll(this.dbDataSet);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
           if (this.Validate())
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
                btnLast.Enabled = false;
                btnNext.Enabled = false;
            }
            else
            {
                btnLast.Enabled = true;
                btnNext.Enabled = true;
            }
        }

        private void tbTeacherBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            EnableDisableButtons();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (tbTeacherBindingSource.Count == 0)
            {
                MessageBox.Show("No more records");
            }
            else
            {
                var userResponse = MessageBox.Show("Do you really want to delete this item?", "Delete", MessageBoxButtons.YesNo);
                if (userResponse == DialogResult.Yes)
                {
                    tbTeacherBindingSource.RemoveCurrent();
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void SaveData()
        {
            if (this.Validate())
            {
                try
                {
                    this.tbTeacherBindingSource.EndEdit();
                    this.tableAdapterManager.UpdateAll(this.dbDataSet);
                    MessageBox.Show("Your changes are saved!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            tbTeacherBindingSource.Filter = $" firstName LIKE '{textBox1.Text}'";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var selectedCountry = ((DataRowView)NewCountry.SelectedItem).Row;
            dbDataSet.tbTeacher.AddtbTeacherRow(NewFirstName.Text,
                NewLastName.Text,
                NewDateOfBirth.Value,
                NewPhone.Text,
                (int)NewGradeLevel.Value,
                NewActive.Checked,
                (dbDataSet.tbCountryRow)selectedCountry);
            NewFirstName.Text = " ";
            NewLastName.Text = " ";
            NewDateOfBirth.Value = DateTime.Today;
            NewPhone.Text = " ";
            NewGradeLevel.Value = 0;
            NewActive.CheckState = 0;
            NewCountry.Text = " ";
            MessageBox.Show("Your fucking record is added, Bitch!");
        }

        private void NewFirstName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(firstNameTextBox.Text))
            {
                MessageBox.Show("Your first name cannot be empty");
                e.Cancel = true;
            }
        }
    }
}
