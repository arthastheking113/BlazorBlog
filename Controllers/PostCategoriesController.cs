using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BlazorServer.Data;
using BlazorServer.Model;
using BlazorServer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace BlazorServer.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class PostCategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IImageService _imageService;
        private readonly ISlugService _slugService;

        public PostCategoriesController(ApplicationDbContext context,
            IImageService imageService,
            ISlugService slugService)
        {
            _context = context;
            _imageService = imageService;
            _slugService = slugService;
        }

        // GET: PostCategories
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PostCategory.Include(p => p.BlogCategory);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PostCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postCategory = await _context.PostCategory
                .Include(p => p.BlogCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (postCategory == null)
            {
                return NotFound();
            }

            return View(postCategory);
        }

        // GET: PostCategories/Create
        public IActionResult Create()
        {
            ViewData["BlogCategoryId"] = new SelectList(_context.BlogCategory, "Id", "Name");
            return View();
        }

        // POST: PostCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Abstract,Content,CreateDate,UpdateDate,IsproductionReady,Slug,ImageData,ContentType,ViewCount,BlogCategoryId")] PostCategory postCategory, IFormFile image)
        {
            if (ModelState.IsValid)
            {

                postCategory.CreateDate = DateTime.Now;
                postCategory.UpdateDate = postCategory.CreateDate;
                postCategory.ViewCount = 0;

                postCategory.ContentType = _imageService.RecordContentType(image);
                postCategory.ImageData = await _imageService.EncodeFileAsync(image);


                var slug = _slugService.URLFriendly(postCategory.Title);
                if (_slugService.IsUnique(_context, slug))
                {
                    postCategory.Slug = slug;
                }
                else
                {
                    ModelState.AddModelError("Title", "This title cannot be used as it results in a duplicate Slug!");
                    ViewData["BlogCategoryId"] = new SelectList(_context.BlogCategory, "Id", "Name", postCategory.BlogCategoryId);
                    return View(postCategory);
                }
                _context.Add(postCategory);
                await _context.SaveChangesAsync();
                return LocalRedirect("/post/manage");
            }
            ViewData["BlogCategoryId"] = new SelectList(_context.BlogCategory, "Id", "Name", postCategory.BlogCategoryId);
            return View(postCategory);
        }
       
    
    

        // GET: PostCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postCategory = await _context.PostCategory.FindAsync(id);
            if (postCategory == null)
            {
                return NotFound();
            }
            ViewData["BlogCategoryId"] = new SelectList(_context.BlogCategory, "Id", "Name", postCategory.BlogCategoryId);
            return View(postCategory);
        }

        // POST: PostCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Abstract,Content,CreateDate,UpdateDate,IsproductionReady,Slug,ImageData,ContentType,ViewCount,BlogCategoryId")] PostCategory postCategory, IFormFile image, Byte[] imageData, string contentType)
        {
            if (id != postCategory.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var slug = _slugService.URLFriendly(postCategory.Title);
                    if (image != null)
                    {
                        postCategory.ContentType = _imageService.RecordContentType(image);
                        postCategory.ImageData = await _imageService.EncodeFileAsync(image);
                    }
                    else
                    {
                        postCategory.ContentType = contentType;
                        postCategory.ImageData = imageData;
                    }

                    if (slug != postCategory.Slug)
                    {
                        if (_slugService.IsUnique(_context, slug))
                        {
                            postCategory.Slug = slug;
                        }
                        else
                        {
                            ModelState.AddModelError("Title", "This title cannot be used as it results in a duplicate Slug!");
                            ViewData["BlogCategoryId"] = new SelectList(_context.BlogCategory, "Id", "Name", postCategory.BlogCategoryId);
                            return View(postCategory);
                        }
                    }
                    postCategory.UpdateDate = DateTime.Now;
                    _context.Update(postCategory);
                    await _context.SaveChangesAsync();
                    return LocalRedirect($"/post/details/{slug}");
                    //return LocalRedirect($"/BlogPost/Details/{slug}");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostCategoryExists(postCategory.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

            }
            ViewData["BlogCategoryId"] = new SelectList(_context.BlogCategory, "Id", "Name", postCategory.BlogCategoryId);
            return View(postCategory);
        }

        // GET: PostCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postCategory = await _context.PostCategory
                .Include(p => p.BlogCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (postCategory == null)
            {
                return NotFound();
            }

            return View(postCategory);
        }

        // POST: PostCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var postCategory = await _context.PostCategory.FindAsync(id);
            _context.PostCategory.Remove(postCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostCategoryExists(int id)
        {
            return _context.PostCategory.Any(e => e.Id == id);
        }
    }
}
