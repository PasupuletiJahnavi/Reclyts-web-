using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Loginregister.Models;
using System.Net.Http.Json;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Loginregister.ViewModels;




namespace Loginregister.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;
        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7125");
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }





        public IActionResult dashboard1()
        {
            return View();
        }
        public async Task<IActionResult> Dashboard13()
        {
            var clients = await _context.Clients.ToListAsync();
            var projects = await _context.Projects.ToListAsync();

            var model = new Dashboard13ViewModel
            {
                Clients = clients,
                Projects = projects
            };

            return View(model);
        }



        public IActionResult Dashboard9()
        {
            var tasks = _context.Tasks.ToList();
            var documents = _context.Documents.ToList();
            var tutorials = _context.Tutorials.ToList();
            var updateInfos = _context.UpdateInfos.ToList();

            var uniqueSections = _context.Comments.Select(c => c.Section).Distinct().ToList();

            var comments = new List<IEnumerable<Comment>>();

            foreach (var section in uniqueSections)
            {
                var commentsForSection = _context.Comments.Where(c => c.Section == section).ToList();
                comments.Add(commentsForSection);
            }

            var viewModel = new DashboardViewModel(uniqueSections.Count)
            {
                Tasks = tasks,
                Documents = documents,
                Tutorials = tutorials,
                UpdateInfos = updateInfos,
                Comments = comments
            };

            return View(viewModel);
        }
        //[HttpPost]
        //[Route("api/Main/task")]
        //public IActionResult Addtask([FromBody] Tasks tasks)
        //{

        //    _context.Tasks.Add(tasks);
        //    _context.SaveChanges();

        //    return Ok(); 
        //}


        [HttpPost]
        [Route("api/Main/comment")] 
        public IActionResult AddComment([FromBody] Comment comment)
        {
            
            _context.Comments.Add(comment);
            _context.SaveChanges();

            return Ok(); 
        }
    
//[HttpPost]
//public IActionResult Add([FromBody] Comment comment)
//{
//    try
//    {
//        _context.Comments.Add(comment);
//        _context.SaveChanges();
//        return Ok("Comment added successfully");
//    }
//    catch (Exception ex)
//    {
//        return StatusCode(500, $"An error occurred: {ex.Message}");
//    }
//}

//// Example method to edit a comment
//[HttpPost]
//public IActionResult EditComment(Comment model)
//{
//    // Add logic to update the comment in the database
//    var comment = _context.Comments.Find(model.Id);
//    if (comment != null)
//    {
//        comment.Text = model.Text;
//        comment.Instructions = model.Instructions;
//        // Update other properties as needed
//        _context.SaveChanges();
//    }
//    return RedirectToAction("Dashboard9", "Home"); // Redirect to the dashboard page or any other page
//}

//// Example method to delete a comment
//[HttpPost]
//public IActionResult DeleteComment(int id)
//{
//    // Add logic to delete the comment from the database
//    var comment = _context.Comments.Find(id);
//    if (comment != null)
//    {
//        _context.Comments.Remove(comment);
//        _context.SaveChanges();
//    }
//    return RedirectToAction("Dashboard9", "Home"); // Redirect to the dashboard page or any other page
//}

[HttpPost]
        public IActionResult AddUpdateInfo([FromBody] UpdateInfo updateInfo)
        {
            if (ModelState.IsValid)
            {
                updateInfo.Timestamp = DateTime.UtcNow;

                _context.UpdateInfos.Add(updateInfo);
                _context.SaveChanges();

                return Ok(updateInfo);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        [HttpPost]
        [Route("api/Main/saveDocument")]
        public IActionResult SaveDocument(Document document)
        {
            _context.Documents.Add(document);
            _context.SaveChanges();

            return Ok();
        }
    






        public IActionResult Register()
        {
            return View();
        }

       
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("/api/User/register", model);
            if (response.IsSuccessStatusCode)
            {
               
                return RedirectToAction("Index");
            }
            else
            {
               
                ModelState.AddModelError("", "Registration failed.");
                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("/api/Main/user/login", model);

            if (response.IsSuccessStatusCode)
            {

                return RedirectToAction("Dashboard13", "Home");
            }
            
            else
            {
               
                ModelState.AddModelError("", "Login failed. Please check your credentials.");
                return View(model);
            }
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
