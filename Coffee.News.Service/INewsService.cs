using System;
using System.Collections.Generic;
using Coffee.News.Domain;
using Coffee.Shared;

namespace Coffee.News.Service
{
    public interface INewsService
    {
        ResponseResult<IEnumerable<ShortNews>> EnumerateNews(int page, int count);
        
        ResponseResult<FullNews> GetNews(Guid newsId);

        ResponseResult<Guid> CreateNews(string content, string subject, string description);
        
        ResponseResult UpdateNews(Guid newsId, string content, string subject, string description);
        
        ResponseResult DeleteNews(Guid newsId);
    }
}