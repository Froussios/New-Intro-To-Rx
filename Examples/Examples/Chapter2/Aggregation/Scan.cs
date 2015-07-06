using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace IntroToRx.Examples.Chapter2.Aggregation
{
    public class Scan
    {
        public void ExampleRunningSum()
        {
            var numbers = new Subject<int>();
            var scan = numbers.Scan(0, (acc, current) => acc + current);
            numbers.Dump("numbers");
            scan.Dump("scan");
            numbers.OnNext(1);
            numbers.OnNext(2);
            numbers.OnNext(3);
            numbers.OnCompleted();

            //numbers-- > 1
            //scan-- > 1
            //numbers-- > 2
            //scan-- > 3
            //numbers-- > 3
            //scan-- > 6
            //numbers completed
            //scan completed
        }

        public void ExampleMin()
        {
            var numbers = new Subject<int>();
            var min = numbers.RunningMin();
            numbers.Dump("numbers");
            min.Dump("min");
            numbers.OnNext(1);
            numbers.OnNext(2);
            numbers.OnNext(3);
            numbers.OnCompleted();

            //numbers-- > 1
            //min-- > 1
            //numbers-- > 2
            //numbers-- > 3
            //numbers completed
            //min completed


        }

        public void ExampleMax()
        {
            var numbers = new Subject<int>();
            var max = numbers.RunningMax();
            numbers.Dump("numbers");
            max.Dump("max");
            numbers.OnNext(1);
            numbers.OnNext(2);
            numbers.OnNext(3);
            numbers.OnCompleted();

            //numbers-- > 1
            //max-- > 1
            //numbers-- > 2
            //max-- > 2
            //numbers-- > 3
            //max-- > 3
            //numbers completed
            //max completed

        }
    }

    static class Extentions
    {
        public static IObservable<T> RunningMin<T>(this IObservable<T> source)
        {
            var comparer = Comparer<T>.Default;
            Func<T, T, T> minOf = (x, y) => comparer.Compare(x, y) < 0 ? x : y;
            return source.Scan(minOf)
                .DistinctUntilChanged();
        }

        public static IObservable<T> RunningMax<T>(this IObservable<T> source)
        {
            return source.Scan(MaxOf)
                .Distinct();
        }

        private static T MaxOf<T>(T x, T y)
        {
            var comparer = Comparer<T>.Default;
            if (comparer.Compare(x, y) < 0)
            {
                return y;
            }
            return x;
        }
    }
}
