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


    }


}
