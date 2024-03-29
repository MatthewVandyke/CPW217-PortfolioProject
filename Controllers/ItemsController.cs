﻿using CPW217_PortfolioProject2021.Data;
using CPW217_PortfolioProject2021.Models;
using FileUploadExample.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPW217_PortfolioProject2021.Controllers
{
	public class ItemsController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly IConfiguration _config;

		public ItemsController(IConfiguration config, ApplicationDbContext context)
		{
			_context = context;
			_config = config;
		}
		public async Task<IActionResult> Index()
		{
			return View(await ItemDb.GetItemsAsync(_context));
		}

		public IActionResult Create()
		{
			return View();
		}

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequestSizeLimit(8388608)] // 8MB
        public async Task<IActionResult> Create(Item item)
        {
            if (ModelState.IsValid)
            {
                IFormFile photo = item.Photo;
                IFormFile model = item.Model;

                BlobStorageHelper helper = new BlobStorageHelper(_config);

                if (!FileUploadHelper.IsFileEmpty(model))
                {
                    if (FileUploadHelper.IsValidExtension(model, FileUploadHelper.FileTypes.Model))
                    {
                        item.ModelUrl = await helper.UploadModelBlob(model);
                    }
                    else
                    {
                        // Add error message
                    }
                }

                if (!FileUploadHelper.IsFileEmpty(photo))
                {
                    if (FileUploadHelper.IsValidExtension(photo, FileUploadHelper.FileTypes.Photo))
                    {
                        item.PhotoUrl = await helper.UploadPhotoBlob(photo);
                    }
                    else
                    {
                        // Add error message
                        // return view
                    }

                }


                await ItemDb.AddItemAsync(_context, item);
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
		{
            return View(await ItemDb.GetItemAsync(_context, id));
		}

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequestSizeLimit(8388608)] // 8MB
        public async Task<IActionResult> Edit(Item item)
		{
            if (ModelState.IsValid)
			{
                IFormFile photo = item.Photo;
                IFormFile model = item.Model;

                BlobStorageHelper helper = new BlobStorageHelper(_config);

                if (!FileUploadHelper.IsFileEmpty(model))
                {
                    if (FileUploadHelper.IsValidExtension(model, FileUploadHelper.FileTypes.Model))
					{
                        await helper.UpdateModelBlob(model, item.ModelUrl);
                    }
                    else
					{
                        // Add error message
                        // return view
                    }
                }

                if (!FileUploadHelper.IsFileEmpty(photo))
				{
                    if (FileUploadHelper.IsValidExtension(photo, FileUploadHelper.FileTypes.Photo))
					{
                        await helper.UpdatePhotoBlob(photo, item.PhotoUrl);
                    }
                    else
					{
                        // Add error message
                        // return view
					}
                    
				}

                await ItemDb.UpdateItemAsync(_context, item);
                return RedirectToAction("Item", "Home", new { id = item.Id });
			}
            return View(item);
		}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int Id)
		{
            Item item = await ItemDb.GetItemAsync(_context, Id);
            BlobStorageHelper blobStorageHelper = new BlobStorageHelper(_config);

            await blobStorageHelper.DeletePhotoBlob(item.PhotoUrl);
            await blobStorageHelper.DeleteModelBlob(item.ModelUrl);

            await ItemDb.DeleteItemAsync(_context, item);

            return RedirectToAction("Index", "Home");
        }
    }
}
