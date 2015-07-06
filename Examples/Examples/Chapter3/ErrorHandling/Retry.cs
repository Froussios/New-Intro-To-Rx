using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroToRx.Examples.Chapter3.ErrorHandling
{
    class Retry
    {
        public void Example()
        {
            var source = Observable.Create<int>(o => 
            {
                o.OnNext(0);
                o.OnNext(1);
                o.OnNext(2);
                o.OnError(new Exception("Fail"));
                return () => { };
            });
            source.Retry().Subscribe(t => Console.WriteLine(t)); //Will always retry
            Console.ReadKey();

            //0
            //1
            //2
            //0
            //1
            //2
            //0
            //1
            //2
            //...
        }

        public void ExampleWithLimit()
        {
            var source = Observable.Create<int>(o =>
            {
                o.OnNext(0);
                o.OnNext(1);
                o.OnNext(2);
                o.OnError(new Exception("Fail"));
                return () => { };
            });
            source.Retry(2).Dump("Retry(2)");

            //Retry(2)-->0
            //Retry(2)-->1
            //Retry(2)-->2
            //Retry(2)-->0
            //Retry(2)-->1
            //Retry(2)-->2
            //Retry(2) failed-->Fail

        }
    }
}
