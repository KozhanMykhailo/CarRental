using CarRental.Abstract;
using CarRental.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.Models
{
	public class EFBillRepository : IBillRepository
	{
		private ApplicationContext _context;
		public EFBillRepository(ApplicationContext context)
		{
			_context = context;
		}
		public IEnumerable<Bill> Bills => _context.Bills;

		public bool SaveBill(Bill bill)
		{
			if (bill.Id == 0)
			{
				if (_context.Bills.Count() == 0)
				{
					bill.Id = 1;
				}
				else
					bill.Id = _context.Bills.Max(m => m.Id) + 1;
				_context.Bills.Add(bill);
			}
			else
			{
				Bill dbBill = _context.Bills.Find(bill.Id);
				if (dbBill != null)
				{
					dbBill.AccountDetails = bill.AccountDetails;
					dbBill.OrderId = bill.OrderId;
					dbBill.TypeBill = bill.TypeBill;
					dbBill.ToPay = bill.ToPay;
					dbBill.UserId = bill.UserId;
				}
			}
			//var result = _context.SaveChanges();
			//if (result > 0)
				return true;
			//else
				//return false;
		}
	}
}
