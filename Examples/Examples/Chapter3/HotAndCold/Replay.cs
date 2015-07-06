using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IntroToRx.Examples.Chapter3.HotAndCold
{
    class Replay
    {
        public void Example()
        {
            var period = TimeSpan.FromSeconds(1);
            var hot = Observable.Interval(period)
                .Take(3)
                .Publish();
            hot.Connect();
            Thread.Sleep(period); //Run hot and ensure a value is lost.
            var observable = hot.Replay();
            observable.Connect();
            observable.Subscribe(i => Console.WriteLine("first subscription : {0}", i));
            Thread.Sleep(period);
            observable.Subscribe(i => Console.WriteLine("second subscription : {0}", i));
            Console.ReadKey();
            observable.Subscribe(i => Console.WriteLine("third subscription : {0}", i));
            Console.ReadKey();

            //first subscription : 1
            //second subscription : 1
            //first subscription : 2
            //second subscription : 2
            //third subscription : 1
            //third subscription : 2
        }
    }
}
