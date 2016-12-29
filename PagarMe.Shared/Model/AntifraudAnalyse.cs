using PagarMe.Enumeration;
using System;
using System.Collections.Generic;
using System.Text;

namespace PagarMe.Model
{
    public class AntifraudAnalyse : Base.Model
    {
        protected override string Endpoint { get { return "/antifraud_analyses"; } }

        public AntifraudAnalyse() : this(null) { }

        public AntifraudAnalyse(PagarMeService service) : base(service) { }

        public AntifraudAnalyse(PagarMeService serivce, string endpointPrefix = "") : base(serivce, endpointPrefix) { }

        public string Name
        {
            get { return GetAttribute<string>("name"); }
        }

        public double Score
        {
            get { return GetAttribute<double>("score"); }
        }

        public int Cost
        {
            get { return GetAttribute<int>("cost"); }
        }

        public AntifraudStatus status
        {
            get { return GetAttribute<AntifraudStatus>("status"); }
        }
    }
}
