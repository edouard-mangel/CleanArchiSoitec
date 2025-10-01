using Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcceptanceTests
{
    internal class DateTimeProviderFake : IDateTimeProvider
    {
        private DateTime _now;

        public DateTime Now() => _now;

        public void FeedDateTime(DateTime dateTime) => _now = dateTime;
    }
}
