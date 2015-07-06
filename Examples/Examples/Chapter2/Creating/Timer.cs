using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroToRx.Examples
{
    class Timer
    {
        public void Example()
        {
            var timer = Observable.Timer(TimeSpan.FromSeconds(1));
            timer.Subscribe(
                Console.WriteLine,
                () => Console.WriteLine("completed"));

            //0
            //completed
        }
    }
}
