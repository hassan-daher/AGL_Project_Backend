using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentsCommunity.Models;
using StudentsCommunity.ViewModels;

namespace StudentsCommunity.Controllers
{ 

    [Route("api/[controller]")]
    [ApiController]
    public class ResourcesController : ControllerBase
    {
        private readonly StudentCommunityContext _context;

        public ResourcesController(StudentCommunityContext context)
        {
            _context = context;
        }

        [HttpPost("Document")]    
        public async Task<IActionResult> AddLostItemAsync([FromForm] DocumentModel doc)
        {         

            foreach (var image in doc.docs)
            {
                var extension = "." + image.FileName.Split('.')[image.FileName.Split('.').Length - 1];
                var fileName = Guid.NewGuid().ToString() + extension; //Create a new Name for the file due to security reasons.

                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\documents", fileName);

                using (var bits = new FileStream(path, FileMode.Create))
                {
                    await image.CopyToAsync(bits);
                }

                var source = "http://localhost:5000/documents/" + fileName;

                _context.Document.Add(new Document()
                {
                    Path = source,
                    Name = doc.Name,
                    CategoryId = doc.CategoryId,
                    Description = doc.Description,
                    UserId = doc.UserId
                });
            }

            _context.SaveChanges();

            return Ok("success");

        }

        [HttpGet("PartialsDocument")]
        public ActionResult<List<DocumentViewModel>> GetPartialsDocuments(int userId)
        {
            var u = _context.User.Where(x => x.Id == userId).FirstOrDefault();
            var facId = u.FacultyId;

            var result = new List<DocumentViewModel>();
            var items = _context.Document.Where(x => x.User.FacultyId == facId & x.CategoryId == 6).ToList();

            foreach (var i in items)
            {
                result.Add(new DocumentViewModel()
                {         
                   Name = i.Name,
                   CategoryId = i.CategoryId,
                   Description = i.Description,
                   doc = i.Path
                });

            }

            return new JsonResult(result);
        }

        [HttpGet("FinalsDocument")]
        public ActionResult<List<DocumentViewModel>> GetFinalsDocuments(int userId)
        {
            var u = _context.User.Where(x => x.Id == userId).FirstOrDefault();
            var facId = u.FacultyId;

            var result = new List<DocumentViewModel>();
            var items = _context.Document.Where(x => x.User.FacultyId == facId & x.CategoryId == 5).ToList();

            foreach (var i in items)
            {
                result.Add(new DocumentViewModel()
                {
                    Name = i.Name,
                    CategoryId = i.CategoryId,
                    Description = i.Description,
                    doc = i.Path
                });

            }

            return new JsonResult(result);
        }

        [HttpGet("QuizzesDocument")]
        public ActionResult<List<DocumentViewModel>> GetQuizzesDocuments(int userId)
        {
            var u = _context.User.Where(x => x.Id == userId).FirstOrDefault();
            var facId = u.FacultyId;

            var result = new List<DocumentViewModel>();
            var items = _context.Document.Where(x => x.User.FacultyId == facId & x.CategoryId == 7).ToList();

            foreach (var i in items)
            {
                result.Add(new DocumentViewModel()
                {
                    Name = i.Name,
                    CategoryId = i.CategoryId,
                    Description = i.Description,
                    doc = i.Path
                });

            }

            return new JsonResult(result);
        }
    }
}