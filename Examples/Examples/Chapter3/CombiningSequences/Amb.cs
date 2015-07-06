using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IntroToRx.Examples.Chapter3.CombiningSequences
{
    class Amb
    {
        public void Example()
        {
            var s1 = new Subject<int>();
            var s2 = new Subject<int>();
            var s3 = new Subject<int>();
            var result = Observable.Amb(s1, s2, s3);
            result.Subscribe(
                Console.WriteLine,
                () => Console.WriteLine("Completed"));
            s1.OnNext(1);
            s2.OnNext(2);
            s3.OnNext(3);
            s1.OnNext(1);
            s2.OnNext(2);
            s3.OnNext(3);
            s1.OnCompleted();
            s2.OnCompleted();
            s3.OnCompleted();

            //1
            //1
            //Completed
        }

        

        public void ExampleEager()
        {
            Provider.GetSequences().Amb().Dump("Amb");

            //GetSequences() called
            //Yield 1st sequence
            //Yield 2nd sequence
            //Yield 3rd sequence
            //GetSequences() complete
            //1st subscribed to
            //2nd subscribed to
            //3rd subscribed to
            //Amb-->3
            //Amb completed
        }
    }
}
