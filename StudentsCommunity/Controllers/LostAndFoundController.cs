using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentsCommunity.Models;
using StudentsCommunity.ViewModels;

namespace StudentsCommunity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LostAndFoundController : ControllerBase
    {
        private readonly StudentCommunityContext _context;

        public LostAndFoundController(StudentCommunityContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<LostItem>> GetLostItems()
        {

            var result = new List<LostItem>();
            var items = _context.LostItems.Include(x => x.Category).Include(y => y.User).Include(z => z.Image).ToList();

            foreach (var i in items)
            {
                result.Add(new LostItem()
                {
                    Name = i.Name,
                    CategoryName = i.Category.Name,
                    Id = i.Id,
                    Description = i.Description,
                    FoundUserName = i.User.FullName,
                    CategoryId = i.CategoryId,
                    UserId = i.UserId,
                    ImageUrls = i.Image.Select(x => x.Path).ToList()

                });

            }

            return new JsonResult(result);
        }


        [HttpPost("LostItem")]
        [Authorize]
        public async Task<IActionResult> AddLostItemAsync([FromForm] AddLostItem addLostItem)
        {

            LostItems li = new LostItems()
            {
                Name = addLostItem.Name,
                CategoryId = addLostItem.CategoryId,
                Description = addLostItem.Description,
                UserId = addLostItem.UserId
            };

            _context.LostItems.Add(li);
            _context.SaveChanges();
            var a = li.Id;

            foreach(var image in addLostItem.img)
            {
                var extension = "." + image.FileName.Split('.')[image.FileName.Split('.').Length - 1];
                var fileName = Guid.NewGuid().ToString() + extension; //Create a new Name for the file due to security reasons.

                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", fileName);

                using (var bits = new FileStream(path, FileMode.Create))
                {
                    await image.CopyToAsync(bits);
                }

                var source = "http://localhost:5000/images/" + fileName;

                _context.Image.Add(new Image()
                {
                    Path = source,
                    Name = image.FileName,  
                    LostItemId = a
                }
                );
            }
           
            _context.SaveChanges();

            return Ok("success");

        }


        [HttpDelete("claimLostItem/{id}")]//delete image
        [Authorize]
        public IActionResult DeleteItem(int id)
        {
            var im = _context.Image.Where(a => a.LostItemId == id);

            foreach(var i in im)
            {
                string[] src = i.Path.Split('/');
                var dir = Directory.GetCurrentDirectory();
                var deleteSrc = (dir + "\\wwwroot\\images\\" + src[4]);
                System.IO.File.Delete(deleteSrc);


                _context.Image.Remove(i);
            }

            var item = _context.LostItems.FirstOrDefault(a => a.Id == id);

            _context.LostItems.Remove(item);

            _context.SaveChanges();

            return new JsonResult("success");
        }


        [HttpGet("getLostItemByName")]
        public ActionResult<List<LostItem>> GetLostItemByName(string name)
        {

            var result = new List<LostItem>();
            var items = _context.LostItems.Where(x => x.Name.Contains(name)).Include(x => x.Category).Include(y => y.User).Include(z => z.Image).ToList();

            foreach (var i in items)
            {
                result.Add(new LostItem()
                {
                    Name = i.Name,
                    CategoryName = i.Category.Name,
                    Id = i.Id,
                    Description = i.Description,
                    FoundUserName = i.User.FullName,
                    CategoryId = i.CategoryId,
                    UserId = i.UserId,
                    ImageUrls = i.Image.Select(x => x.Path).ToList()

                });

            }

            return new JsonResult(result);
        }

    }
}