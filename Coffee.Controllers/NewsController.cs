using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http;
using Coffee.News.Domain;
using Coffee.News.Service;
using Coffee.Shared;

namespace Coffee.Controllers
{
    public class NewsController : ApiController, INewsService
    {
        private readonly INewsService _newsService;

        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }

        [HttpGet]
        public ResponseResult<IEnumerable<ShortNews>> EnumerateNews(int page, int count)
        {
            return _newsService.EnumerateNews(page, count);
        }

        [HttpGet]
        public ResponseResult<FullNews> GetNews(Guid newsId)
        {
            return _newsService.GetNews(newsId);
        }

        [HttpPost]
        public ResponseResult<Guid> CreateNews(string content, string subject, string description)
        {
            return _newsService.CreateNews(content, subject, description);
        }

        [HttpPost]
        public ResponseResult UpdateNews(Guid newsId, string content, string subject, string description)
        {
            return _newsService.UpdateNews(newsId, content, subject, description);
        }

        [HttpPost]
        public ResponseResult DeleteNews(Guid newsId)
        {
            return _newsService.DeleteNews(newsId);
        }
    }
}
