using Microsoft.AspNetCore.Mvc;
using Marvel.Services.Marvel.Character;
using Marvel.Models.Marvel;

namespace Marvel.Controllers
{
    public class Character : Controller
    {
        public Character()
        {
        }

        public IActionResult Index()
        {
            Services.Marvel.Character.Character characters = new Services.Marvel.Character.Character();
            string allCharacter = characters.Get().Result;
            Models.Marvel.APIReturn? aPIReturn = System.Text.Json.JsonSerializer.Deserialize<Models.Marvel.APIReturn>(allCharacter);
            List<Models.Marvel.Character>? charactersList = aPIReturn?.data.results;
            return View(charactersList);
        }
    }
}
