using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroToRx.Examples
{
    class Generate
    {
        public static IObservable<int> Range(int start, int count)
        {
            var max = start + count;
            return Observable.Generate(
                start,
                value => value < max,
                value => value + 1,
                value => value);
        }

        public static IObservable<long> Timer(TimeSpan dueTime)
        {
            return Observable.Generate(
                0L,
                i => i < 1,
                i => i + 1,
                i => i,
                i => dueTime);
        }

        public static IObservable<long> Timer(TimeSpan dueTime, TimeSpan period)
        {
            return Observable.Generate(
                0L,
                i => true,
                i => i + 1,
                i => i,
                i => i == 0 ? dueTime : period);
        }

        public static IObservable<long> Interval(TimeSpan period)
        {
            return Observable.Generate(
                0L,
                i => true,
                i => i + 1,
                i => i,
                i => period);
        }
    }
}
