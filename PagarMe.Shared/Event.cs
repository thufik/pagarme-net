using System;
namespace PagarMe
{
    public class Event : Base.Model
    {
        public Event () : this(null)
        {
        }

        public Event (PagarMeService service, string endpointPrefix = "") : base(service, endpointPrefix)
        {
        }

		protected override string Endpoint {
            get {
                return "/events";
            }
        }
    }
}

