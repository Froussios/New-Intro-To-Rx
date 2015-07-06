using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroToRx.Examples.Chapter2.Aggregation
{
    class Aggregate
    {
        public void Examples()
        {
            var source = Observable.Range(0, 5);

            var sum = source.MySum();
            var count = source.MyCount();
            var min = source.MyMin();
            var max = source.MyMax();

            sum.Dump("Custom Sum");
            count.Dump("Custom Count");
            min.Dump("Custom Min");
            max.Dump("Custom Max");

            //Custom Sum--> 10
            //Custom Sum completed
            //Custom Count-- > 5
            //Custom Count completed
            //Custom Min-- > 0
            //Custom Min completed
            //Custom Max-- > 4
            //Custom Max completed

        }
    }


    static class Extensions
    {
        public static IObservable<int> MySum(this IObservable<int> source)
        {
            return source.Aggregate(0, (acc, currentValue) => acc + currentValue);
        }

        public static IObservable<int> MyCount(this IObservable<int> source)
        {
            return source.Aggregate(0, (acc, _) => acc + 1);
        }

        public static IObservable<T> MyMin<T>(this IObservable<T> source)
        {
            return source.Aggregate(
                (min, current) => Comparer<T>
                    .Default
                    .Compare(min, current) > 0
                        ? current
                        : min);
        }

        public static IObservable<T> MyMax<T>(this IObservable<T> source)
        {
            var comparer = Comparer<T>.Default;
            Func<T, T, T> max =
                (x, y) =>
                {
                    if (comparer.Compare(x, y) < 0)
                    {
                        return y;
                    }
                    return x;
                };
            return source.Aggregate(max);
        }
    }

}
