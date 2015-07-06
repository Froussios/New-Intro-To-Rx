using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroToRx.Examples.Chapter3.CombiningSequences
{
    class Merge
    {
        public void Example()
        {
            //Generate values 0,1,2
            var s1 = Observable.Interval(TimeSpan.FromMilliseconds(250))
                .Take(3);
            //Generate values 100,101,102,103,104
            var s2 = Observable.Interval(TimeSpan.FromMilliseconds(150))
                .Take(5)
                .Select(i => i + 100);
            s1.Merge(s2)
                .Subscribe(
                    Console.WriteLine,
                    () => Console.WriteLine("Completed"));

            //100
            //0
            //101
            //102
            //1
            //103
            //104 // Note this is a race condition. 2 could be
            //2   // published before 104.
        }

        public void ExampleSubscriptionsToSource()
        {
            Provider.GetSequences().Merge().Dump("Merge");

            //GetSequences() called
            //Yield 1st sequence
            //1st subscribed to
            //Yield 2nd sequence
            //2nd subscribed to
            //Merge-->2
            //Merge-->1
            //Yield 3rd sequence
            //3rd subscribed to
            //GetSequences() complete
            //Merge-->3
            //Merge completed
        }
    }
}
