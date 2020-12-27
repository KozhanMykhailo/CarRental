using CarRental.Abstract;
using CarRental.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.Models
{
	public class EFOrderRepository : IOrderRepository
	{
		private ApplicationContext _context;
		public EFOrderRepository(ApplicationContext context)
		{
			_context = context;
		}
		public IEnumerable<Order> Orders => _context.Orders;
		public void SaveOrder(Order order)
		{
			if (order.Id == 0)
			{
				if (_context.Orders.Count() == 0)
				{
					order.Id = 1;
				}
				else
					order.Id = _context.Orders.Max(m => m.Id) + 1;
				order.Status = StatusOrder.Approval;
				_context.Orders.Add(order);
			}
			else
			{
				Order dbOrder = _context.Orders.Find(order.Id);
				if (dbOrder != null)
				{
					dbOrder.Comment = order.Comment;
					dbOrder.FirstName = order.FirstName;
					dbOrder.INN = order.INN;
					dbOrder.CarId = order.CarId;
					dbOrder.LastName = order.LastName;
					dbOrder.OptionDriver = order.OptionDriver;
					dbOrder.PassportData = order.PassportData;
					dbOrder.RentalTime = order.RentalTime;
					dbOrder.Status = order.Status;
					dbOrder.UserId = order.UserId;
				}
			}
			_context.SaveChanges();
		}
	}
}
