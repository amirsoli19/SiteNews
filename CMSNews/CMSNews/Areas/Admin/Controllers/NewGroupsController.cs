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
using CMSNewService.Service;
using CMSNews.Models.ViewModels;
using CMSNews.App_Start;

namespace CMSNews.Areas.Admin.Controllers
{
    public class NewGroupsController : Controller
    {
        private DbCMSNewsContext db = new DbCMSNewsContext();
        NewsGroupService _newsGroupService;
        public NewGroupsController()
        {
            _newsGroupService = new NewsGroupService(db);
        }

        // GET: Admin/NewGroups
        public ActionResult Index()
        {
            var newGroups = _newsGroupService.GetAll();
            var newsGroupViewModels = AutoMapperConfig.mapper.Map<IEnumerable<NewGroup>,List <NewsGroupsViewModel>>(newGroups);
            return View(newsGroupViewModels);
        }

        // GET: Admin/NewGroups/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewGroup newGroup = _newsGroupService.GetEntity(id.Value);
            if (newGroup == null)
            {
                return HttpNotFound();
            }
            var newsGroupViewModel = AutoMapperConfig.mapper.Map<NewGroup, NewsGroupsViewModel>(newGroup);

            return View(newsGroupViewModel);
        }

        // GET: Admin/NewGroups/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/NewGroups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NewsGroupTitle")] NewsGroupsViewModel newsGroupsViewModel,HttpPostedFileBase imgUpload)
        {
            if (ModelState.IsValid)
            {
                string imageName = "nophoto.png";
                if(imgUpload!=null)
                {
                    imageName = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(imgUpload.FileName);
                    imgUpload.SaveAs(Server.MapPath("~/Images/news-groups/") + imageName);
                }
                newsGroupsViewModel.ImageName = imageName;
                newsGroupsViewModel.NewsGroupId = _newsGroupService.NextNewsGroupId();
                NewGroup newsGroup = AutoMapperConfig.mapper.Map<NewsGroupsViewModel, NewGroup>(newsGroupsViewModel);
                _newsGroupService.Add(newsGroup);
                _newsGroupService.Save();
                return RedirectToAction("Index");
            }

            return View(newsGroupsViewModel);
        }

        // GET: Admin/NewGroups/Edit/5




        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewGroup newGroup = _newsGroupService.GetEntity(id.Value);
            if (newGroup == null)
            {
                return HttpNotFound();
            }
            var newsGroupViewModel = AutoMapperConfig.mapper.Map<NewGroup, NewsGroupsViewModel>(newGroup);

            return View(newsGroupViewModel);
        }

        // POST: Admin/NewGroups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.






        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NewsGroupId,NewsGroupTitle,ImageName")] NewsGroupsViewModel newsGroupsViewModel, HttpPostedFileBase imgUpload)
        {
            if (ModelState.IsValid)
            {
                if(imgUpload!=null)
                {
                    if(newsGroupsViewModel.ImageName !="nophoto.png")
                    {
                        System.IO.File.Delete(Server.MapPath("/Images/news-groups/") + newsGroupsViewModel.ImageName);
                    }
                    else
                    {
                        newsGroupsViewModel.ImageName = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(imgUpload.FileName);
                    }
                    imgUpload.SaveAs(Server.MapPath("/Images/news-groups/") + newsGroupsViewModel.ImageName);

                }
                NewGroup newGroup = AutoMapperConfig.mapper.Map<NewsGroupsViewModel,NewGroup> (newsGroupsViewModel);

                _newsGroupService.Update(newGroup);
                _newsGroupService.Save();
                return RedirectToAction("Index");
            }
            return View(newsGroupsViewModel);
        }

        // GET: Admin/NewGroups/Delete/5






        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewGroup newGroup = _newsGroupService.GetEntity(id.Value);
            if (newGroup == null)
            {
                return HttpNotFound();
            }
            var newsGroupViewModel = AutoMapperConfig.mapper.Map<NewGroup, NewsGroupsViewModel>(newGroup);

            return View(newsGroupViewModel);
        }

        // POST: Admin/NewGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var newsGroup = _newsGroupService.GetEntity(id);
            if (newsGroup.ImageName != "nophoto.png")
            {
                System.IO.File.Delete(Server.MapPath("/Images/news-groups/") + newsGroup.ImageName);
            }

            _newsGroupService.Delete(id);
            _newsGroupService.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            _newsGroupService.Dispose();
        }
    }
}
