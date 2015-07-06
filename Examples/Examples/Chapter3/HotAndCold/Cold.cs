using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IntroToRx.Examples.Chapter3.HotAndCold
{
    class Cold
    {
        public void SimpleColdSample()
        {
            var period = TimeSpan.FromSeconds(1);
            var observable = Observable.Interval(period);
            observable.Subscribe(i => Console.WriteLine("first subscription : {0}", i));
            Thread.Sleep(period + TimeSpan.FromMilliseconds(50));
            observable.Subscribe(i => Console.WriteLine("second subscription : {0}", i));
            Console.ReadKey();
            
            //first subscription : 0
            //first subscription : 1
            //second subscription : 0
            //first subscription : 2
            //second subscription : 1
            //first subscription : 3
            //second subscription : 2
            //...
        }
    }
}
