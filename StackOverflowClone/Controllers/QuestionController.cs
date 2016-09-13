using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using StackOverflowClone.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace StackOverflowClone.Controllers
{
    [Authorize]
    public class QuestionController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public QuestionController (UserManager<ApplicationUser> userManager, ApplicationDbContext db)
        {
            _userManager = userManager;
            _db = db;
        }
        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            return View(_db.Questions.Where(x => x.User.Id == currentUser.Id));
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Question question)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            question.User = currentUser;
            _db.Questions.Add(question);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Details(int id)
        {
            int questionId = id;
            ViewBag.Question = this._db.Questions.FirstOrDefault(question => question.QuestionId == questionId);
            ViewBag.Answers = this._db.Answers.Where(answer => answer.QuestionId == questionId).ToList();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Details (Answer answer) 
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            answer.User = currentUser;
            _db.Answers.Add(answer);
            _db.SaveChanges();

            ViewBag.Question = this._db.Questions.FirstOrDefault(question => question.QuestionId == answer.QuestionId);
            ViewBag.Answers = this._db.Answers.Where(a => a.QuestionId == answer.QuestionId).ToList();

            return View();

        }
    }
}
