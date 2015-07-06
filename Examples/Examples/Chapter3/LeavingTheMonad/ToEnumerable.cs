using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroToRx.Examples.Chapter3.LeavingTheMonad
{
    class ToEnumerable
    {
        public void Example()
        {
            var period = TimeSpan.FromMilliseconds(200);
            var source = Observable.Timer(TimeSpan.Zero, period)
                .Take(5);
            var result = source.ToEnumerable();
            foreach (var value in result)
            {
                Console.WriteLine(value);
            }
            Console.WriteLine("done");

            //0
            //1
            //2
            //3
            //4
            //done
        }
    }
}
