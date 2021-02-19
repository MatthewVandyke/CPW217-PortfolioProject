using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPW217_PortfolioProject2021.Models
{
	public class BlobConnection
	{
		public BlobConnection(string key)
		{
			ConnectionString = key;
		}

		public string ConnectionString { get; set; }
	}
}
