using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DbConnection;

namespace Superheroes
{
    public class HeroController : Controller
    {
        [HttpGet]
        [Route("heroes")]
        //render a page that shows all the heroes in the database 
        //and allows the user to add a new hero
        public IActionResult ShowHeroes() 
        {
            ViewBag.username = HttpContext.Session.GetString("username");
            ViewBag.allTeams = MyDbConnector.Query("SELECT * FROM teams");
            ViewBag.allHeroes = MyDbConnector.Query("SELECT heroes.*, teams.Name as teamname FROM heroes JOIN teams ON teams.TeamId = heroes.TeamId");
            return View("Index");
        }

        [HttpPost]
        [Route("heroes/add")]
        public IActionResult AddHeroToDB(string realName, string superheroName, string villainCheck, string power, int team)
        {
            bool isVillain = true;
            if (villainCheck == null) {
                isVillain = false;
            }
            MyDbConnector.Execute($"INSERT INTO heroes (realname, SuperheroName, isvillain, power, TeamId) VALUES ('{realName}', '{superheroName}', {isVillain}, '{power}', {team});");
            return RedirectToAction("ShowHeroes");
        }
    }
}