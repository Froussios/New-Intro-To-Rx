using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroToRx.Examples.Chapter3.CombiningSequences
{
    class Switch
    {
        public void Example()
        {
            var source = Observable.Interval(TimeSpan.FromMilliseconds(500))
                .Select(i => Observable.Interval(TimeSpan.FromMilliseconds(150))
                    .Select(_ => i));

            Observable.Switch(source)
                .Take(9)
                .Dump("Switch");

            //Switch-->0
            //Switch-->0
            //Switch-->0
            //Switch-->1
            //Switch-->1
            //Switch-->1
            //Switch-->2
            //Switch-->2
            //Switch-->2
            //Switch completed
        }
    }
}
