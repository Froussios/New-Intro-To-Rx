using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroToRx.Examples.Chapter3.TimeShifted
{
    class Sample
    {
        public void Example()
        {
            var interval = Observable.Interval(TimeSpan.FromMilliseconds(150));
            interval.Sample(TimeSpan.FromSeconds(1))
                .Subscribe(Console.WriteLine);

            //5
            //11
            //18
        }
    }
}
