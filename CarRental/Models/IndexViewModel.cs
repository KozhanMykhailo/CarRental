using CarRental.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.Models
{
	public class IndexViewModel
	{
		public IEnumerable<Car> Cars { get; set; }
		public PageViewModel PageViewModel { get; set; }
		public FilterViewModel FilterViewModel { get; set; }
		public SortViewModel SortViewModel { get; set; }
	}
}
