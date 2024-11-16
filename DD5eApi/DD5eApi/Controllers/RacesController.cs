using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DD5eApi.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class RacesController : Controller
	{
		HttpClient _client;

		public RacesController(HttpClient client)
		{
			_client = client;
		}
		[HttpGet]
		public async Task<ActionResult> Get(string race)
		{
			race = race.ToLower();
			if (!Enum.IsDefined(typeof(ValidRacesEnum), race))
			{
				return BadRequest("Please enter a valid race");
			}
			var response = await _client.GetAsync($"https://www.dnd5eapi.co/api/races/{race}");
			if (response.IsSuccessStatusCode)
			{
				// Read the response content as a string (typically JSON)
				var data = await response.Content.ReadAsStringAsync();

				// Return the content as JSON
				return Content(data, "application/json");

			}
			else
			{
				// Handle failure (e.g., race not found)
				return NotFound(); // Or another appropriate error result
			}
		}
	}
}
