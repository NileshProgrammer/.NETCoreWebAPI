using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

	/*	[HttpGet]

		public JsonResult Get()
		{
			string query = @"
							select * from department";
		}
	*/

	}
}
