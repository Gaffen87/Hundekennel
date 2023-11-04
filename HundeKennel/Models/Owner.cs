using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HundeKennel.Models
{
	public class Owner
	{
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Zip { get; set; }
        public string? City { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? HomePage { get; set; }
    }
}
