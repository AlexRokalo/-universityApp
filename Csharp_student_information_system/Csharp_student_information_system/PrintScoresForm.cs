using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Csharp_student_information_system
{
    public partial class PrintScoresForm : Form
    {
        public PrintScoresForm()
        {
            InitializeComponent();
        }

        SCORE score = new SCORE();
        COURSE course = new COURSE();
        STUDENT student = new STUDENT();

        private void PrintScoresForm_Load(object sender, EventArgs e)
        {
            
            DataGridViewStudentsScore.DataSource = score.getStudentsScore();

        
            dataGridView1.DataSource = student.getStudents(new MySqlCommand("Select id, first_name, last_name FROM `student`"));

          
            ListBoxCourses.DataSource = course.getAllCourses();
            ListBoxCourses.ValueMember = "id";
            ListBoxCourses.DisplayMember = "label";
        }



        private void ListBoxCourses_Click(object sender, EventArgs e)
        {
            DataGridViewStudentsScore.DataSource = score.getCourseScores(int.Parse(ListBoxCourses.SelectedValue.ToString()));
        }


        private void dataGridView1_Click(object sender, EventArgs e)
        {
            DataGridViewStudentsScore.DataSource = score.getStudentScores(int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString()));
        }



        private void ButtonPrint_Click(object sender, EventArgs e)
        {
      
            String path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\scores_list.txt";

            using (var writer = new StreamWriter(path))
            {
       
                if (!File.Exists(path))
                {
                    File.Create(path);
                }

          
                for (int i = 0; i < DataGridViewStudentsScore.Rows.Count; i++)
                {
                    for (int j = 0; j < DataGridViewStudentsScore.Columns.Count; j++)
                    {
                        writer.Write("\t" + DataGridViewStudentsScore.Rows[i].Cells[j].Value.ToString() + "\t" + "|");
                    }
                    writer.WriteLine("");
                    writer.WriteLine("-----------------------------------------------------------------------------------------------------------");
                }
                writer.Close();
                MessageBox.Show("Data Exported");

            }
        }

        private void labelReset_Click(object sender, EventArgs e)
        {
           
            DataGridViewStudentsScore.DataSource = score.getStudentsScore();
        }
    }
}
