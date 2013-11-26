using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace PagarMe
{
    /// <summary>
    /// Represents an error in the remote API request
    /// </summary>
    [UsedImplicitly]
    public class PagarMeError
    {
        /// <summary>
        /// Error type
        /// </summary>
        [JsonProperty(PropertyName = "type"), UsedImplicitly]
        public string Type { get; private set; }

        /// <summary>
        /// Parameter that caused the error
        /// </summary>
        [JsonProperty(PropertyName = "parameter_name"), UsedImplicitly]
        public string ParameterName { get; private set; }

        /// <summary>
        /// Error message
        /// </summary>
        [JsonProperty(PropertyName = "message"), UsedImplicitly]
        public string Message { get; private set; }

        [JsonConstructor]
        internal PagarMeError()
        {
            
        }
    }
}
