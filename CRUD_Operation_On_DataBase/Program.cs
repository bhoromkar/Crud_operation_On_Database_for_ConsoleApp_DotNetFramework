using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Security.Principal;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.Remoting.Messaging;

namespace CRUD_Operation_On_DataBase
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string info = @"server = HP\SQLEXPRESS;
                            database= HospitaldB; 
                            integrated security = sspi; 
                            Trusted_Connection = true";
            SqlConnection conn = new SqlConnection(info);
       
          /*insert_data(conn);
            
            
            
            getByAge(conn);
            updatedata(conn);
            errorcolumn(conn);
          */
            deletedata(conn);
            
           // display_table(conn);
              
            
          
            


        }
        public static void insert_data(SqlConnection conn)
        {

            conn.Open();
            SqlCommand insertCommand = new SqlCommand("insert into patient(PatientName,Age,Gender,PhoneNumber,PatientAddress,BloodGroup) values ('dsn', '45','male', '9256654885','nkfjsdlvsdml','a+')", conn);
            insertCommand.ExecuteNonQuery();
            Console.WriteLine("data inserted successfully");

            SqlCommand insertwithparameter = new SqlCommand("insert into patient(PatientName,Age,Gender,PhoneNumber,PatientAddress,BloodGroup) values(@0,@1,@2,@3,@4,@5)", conn);
            insertwithparameter.Parameters.Add(new SqlParameter("0", "keshav"));
            insertwithparameter.Parameters.Add(new SqlParameter("1", "25"));
            insertwithparameter.Parameters.Add(new SqlParameter("2", "male"));
            insertwithparameter.Parameters.Add(new SqlParameter("3", "51654845"));
            insertwithparameter.Parameters.Add(new SqlParameter("4", "z-jndkn-nmnnv"));
            insertwithparameter.Parameters.Add(new SqlParameter("5", "o"));
            Console.WriteLine("Commands executed! Total rows affected are " + insertwithparameter.ExecuteNonQuery());
            conn.Close();
        }
            public static void getByAge(SqlConnection conn)
        {  conn.Open(); 
            Console.WriteLine("person with age > 30");
            SqlCommand getPatientAge = new SqlCommand("select * from patient where age>@Age  ", conn);
            getPatientAge.Parameters.Add(new SqlParameter("Age", 30));
            getPatientAge.ExecuteNonQuery();

            SqlDataReader reader = getPatientAge.ExecuteReader();
            while (reader.Read())
            {


                string id = reader["patientID"].ToString();
                string firstName = reader["PatientName"].ToString();
                string age = reader["Age"].ToString();
                string gender = reader["Gender"].ToString();
                string PhoneNumber = reader["PhoneNumber"].ToString();
                string PatientAddress = reader["PatientAddress"].ToString();
                string bloodgroup = reader["BloodGroup"].ToString();
                Console.WriteLine(id + " " + firstName + "  " + age + "  " + gender + "  " + PhoneNumber + "  " + PatientAddress + " " + bloodgroup);

            }
            Console.WriteLine("person with age > 30");
           reader.Close();
            conn.Close();
        }  
        //exception not working
        public static void deletedata(SqlConnection conn)
            
        {
            conn.Open();
            try
            {
                SqlCommand deletedata = new SqlCommand("delete from patient where PatientName='bdjfb'", conn);
                deletedata.ExecuteNonQuery();
               // Console.WriteLine("data deleted succesfully");
                Console.WriteLine();
            }
            catch (SqlException er)
            {
                Console.WriteLine("item deleted already! , "+ er.Message);
            }
            finally
            {
                conn.Close();

            }



        }

        public static void updatedata(SqlConnection conn)
        {
            conn.Open();
            try
            {
                SqlCommand updatedata = new SqlCommand("update from patient set age = '20' where PatientName='abc' ", conn);
                updatedata.ExecuteNonQuery();
                Console.WriteLine("data updated succesfully");

            }
            catch (SqlException er)
            {
                Console.WriteLine("The executable query is incorect, "+ er.Message);
            }
            finally 
            { 
            conn.Close();

        }
        }

        public static void errorcolumn(SqlConnection conn)
        { conn.Open();  
            try
            {  
                SqlCommand errorCommand = new SqlCommand("SELECT * FROM someErrorColumn", conn);
               
                errorCommand.ExecuteNonQuery();

            }
          
            catch (SqlException er)
            {
                 
                Console.WriteLine("There was an error reported by SQL Server, " + er.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        
          public static void display_table(SqlConnection conn) { 
          conn.Open();

           SqlCommand cmd1 = new SqlCommand("select * from patient", conn);
            SqlDataReader reader1 = cmd1.ExecuteReader(); 
            while (reader1.Read())
            {

            
                string id = reader1["patientID"].ToString();    
                string firstName= reader1["PatientName"].ToString();
                string age = reader1["Age"].ToString() ;
                string gender = reader1["Gender"].ToString();
                string PhoneNumber = reader1["PhoneNumber"].ToString();
                string PatientAddress = reader1["PatientAddress"].ToString();
                 string bloodgroup = reader1["BloodGroup"].ToString();
                Console.WriteLine(id + " " + firstName + "  " + age + "  " + gender + "  " + PhoneNumber + "  " + PatientAddress + " " + bloodgroup);

            }
            Console.WriteLine( "data feched succesfully");
           
            reader1.Close();    
            conn.Close();
            Console.Read();

        }
    }
}
