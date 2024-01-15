// Copyright © Microsoft Corporation.  All Rights Reserved.
// This code released under the terms of the 
// Microsoft Public License (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//Copyright (C) Microsoft Corporation.  All rights reserved.

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using SampleSupport;
using Task.Data;

// Version Mad01

namespace SampleQueries
{
	[Title("LINQ Module")]
	[Prefix("Linq")]
	public class LinqSamples : SampleHarness
	{

		private DataSource dataSource = new DataSource();

		[Category("Restriction Operators")]
		[Title("Where - Task 1")]
		[Description("This sample get customers with total orders sum over...")]
		public void Linq1()
		{
			int[] totalSum = { 50000, 40000, 100000 };

			foreach (var x in totalSum)
			{
				ObjectDumper.Write($"Customers with total orders sum over {x}:");
				ObjectDumper.Write(dataSource.Customers.Where(c => 
					c.Orders.Select(o => o.Total).Sum() > x), 1);
			}
		}

		[Category("Restriction Operators")]
		[Title("Where - Task 2")]
		[Description("This sample get list of customers with suppliers from the same country and city.")]
		public void Linq2()
		{
			var customersWithTheirSuppliers = dataSource.Customers.Select(c => new
			{
				customer = c,
				suppliers = dataSource.Suppliers.Where(s => 
					s.Country.Equals(c.Country) && s.City.Equals(c.City))
			}).Where(c => c.suppliers.Any());
			
			var customersWithTheirSuppliersByGroup = dataSource.Customers
				.GroupJoin(dataSource.Suppliers,
					c => new { c.City, c.Country },
					s => new { s.City, s.Country },
					(customer, suppliers) => new
					{
						customer,
						suppliers
					}).Where(c => c.suppliers.Any());

			ObjectDumper.Write(customersWithTheirSuppliers, 1);
			ObjectDumper.Write(customersWithTheirSuppliersByGroup, 1);
		}

		[Category("Restriction Operators")]
		[Title("Where - Task 3")]
		[Description("This sample get customers which had order's sum over....")]
		public void Linq3()
		{
			var x = 10000;
			ObjectDumper.Write(dataSource.Customers.Where(c => c.Orders.Any(o => o.Total > x)));
		}

		[Category("Restriction Operators")]
		[Title("Where - Task 4")]
		[Description("This sample get customers with their created date.")]
		public void Linq4()
		{
			var listCustomers = dataSource.Customers
				.Where(c => c.Orders.Any())
				.Select(c => new 
				{ 
					c.CustomerID, 
					Customer = c, 
					CreatedFrom = c.Orders.OrderBy(o => o.OrderDate).FirstOrDefault()?.OrderDate.ToString("MMMM yyyy") 
				});
			
			ObjectDumper.Write(listCustomers);
		}
		
		[Category("Restriction Operators")]
		[Title("Where - Task 5")]
		[Description("This sample get customers with sorting.")]
		public void Linq5()
		{
			var listCustomers = dataSource.Customers
				.Where(c => c.Orders.Any())
				.Select(c => new 
				{ 
					c.CustomerID, 
					Customer = c, 
					CreatedFrom = c.Orders.OrderBy(o => o.OrderDate).FirstOrDefault()?.OrderDate 
				}).OrderBy(c => c.CreatedFrom?.Year)
				.ThenBy(c => c?.CreatedFrom?.Month)
				.ThenByDescending(c => c.Customer.Orders.Sum(o => o.Total))
				.ThenBy(c => c.Customer.CompanyName);
			
			ObjectDumper.Write(listCustomers);
		}
		
		[Category("Restriction Operators")]
		[Title("Where - Task 6")]
		[Description("This sample get customers, who has wrong symbols in PostCode or without code in Phone or without Region.")]
		public void Linq6()
		{
			var listCustomers = dataSource.Customers
				.Where(c => string.IsNullOrEmpty(c.PostalCode) || !c.PostalCode.All(char.IsDigit)
				            || string.IsNullOrEmpty(c.Region) || string.IsNullOrWhiteSpace(c.Region) 
				            || c.Phone.IndexOf("(", StringComparison.Ordinal) < 0 
				            || c.Phone.IndexOf(")", StringComparison.Ordinal) < 0);
			
			ObjectDumper.Write(listCustomers);
		}
		
		[Category("Restriction Operators")]
		[Title("Where - Task 7")]
		[Description("This sample get grouped products.")]
		public void Linq7()
		{
			var listProducts = dataSource.Products
				.GroupBy(p => p.Category)
				.Select(p => new
				{
					Category = p.Key,
					InStock = p.GroupBy(c => c.UnitsInStock > 0)
						.Select(c => new
						{
							c.Key,
							Products = c.OrderBy(o => o.UnitPrice)
						})
				});
			
			ObjectDumper.Write(listProducts, 2);
		}
		
		[Category("Restriction Operators")]
		[Title("Where - Task 8")]
		[Description("This sample get grouped products.")]
		public void Linq8()
		{
			var listProducts = dataSource.Products
				.GroupBy(p => (p.UnitPrice <= 10 ? "1. Cheap" : 
					((p.UnitPrice > 10 && p.UnitPrice <= 20) ? "2. Middle" : "3. Expensive")))
				.OrderBy(p => p.Key);

			ObjectDumper.Write(listProducts, 1);
		}
		
		[Category("Restriction Operators")]
		[Title("Where - Task 9")]
		[Description("This sample get average values of profitability and intensity.")]
		public void Linq9()
		{
			var listCustomers = dataSource.Customers
				.GroupBy(c => c.City)
				.Select(c => new
				{
					City = c.Key,
					Profit = Math.Round(c.Sum(p => p.Orders.Sum(o => o.Total)) / c.Sum(p => p.Orders.Length), 2),
					Intensity = Math.Round(c.Average(i => i.Orders.Length), 1)
				});
			ObjectDumper.Write(listCustomers, 2);
		}
		
		[Category("Restriction Operators")]
		[Title("Where - Task 10")]
		[Description("This sample get customers activity.")]
		public void Linq10()
		{
			var orders = dataSource.Customers.SelectMany(c => c.Orders);
			
			var monthsActivity = orders.GroupBy(o => o.OrderDate.Month)
				.OrderBy(o => o.Key)
				.Select(o => new
				{
					Month = o.Key,
					Activity = o.Count()
				});
			
			var yearsActivity = orders.GroupBy(o => o.OrderDate.Year)
				.OrderBy(o => o.Key)
				.Select(o => new
				{
					Year = o.Key,
					Activity = o.Count()
				});
			
			var yearsAndMonthsActivity = orders.GroupBy(o => o.OrderDate.Year)
				.OrderBy(o => o.Key)
				.Select(o => new
				{
					Year = o.Key,
					Months = o.GroupBy(m => m.OrderDate.Month)
						.OrderBy(m => m.Key)
						.Select(m => new
						{
							Month = m.Key,
							Activity = m.Count()
						})
				});
			
			ObjectDumper.Write(monthsActivity, 2);
			Console.WriteLine();
			ObjectDumper.Write(yearsActivity, 2);
			Console.WriteLine();
			ObjectDumper.Write(yearsAndMonthsActivity, 3);
		}
	}
}
