using ExcelMapper;

namespace HundeKennel.Services.Helpers
{
	public class ExcelDogHelper
    {
        [ExcelColumnName("Stambog")]
        public string? Pedigree { get; set; }
        [ExcelColumnName("Tato")]
        public string? Chip { get; set; }
        [ExcelColumnName("Navn")]
        public string? Name { get; set; }
        [ExcelColumnName("Born")]
        public string? BirthDate { get; set; }
        public string? Sex { get; set; }
        [ExcelColumnName("Farve")]
        public string? Color { get; set; }
        [ExcelColumnName("DKKtitler")]
        public string? DKTitles { get; set; }
        [ExcelColumnName("Titler")]
        public string? Titles { get; set; }
        public string? HD { get; set; }
        public string? AD { get; set; }
        public string? HZ { get; set; }
        public string? SP { get; set; }
        [ExcelColumnName("Indexdato")]
        public string? IndexDate { get; set; }
        public string? HDIndex { get; set; }
        public string? Dead { get; set; }
        public string? AK { get; set; }
        [ExcelColumnName("Avlsstatus")]
        public string? BreedingStatus { get; set; }
        public string? MB { get; set; }
        [ExcelColumnName("Far")]
        public string? Dad { get; set; }
        [ExcelColumnName("Mor")]
        public string? Mom { get; set; }
    }
}
