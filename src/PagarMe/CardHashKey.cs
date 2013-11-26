using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace PagarMe
{
    internal class CardHashKey
    {
        [JsonProperty(PropertyName = "id"), UsedImplicitly]
        public int Id { get; private set; }

        [JsonProperty(PropertyName = "date_created"), UsedImplicitly]
        public DateTime DateCreated { get; private set; }

        [JsonProperty(PropertyName = "public_key"), UsedImplicitly]
        public string PublicKey { get; private set; }
    }
}
