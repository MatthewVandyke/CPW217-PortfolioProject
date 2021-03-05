using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CPW217_PortfolioProject2021.Models
{
	public class BlobStorageHelper
	{
		private readonly IConfiguration _config;

		public BlobStorageHelper(IConfiguration configuration)
		{
			_config = configuration;
		}

		public async Task<string> UploadBlob(IFormFile file)
		{
			string con = _config["BlobConnection"];
			BlobServiceClient blobService = new BlobServiceClient(con);

			// Ensure create container to hold BLOBs
			BlobContainerClient containerClient = blobService.GetBlobContainerClient("photos");
			if (!containerClient.Exists())
			{
				await containerClient.CreateAsync();
				await containerClient.SetAccessPolicyAsync(PublicAccessType.Blob);
			}

			// Add BLOB to container
			string newFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
			BlobClient blobClient = containerClient.GetBlobClient(newFileName);

			await blobClient.UploadAsync(file.OpenReadStream());
			return newFileName;
		}
	}
}
