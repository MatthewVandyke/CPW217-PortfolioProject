using CPW217_PortfolioProject2021.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPW217_PortfolioProject2021.Models
{
	public class ApplicationUser : IdentityUser
	{
		List<ThreeDModel> UserModels { get; set; }
	}
}
