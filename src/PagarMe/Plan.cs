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
using System.Security;
using JetBrains.Annotations;
using Newtonsoft.Json;
using PagarMe.Converters;

namespace PagarMe
{
    /// <summary>
    ///     Represents a plan in the remote API
    /// </summary>
    [PagarMeModel("plans")]
    public class Plan : PagarMeModel
    {
        private decimal _amount;
        private string _color;
        private int _days;
        private bool _freezed;
        private string _name;
        private int _trialDays;

        public Plan(PagarMeProvider provider)
            : base(provider)
        {
        }

        internal Plan(PagarMeProvider provider, PagarMeQueryResponse result)
            : base(provider, result)
        {
        }

        /// <summary>
        ///     Transaction status
        /// </summary>
        [JsonProperty(PropertyName = "name"), UsedImplicitly]
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(() => Name);
            }
        }

        /// <summary>
        ///     Plan value
        /// </summary>
        [JsonProperty(PropertyName = "amount"), UsedImplicitly]
        [JsonConverter(typeof(AmountConverter))]
        public decimal Amount
        {
            get { return _amount; }
            set
            {
                if (_freezed)
                    throw new InvalidOperationException("This value is read-only.");

                _amount = value;
                OnPropertyChanged(() => Amount);
            }
        }

        /// <summary>
        ///     Days betwen each payment
        /// </summary>
        [JsonProperty(PropertyName = "days"), UsedImplicitly]
        public int Days
        {
            get { return _days; }
            set
            {
                if (_freezed)
                    throw new InvalidOperationException("This value is read-only.");

                _days = value;
                OnPropertyChanged(() => Days);
            }
        }

        /// <summary>
        ///     Trial days
        /// </summary>
        [JsonProperty(PropertyName = "trial_days"), UsedImplicitly]
        public int TrialDays
        {
            get { return _trialDays; }
            set
            {
                if (_freezed)
                    throw new InvalidOperationException("This value is read-only.");

                _trialDays = value;
                OnPropertyChanged(() => TrialDays);
            }
        }

        /// <summary>
        ///     Plan color on dashboard
        /// </summary>
        [JsonProperty(PropertyName = "color"), UsedImplicitly]
        public string Color
        {
            get { return _color; }
            set
            {
                _color = value;
                OnPropertyChanged(() => Color);
            }
        }

        protected override void Validate()
        {
            if (_days <= 0)
                throw new VerificationException("Days should be positive");

            if (_trialDays < 0)
                throw new VerificationException("TrialDays should be zero or positive");

            base.Validate();
        }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            _freezed = true;
        }

        /// <summary>
        ///     Converts this class to it string representation
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("#{0} {1}", Id, Name);
        }
    }
}