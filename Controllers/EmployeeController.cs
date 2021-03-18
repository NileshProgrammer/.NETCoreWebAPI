using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using WebAPI.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class EmployeeController : ControllerBase
	{
		private readonly IConfiguration configuration;
		private readonly IWebHostEnvironment webHostEnvironment;

		public EmployeeController(IConfiguration configuration,IWebHostEnvironment webHostEnvironment)
		{
			this.configuration = configuration;
			this.webHostEnvironment = webHostEnvironment;
		}

		[HttpGet]
		public JsonResult Get()
		{
			string query = @"
							select employeeId,employeeName,department,convert(varchar,dateOfJoining,23) as dateOfJoining,photoFileName from employee";
			DataTable dT = new DataTable();
			string sqlDataSource = configuration.GetConnectionString("EmployeeAppCon");
			SqlDataReader dataReader;
			using (SqlConnection connection = new SqlConnection(sqlDataSource))
			{
				connection.Open();
				using (SqlCommand sqlCommand = new SqlCommand(query, connection))
				{
					dataReader = sqlCommand.ExecuteReader();
					dT.Load(dataReader);
					dataReader.Close();
					connection.Close();
				}
			}
			return new JsonResult(dT);
		}

		[HttpPost]
		public JsonResult Post(Employee emp)
		{
			string query = @"
							insert into employee values (
							'" + emp.employeeName+ "'" +
							",'"+emp.department+ "' " +
							",'" + emp.dateOfJoining + "' " +
							",'" + emp.photoFileName + "')";
			DataTable dT = new DataTable();
			string sqlDataSource = configuration.GetConnectionString("EmployeeAppCon");
			SqlDataReader dataReader;
			using (SqlConnection connection = new SqlConnection(sqlDataSource))
			{
				connection.Open();
				using (SqlCommand sqlCommand = new SqlCommand(query, connection))
				{
					dataReader = sqlCommand.ExecuteReader();
					dT.Load(dataReader);
					dataReader.Close();
					connection.Close();
				}
			}
			return new JsonResult("Data Added Successfully");
		}

		[HttpPut]
		public JsonResult Put(Employee emp)
		{
			string query = @"
							update employee 
							set employeeName = '" + emp.employeeName + "' " +
							", department = '"+emp.department+"'" +
							", dateOfJoining = '"+emp.dateOfJoining+"'" +
							",photoFileName='"+emp.photoFileName+"' " +
							"where employeeId = '" + emp.employeeId + "'";

			DataTable dT = new DataTable();
			string sqlDataSource = configuration.GetConnectionString("EmployeeAppCon");
			SqlDataReader dataReader;
			using (SqlConnection connection = new SqlConnection(sqlDataSource))
			{
				connection.Open();
				using (SqlCommand sqlCommand = new SqlCommand(query, connection))
				{
					dataReader = sqlCommand.ExecuteReader();
					dT.Load(dataReader);
					dataReader.Close();
					connection.Close();
				}
			}
			return new JsonResult("Data Updated Successfully");
		}
		[HttpDelete("{id}")]
		public JsonResult Delete(int id)
		{
			string query = @"
							delete from employee 
							where employeeId = '" + id + "'";

			DataTable dT = new DataTable();
			string sqlDataSource = configuration.GetConnectionString("EmployeeAppCon");
			SqlDataReader dataReader;
			using (SqlConnection connection = new SqlConnection(sqlDataSource))
			{
				connection.Open();
				using (SqlCommand sqlCommand = new SqlCommand(query, connection))
				{
					dataReader = sqlCommand.ExecuteReader();
					dT.Load(dataReader);
					dataReader.Close();
					connection.Close();
				}
			}
			return new JsonResult("Data Deleted Successfully");
		}

		[Route("SaveFile")]
		[HttpPost]
		public JsonResult SaveFile()
		{
			try
			{
				var httpRequest = Request.Form;
				var postedFile = httpRequest.Files[0];
				string filename = postedFile.FileName;
				var physicalPath = webHostEnvironment.ContentRootPath + "/Photos/" + filename;
				using (var stream = new FileStream(physicalPath,FileMode.Create))
				{
					postedFile.CopyTo(stream);
				}
				return new JsonResult(filename);

			}
			catch(Exception)
			{
				return new JsonResult("anonymous.png");
			}

		}

	}

}






















