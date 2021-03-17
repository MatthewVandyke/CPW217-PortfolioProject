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

		public async Task<string> UploadPhotoBlob(IFormFile file)
		{
			string con = _config["BlobConnection"];
			BlobServiceClient blobService = new BlobServiceClient(con);

			// Ensure create container to hold BLOBs
			BlobContainerClient containerClient = blobService.GetBlobContainerClient("photos");
			if (!await containerClient.ExistsAsync())
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

		public async Task<string> UploadModelBlob(IFormFile file)
		{
			string con = _config["BlobConnection"];
			BlobServiceClient blobService = new BlobServiceClient(con);

			// Ensure create container to hold BLOBs
			BlobContainerClient containerClient = blobService.GetBlobContainerClient("models");
			if (!await containerClient.ExistsAsync())
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

		public async Task UpdatePhotoBlob(IFormFile file, string fileName)
		{
			string con = _config["BlobConnection"];
			BlobServiceClient blobService = new BlobServiceClient(con);

			// Ensure create container to hold BLOBs
			BlobContainerClient containerClient = blobService.GetBlobContainerClient("photos");
			if (!await containerClient.ExistsAsync())
			{
				await containerClient.CreateAsync();
				await containerClient.SetAccessPolicyAsync(PublicAccessType.Blob);
			}

			BlobClient blobClient = containerClient.GetBlobClient(fileName);
			await blobClient.UploadAsync(file.OpenReadStream(), true);
		}

		public async Task UpdateModelBlob(IFormFile file, string fileName)
		{
			string con = _config["BlobConnection"];
			BlobServiceClient blobService = new BlobServiceClient(con);

			// Ensure create container to hold BLOBs
			BlobContainerClient containerClient = blobService.GetBlobContainerClient("models");
			if (!await containerClient.ExistsAsync())
			{
				await containerClient.CreateAsync();
				await containerClient.SetAccessPolicyAsync(PublicAccessType.Blob);
			}

			BlobClient blobClient = containerClient.GetBlobClient(fileName);
			await blobClient.UploadAsync(file.OpenReadStream(), true);
		}

		public async Task<bool> DeletePhotoBlob(string fileName)
		{
			string con = _config["BlobConnection"];
			BlobServiceClient blobService = new BlobServiceClient(con);
			BlobContainerClient containerClient = blobService.GetBlobContainerClient("photos");
			BlobClient blobClient = containerClient.GetBlobClient(fileName);

			return await blobClient.DeleteIfExistsAsync();
		}

		public async Task<bool> DeleteModelBlob(string fileName)
		{
			string con = _config["BlobConnection"];
			BlobServiceClient blobService = new BlobServiceClient(con);
			BlobContainerClient containerClient = blobService.GetBlobContainerClient("models");
			BlobClient blobClient = containerClient.GetBlobClient(fileName);

			return await blobClient.DeleteIfExistsAsync();
		}
	}
}
