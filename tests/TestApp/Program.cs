using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagarMe;

namespace TestApp
{
    static class Program
    {
        static void Main(string[] args)
        {
            var pagarme = new PagarMeProvider("ak_test_RBORKsHflgcrO7gISMyhatMx8UyiJY", "ek_test_nV2WKtwCedTzEGSLKQpbgDpRj8jdfR");
            var plan = new Plan(pagarme);
            plan.Name += "_Test";
            plan.Amount = 133.34m;
            plan.Days = 30;
            plan.TrialDays = 0;
            plan.Color = "#FF88FF";
            plan.Save();
        }
    }
}
