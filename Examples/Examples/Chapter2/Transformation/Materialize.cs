using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace IntroToRx.Examples.Chapter2.Transformation
{
    class Materialize
    {
        public void Example()
        {
            Observable.Range(1, 3)
                .Materialize()
                .Dump("Materialize");

            //Materialize-->OnNext(1)
            //Materialize-->OnNext(2)
            //Materialize-->OnNext(3)
            //Materialize-->OnCompleted()
            //Materialize completed
        }

        public void ExampleOnError()
        {
            var source = new Subject<int>();
            source.Materialize()
                .Dump("Materialize");
            source.OnNext(1);
            source.OnNext(2);
            source.OnNext(3);
            source.OnError(new Exception("Fail?"));

            //Materialize-->OnNext(1)
            //Materialize-->OnNext(2)
            //Materialize-->OnNext(3)
            //Materialize-->OnError(System.Exception)
            //Materialize completed
        }
    }
}
