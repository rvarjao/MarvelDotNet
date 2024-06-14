using Microsoft.AspNetCore.Mvc;

namespace Marvel.Controllers
{
    public class Character : Controller
    {
        public string Index()
        {
            return "view character";
        }
    }
}
