using CarRental.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.Abstract
{
	public interface IBillRepository
	{
		IEnumerable<Bill> Bills { get; }
		bool SaveBill(Bill bill);
	}
}
