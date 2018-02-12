namespace Superheroes
{
    public class HomeController : Controller
    {
        [HttpGet]
        [Route("")]
        public IActionResult Home()
        {
            return View();
        }

        [HttpGet]
        [Route("signin")]
        public IActionResult SignIn(string username)
        {
            HttpContext.Session.SetString("username", username);
            return RedirectToAction("Index", "Team");
        }
    }
}