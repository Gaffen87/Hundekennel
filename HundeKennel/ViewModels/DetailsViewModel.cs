using HundeKennel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HundeKennel.ViewModels
{
	public class DetailsViewModel
	{
        public DetailsViewModel()
        {
            SelectedDog = new Dog
            {
                Pedigree = "sdfjsdf09342",
                Name = "Chili",
                BirthDate = DateTime.Now,
                Dead = false
            };
        }

        public Dog? SelectedDog { get; set; }
    }
}
