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
            if(!Uri.TryCreate(longUrl, UriKind.Absolute, out _))
            {
                return null;
            }

            var urlEntity = _urlDbContext.Urls.SingleOrDefault(entity => string.Equals(entity.LongUrl, longUrl));

            if(urlEntity != null)
            {
                return urlEntity.ShortRelativeUrl;
            }

            using (var transaction = _urlDbContext.Database.BeginTransaction())
            {
                try
                {
                    urlEntity = new UrlEntity() { LongUrl = longUrl };
                    _urlDbContext.Urls.Add(urlEntity);
                    _urlDbContext.SaveChanges();

                    var shortRelativeUrl = GetByteString(urlEntity.Id);
                    urlEntity.ShortRelativeUrl = shortRelativeUrl;

                    _urlDbContext.Update(urlEntity);

                    _urlDbContext.SaveChanges();

                    transaction.Commit();

                    return shortRelativeUrl;
                }
                catch(Exception)
                {
                    transaction.Rollback();

                    return null;
                }
            }
        }

        public string GetLongUrlFromShortRelativeUrl(string shortRelativeUrl)
        {
            var urlEntity = _urlDbContext.Urls.SingleOrDefault(entity => string.Equals(entity.ShortRelativeUrl, shortRelativeUrl));

            return urlEntity != null ? urlEntity.LongUrl : null;
        }

        private string GetByteString(int number)
        {
            var bytes = BitConverter.GetBytes(number);

            var sb = new StringBuilder();
            foreach(var b in bytes)
            {
                sb.Append(b.ToString("x2"));
            }

            return sb.ToString();
        }
    }
}
