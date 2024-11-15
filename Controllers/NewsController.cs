using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Portal.Web.App.Data;
using Portal.Web.App.Models;
using Portal.Web.App.ViewModels;

namespace Portal.Web.App.Controllers
{
    public class NewsController : Controller
    {
        private readonly NewsContext _context;

        public NewsController(NewsContext context)
        {
            _context = context;
        }

        // GET: News
        public async Task<IActionResult> Manage(int? id,bool? mode=false)
        {
                      
            ViewBag.IsAddNew = mode;
            

            var model = new NewsViewModel();
            model.NewsList = await _context.News.ToListAsync();
            if (id > 0)
            {
                model.News =await _context.News.FindAsync(id);
                ViewBag.IsAddNew = true;

            }
            else
            {
                model.News = new News();
            }

            return View(model);
        }

        public async Task<IActionResult> Index()
        {
            var data = await _context.News.ToListAsync();

            return View(data.OrderByDescending(a=>a.PostDate).Take(2));
        }

        // GET: News/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = await _context.News
                .FirstOrDefaultAsync(m => m.NewsId == id);
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }

        // GET: News/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: News/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NewsId,Title,Body,Image,PostDate,PostedBy")] News news)
        {
            if (ModelState.IsValid)
            {
                if(news.NewsId > 0)
                {
                    var updata= await _context.News.FindAsync(news.NewsId);
                    if(updata != null)
                    {
                        updata.PostDate = DateTime.Now;

                        if (updata.Title != news.Title)
                            updata.Title = news.Title;

                        if (updata.PostedBy != news.PostedBy)
                            updata.PostedBy = news.PostedBy;

                        if(updata.Body != news.Body)
                            updata.Body = news.Body;

                        _context.News.Update(updata);
                        await _context.SaveChangesAsync();

                    }
                }
                else
                {
                    _context.Add(news);
                    await _context.SaveChangesAsync();
                }
                
                return RedirectToAction(nameof(Manage));
            }
            return View("Manage");
        }

        // GET: News/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = await _context.News.FindAsync(id);
            if (news == null)
            {
                return NotFound();
            }
            return View(news);
        }

        // POST: News/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NewsId,Title,Body,Image,PostDate,PostedBy")] News news)
        {
            if (id != news.NewsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(news);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NewsExists(news.NewsId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Manage));
            }
            return View("Manage");
        }

        // GET: News/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = await _context.News
                .FirstOrDefaultAsync(m => m.NewsId == id);
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }

        // POST: News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var news = await _context.News.FindAsync(id);
            if (news != null)
            {
                _context.News.Remove(news);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NewsExists(int id)
        {
            return _context.News.Any(e => e.NewsId == id);
        }
    }
}
