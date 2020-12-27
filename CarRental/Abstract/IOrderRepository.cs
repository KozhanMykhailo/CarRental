using CarRental.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.Abstract
{
	public interface IOrderRepository
	{
		IEnumerable<Order> Orders { get; }
		void SaveOrder(Order order);
	}
}
