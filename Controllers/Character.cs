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

        public HttpRequest GetRequest()
        {
            return Request;
        }

        public IActionResult Index()
        {
            Services.Marvel.Character.Character characters = new Services.Marvel.Character.Character();

            // get from query string passed in the controller call
            int offset = 0;
            if (Request.Query.ContainsKey("offset"))
            {
                offset = int.Parse(s: Request.Query["offset"]);
            }

            characters.offset = offset;

            string allCharacter = characters.Get().Result;
            Models.Marvel.APIReturn? aPIReturn = System.Text.Json.JsonSerializer.Deserialize<Models.Marvel.APIReturn>(allCharacter);
            return View(aPIReturn.data);
        }
    }
}
