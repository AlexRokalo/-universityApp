using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csharp_student_information_system
{
    class MY_DB
    {

        // the connection
        MySqlConnection con = new MySqlConnection("datasource=localhost;port=3306;username=root;password=;database=student_db");


        // get the connection
        public MySqlConnection getConnection
        {
            get
            {
                return con;
            }
        }


        // open the connection
       public void openConnection()
        {
            if ((con.State == ConnectionState.Closed))
            {
                con.Open();
            }

        }


        // close the connection
       public void closeConnection()
        {
            if ((con.State == ConnectionState.Open))
            {
                con.Close();
            }

        }

    }
}
