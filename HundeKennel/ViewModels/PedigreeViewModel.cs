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
        PedigreeLeft = new();
        PedigreeRight = new();
        Matches = new();
    }
    
    // Used to populate the treeviews with the full pedigree of a dog
    public ObservableCollection<Dog>? TreeViewDogLeft { get; set; }
    public ObservableCollection<Dog>? TreeViewDogRight { get; set; }

    // The full pedigree of the dogs in List form
    public List<Dog> PedigreeLeft { get; set; }
    public List<Dog> PedigreeRight { get; set; }

    // List containing dogs that are in both PedigreeLeft and PedigreeRight
    public List<Dog> Matches { get; set; }
    
    public void Init(Dog left, Dog right)
    {
        TreeViewDogLeft = new() { left };
        TreeViewDogRight = new() { right };

        FindPedigreeLeft(TreeViewDogLeft);
        FindPedigreeRight(TreeViewDogRight);

        FindMatches();
    }

    /// <summary>
    /// Populates a List in the PedigreeViewModel with the full Pedigree lineage of a dog
    /// </summary>
    /// <param name="dogs">Collection containing the LeftCompare dog used to bind to a TreeView</param>
    public void FindPedigreeLeft(ObservableCollection<Dog>? dogs)
    {
        foreach (var dog in dogs!)
        {
            dog.IsMatch = false;
            PedigreeLeft.Add(dog);
            if (dog.Parents != null)
                FindPedigreeLeft(dog.Parents!);
        }
    }
	/// <summary>
	/// Populates a List in the PedigreeViewModel with the full Pedigree lineage of a dog
	/// </summary>
	/// <param name="dogs">Collection containing the RightCompare dog used to bind to a TreeView</param>
	public void FindPedigreeRight(ObservableCollection<Dog>? dogs)
    {
        foreach (var dog in dogs!)
        {
            dog.IsMatch = false;
            PedigreeRight.Add(dog);
            if (dog.Parents != null)
                FindPedigreeRight(dog.Parents!);
        }
    }

    /// <summary>
    /// Compares the pedigrees of 2 dogs and adds all matches into a new list
    /// Also sets the IsMatch attribute of the dogs to true
    /// </summary>
    public void FindMatches()
    {
        foreach (var dog in PedigreeLeft)
        {
            if (PedigreeRight.Any(x => x.Pedigree == dog.Pedigree))
                Matches.Add(dog);
        }

        Matches.RemoveAll(x => x.Name == "Ingen Data");

        foreach (var dog in Matches)
            dog.IsMatch = true;
    }

}
