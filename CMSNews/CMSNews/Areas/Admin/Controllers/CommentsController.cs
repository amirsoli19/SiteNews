﻿using System;
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
    public class CommentsController : Controller
    {
        private DbCMSNewsContext db = new DbCMSNewsContext();
        CommentService _commentService;

        public CommentsController()
        {
            _commentService = new CommentService(db);
        }
        

        // GET: Admin/Comments
        public ActionResult Index()
        {
            var comments = _commentService.GetAll();
            var commentViewMode = AutoMapperConfig.mapper.Map<IEnumerable<Comment> , List<CommentViewModel>>(comments);
            return View(commentViewMode);
        }

        // GET: Admin/Comments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

       
        

        // GET: Admin/Comments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = _commentService.GetEntity(id.Value);
            if (comment == null)
            {
                return HttpNotFound();
            }
            ViewBag.NewsTitle = comment.News.NewsTitle;
            var commentViewModel = AutoMapperConfig.mapper.Map<Comment, CommentViewModel>(comment);
            return View(commentViewModel);
        }

        // POST: Admin/Comments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CommentId,CommentText,Name,Email,RegisterDate,IsActive,NewsId")] CommentViewModel commentView)
        {
            if (ModelState.IsValid)
            {
                var comment = AutoMapperConfig.mapper.Map<CommentViewModel, Comment>(commentView);
                _commentService.Update(comment);
                _commentService.Save();
                db.Entry(comment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.NewsId = new SelectList(db.News, "NewsId", "NewsTitle", commentView.NewsId);
            return View(commentView);
        }

        // GET: Admin/Comments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: Admin/Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comment comment = db.Comments.Find(id);
            db.Comments.Remove(comment);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
