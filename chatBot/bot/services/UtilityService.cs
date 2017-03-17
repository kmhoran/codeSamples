using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuoteMuleBot1.Services
{
    public static class UtilityService
    {
        public static int GetRandomFromRange(int Length)
        {
            Random randomInt = new Random();

            return randomInt.Next(0, Length);
        }
    }
}