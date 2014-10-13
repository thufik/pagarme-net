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
using System.Runtime.Serialization;
using JetBrains.Annotations;
using Newtonsoft.Json;
using PagarMe.Serializer;

namespace PagarMe
{
    /// <summary>
    ///     Customer phone
    /// </summary>
    public class CustomerPhone
    {
        private int _ddd;
        private int _ddi;
        private bool _freezed;
        private int _number;

        /// <summary>
        ///     Phone ID in the remote API
        /// </summary>
        [UrlIgnore]
        [JsonProperty(PropertyName = "id"), UsedImplicitly]
        public int Id { get; private set; }

        /// <summary>
        ///     Phone DDI
        /// </summary>
        /// <remarks>
        ///     Optional.
        /// </remarks>
        [JsonProperty(PropertyName = "ddi"), UsedImplicitly]
        public int Ddi
        {
            get { return _ddi; }
            set
            {
                if (_freezed)
                    throw new InvalidOperationException("This value is read-only.");

                _ddi = value;
            }
        }

        /// <summary>
        ///     Phone DDD
        /// </summary>
        [JsonProperty(PropertyName = "ddd"), UsedImplicitly]
        public int Ddd
        {
            get { return _ddd; }
            set
            {
                if (_freezed)
                    throw new InvalidOperationException("This value is read-only.");

                _ddd = value;
            }
        }

        /// <summary>
        ///     Phone number
        /// </summary>
        [JsonProperty(PropertyName = "number"), UsedImplicitly]
        public int Number
        {
            get { return _number; }
            set
            {
                if (_freezed)
                    throw new InvalidOperationException("This value is read-only.");

                _number = value;
            }
        }

        [OnDeserializing]
        private void OnDeserializing(StreamingContext context)
        {
            _freezed = false;
        }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            _freezed = true;
        }
    }
}