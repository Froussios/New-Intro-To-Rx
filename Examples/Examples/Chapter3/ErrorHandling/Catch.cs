using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace IntroToRx.Examples.Chapter3.ErrorHandling
{
    class Catch
    {
        public void ExampleSwallow()
        {
            var source = new Subject<int>();
            var result = source.Catch(Observable.Empty<int>());
            result.Dump("Catch");
            source.OnNext(1);
            source.OnNext(2);
            source.OnError(new Exception("Fail!"));

            //Catch-->1
            //Catch-->2
            //Catch completed

        }

        public void ExampleContinue()
        {
            var source = new Subject<int>();
            var result = source.Catch<int, TimeoutException>(tx => Observable.Return(-1));
            result.Dump("Catch");
            source.OnNext(1);
            source.OnNext(2);
            source.OnError(new TimeoutException());

            //Catch-->1
            //Catch-->2
            //Catch-->-1
            //Catch completed
        }

        public void ExampleAllowThrough()
        {
            var source = new Subject<int>();
            var result = source.Catch<int, TimeoutException>(tx => Observable.Return(-1));
            result.Dump("Catch");
            source.OnNext(1);
            source.OnNext(2);
            source.OnError(new ArgumentException("Fail!"));

            //Catch-->1
            //Catch-->2
            //Catch failed-->Fail!
        }
    }
}
