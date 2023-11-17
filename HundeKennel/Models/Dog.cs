using Dapper;
using DateTimeExtensions;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows.Media.Imaging;

namespace HundeKennel.Models;

[Dapper.Contrib.Extensions.Table("Dogs")]
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

    [DisplayName("Danske Titler")]
    public string? DKTitles { get; set; }
    [DisplayName("Titler")]
    public string? Titles { get; set; }

    public string? HD { get; set; }
    public string? AD { get; set; }
    public string? HZ { get; set; }
    public string? SP { get; set; }
    [DisplayName("Index Dato")]
    public DateTime? IndexDate { get; set; }
    public string? HDIndex { get; set; }
    [DisplayName("Død?")]
    public bool Dead { get; set; }

    public string? AK { get; set; }
    [DisplayName("Avlsstatus")]
    public string? BreedingStatus { get; set; }
    public string? MB { get; set; }
    public byte[]? Image { get; set; }

    [DisplayName("Far stambog")]
    public string? Dad { get; set; }
    [DisplayName("Mor stambog")]
    public string? Mom { get; set; }

    public int? BreederID { get; set; }
    public int? OwnerID { get; set; }

    [NotMapped]
    public bool IsMatch { get; set; } = false;

    [Dapper.NotMapped]
    public ObservableCollection<Dog?>? Parents { get; set; } = new();
    [Dapper.NotMapped]
    public BitmapImage BitImage
    {
        get => ImageConverter();
    }
    [Dapper.NotMapped]
    public BitmapImage Icon 
    { 
        get => IconConverter();
    }
    [Dapper.NotMapped]
    public int Age
    {
        get => BirthDate.Age();
    }

    BitmapImage IconConverter()
    {
		if (Image != null)
		{
			using MemoryStream stream = new MemoryStream(Image);
			BitmapImage tmp = new();
			tmp.BeginInit();
			tmp.DecodePixelHeight = 80;
			tmp.DecodePixelWidth = 80;
			tmp.StreamSource = stream;
			tmp.CacheOption = BitmapCacheOption.OnLoad;
			tmp.EndInit();
			return tmp;
		}
		return new BitmapImage(new(@"pack://application:,,,/Resources/dogicon.bmp"));
	}

    BitmapImage ImageConverter()
    {
        if (Image != null)
        {
            using MemoryStream stream = new MemoryStream(Image);
            BitmapImage tmp = new();
            tmp.BeginInit();
            tmp.StreamSource = stream;
            tmp.CacheOption = BitmapCacheOption.OnLoad;
            tmp.EndInit();
            return tmp;
        }
        return new BitmapImage(new(@"pack://application:,,,/Resources/dogicon.bmp"));
	}
}
