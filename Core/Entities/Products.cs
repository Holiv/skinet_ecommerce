using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
	public class Products
	{
		[Key]
		public int Id { get; set; }

		public string? Name { get; set; }
	}
}

