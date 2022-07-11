using marvel.Models;
using Marvel.Api;
using Marvel.Api.Filters;
using Microsoft.AspNetCore.Mvc;

namespace marvel.Controllers
{
    public class HomeController : Controller
    {
        [Route("/v1/public/characters")]
        [HttpGet]
        public JsonResult SomeActionMethod(int orderBy = 0)
        {
            var marvelController = new MarvelController();
            return Json(marvelController.GetAllCharacters(orderBy));
        }

        [Route("/v1/public/character")]
        [HttpGet]
        public JsonResult SomeActionMethod(string name)
        {
            var marvelController = new MarvelController();
            return Json(marvelController.GetCharactersById(name));
        }
    }
}
