using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace IntroToRx.Examples
{
    class Unsubscribing
    {
        public void Example()
        {
            var values = new Subject<int>();
            var firstSubscription = values.Subscribe(value =>
                Console.WriteLine("1st subscription received {0}", value));
            var secondSubscription = values.Subscribe(value =>
                Console.WriteLine("2nd subscription received {0}", value));
            values.OnNext(0);
            values.OnNext(1);
            values.OnNext(2);
            values.OnNext(3);
            firstSubscription.Dispose();
            Console.WriteLine("Disposed of 1st subscription");
            values.OnNext(4);
            values.OnNext(5);

            //1st subscription received 0
            //2nd subscription received 0
            //1st subscription received 1
            //2nd subscription received 1
            //1st subscription received 2
            //2nd subscription received 2
            //1st subscription received 3
            //2nd subscription received 3
            //Disposed of 1st subscription
            //2nd subscription received 4
            //2nd subscription received 5
        }
    }
}
