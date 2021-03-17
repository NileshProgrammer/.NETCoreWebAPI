using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DepartmentController : ControllerBase
	{
		private readonly IConfiguration configuration;

		public DepartmentController(IConfiguration configuration)
		{
			this.configuration = configuration;
		}

		[HttpGet]
		public JsonResult Get()
		{
			string query = @"
							select * from department";
			DataTable dT = new DataTable();
			string sqlDataSource = configuration.GetConnectionString("EmployeeAppCon");
			SqlDataReader dataReader;
			using(SqlConnection connection = new SqlConnection(sqlDataSource))
			{
				connection.Open();
				using(SqlCommand sqlCommand = new SqlCommand(query,connection))
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
		public JsonResult Post(Department dep)
		{
			string query = @"
							insert into department values ('"+ dep.departmentName +"')" ;
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
		public JsonResult Put(Department dep)
		{
			string query = @"
							update department 
							set departmentName = '" + dep.departmentName + "' " +
							"where departmentId = '" + dep.departmentId + "'";
							
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
		public JsonResult Delete(int id )
		{
			string query = @"
							delete from department 
							where departmentId = '" + id + "'";

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
		
		
		[Route("GetAllDepartment")]
		public JsonResult GetAllDepartment()
		{
			string query = @"
							select departmentName from department";
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
	}
}
