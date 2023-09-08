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
    public class NewsesController : Controller
    {
        DbCMSNewsContext db = new DbCMSNewsContext();
        NewsGroupService _newsGroupsService;
        NewsService _NewsService;
        public NewsesController()
        {
            _newsGroupsService = new NewsGroupService(db);
            _NewsService = new NewsService(db);
        }
        // GET: Newses
        public ActionResult ShowNewsGroup(int locid)
        {
            ViewBag.Locid = locid;
            var newsgroup = _newsGroupsService.GetAll();
            List<NewsGroupsViewModel> NewsGroupsViewModel = AutoMapperConfig.mapper.Map<IEnumerable<NewGroup>, List<NewsGroupsViewModel>>(newsgroup);
            return PartialView(NewsGroupsViewModel);
        }
        public ActionResult LastNews(int count)
        {
            var lastnews = _NewsService.GetAll().Where(t => t.IsActive).OrderByDescending(u => u.NewsId).Take(count);
            List<NewsViewModel> lastnewsviewmodel = AutoMapperConfig.mapper.Map<IEnumerable<News>, List<NewsViewModel>>(lastnews);
            return PartialView(lastnewsviewmodel);
        }

        public ActionResult BestNews(int count)
        {
            var lastnews = _NewsService.GetAll().Where(t => t.IsActive).OrderByDescending(u => u.See).Take(count);
            List<NewsViewModel> lastnewsviewmodel = AutoMapperConfig.mapper.Map<IEnumerable<News>, List<NewsViewModel>>(lastnews);
            return PartialView(lastnewsviewmodel);
        }

        public ActionResult Lastnews1()
        {
            var lastnews = _NewsService.GetAll().Where(t => t.IsActive).LastOrDefault();
            NewsViewModel lastnewsviewmodel = AutoMapperConfig.mapper.Map<News, NewsViewModel>(lastnews);
            return PartialView(lastnewsviewmodel);
        }
        public ActionResult NewsDetails(int id)
        {
            var news = _NewsService.GetEntity(id);
            if (news == null || !news.IsActive)
            {
                return HttpNotFound();
            }
            news.See++;
            _NewsService.Update(news);
            _NewsService.Save();
            NewsViewModel newsviewmodel = AutoMapperConfig.mapper.Map<News, NewsViewModel>(news);
            return View(newsviewmodel);
        }
        public ActionResult ShowLike(int newsId, bool state)
        {
            var news = _NewsService.GetEntity(newsId);
            NewsLikeViewModel newsLikeViewModel = new NewsLikeViewModel()
            {
                NewsId = newsId,
                Like = news.Like,
                NewsState = state
            };
            return PartialView(newsLikeViewModel);
        }
        public ActionResult ChangeLikeState(int newsId, bool state)
        {
            var news = _NewsService.GetEntity(newsId);
            news.Like = (state) ? (news.Like - 1) : (news.Like + 1);
            _NewsService.Update(news);
            _NewsService.Save();
            return RedirectToAction("ShowLike", new { newsId, state });
        }
        public ActionResult ShowNews(int ?id) //id=newsgroupid
        {

            var listnews=_NewsService.GetAll().Where(t => t.IsActive).OrderByDescending(u => u.RegisterDate).ToList();
            if(id!=null)
            {
                listnews=listnews.Where(t => t.NewsGroupId == id).ToList();
            }
            List<NewsViewModel> lastnewsviewmodel = AutoMapperConfig.mapper.Map<IEnumerable<News>, List<NewsViewModel>>(listnews);

            return View(lastnewsviewmodel);
        }
    }
}