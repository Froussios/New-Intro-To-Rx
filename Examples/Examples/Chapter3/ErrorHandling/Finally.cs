using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace IntroToRx.Examples.Chapter3.ErrorHandling
{
    class Finally
    {
        public void Example()
        {
            var source = new Subject<int>();
            var result = source.Finally(() => Console.WriteLine("Finally action ran"));
            result.Dump("Finally");
            source.OnNext(1);
            source.OnNext(2);
            source.OnNext(3);
            source.OnCompleted();

            //Finally-- > 1
            //Finally-- > 2
            //Finally-- > 3
            //Finally completed
            //Finally action ran
        }

        public void ExampleUnsubscription()
        {
            var source = new Subject<int>();
            var result = source.Finally(() => Console.WriteLine("Finally"));
            var subscription = result.Subscribe(
                Console.WriteLine,
                Console.WriteLine,
                () => Console.WriteLine("Completed"));
            source.OnNext(1);
            source.OnNext(2);
            source.OnNext(3);
            subscription.Dispose();

            //1
            //2
            //3
            //Finally
        }
    }
}
