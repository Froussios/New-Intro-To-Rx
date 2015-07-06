using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroToRx.Examples.Chapter4.SequencesOfCoincidence
{
    class GroupJoin
    {
        public void Example()
        {
            var left = Observable.Interval(TimeSpan.FromMilliseconds(100))
                .Take(6);
            var right = Observable.Interval(TimeSpan.FromMilliseconds(200))
                .Select(i => "ABCDEFGHIJKLMNOPQRSTUVWXYZ"[(int)i])
                .Take(3);

            Observable.GroupJoin(
                    left,
                    right,
                    _ => Observable.Never<Unit>(),
                    _ => Observable.Return(Unit.Default),
                    (l, rs) => rs.Subscribe(r => Console.WriteLine(l + " - " + r)))
                .Subscribe();

            //0 - A
            //1 - A
            //0 - B
            //1 - B
            //2 - B
            //3 - B
            //0 - C
            //1 - C
            //2 - C
            //3 - C
            //4 - C
            //5 - C

        }
    }
}
