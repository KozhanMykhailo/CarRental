using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.Entities
{
	public class Role
	{
        /// <summary>
		/// ID
		/// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
		/// Имя
		/// </summary>
        public string Name { get; set; }
        /// <summary>
		/// Список пользователей
		/// </summary>
        public List<User> Users { get; set; }
        /// <summary>
		/// Конструктор , 
		/// </summary>
        public Role()
        {
            Users = new List<User>();
        }
    }
}
