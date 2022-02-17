using Microsoft.AspNetCore.Mvc;

namespace MusicApp.Controllers
{
	public class AlbumController : Controller
	{
		public IActionResult Album()
		{
			return View();
		}
	}
}
