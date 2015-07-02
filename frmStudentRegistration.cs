using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sagment5
{
    public partial class frmStudentRegistration : Form
    {

        ConnectwithDatabase con = new ConnectwithDatabase();
        public frmStudentRegistration()
        {
            InitializeComponent();
            this.UpdateStudentId();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            var form1 = new frmStudentdetail();
            form1.InitializeLifetimeService();
            form1.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if(!string.IsNullOrEmpty(textBox2.Text))
                {
                    bool isActive=false;
                    if(checkBox1.Checked)
                        isActive=true;
                    if (MessageBox.Show("Are you sure Want to save Record .?", "Save Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                    {
                        con.Insertdata(textBox2.Text, Convert.ToDateTime(dateTimePicker1.Text), Convert.ToDouble(numericUpDown1.Value), isActive);
                        this.clear();
                        MessageBox.Show("Save success.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                   
                    }
                }
                else
                {
                    MessageBox.Show("Name cannot be empty.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        public void UpdateStudentId()
        {
            int Id = con.getID();
            textBox1.Text = Id.ToString();
        }
        public void clear()
        {
            int Id = con.getID();
            textBox1.Text = Id.ToString();
            textBox2.Text = "";
            checkBox1.Checked = false;
            dateTimePicker1.Text = System.DateTime.Now.ToString(); 
            
        }

    }
}
