using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Linq;

namespace IntroToRx.Examples.Chapter3.ErrorHandling
{
    class OnErrorResumeNext
    {
        public void Example()
        {
            var source = Observable.Create<int>(o =>
            {
                o.OnNext(0);
                o.OnNext(1);
                o.OnError(new Exception("Fail"));
                return () => { };
            });

            source
                .OnErrorResumeNext(Observable.Return(-1))
                .Subscribe(Console.WriteLine);

            //0
            //1
            //-1
        }
    }
}
