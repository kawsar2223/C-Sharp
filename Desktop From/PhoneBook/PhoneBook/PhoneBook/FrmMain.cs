using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Windows.Forms;
using PhoneBook.DbContext;

namespace PhoneBook
{
    public partial class FrmMain : Form
    {
        private int _id;
        public FrmMain()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (btnSave.Text.Equals("Save"))
            {
                var result = Person.Save(txtName.Text, txtMobile1.Text, txtMobile2.Text, txtMobile3.Text, txtEmail.Text, txtAddress.Text);
                if (result)
                {
                    LoadPerson();
                    ResetForm();
                    MessageBox.Show(@"Saved successfully", "Phone Book", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show(@"Ooops! Something is not right!", "Phone Book", MessageBoxButtons.OK, MessageBoxIcon.Error);


            }
            else
            {
                var result = Person.Update(_id, txtName.Text, txtMobile1.Text, txtMobile2.Text, txtMobile3.Text, txtEmail.Text, txtAddress.Text);
                if (result)
                {
                    LoadPerson();
                    ResetForm();
                    MessageBox.Show(@"Updated successfully", "Phone Book", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show(@"Ooops! Something is not right!", "Phone Book", MessageBoxButtons.OK, MessageBoxIcon.Error);


            }


        }

        private void ResetForm()
        {
            txtName.Clear();
            txtMobile1.Clear();
            txtMobile2.Clear();
            txtMobile3.Clear();
            txtEmail.Clear();
            txtAddress.Clear();
            txtName.Focus();
            _id = 0;
            btnSave.Text = @"Save";
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            dgvPerson.AutoGenerateColumns = false;
            LoadPerson();

        }

        private void LoadPerson()
        {
            var data = Person.Get();
            dgvPerson.DataSource = data;
        }


        private void Delete(int id)
        {
            var result = Person.Delete(id);
            if (result)
            {

                MessageBox.Show(@"Deleted successfully", @"Phone Book", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadPerson();
            }
            else
                MessageBox.Show(@"Ooops! Something is not right!", @"Phone Book", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void dgvPerson_DoubleClick(object sender, EventArgs e)
        {
            var cr = dgvPerson.CurrentRow;
            if (cr == null) return;



            _id = Convert.ToInt32(cr.Cells["colId"].Value);
            txtName.Text = cr.Cells["colName"].Value.ToString();
            txtMobile1.Text = cr.Cells["colMobile1"].Value.ToString();
            txtMobile2.Text = cr.Cells["colMobile2"].Value.ToString();
            txtMobile3.Text = cr.Cells["colMobile3"].Value.ToString();
            txtEmail.Text = cr.Cells["colEmail"].Value.ToString();
            txtAddress.Text = cr.Cells["colAddress"].Value.ToString();
            btnSave.Text = @"Update";

        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetForm();
        }

        private void dgvPerson_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.ColumnIndex != 7) return;
            var cr = dgvPerson.CurrentRow;
            if (cr == null) return;
            var id = Convert.ToInt32(cr.Cells["colId"].Value);

            var confirm = MessageBox.Show("Are you sure want to delete?", "Phone Book", MessageBoxButtons.YesNo,
                              MessageBoxIcon.Question) == DialogResult.Yes;
            if (confirm)
                Delete(id);
        }
    }
}
