using CPW217_PortfolioProject2021.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPW217_PortfolioProject2021.Data
{
	public static class ItemDb
	{
		public static async Task<List<Item>> GetItemsAsync(ApplicationDbContext _context)
		{
			return await _context.Items.ToListAsync();
		}

		public static async Task<Item> GetItemAsync(ApplicationDbContext _context, int id)
		{
			return await _context
							.Items
							.Where(p => p.Id == id)
							.SingleAsync();
		}

		public static async Task<Item> AddItemAsync(ApplicationDbContext _context, Item item)
		{
			_context.Items.Add(item);
			await _context.SaveChangesAsync();
			return item;
		}

		public static async Task<Item> UpdateItemAsync(ApplicationDbContext _context, Item item)
		{
			_context.Items.Update(item);
			await _context.SaveChangesAsync();
			return item;
		}

		public static async Task DeleteItemAsync(ApplicationDbContext _context, Item item)
		{
			_context.Items.Remove(item);
			await _context.SaveChangesAsync();
		}
	}
}
