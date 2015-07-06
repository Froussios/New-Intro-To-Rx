using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroToRx.Examples.Chapter3.TimeShifted
{
    class Delay
    {
        public void Example()
        {
            var source = Observable.Interval(TimeSpan.FromSeconds(1))
                .Take(5)
                .Timestamp();
            var delay = source.Delay(TimeSpan.FromSeconds(2));
            source.Subscribe(
                value => Console.WriteLine("source : {0}", value),
                () => Console.WriteLine("source Completed"));
            delay.Subscribe(
                value => Console.WriteLine("delay : {0}", value),
                () => Console.WriteLine("delay Completed"));

            //source: 0@01/01/2012 12:00:00 pm + 00:00
            //source: 1@01/01/2012 12:00:01 pm + 00:00
            //source: 2@01/01/2012 12:00:02 pm + 00:00
            //delay: 0@01/01/2012 12:00:00 pm + 00:00
            //source: 3@01/01/2012 12:00:03 pm + 00:00
            //delay: 1@01/01/2012 12:00:01 pm + 00:00
            //source: 4@01/01/2012 12:00:04 pm + 00:00
            //source Completed
            //delay: 2@01/01/2012 12:00:02 pm + 00:00
            //delay: 3@01/01/2012 12:00:03 pm + 00:00
            //delay: 4@01/01/2012 12:00:04 pm + 00:00
            //delay Completed
        }
    }
}
