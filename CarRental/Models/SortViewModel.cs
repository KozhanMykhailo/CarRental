using CarRental.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.Models
{
	public class SortViewModel
	{
        public SortState NameSort { get; private set; } // значение для сортировки по наименованию
        public SortState PriceSort { get; private set; }    // значение для сортировки по цене
        public SortState Current { get; private set; }     // текущее значение сортировки
        public SortViewModel(SortState sortOrder)
        {
            NameSort = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            PriceSort = sortOrder == SortState.PriceAsc ? SortState.PriсeDesc : SortState.PriceAsc;
            Current = sortOrder;
        }
    }
}
