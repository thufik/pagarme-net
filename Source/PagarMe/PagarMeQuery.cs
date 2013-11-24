using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PagarMe
{
    public class PagarMeQuery
    {
        private const string ApiEndpoint = "https://api.pagar.me/1/";

        private static readonly string UserAgent = "pagarme-net " +
                                                   typeof(PagarMeQuery).Assembly.GetName().Version;

        private readonly string _method, _path;
        private readonly List<Tuple<string, string>> _query;

        public int Take { get; set; }

        public PagarMeQuery(PagarMeProvider provider, string method, string path)
        {
            _path = path;
            _method = method;
            _query = new List<Tuple<string, string>>();
            _query.Add(new Tuple<string, string>("api_key", provider.ApiKey));
        }

        public void AddQuery(string key, string value)
        {
            _query.Add(new Tuple<string, string>(key, value));
        }

        public PagarMeQueryResponse Execute()
        {
            UriBuilder builder = new UriBuilder(ApiEndpoint);
            string query =
                _query.Aggregate("",
                    (current, tuple) =>
                        current + (Uri.EscapeUriString(tuple.Item1) + "=" + Uri.EscapeDataString(tuple.Item2) + "&"))
                    .TrimEnd('&');

            builder.Path += _path;
            builder.Query = query;

            if (Take > 0)
                AddQuery("count", Take.ToString(CultureInfo.InvariantCulture));

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(builder.Uri);

            request.Method = _method;
            request.Proxy = null;
            request.UserAgent = UserAgent;

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            PagarMeQueryResponse queryResponse = new PagarMeQueryResponse();

            queryResponse.Status = (int)response.StatusCode;

            using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                queryResponse.Data = reader.ReadToEnd();

            return queryResponse;
        }
    }
}
