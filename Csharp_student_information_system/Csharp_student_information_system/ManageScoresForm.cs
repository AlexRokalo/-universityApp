using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Csharp_student_information_system
{
    public partial class ManageScoresForm : Form
    {
        public ManageScoresForm()
        {
            InitializeComponent();
        }

        SCORE score = new SCORE();
        STUDENT student = new STUDENT();
        COURSE course = new COURSE();
        string data = "score";

        // on form load
        private void ManageScoresForm_Load(object sender, EventArgs e)
        {
         
            DataGridViewStudentsScore.DataSource = score.getStudentsScore();
            ComboBoxCourse.DataSource = course.getAllCourses();
            ComboBoxCourse.DisplayMember = "label";
            ComboBoxCourse.ValueMember = "id";
        }


      
        private void ButtonShowScores_Click(object sender, EventArgs e)
        {
            data = "score";
            DataGridViewStudentsScore.DataSource = score.getStudentsScore();
        }


        // button to display students data on the datagridview
        private void ButtonShowStudents_Click(object sender, EventArgs e)
        {
            data = "student";
            MySqlCommand command = new MySqlCommand("SELECT `id`, `first_name`, `last_name`, `birthdate` FROM `student`");
            DataGridViewStudentsScore.DataSource = student.getStudents(command);
        }

        void getDataFromDatagridView()
        {
         
            if ( data == "student" )
            {
                TextBoxStudentID.Text = DataGridViewStudentsScore.CurrentRow.Cells[0].Value.ToString();
          
            }
            else if ( data == "score" )
            {
                TextBoxStudentID.Text = DataGridViewStudentsScore.CurrentRow.Cells[0].Value.ToString();
                ComboBoxCourse.SelectedValue = DataGridViewStudentsScore.CurrentRow.Cells[3].Value;
            }

        }

    
        private void DataGridViewStudentsScore_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            getDataFromDatagridView();
        }


       
        private void ButtonAvgScore_Click(object sender, EventArgs e)
        {
            avgScoreByCourseForm avgScoreByCourseF = new avgScoreByCourseForm();
            avgScoreByCourseF.Show(this);
        }



        private void ButtonAddScore_Click(object sender, EventArgs e)
        {

            try
            {
                int studentID = Convert.ToInt32(TextBoxStudentID.Text);
                int courseID = Convert.ToInt32(ComboBoxCourse.SelectedValue);
                double scoreValue = Convert.ToDouble(TextBoxScore.Text);
                string description = TextBoxDescription.Text;

             
                if (!score.studentScoreExist(studentID, courseID))
                {
                    if (score.insertScore(studentID, courseID, scoreValue, description))
                    {
                        MessageBox.Show("Student Score Inserted", "Add Score", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Student Score Not Inserted", "Add Score", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                }
                else
                {
                    MessageBox.Show("The Score For This Course Are Already Set", "Add Score", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Add Score", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        
        private void ButtonRemoveScore_Click(object sender, EventArgs e)
        {

            int studentID = int.Parse(DataGridViewStudentsScore.CurrentRow.Cells[0].Value.ToString());
            int courseID = int.Parse(DataGridViewStudentsScore.CurrentRow.Cells[3].Value.ToString());

            if (MessageBox.Show("Do Want To Delete This Score", "Delete Score", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (score.deleteScore(studentID, courseID))
                {
                    MessageBox.Show("Score Deleted", "Delete Score", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DataGridViewStudentsScore.DataSource = score.getStudentsScore();
                }
                else
                {
                    MessageBox.Show("Score Not Deleted", "Delete Score", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }

        }

    }
}
