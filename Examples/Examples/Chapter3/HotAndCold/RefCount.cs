using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroToRx.Examples.Chapter3.HotAndCold
{
    class RefCount
    {
        public void Example()
        {
            var period = TimeSpan.FromSeconds(1);
            var observable = Observable.Interval(period)
                .Do(l => Console.WriteLine("Publishing {0}", l)) //side effect to show it is running
                .Publish()
                .RefCount();
            //observable.Connect(); Use RefCount instead now
            Console.WriteLine("Press any key to subscribe");
            Console.ReadKey();
            var subscription = observable.Subscribe(i => Console.WriteLine("subscription : {0}", i));
            Console.WriteLine("Press any key to unsubscribe.");
            Console.ReadKey();
            subscription.Dispose();
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();

            //Press any key to subscribe
            //Press any key to unsubscribe.
            //Publishing 0
            //subscription : 0
            //Publishing 1
            //subscription : 1
            //Publishing 2
            //subscription : 2
            //Press any key to exit.
            
        }
    }
}
