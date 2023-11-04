using Syncfusion.Windows.Tools;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HundeKennel.Models;

public class Dog
{
    public string? Pedigree { get; set; }
    public string? Chip { get; set; }
    public string? Name { get; set; }
    public DateTime? BirthDate { get; set; }
    public string? Sex { get; set; }
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

    public Breeder? Breeder { get; set; }
    public Owner? Owner { get; set; }

    public string? DadString { get; set; }
    public string? MomString { get; set; }
    public ObservableCollection<Dog?>? Parents { get; set; } = new();
}
