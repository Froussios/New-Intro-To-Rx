using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Linq;
using System.Reactive;
using Microsoft.Reactive.Testing;

namespace IntroToRx.Examples.Chapter4.SequencesOfCoincidence
{
    class Join
    {
        public void Example()
        {
            var left = Observable.Interval(TimeSpan.FromMilliseconds(100));
            var right = Observable.Interval(TimeSpan.FromMilliseconds(200))
                .Select(i => "ABCDEFGHIJKLMNOPQRSTUVWXYZ"[(int)i])
                .Take(26);

            Observable.Join(
                    left,
                    right,
                    i => Observable.Never<Unit>(),
                    i => Observable.Return(Unit.Default),
                    (l, r) => l + " - " + r)
                .Take(12)
                .Subscribe(Console.WriteLine);

            //0 - B
            //1 - B
            //0 - C
            //1 - C
            //2 - C
            //3 - C
            //0 - D
            //1 - D
            //2 - D
            //3 - D
            //4 - D
            //5 - D
        }
    }
}
