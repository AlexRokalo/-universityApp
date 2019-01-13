using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csharp_student_information_system
{
    class SCORE
    {


        MY_DB mydb = new MY_DB();


   
        public bool insertScore(int studentID, int courseID, double scoreValue, string description)
        {
            MySqlCommand command = new MySqlCommand("INSERT INTO `score`(`student_id`, `course_id`, `student_score`, `description`) VALUES (@sid,@cid,@scr" +
                ",@descr)", mydb.getConnection);
            
            command.Parameters.Add("@sid", MySqlDbType.Int32).Value = studentID;
            command.Parameters.Add("@cid", MySqlDbType.Int32).Value = courseID;
            command.Parameters.Add("@scr", MySqlDbType.Double).Value = scoreValue;
            command.Parameters.Add("@descr", MySqlDbType.VarChar).Value = description;
            
            mydb.openConnection();
            
            if ((command.ExecuteNonQuery() == 1))
            {
                return true;
            }
            else
            {
                return false;
            }

        }


  
        public bool studentScoreExist(int studentId, int courseID)
        {
            MySqlCommand command = new MySqlCommand("SELECT * FROM `score` WHERE `student_id` = @sid AND `course_id` = @cid", mydb.getConnection);
            
            command.Parameters.Add("@sid", MySqlDbType.Int32).Value = studentId;
            command.Parameters.Add("@cid", MySqlDbType.Int32).Value = courseID;
            
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            
            adapter.Fill(table);
            
            if ((table.Rows.Count == 0))
            {
                return false;
            }
            else
            {
                return true;
            }

        }


        public DataTable getAvgScoreByCourse()
        {
            MySqlCommand command = new MySqlCommand();
            
            command.Connection = mydb.getConnection;
            command.CommandText = "SELECT course.label, AVG(score.student_score) as 'Average Score' FROM course, score WHERE course.id =" +
            " score.course_id GROUP BY course.label";
            
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            
            DataTable table = new DataTable();
            
            adapter.Fill(table);
            
            return table;
        }


       public DataTable getStudentsScore()
        {
            MySqlCommand command = new MySqlCommand();
            
            command.Connection = mydb.getConnection;
            command.CommandText = ("SELECT SCORE.student_id, STUDENT.first_name, STUDENT.last_name, SCORE.course_id, COURSE.label, SCORE." +
            "student_score FROM STUDENT INNER JOIN score on student.id = score.student_id INNER JOIN course on score.course_id = course.id");
            
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            
            DataTable table = new DataTable();
            
            adapter.Fill(table);
            
            return table;
        }


       public DataTable getCourseScores(int courseId)
       {
           MySqlCommand command = new MySqlCommand();
           
           command.Connection = mydb.getConnection;
           command.CommandText = ("SELECT SCORE.student_id, STUDENT.first_name, STUDENT.last_name, SCORE.course_id, COURSE.label, SCORE." +
           "student_score FROM STUDENT INNER JOIN score on student.id = score.student_id INNER JOIN course on score.course_id = course.id WHERE score.course_id = " + courseId);
           
           MySqlDataAdapter adapter = new MySqlDataAdapter(command);
           
           DataTable table = new DataTable();
           
           adapter.Fill(table);
           
           return table;
       }


        // get student's scores by id
       public DataTable getStudentScores(int studentId)
       {
           MySqlCommand command = new MySqlCommand();
           
           command.Connection = mydb.getConnection;
           command.CommandText = ("SELECT SCORE.student_id, STUDENT.first_name, STUDENT.last_name, SCORE.course_id, COURSE.label, SCORE." +
           "student_score FROM STUDENT INNER JOIN score on student.id = score.student_id INNER JOIN course on score.course_id = course.id WHERE score.student_id = " + studentId);
           
           MySqlDataAdapter adapter = new MySqlDataAdapter(command);
           
           DataTable table = new DataTable();
           
           adapter.Fill(table);
           
           return table;
       }


        // delete score using student id, and course id
        public bool deleteScore(int studentID, int courseID)
        {
            MySqlCommand command = new MySqlCommand("DELETE FROM `score` WHERE `student_id` = @sid AND course_id = @cid", mydb.getConnection);
            
            command.Parameters.Add("@sid", MySqlDbType.Int32).Value = studentID;
            command.Parameters.Add("@cid", MySqlDbType.Int32).Value = courseID;
            
            mydb.openConnection();
            
            if ( command.ExecuteNonQuery() == 1 )
            {
                return true;
            }
            else
            {
                return false;
            }

        }


    }
}
