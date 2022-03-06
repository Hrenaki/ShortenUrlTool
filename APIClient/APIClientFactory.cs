using System;
using System.Collections.Generic;
using System.Text;

namespace APICommunication
{
    public static class APIClientFactory
    {
        public static IAPIClient CreateDefault()
        {
            return new APIClient();
        }
    }
}
