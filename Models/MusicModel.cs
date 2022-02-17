// Modell
// Importerar data annotations och JSON
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace MusicApp.Models
{
	// Klass som hanterar skivor
	public class MusicModel
	{
		// Properties med skräddarsydda felmeddelanden och fältetiketter
		// Skivans namn, obligatorisk uppgift
		[Required(ErrorMessage = "Ange skivans namn.")]
		[Display(Name = "Namn *")]
		public string? Name { get; set; }

		// Artist, obligatorisk uppgift
		[Required(ErrorMessage = "Ange artist.")]
		[Display(Name = "Artist *")]
		public string? Artist { get; set; }

		// Året då skivan släpptes, obligatorisk uppgift. Max fyra tecken (åååå)
		[Required(ErrorMessage = "Ange vilket år skivan släpptes.")]
		[Display(Name = "År *")]
		[MaxLength(4)]
		public string? Year { get; set; }

		// Skivans längd
		[Display(Name = "Längd")]
		public string? Length { get; set; }

		// Skivbolag
		[Display(Name = "Skivbolag")]
		public string? Label { get; set; }

		// Genre, obligatorisk uppgift
		[Required(ErrorMessage = "Ange genre.")]
		[Display(Name = "Genre *")]
		public string? Genre { get; set; }

		// Betyg
		[Display(Name = "Betyg")]
        public string? Rating { get; set; }

		// Tom konstruerare
        public MusicModel() { }

		// Konstruerare som används för att hämta en skiva baserat på det slumpmässiga talet
		public MusicModel(List<MusicModel> albums, int rand)
        {
			/* Lägger till all information i respektive property.
				För icke-obligatoriska properties görs en kontroll för att se om dessa är null. */
			Name = albums[rand].Name;
			Artist = albums[rand].Artist;
			Year = albums[rand].Year;

			if(albums[rand].Length != null)
            {
				Length = albums[rand].Length;
			}

			if(albums[rand].Label != null)
            {
				Label = albums[rand].Label;
            }

			Genre = albums[rand].Genre;

			if(albums[rand].Rating != null)
            {
				Rating = albums[rand].Rating;
            }
		}
	}

	// Vymodell
	public class ViewModel
    {
		// Lista på alla skivor
		public List<MusicModel>? albums { get; set; } = new List<MusicModel>();

		// Konstruerare
		public ViewModel()
        {
			// Lagrar alla skivor om filen finns
			if(System.IO.File.Exists("./wwwroot/albums.json"))
            {
				string jsonString = System.IO.File.ReadAllText("./wwwroot/albums.json");
				albums = JsonSerializer.Deserialize<List<MusicModel>>(jsonString);
			}
		}
	}
}