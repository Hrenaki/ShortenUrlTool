using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using APICommunication;

namespace ShortenUrlDesktop
{
    internal class ShortenUrlViewModel : INotifyPropertyChanged
    {
        private IAPIClient client;

        #region Properties
        private string longUrl;
        public string LongUrl
        {
            get => longUrl;
            set
            {
                if(!string.IsNullOrWhiteSpace(value) && value != longUrl)
                {
                    longUrl = value;
                    OnPropertyChanged(nameof(LongUrl));
                }
            }
        }

        private string shortUrl;
        public string ShortUrl
        {
            get => shortUrl;
            set
            {
                if (!string.IsNullOrWhiteSpace(value) && value != shortUrl)
                {
                    shortUrl = value;
                    OnPropertyChanged(nameof(ShortUrl));
                }
            }
        }
        #endregion

        #region Commands
        private RelayCommand getShortUrlCommand;
        public RelayCommand GetShortUrlCommand
        {
            get => getShortUrlCommand ?? (getShortUrlCommand = new RelayCommand(obj => GetShortUrl()));
        }

        private RelayCommand getLongUrlByShortCommand;
        public RelayCommand GetLongUrlByShortCommand
        {
            get => getLongUrlByShortCommand ?? (getLongUrlByShortCommand = new RelayCommand(obj => GetLongUrlByShort()));
        }
        #endregion

        public ShortenUrlViewModel()
        {
            client = APIClientFactory.CreateDefault();
        }

        public async void GetShortUrl()
        {
            ShortUrl = await client.GetAbsoluteShortURLAsync(LongUrl);
        }

        public async void GetLongUrlByShort()
        {
            LongUrl = await client.GetOriginURLByAbsoluteShortURL(shortUrl);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
