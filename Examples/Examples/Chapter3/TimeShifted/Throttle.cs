using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Linq;

namespace IntroToRx.Examples.Chapter3.TimeShifted
{
    class Throttle
    {
        public void Example()
        {
            var source = Observable.Concat(
                    Observable.Interval(TimeSpan.FromMilliseconds(200)).Take(3),
                    Observable.Interval(TimeSpan.FromMilliseconds(100)).Take(3),
                    Observable.Interval(TimeSpan.FromMilliseconds(200)).Take(3)
                )
                .Select((_, i) => i);

            source.Throttle(TimeSpan.FromMilliseconds(150))
                .Subscribe(Console.WriteLine);

            //0
            //1
            //5
            //6
            //7
            //8
        }
    }
}
