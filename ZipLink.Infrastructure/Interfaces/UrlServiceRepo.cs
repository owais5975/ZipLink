using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZipLink.Core;
using ZipLink.Core.Commons;
using ZipLink.Core.Entities;
using ZipLink.Infrastructure.Implementation;

namespace ZipLink.Infrastructure.Interfaces
{
    public class UrlServiceRepo : IUrlServiceRepo
    {
        private ApplicationDBContext _context;
        private readonly IConfiguration _config;

        public UrlServiceRepo(ApplicationDBContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public string CreateURL(string url)
        {
            string shortUrl;
            do shortUrl = string.Format(_config.GetSection("BaseURL").Value, Guid.NewGuid().ToString().Replace("-", "").Substring(0, 8));
            while (IsExist(shortUrl));

            _context.Urls.Add(new Url
            {
                LongURL = url,
                ShortURL = shortUrl,
            });

            _context.SaveChanges();

            return shortUrl;
        }

        public string? GetURL(string url) =>
            _context.Urls.AsNoTracking().FirstOrDefault(_ => _.ShortURL == url && !_.IsDeleted && _.ExpiryDateTime > DateTime.Now)?.LongURL
            ?? CustomMessages.URL_NOT_FOUND;

        // Private Methods
        private bool IsExist(string url) =>
            _context.Urls.AsNoTracking().Any(_ => _.ShortURL == url);
    }
}
