using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroToRx.Examples.Chapter3.TimeShifted
{
    class Timeout
    {
        public void Example()
        {
            var source = Observable.Interval(TimeSpan.FromMilliseconds(100)).Take(5)
                .Concat(Observable.Interval(TimeSpan.FromSeconds(2)));
            var timeout = source.Timeout(TimeSpan.FromSeconds(1));
            timeout.Subscribe(
                Console.WriteLine,
                Console.WriteLine,
                () => Console.WriteLine("Completed"));

            //0
            //1
            //2
            //3
            //4
            //System.TimeoutException: The operation has timed out.
        }

        public void ExampleByDate()
        {
            var dueDate = DateTimeOffset.UtcNow.AddSeconds(4);
            var source = Observable.Interval(TimeSpan.FromSeconds(1));
            var timeout = source.Timeout(dueDate);
            timeout.Subscribe(
                Console.WriteLine,
                Console.WriteLine,
                () => Console.WriteLine("Completed"));

            //0
            //1
            //2
            //System.TimeoutException: The operation has timed out.
        }
    }
}
