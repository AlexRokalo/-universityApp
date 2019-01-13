using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csharp_student_information_system
{
    class COURSE
    {


        MY_DB mydb = new MY_DB();


        //  function to insert a new course
    public bool insertCourse(string courseName, int hoursNumber, string description) {
        
        MySqlCommand command = new MySqlCommand("INSERT INTO `course`(`label`, `hours_number`, `description`) VALUES (@name,@hrs,@descr)", mydb.getConnection);
       
        command.Parameters.Add("@name", MySqlDbType.VarChar).Value = courseName;
        command.Parameters.Add("@hrs", MySqlDbType.Int32).Value = hoursNumber;
        command.Parameters.Add("@descr", MySqlDbType.VarChar).Value = description;

        mydb.openConnection();

        if ((command.ExecuteNonQuery() == 1)) {
            return true;
        }
        else {
            return false;
        }
        
    }

    //  function to uodate a course data
    public bool updateCourse(int courseID, string courseName, int hoursNumber, string description) {
        
        MySqlCommand command = new MySqlCommand("UPDATE `course` SET `label`=@name,`hours_number`=@hrs,`description`=@descr WHERE `id` = @cid", mydb.getConnection);
        
        command.Parameters.Add("@cid", MySqlDbType.Int32).Value = courseID;
        command.Parameters.Add("@name", MySqlDbType.VarChar).Value = courseName;
        command.Parameters.Add("@hrs", MySqlDbType.Int32).Value = hoursNumber;
        command.Parameters.Add("@descr", MySqlDbType.VarChar).Value = description;
       
        mydb.openConnection();
        
        if ((command.ExecuteNonQuery() == 1)) {
            return true;
        }
        else {
            return false;
        }
        
    }


    //  function to remove a course by id
    public bool deleteCourse(int courseID) {
       
        MySqlCommand command = new MySqlCommand("DELETE FROM `course` WHERE `id` = @CID", mydb.getConnection);
       
        command.Parameters.Add("@CID", MySqlDbType.Int32).Value = courseID;
       
        mydb.openConnection();
       
        if ( command.ExecuteNonQuery() == 1 ) {
            return true;
        }
        else {
            return false;
        }
        
    }

    // function to check if the course name already exists in the database
    public bool checkCourseName(string courseName, int courseId = 0) {
       
        //  id <> @cID mean when the id is diffrent of the current course id
        // like if we want to edit only the course description
        MySqlCommand command = new MySqlCommand("SELECT * FROM course WHERE label=@cName And id <> @cID", mydb.getConnection);
       
        command.Parameters.Add("@cName", MySqlDbType.VarChar).Value = courseName;
        command.Parameters.Add("@cID", MySqlDbType.Int32).Value = courseId;
       
        MySqlDataAdapter adapter = new MySqlDataAdapter(command);
       
        DataTable table = new DataTable();
        
        adapter.Fill(table);
       
        if ((table.Rows.Count > 0)) {
            //  this course name aleready exists
            return false;
        }
        else {
            return true;
        }
        
    }

    //  function to get all courses from the database
    public DataTable getAllCourses() {
       
        MySqlCommand command = new MySqlCommand("SELECT * FROM course", mydb.getConnection);
       
        MySqlDataAdapter adapter = new MySqlDataAdapter(command);
       
        DataTable table = new DataTable();
       
        adapter.Fill(table);
        
        return table;
    }


    //  function to return a course by id
    public DataTable getCourseById(int courseID) {
       
        MySqlCommand command = new MySqlCommand("SELECT * FROM course WHERE id = @cid", mydb.getConnection);
       
        command.Parameters.Add("@cid", MySqlDbType.VarChar).Value = courseID;
       
        MySqlDataAdapter adapter = new MySqlDataAdapter(command);
       
        DataTable table = new DataTable();
       
        adapter.Fill(table);
       
        return table;
    }


    //  function to execute the count query
    string execCount(string query) {
        MySqlCommand command = new MySqlCommand(query, mydb.getConnection);
        mydb.openConnection();
        String count = command.ExecuteScalar().ToString();
        mydb.closeConnection();
        return count;
    }
    
    //  function to return the total courses in the database
    public string totalCourses() {
        return execCount("SELECT COUNT(*) FROM `course`");
    }


    }
}
