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
    public partial class frmStudentdetail : Form
    {

        ConnectwithDatabase con = new ConnectwithDatabase();
        List<int> studentId = new List<int>();
        public frmStudentdetail()
        {
            InitializeComponent();
            this.fillgrid();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.Close();
            var form = new frmStudentRegistration();
            form.Show();
        }

        public void fillgrid()
        {
            DataTable dt = con.getStudentDtl();
            dataGridView1.DataSource = dt;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                bool ischange = false;
                List<int> student = new List<int>();
                if (studentId != null)
                    student = studentId.Distinct().ToList();

                for (int x = 0; x < dataGridView1.Rows.Count-1; x++)
                {
                    //if (dataGridView1.Rows[x].Cells[1].Value != DBNull.Value)
                    //{
                        string value = dataGridView1.Rows[x].Cells[1].Value.ToString();
                        if (string.IsNullOrEmpty(value))
                        {
                            bool isActice = false;
                            DataGridViewCheckBoxCell checkbox = dataGridView1.Rows[x].Cells[5] as DataGridViewCheckBoxCell;
                            if (checkbox != null)
                            {
                                if (checkbox.Selected)
                                    isActice = true;
                            }

                            string name = dataGridView1.Rows[x].Cells[2].Value.ToString();
                            string date = dataGridView1.Rows[x].Cells[3].Value != DBNull.Value ? dataGridView1.Rows[x].Cells[3].Value.ToString() : System.DateTime.Now.ToString();
                            string avg = dataGridView1.Rows[x].Cells[4].Value != DBNull.Value ? dataGridView1.Rows[x].Cells[4].Value.ToString() : "0.34";
                            if (!string.IsNullOrEmpty(name))
                            {
                                con.Insertdata(name, Convert.ToDateTime(date), Convert.ToDouble(avg), isActice);
                                ischange = true;
                            }
                            else
                            {
                                MessageBox.Show("Name cannot be Empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                                return;
                            }
                        }

                        else
                        {
                            foreach (var row in student)
                            {
                                if (row == Convert.ToInt32(dataGridView1.Rows[x].Cells[1].Value.ToString()))
                                {
                                    bool isActice = false;
                                    DataGridViewCheckBoxCell checkbox = dataGridView1.CurrentCell as DataGridViewCheckBoxCell;
                                    if (checkbox != null)
                                    {
                                        if (checkbox.Selected)
                                            isActice = true;
                                    }

                                    con.Updatedata(Convert.ToInt32(row), dataGridView1.Rows[x].Cells[2].Value.ToString(), Convert.ToDateTime(dataGridView1.Rows[x].Cells[3].Value.ToString()), Convert.ToDouble(dataGridView1.Rows[x].Cells[4].Value.ToString()), isActice);
                                    ischange = true;
                                    student.Remove(row);
                                    break;
                                }


                            }
                        }
                //    }
                }
                if (ischange)
                    MessageBox.Show("Save success.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                this.fillgrid();
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1); }
        }
        DateTimePicker picker = new DateTimePicker();
        NumericUpDown numeric = new NumericUpDown();

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.ColumnIndex == 3)
            {


                dataGridView1.Controls.Add(picker);
                picker.Format = DateTimePickerFormat.Short;
                Rectangle oRectangle = dataGridView1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                picker.Size = new Size(oRectangle.Width, oRectangle.Height);
                picker.Location = new Point(oRectangle.X, oRectangle.Y);
                picker.TextChanged += new EventHandler(picker_OnTextChange);

            }
            if (e.ColumnIndex == 4)
            {
                dataGridView1.Controls.Add(numeric);
                numeric.Value = Convert.ToDecimal("0.34");
                numeric.Maximum = Convert.ToDecimal("2.05");
                numeric.Minimum = Convert.ToDecimal("0.34");
                numeric.Increment = Convert.ToDecimal("0.01");
                numeric.DecimalPlaces = 2;
                Rectangle oRectangle = dataGridView1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                numeric.Size = new Size(oRectangle.Width, oRectangle.Height);
                numeric.Location = new Point(oRectangle.X, oRectangle.Y);
                numeric.ValueChanged += new EventHandler(numeric_change);

            }

            if (dataGridView1.Rows[e.RowIndex].Cells[1].Value != DBNull.Value)
            {
                studentId.Add(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[1].Value));
            }


        }
        private void picker_OnTextChange(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell.Value = picker.Text.ToString();
        }

        private void numeric_change(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell.Value = numeric.Value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();         
        }

    }
}
