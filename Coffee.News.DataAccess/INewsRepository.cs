using System;
using System.Collections.Generic;
using Coffee.News.Domain;

namespace Coffee.News.DataAccess
{
    public interface INewsRepository
    {
        IEnumerable<ShortNews> EnumerateNews(int startNumberRow, int endNumberRow);
        
        FullNews GetNews(Guid newsId);

        Guid CreateNews(string content, string subject, string description);
        
        bool UpdateNews(Guid newsId, string content, string subject, string description);
        
        bool DeleteNews(Guid newsId);
    }
}