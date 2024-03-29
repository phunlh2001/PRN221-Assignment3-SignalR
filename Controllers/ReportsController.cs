﻿using Assignment3.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Assignment3.Controllers
{
    public class ReportsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReportsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var isLogin = HttpContext.Session.GetString("user") != null; // logined
            var isAdmin = HttpContext.Session.GetString("userEmail") == "admin@gmail.com";
            if (!isLogin && !isAdmin)
            {
                return Redirect("/");
            }
            return View();
        }

        [HttpPost]
        public IActionResult GetData(string startDate, string endDate)
        {
            var start = DateTime.Parse(startDate).ToString("dd/MM/yyyy");
            var end = DateTime.Parse(endDate).ToString("dd/MM/yyyy");

            var post = _context.Posts
                .Where(p => p.CreatedDate.CompareTo(start) > 0 && p.UpdatedDate.CompareTo(end) < 0);

            if (post.Count() > 0)
            {
                return Ok(post);
            }

            return NotFound();
        }
    }
}
