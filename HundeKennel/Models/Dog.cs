using Syncfusion.Windows.Tools;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HundeKennel.Models;

[Table("Dogs")]
public class Dog
{
    [DisplayName("Stambog")]
    public string? Pedigree { get; set; }
    public string? Chip { get; set; }
    [DisplayName("Navn")]
    public string? Name { get; set; }
    [DisplayName("Fødselsdato")]
    public DateTime? BirthDate { get; set; }
    [DisplayName("Køn")]
    public string? Sex { get; set; }
    [DisplayName("Farve")]
    public string? Color { get; set; }

    public string? DKTitles { get; set; }
    public string? Titles { get; set; }

    public string? HD { get; set; }
    public string? AD { get; set; }
    public string? HZ { get; set; }
    public string? SP { get; set; }
    public DateTime? IndexDate { get; set; }
    public string? HDIndex { get; set; }
    public bool Dead { get; set; }

    public string? AK { get; set; }
    public string? BreedingStatus { get; set; }
    public string? MB { get; set; }
    public byte[]? Image { get; set; }

    public string? Dad { get; set; }
    public string? Mom { get; set; }

    public int? BreederID { get; set; }
    public int? OwnerID { get; set; }
    //   [NotMapped]
    //public Breeder? Breeder { get; set; }
    //[NotMapped]
    //public Owner? Owner { get; set; }
    [NotMapped]
    public ObservableCollection<Dog?>? Parents { get; set; } = new();
}
