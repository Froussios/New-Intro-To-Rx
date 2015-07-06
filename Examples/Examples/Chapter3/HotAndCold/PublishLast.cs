using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace IntroToRx.Examples.Chapter3.HotAndCold
{
    class PublishLast
    {
        public void Example()
        {
            var period = TimeSpan.FromSeconds(1);
            var observable = Observable.Interval(period)
                .Take(5)
                .Do(l => Console.WriteLine("Publishing {0}", l)) //side effect to show it is running
                                                                 //.PublishLast();
                .Multicast(new AsyncSubject<long>());
            observable.Connect();
            Console.WriteLine("Press any key to subscribe");
            Console.ReadKey();
            var subscription = observable.Subscribe(i => Console.WriteLine("subscription : {0}", i));
            Console.WriteLine("Press any key to unsubscribe.");
            Console.ReadKey();
            subscription.Dispose();
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();

            //Press any key to subscribe
            //Publishing 0
            //Publishing 1
            //Publishing 2
            //Publishing 3
            //Press any key to unsubscribe.
            //Publishing 4
            //subscription: 4
            //Press any key to exit.


        }
    }
}
