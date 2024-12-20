using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BaiTap_6.Model;

namespace BaiTap_6
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
                Model1 context = new Model1();
                List<Facutly> listFacultys = context.Facutly.ToList();
                List<student> listStudents = context.student.ToList();
                FillFacultyCombobox(listFacultys);
                BindGrid(listStudents);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}");
            }
        }

        private void FillFacultyCombobox(List<Facutly> listFacultys)
        {
            cmbFaculty.DataSource = listFacultys;
            cmbFaculty.DisplayMember = "FacultyID"; 
            cmbFaculty.ValueMember = "FacultyName";      
        }

        private void BindGrid(List<student> listStudents)
        {
            dgvStudent.Rows.Clear();
            foreach (var item in listStudents)
            {
                int index = dgvStudent.Rows.Add();
                dgvStudent.Rows[index].Cells[0].Value = item.studentID;
                dgvStudent.Rows[index].Cells[1].Value = item.FullName;
                dgvStudent.Rows[index].Cells[2].Value = item.FacultyID; 
                dgvStudent.Rows[index].Cells[3].Value = item.AverageScore;
            }
        }

        private void dgvStudent_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0) 
                {
                    DataGridViewRow row = dgvStudent.Rows[e.RowIndex];

                    txtStudentID.Text = row.Cells[0].Value.ToString();
                    txtFullName.Text = row.Cells[1].Value.ToString();
                    cmbFaculty.SelectedValue = row.Cells[2].Value.ToString(); 
                    txtAverageScore.Text = row.Cells[3].Value.ToString();

                    // Hiển thị GroupBox
                    groupBoxStudentDetails.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error selecting student: {ex.Message}");
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                Model1 context = new Model1();
                int studentID = int.Parse(txtStudentID.Text);

                if (context.student.Any(s => s.studentID == studentID))
                {
                    MessageBox.Show("Student ID already exists. Please use a different ID.");
                    return;
                }

                student newStudent = new student()
                {
                    studentID = studentID,
                    FullName = txtFullName.Text,
                    FacultyID = cmbFaculty.SelectedValue.ToString(),
                    AverageScore = txtAverageScore.Text
                };

                context.student.Add(newStudent);
                context.SaveChanges();

                MessageBox.Show("Student added successfully!");
                BindGrid(context.student.ToList());
            }
            catch (FormatException)
            {
                MessageBox.Show("Invalid input format. Please check your data.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding student: {ex.Message}");
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                Model1 context = new Model1();
                int studentID = int.Parse(txtStudentID.Text);
                student studentToUpdate = context.student.FirstOrDefault(s => s.studentID == studentID);

                if (studentToUpdate != null)
                {
                    studentToUpdate.FullName = txtFullName.Text;
                    studentToUpdate.FacultyID = cmbFaculty.SelectedValue.ToString();
                    studentToUpdate.AverageScore = txtAverageScore.Text;

                    context.SaveChanges();

                    MessageBox.Show("Student updated successfully!");
                    BindGrid(context.student.ToList());
                }
                else
                {
                    MessageBox.Show("Student not found.");
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Invalid input format. Please check your data.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating student: {ex.Message}");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                using (Model1 context = new Model1())
                {
                    int selectedID = int.Parse(txtStudentID.Text);

                    student studentToDelete = context.student.FirstOrDefault(s => s.studentID == selectedID);

                    if (studentToDelete != null)
                    {
                        context.student.Remove(studentToDelete);
                        context.SaveChanges();

                        MessageBox.Show("Student deleted successfully!");
                        BindGrid(context.student.ToList());
                    }
                    else
                    {
                        MessageBox.Show("Student not found.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting student: {ex.Message}");
            }
        }

        private void dgvStudent_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0) // Đảm bảo chọn hàng hợp lệ
                {
                    DataGridViewRow row = dgvStudent.Rows[e.RowIndex];

                    txtStudentID.Text = row.Cells[0].Value.ToString();
                    txtFullName.Text = row.Cells[1].Value.ToString();
                    cmbFaculty.SelectedValue = row.Cells[2].Value.ToString();
                    txtAverageScore.Text = row.Cells[3].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}");
            }
        }
    
    }
}
