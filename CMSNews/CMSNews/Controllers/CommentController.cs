using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMSNewService.Service;
using CMSNews.Models;
using CMSNews.Models.ViewModels;
using CMSNewModels.Context;
using CMSNews.App_Start;
using CMSNewModels.Models;

namespace CMSNews.Controllers
{
    
    public class CommentController : Controller
    {
        DbCMSNewsContext db = new DbCMSNewsContext();
        CommentService _commentservice;
        public CommentController()
        {
            _commentservice = new CommentService(db);
        }
        // GET: Comment
        public ActionResult ShowComments(int id) //id is newsId
        {
            var comments=_commentservice.GetAll().Where(t => t.IsActive && t.NewsId == id).OrderByDescending(u => u.CommentId).ToList();
            var commentviewmodels = AutoMapperConfig.mapper.Map < IEnumerable < Comment >, List < CommentViewModel >> (comments);
            return PartialView(commentviewmodels);
        }
        public ActionResult CreateCommend(int id) //id is newsId
        {
            var commentviewmodels = new CommentViewModel();
            commentviewmodels.NewsId = id;
            return PartialView(commentviewmodels);
        }
        [HttpPost]
        public ActionResult CreateCommend([Bind(Include ="NewsId,Name,Email,CommentText")] CommentViewModel commentViewModel) //id is newsId
        {
            if(ModelState.IsValid)
            {
                Comment comment = AutoMapperConfig.mapper.Map<CommentViewModel, Comment>(commentViewModel);
                comment.RegisterDate = DateTime.Now;
                comment.IsActive = false;
                _commentservice.Add(comment);
                _commentservice.Save();
                return Content("نظر شما با موفقیت ارسال و منتظر تایید آن از سوی آدمین سایت بمانید");
            }
            
            return PartialView(commentViewModel);
        }
    }
}