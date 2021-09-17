using Data;
using LiteX.Guard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class ShortenUrlService : IShortenUrlService
    {
        private UrlDbContext _urlDbContext;

        public ShortenUrlService(UrlDbContext urlDbContext)
        {
            Guard.NotNull(urlDbContext, nameof(urlDbContext));

            _urlDbContext = urlDbContext;
        }

        public string CreateShortRelativeUrl(string longUrl)
        {
            var urlEntity = new UrlEntity() { LongUrl = longUrl };
            _urlDbContext.Urls.Add(urlEntity);

            var shortRelativeUrl = GetByteString(urlEntity.Id);
            urlEntity.ShortRelativeUrl = shortRelativeUrl;

            _urlDbContext.Update(urlEntity);

            _urlDbContext.SaveChanges();

            return shortRelativeUrl;
        }

        public string GetLongUrlFromShortRelativeUrl(string shortRelativeUrl)
        {
            var urlEntity = _urlDbContext.Urls.SingleOrDefault(entity => string.Equals(entity.ShortRelativeUrl, shortRelativeUrl));

            return urlEntity != null ? urlEntity.LongUrl : null;
        }

        private string GetByteString(int number)
        {
            var bytes = BitConverter.GetBytes(number);

            var result = Convert.ToBase64String(bytes);
            return result;
        }
    }
}
