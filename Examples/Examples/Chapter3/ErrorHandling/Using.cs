using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroToRx.Examples.Chapter3.ErrorHandling
{
   

    class Using
    {
        public class TimeIt : IDisposable
        {
            private readonly string _name;
            private readonly Stopwatch _watch;
            public TimeIt(string name)
            {
                _name = name;
                _watch = Stopwatch.StartNew();
            }
            public void Dispose()
            {
                _watch.Stop();
                Console.WriteLine("{0} took {1}", _name, _watch.Elapsed);
            }
        }

        public void Example()
        {
            var source = Observable.Interval(TimeSpan.FromSeconds(1));
            var result = Observable.Using(
                () => new TimeIt("Subscription Timer"),
                timeIt => source);
            result.Take(5).Dump("Using");

            //Using-->0
            //Using-->1
            //Using-->2
            //Using-->3
            //Using-->4
            //Using completed
            //Subscription Timer took 00:00:05.0138199
        }
    }
}
