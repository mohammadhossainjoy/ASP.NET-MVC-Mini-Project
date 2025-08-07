using HR.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace HR.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee/NewEmployee
        [HttpGet]
        public ActionResult NewEmployee()
        {
            return View();
        }

        // POST: Employee/NewEmployee
        [HttpPost]
        public ActionResult NewEmployee(Employee objEmployee)
        {
            try
            {
                string employeename = objEmployee.FullName;
                string jobtitle = objEmployee.JobTitle;
                string department = objEmployee.Department;
                string dob = objEmployee.DateOfBirth;
                string email = objEmployee.Email;
                string phone = objEmployee.Phone;
                string address = objEmployee.Address;
                string joinDate = objEmployee.JoinDate;

                string connStr = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=dbERP;User ID=mh;Password=123456";

                string sql = @"INSERT INTO employeeInformation
                    ([Name], [JobTitle], [Department], [DateOfBirth], [Email], [Phone], [Address], [JoinDate])
                    VALUES (@Name, @JobTitle, @Department, @DOB, @Email, @Phone, @Address, @JoinDate)";

                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Name", employeename);
                    cmd.Parameters.AddWithValue("@JobTitle", jobtitle);
                    cmd.Parameters.AddWithValue("@Department", department);
                    cmd.Parameters.AddWithValue("@DOB", dob);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Phone", phone);
                    cmd.Parameters.AddWithValue("@Address", address);
                    cmd.Parameters.AddWithValue("@JoinDate", joinDate);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                
            }
            
            TempData["Success"] = "Employee saved successfully!";
            return RedirectToAction("NewEmployee"); // so form clears

          
        }

        //Employee Show

        public ActionResult EmployeeShow()
        {
            List<Employee> list = new List<Employee>();

            string connStr = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=dbERP;User ID=mh;Password=123456";
            string query = "SELECT * FROM employeeInformation";

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Employee emp = new Employee
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        FullName = reader["Name"].ToString(),
                        JobTitle = reader["JobTitle"].ToString(),
                        Department = reader["Department"].ToString(),
                        DateOfBirth = reader["DateOfBirth"].ToString(),
                        Email = reader["Email"].ToString(),
                        Phone = reader["Phone"].ToString(),
                        Address = reader["Address"].ToString(),
                        JoinDate = reader["JoinDate"].ToString()
                    };
                    list.Add(emp);
                }
            }

            return View("EmployeeShow", list);
        }

        //Edit
        // GET :Edit
        public ActionResult EditEmployee(int id)
        {
            Employee emp = new Employee();

            try
            {
                string connStr = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=dbERP;User ID=mh;Password=123456";
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    string query = "SELECT * FROM employeeInformation WHERE Id = @Id";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Id", id);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        emp.Id = Convert.ToInt32(reader["Id"]);
                        emp.FullName = reader["Name"].ToString();
                        emp.JobTitle = reader["JobTitle"].ToString();
                        emp.Department = reader["Department"].ToString();
                        emp.DateOfBirth = reader["DateOfBirth"].ToString();
                        emp.Email = reader["Email"].ToString();
                        emp.Phone = reader["Phone"].ToString();
                        emp.Address = reader["Address"].ToString();
                        emp.JoinDate = reader["JoinDate"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error loading employee: " + ex.Message);
            }

            return View("EmployeeEdit", emp); // View name: EmployeeEdit.cshtml
        }

        // POST: Employee/Edit
        [HttpPost]
        public ActionResult EditEmployee(Employee emp)
        {
            try
            {
                string connStr = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=dbERP;User ID=mh;Password=123456";

                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    string query = @"UPDATE employeeInformation SET 
                        Name = @Name, JobTitle = @JobTitle, Department = @Department,
                        DateOfBirth = @DOB, Email = @Email, Phone = @Phone,
                        Address = @Address, JoinDate = @JoinDate 
                        WHERE Id = @Id";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Id", emp.Id);
                    cmd.Parameters.AddWithValue("@Name", emp.FullName);
                    cmd.Parameters.AddWithValue("@JobTitle", emp.JobTitle);
                    cmd.Parameters.AddWithValue("@Department", emp.Department);
                    cmd.Parameters.AddWithValue("@DOB", emp.DateOfBirth);
                    cmd.Parameters.AddWithValue("@Email", emp.Email);
                    cmd.Parameters.AddWithValue("@Phone", emp.Phone);
                    cmd.Parameters.AddWithValue("@Address", emp.Address);
                    cmd.Parameters.AddWithValue("@JoinDate", emp.JoinDate);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

               
                return RedirectToAction("EmployeeShow");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Update Error: " + ex.Message);
                return View("EmployeeEdit", emp); // Show form again with same data
            }
        }





        //Delete
        //Get 
        public ActionResult DeleteEmployee(int id)
        {
            try
            {
                string connStr = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=dbERP;User ID=mh;Password=123456";

                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    string query = "DELETE FROM employeeInformation WHERE Id = @Id";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Id", id);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

              
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Delete Error: " + ex.Message);
              
            }

            return RedirectToAction("EmployeeShow");
        }



        // GET: Employee Details
        public ActionResult Details(int id)
        {
            Employee emp = new Employee();

            try
            {
                string connStr = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=dbERP;User ID=mh;Password=123456";
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    string query = "SELECT * FROM employeeInformation WHERE Id = @Id";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Id", id);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        emp.Id = Convert.ToInt32(reader["Id"]);
                        emp.FullName = reader["Name"].ToString();
                        emp.JobTitle = reader["JobTitle"].ToString();
                        emp.Department = reader["Department"].ToString();
                        emp.DateOfBirth = reader["DateOfBirth"].ToString();
                        emp.Email = reader["Email"].ToString();
                        emp.Phone = reader["Phone"].ToString();
                        emp.Address = reader["Address"].ToString();
                        emp.JoinDate = reader["JoinDate"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error loading details: " + ex.Message);
            }

            return View("EmployeeDetails", emp); // View name: EmployeeDetails.cshtml
        }




    }
}



