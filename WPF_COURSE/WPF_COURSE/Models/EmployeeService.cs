using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WPF_COURSE.Models
{
    public class EmployeeService
    {
        // Create the SQL Connection, the SQL Command and the connection string
        SqlConnection ObjSqlConnection;
        SqlCommand ObjSqlCommand;
        string connectionString = 
            "Data Source=DESKTOP-G6P9E0C\\SQLEXPRESS;" +
            "Initial Catalog=EMSDB;" +
            "Integrated Security=True";

        /* 
         * Create the constructor 
         *      - Initialize the SqlConnection
         *      - Initialise the SqlCommand
         *      - Set the command type to stored procedure
         */
        public EmployeeService()
        {
            ObjSqlConnection = new SqlConnection(connectionString);
            ObjSqlCommand = new SqlCommand();
            ObjSqlCommand.Connection = ObjSqlConnection;
            ObjSqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
        }

        #region GetAllEmployees_Operation
        // The method GetAll to return all employees to show them
        public List<Employee>? GetAll() // before we had observablelist
        {
            // Create a list to store all employees
            List<Employee> ObjEmployeeList = new List<Employee>();
            try
            {
                ObjSqlCommand.Parameters.Clear();
                ObjSqlCommand.CommandText = "udp_SelectAllEmployees";

                ObjSqlConnection.Open();
                var ObjSqlDataReader = ObjSqlCommand.ExecuteReader();
                if(ObjSqlDataReader.HasRows)
                {
                    Employee? ObjEmployee = null;
                    while(ObjSqlDataReader.Read())
                    {
                        ObjEmployee = new Employee();
                        ObjEmployee.Id = ObjSqlDataReader.GetInt32(0);
                        ObjEmployee.Name = ObjSqlDataReader.GetString(1);
                        ObjEmployee.Age = ObjSqlDataReader.GetInt32(2);
                        ObjEmployee.Email = ObjSqlDataReader.GetString(3);
                        ObjEmployee.Phone = ObjSqlDataReader.GetString(4);
                        ObjEmployee.Job = ObjSqlDataReader.GetString(5);
                        ObjEmployeeList.Add(ObjEmployee);
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                ObjSqlConnection.Close();
            }
            return ObjEmployeeList;
        }
        #endregion

        #region AddEmployee_Operation
        public bool AddEmployee(Employee employee)
        {
            if (employee.Age < 21 || employee.Age > 60)
            {
                throw new ArgumentException("Invalid Age Limit!");
            }

            try
            {
                ObjSqlCommand.Parameters.Clear();
                ObjSqlCommand.CommandText = "udp_InsertEmployee";

                // Assuming the stored procedure expects parameters for employee properties
                ObjSqlCommand.Parameters.AddWithValue("@Id", employee.Id);
                ObjSqlCommand.Parameters.AddWithValue("@Name", employee.Name);
                ObjSqlCommand.Parameters.AddWithValue("@Age", employee.Age);
                ObjSqlCommand.Parameters.AddWithValue("@Email", employee.Email);
                ObjSqlCommand.Parameters.AddWithValue("@Phone", employee.Phone);
                ObjSqlCommand.Parameters.AddWithValue("@Job", employee.Job);

                ObjSqlConnection.Open();
                ObjSqlCommand.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return false; // Return false to indicate failure
            }
            finally
            {
                ObjSqlConnection.Close();
            }

            return true; // Return true to indicate success
        }
        #endregion

        #region UpdateEmployee_Operation
        public bool UpdateEmployee(Employee employee)
        {
            bool isUpdated = false;

            try
            {
                ObjSqlCommand.Parameters.Clear();
                ObjSqlCommand.CommandText = "udp_UpdateEmployee";

                // Assuming the stored procedure expects parameters for employee properties
                ObjSqlCommand.Parameters.AddWithValue("@Id", employee.Id);
                ObjSqlCommand.Parameters.AddWithValue("@Name", employee.Name);
                ObjSqlCommand.Parameters.AddWithValue("@Age", employee.Age);
                ObjSqlCommand.Parameters.AddWithValue("@Email", employee.Email);
                ObjSqlCommand.Parameters.AddWithValue("@Phone", employee.Phone);
                ObjSqlCommand.Parameters.AddWithValue("@Job", employee.Job);

                ObjSqlConnection.Open();
                int rowsAffected = ObjSqlCommand.ExecuteNonQuery();


                if (rowsAffected > 0)
                {
                    isUpdated = true; // Set isUpdated to true if the update was successful
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                ObjSqlConnection.Close();
            }

            /*
            for(int index = 0; index < ObjEmployeesList?.Count; index++)
            {
                if (ObjEmployeesList?[index].Id == employee.Id)
                { 
                    ObjEmployeesList[index].Name = employee.Name;
                    ObjEmployeesList[index].Age = employee.Age;
                    isUpdated = true;
                    break; 
                }
            }
            */
            return isUpdated;
        }
        #endregion

        #region DeleteEmployee_Operation
        public bool DeleteEmployee(int employeeId)
        {
            bool isDeleted = false;
            try
            {
                ObjSqlCommand.Parameters.Clear();
                ObjSqlCommand.CommandText = "udp_DeleteEmployee";
                ObjSqlCommand.Parameters.AddWithValue("@Id", employeeId);
                ObjSqlConnection.Open();
                int rowsAffected = ObjSqlCommand.ExecuteNonQuery();
                isDeleted = rowsAffected > 0;
            }
            catch(SqlException ex)
            {
                Console.WriteLine(ex.Message);
                isDeleted = false;
            }
            finally
            {
                ObjSqlConnection.Close();
            }
            /*
            for (int index = 0; index < ObjEmployeesList?.Count; index++)
            {
                if (ObjEmployeesList[index].Id == employeeId)
                {
                    ObjEmployeesList.RemoveAt(index);
                    isDeleted = true; 
                    break;
                }
            }*/
            return isDeleted;
        }
        #endregion

        #region SearchEmployee_Operation
        public Employee? SearchEmployee(int employeeId)
        {
            Employee? ObjEmployee = null;

            try
            {
                ObjSqlCommand.Parameters.Clear();
                ObjSqlCommand.CommandText = "udp_SelectEmployeeById";
                ObjSqlCommand.Parameters.AddWithValue("@Id", employeeId);
                ObjSqlConnection.Open();

                var ObjSqlDataReader = ObjSqlCommand.ExecuteReader();

                if (ObjSqlDataReader.HasRows)
                {
                    while (ObjSqlDataReader.Read())
                    {
                        ObjEmployee = new Employee();
                        ObjEmployee.Id = ObjSqlDataReader.GetInt32(0);
                        ObjEmployee.Name = ObjSqlDataReader.GetString(1);
                        ObjEmployee.Age = ObjSqlDataReader.GetInt32(2);
                        ObjEmployee.Email = ObjSqlDataReader.GetString(3);
                        ObjEmployee.Phone = ObjSqlDataReader.GetString(4);
                        ObjEmployee.Job = ObjSqlDataReader.GetString(5);
                    }
                }


            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                ObjSqlConnection.Close();
            }
            // return ObjEmployeesList?.FirstOrDefault(e => e.Id == employeeId);
            return ObjEmployee;
        }
        #endregion

    }
}
