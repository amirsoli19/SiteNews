using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CMSNewModels.Context;
using CMSNewModels.Models;
using CMSNews.App_Start;
using CMSNews.Models.ViewModels;
using CMSNewService.Service;

namespace CMSNews.Areas.Admin.Controllers
{
    public class NewsController : Controller
    {
        private DbCMSNewsContext db = new DbCMSNewsContext();
        private NewsService _newsService;
        private NewsGroupService _newsGroupService;
        private UserService _userService;
        public NewsController()
        {
            _newsService = new NewsService(db);
            _newsGroupService = new NewsGroupService(db);
            _userService = new UserService(db);
        }

        // GET: Admin/News
        public ActionResult Index()
        {
            var news = (_newsService.GetAll());
            var newsViewModel=AutoMapperConfig.mapper.Map<IEnumerable<News>, List<NewsViewModel>>(news);
            return View(newsViewModel);
        }

        // GET: Admin/News/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = _newsService.GetEntity(id.Value);
            if (news == null)
            {
                return HttpNotFound();
            }
            NewsViewModel newsViewModel = AutoMapperConfig.mapper.Map<News, NewsViewModel>(news);
            return View(newsViewModel);
        }

        // GET: Admin/News/Create
        public ActionResult Create()
        {
            ViewBag.NewsGroupId = new SelectList(_newsGroupService.GetAll(), "NewsGroupId", "NewsGroupTitle");
            
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NewsId,NewsTitle,Description,NewsGroupId")] NewsViewModel newsViewModel, HttpPostedFileBase imgUpload)
        {
            if (ModelState.IsValid)
            {
                
                string imageName = "nophoto.png";
                if (imgUpload != null)
                {
                    imageName = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(imgUpload.FileName);
                    imgUpload.SaveAs(Server.MapPath("~/Images/news/") + imageName);
                }

                var news = AutoMapperConfig.mapper.Map<NewsViewModel, News>(newsViewModel);
                news.IsActive = true;
                news.Like = 0;
                news.See = 0;
                news.RegisterDate = DateTime.Now;
                news.UserId = _userService.GetUserId(User.Identity.Name);
                news.ImageName = imageName;
                
                _newsService.Add(news);
                _newsService.Save();
                
                
                return RedirectToAction("Index");
            }

            ViewBag.NewsGroupId = new SelectList(db.NewGroups, "NewsGroupId", "NewsGroupTitle", newsViewModel.NewsGroupId);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "MobileNumber", newsViewModel.UserId);
            return View(newsViewModel);
        }

        // GET: Admin/News/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = _newsService.GetEntity(id.Value);

            if (news == null)
            {
                return HttpNotFound();
            }
            NewsViewModel newsViewModel = AutoMapperConfig.mapper.Map<News, NewsViewModel>(news);

            ViewBag.NewsGroupId = new SelectList(db.NewGroups, "NewsGroupId", "NewsGroupTitle", newsViewModel.NewsGroupId);
            
            return View(newsViewModel);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NewsId,NewsTitle,Description,ImageName,RegisterDate,IsActive,See,Like,NewsGroupId,UserId")] NewsViewModel newsViewModel ,HttpPostedFile imgUpload)
        {
            if (ModelState.IsValid)
            {
                if (imgUpload != null)
                {
                    if (newsViewModel.ImageName != "nophoto.png")
                    {
                        System.IO.File.Delete(Server.MapPath("/Images/news-groups/") + newsViewModel.ImageName);
                    }
                    else
                    {
                        newsViewModel.ImageName = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(imgUpload.FileName);
                    }
                    imgUpload.SaveAs(Server.MapPath("/Images/news/") + newsViewModel.ImageName);

                }
                News news = AutoMapperConfig.mapper.Map<NewsViewModel, News>(newsViewModel);
                _newsService.Update(news);
                _newsService.Save();
                return RedirectToAction("Index");
            }
            ViewBag.NewsGroupId = new SelectList(_newsService.GetAll(), "NewsGroupId", "NewsGroupTitle", newsViewModel.NewsGroupId);
            
            return View(newsViewModel);
        }

        // GET: Admin/News/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = _newsService.GetEntity(id.Value);
            NewsViewModel newsViewModel = AutoMapperConfig.mapper.Map<News, NewsViewModel>(news);

            if (news == null)
            {
                return HttpNotFound();
            }

            return View(newsViewModel);
        }

        // POST: Admin/News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var news = _newsService.GetEntity(id);
            _newsService.Delete(id);
            _newsService.Save();
            
            if (news.ImageName != "nophoto.png")
            {
                System.IO.File.Delete(Server.MapPath("/Images/news-groups/") + news.ImageName);
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            _newsService.Dispose();
            _newsGroupService.Dispose();
            _userService.Dispose();
        }
    }
}
