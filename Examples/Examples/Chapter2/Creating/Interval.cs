using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroToRx.Examples
{
    class Interval
    {
        public void Example()
        {
            var interval = Observable.Interval(TimeSpan.FromMilliseconds(250));
            interval.Subscribe(
                Console.WriteLine,
                () => Console.WriteLine("completed"));

            //0
            //1
            //2
            //3
            //4
            //5
        }
    }
}
