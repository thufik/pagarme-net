#region License

// The MIT License (MIT)
// 
// Copyright (c) 2013 Pagar.me
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

#endregion

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace PagarMe
{
    internal class PagarMeQuery
    {
        private const string ApiEndpoint = "https://api.pagar.me/1/";

        private static readonly string UserAgent = "pagarme-net " +
                                                   typeof(PagarMeQuery).Assembly.GetName().Version;

        private readonly string _method, _path;
        private readonly List<Tuple<string, string>> _query;

        public PagarMeQuery(PagarMeProvider provider, string method, string path)
        {
            _path = path;
            _method = method;
            _query = new List<Tuple<string, string>>();
            _query.Add(new Tuple<string, string>("api_key", provider.ApiKey));
        }

        public int Take { get; set; }

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

            if (_method != "POST" || _method == "PUT")
                builder.Query = query;

            if (Take > 0)
                AddQuery("count", Take.ToString(CultureInfo.InvariantCulture));

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(builder.Uri);

            request.Method = _method;
            request.Proxy = null;
            request.UserAgent = UserAgent;

            if (_method == "POST" || _method == "PUT")
            {
                byte[] payload = Encoding.UTF8.GetBytes(query);

                request.ContentLength = payload.Length;
                request.ContentType = "application/x-www-form-urlencoded; charset=utf8";
                request.GetRequestStream().Write(payload, 0, payload.Length);
            }

            HttpWebResponse response;
            PagarMeQueryResponse queryResponse = new PagarMeQueryResponse();

            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                queryResponse.Status = (int)ex.Status;

                using (StreamReader reader = new StreamReader(ex.Response.GetResponseStream(), Encoding.UTF8))
                    queryResponse.Data = reader.ReadToEnd();

                return queryResponse;
            }

            queryResponse.Status = (int)response.StatusCode;

            using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                queryResponse.Data = reader.ReadToEnd();

            return queryResponse;
        }
    }
}