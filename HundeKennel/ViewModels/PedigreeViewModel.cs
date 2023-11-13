using CommunityToolkit.Mvvm.ComponentModel;
using HundeKennel.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace HundeKennel.ViewModels;

public partial class PedigreeViewModel : ObservableObject
{
    public PedigreeViewModel()
    {
        
    }

    public ObservableCollection<Dog>? TreeViewDogLeft { get; set; }
    public ObservableCollection<Dog>? TreeViewDogRight { get; set; }

}
