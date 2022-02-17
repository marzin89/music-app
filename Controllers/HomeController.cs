// Controller
// Inkluderar MVC, modellen samt JSON
using Microsoft.AspNetCore.Mvc;
using MusicApp.Models;
using System.Text.Json;

namespace MusicApp.Controllers
{
    public class HomeController : Controller
    {
        // Visar startsidan
        public IActionResult Index()
        {
            // Ny instans av vymodellen
            ViewModel vm = new ViewModel();

            // Väljer ut en skiva slumpmässigt ut listan
            Random random = new Random();

            // Kontrollerar att listan inte är tom
            if(vm.albums.Count > 0)
            {
                int rand = random.Next(0, vm.albums.Count);
                MusicModel album = new MusicModel(vm.albums, rand);

                // Lagrar det slumpmässiga talet i en sessionsvariabel
                HttpContext.Session.SetInt32("random", rand);

                // Lagrar den slumpmässigt utvalda skivan
                ViewData["name"] = album.Name;
                ViewData["artist"] = album.Artist;
                ViewData["year"] = album.Year;
                ViewData["length"] = album.Length;
                ViewData["label"] = album.Label;
                ViewData["genre"] = album.Genre;
                ViewData["rating"] = album.Rating;
            }

            // Returnerar vyn
            return View();
        }

        // Visar musiksidan
        public IActionResult Music()
        {
            // Ny instans av vymodellen
            ViewModel vm = new ViewModel();

            // Returnerar vyn med listan på skivor som parameter
            return View(vm.albums);
        }

        // Visar formuläret
        public IActionResult Add()
        {
            // Returnerar vyn
            return View();
        }

        // Lägger till en skiva när formuläret skickas
        [HttpPost]
        public IActionResult Add(MusicModel album)
		{
            // Kontrollerar om formuläret är korrekt ifyllt
            if(ModelState.IsValid)
			{
                // Ny instans av vymodellen
                ViewModel vm = new ViewModel();

                // Lägger till skivan i listan
                vm.albums.Add(album);

                // Serialiserar listan och uppdaterar filen
                JsonSerializerOptions options = new() { WriteIndented = true };
                string jsonString = JsonSerializer.Serialize(vm.albums, options);
                System.IO.File.WriteAllText("./wwwroot/albums.json", jsonString);

                // Lägger till ett bekräftelsemeddelande
                ViewData["confirm"] = "Skivan har lagts till.";

                // Rensar formuläret
                ModelState.Clear();
			}
            else
            {
                // Lägger till ett felmeddelande om det inte gick att lägga till skivan
                ViewData["error"] = "Det gick inte att lägga till skivan.";
            }

            // Returnerar den uppdaterade vyn
            return View();
		}

        // Visar undersidan för utvald skiva
        public  IActionResult Album()
        {
            // Kontrollerar att sessionen inte har gått ut
            if(HttpContext.Session.GetInt32("random") != null)
            {
                // Konverterar det slumpmässiga talet till ett heltal
                int rand = (int)HttpContext.Session.GetInt32("random");

                // Ny instans av vymodellen
                ViewModel vm = new ViewModel();

                // Hämtar den slumpmässigt utvalda skivan
                MusicModel album = new MusicModel(vm.albums, rand);

                // Lagrar den slumpmässigt utvalda skivan
                ViewBag.Name = album.Name;
                ViewBag.Artist = album.Artist;
                ViewBag.Year = album.Year;
                ViewBag.Length = album.Length;
                ViewBag.Label = album.Label;
                ViewBag.Genre = album.Genre;
                ViewBag.Rating = album.Rating;
            }
            else
            {
                // Lägger till statuskod 400 om sessionen har gått ut
                ViewBag.Name = "400";
            }

            // Returnerar vyn
            return View();
        }

        // Lagrar betyget när formuläret skickas
        [HttpPost]
        public IActionResult Album(string Rating)
		{
            // Kontrollerar att sessionen inte har gått ut
            if(HttpContext.Session.GetInt32("random") != null)
            {
                // Konverterar det slumpmässiga talet till ett heltal
                int rand = (int)HttpContext.Session.GetInt32("random");

                // Ny instans av vymodellen
                ViewModel vm = new ViewModel();

                // Hämtar den slumpmässigt utvalda skivan
                MusicModel album = new MusicModel(vm.albums, rand);

                // Lagrar den slumpmässigt utvalda skivan, denna gång med betyg
                ViewBag.Name = album.Name;
                ViewBag.Artist = album.Artist;
                ViewBag.Year = album.Year;
                ViewBag.Length = album.Length;
                ViewBag.Label = album.Label;
                ViewBag.Genre = album.Genre;
                ViewBag.Rating = Rating;
                
                // Loopar igenom listan på skivor och lägger till betyget
                foreach(MusicModel item in vm.albums)
				{
                    if(item.Name == album.Name)
					{
                        item.Rating = Rating;
					}
				}

                // Serialiserar listan och uppdaterar filen
                JsonSerializerOptions options = new() { WriteIndented = true };
                string jsonString = JsonSerializer.Serialize(vm.albums, options);
                System.IO.File.WriteAllText("./wwwroot/albums.json", jsonString);


                /* Lägger till ett bekräftelsemeddelande eller ett felmeddelande beroende på
                    om det gick att lägga till betyget eller inte */
                if (ViewBag.Rating != null)
                {
                    ViewBag.Confirm = "Betyget har lagts till.";
                }
                else
                {
                    ViewBag.Error = "Det gick inte att lägga till betyget.";
                }
            }
            // Lägger till statuskod 400 om sessionen har gått ut
            else
            {
                ViewBag.Name = "400";
            }   

            // Returnerar den uppdaterade vyn
            return View();
		}
    }
}