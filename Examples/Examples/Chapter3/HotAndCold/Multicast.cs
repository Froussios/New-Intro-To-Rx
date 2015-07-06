using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IntroToRx.Examples.Chapter3.HotAndCold
{
    class Multicast
    {
        public void ExampleWithoutmulticast()
        {
            var period = TimeSpan.FromSeconds(1);
            //var observable = Observable.Interval(period).Publish();
            var observable = Observable.Interval(period);
            var shared = new Subject<long>();
            shared.Subscribe(i => Console.WriteLine("first subscription : {0}", i));
            observable.Subscribe(shared); //'Connect' the observable.
            Thread.Sleep(period);
            Thread.Sleep(period);
            shared.Subscribe(i => Console.WriteLine("second subscription : {0}", i));

            //first subscription : 0
            //first subscription : 1
            //second subscription : 1
            //first subscription : 2
            //second subscription : 2
        }
    }
}
