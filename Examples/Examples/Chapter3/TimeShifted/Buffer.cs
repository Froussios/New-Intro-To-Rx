using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroToRx.Examples.Chapter3.TimeShifted
{
    class Buffer
    {
        public void ExampleByCountTime()
        {
            var idealBatchSize = 15;
            var maxTimeDelay = TimeSpan.FromSeconds(3);
            var source = Observable.Interval(TimeSpan.FromSeconds(1)).Take(10)
                .Concat(Observable.Interval(TimeSpan.FromSeconds(0.01)).Take(100));
            source.Buffer(maxTimeDelay, idealBatchSize)
                .Subscribe(
                    buffer => Console.WriteLine("Buffer of {1} @ {0}", DateTime.Now, buffer.Count),
                    () => Console.WriteLine("Completed"));

            //Buffer of 3 @ 01/01/2012 12:00:03
            //Buffer of 3 @ 01/01/2012 12:00:06
            //Buffer of 3 @ 01/01/2012 12:00:09
            //Buffer of 15 @ 01/01/2012 12:00:10
            //Buffer of 15 @ 01/01/2012 12:00:10
            //Buffer of 15 @ 01/01/2012 12:00:10
            //Buffer of 15 @ 01/01/2012 12:00:11
            //Buffer of 15 @ 01/01/2012 12:00:11
            //Buffer of 15 @ 01/01/2012 12:00:11
            //Buffer of 11 @ 01/01/2012 12:00:11
        }

        public void ExampleOverlappingCount()
        {
            var source = Observable.Interval(TimeSpan.FromSeconds(1)).Take(10);
            source.Buffer(3, 1)
                .Subscribe(
                    buffer =>
                    {
                        Console.WriteLine("--Buffered values");
                        foreach (var value in buffer)
                        {
                            Console.WriteLine(value);
                        }
                    }, () => Console.WriteLine("Completed"));

            //--Buffered values
            //0
            //1
            //2
            //--Buffered values
            //1
            //2
            //3
            //--Buffered values
            //2
            //3
            //4
            //--Buffered values
            //3
            //4
            //5
            //...
        }

        public void ExampleSkippingCount()
        {
            var source = Observable.Interval(TimeSpan.FromSeconds(1)).Take(10);
            source.Buffer(3, 5)
                .Subscribe(
                    buffer =>
                    {
                        Console.WriteLine("--Buffered values");
                        foreach (var value in buffer)
                        {
                            Console.WriteLine(value);
                        }
                    }, () => Console.WriteLine("Completed"));

            //--Buffered values
            //0
            //1
            //2
            //--Buffered values
            //5
            //6
            //7
            //Completed
        }

        public void ExampleOverlappingTime()
        {
            var source = Observable.Interval(TimeSpan.FromSeconds(1)).Take(10);
            var overlapped = source.Buffer(TimeSpan.FromSeconds(3), TimeSpan.FromSeconds(1));
            overlapped.Subscribe(
                buffer =>
                {
                    Console.WriteLine("--Buffered values");
                    foreach (var value in buffer)
                    {
                        Console.WriteLine(value);
                    }
                }, () => Console.WriteLine("Completed"));

            //--Buffered values
            //0
            //1
            //2
            //--Buffered values
            //0
            //1
            //2
            //3
            //--Buffered values
            //2
            //3
            //4
            //--Buffered values
            //3
            //4
            //5
            //--Buffered values
            //4
            //5
            //6
            //--Buffered values
            //5
            //6
            //7
            //--Buffered values
            //6
            //7
            //8
            //--Buffered values
            //7
            //8
            //9
            //--Buffered values
            //8
            //9
            //--Buffered values
            //9
            //Completed

        }

        public void ExampleSkippingTime()
        {
            var source = Observable.Interval(TimeSpan.FromSeconds(1)).Take(10);
            var skipped = source.Buffer(TimeSpan.FromSeconds(3), TimeSpan.FromSeconds(5));
            skipped.Subscribe(
                buffer =>
                {
                    Console.WriteLine("--Buffered values");
                    foreach (var value in buffer)
                    {
                        Console.WriteLine(value);
                    }
                }, () => Console.WriteLine("Completed"));

            //--Buffered values
            //0
            //1
            //--Buffered values
            //4
            //5
            //6
            //--Buffered values
            //9
            //Completed

        }

        public void ExampleDynamic1()
        {
            Observable.Interval(TimeSpan.FromMilliseconds(100))
                .Buffer(Observable.Interval(TimeSpan.FromMilliseconds(300)))
                .Subscribe(b => Console.WriteLine(String.Join(", ", b)));

            //0, 1
            //2, 3, 4
            //5, 6, 7
            //8, 9, 10
            //11, 12, 13
            //14, 15
            //16, 17, 18
            //19, 20, 21
            //22, 23, 24
            //25, 26, 27
        }

        public void ExampleDynamic2()
        {
            Observable.Interval(TimeSpan.FromMilliseconds(100))
                .Buffer(() => Observable.Timer(TimeSpan.FromMilliseconds(300)))
                .Subscribe(b => Console.WriteLine(String.Join(", ", b)));

            //0, 1
            //2, 3, 4
            //5, 6, 7
            //8, 9, 10
            //11, 12, 13
            //14, 15
            //16, 17, 18
            //19, 20, 21
            //22, 23, 24
            //25, 26, 27
        }

        public void ExampleDynamicOverlapping()
        {
            Observable.Interval(TimeSpan.FromMilliseconds(100))
                .Buffer(
                    Observable.Timer(TimeSpan.Zero, TimeSpan.FromMilliseconds(100)),
                    (v) => Observable.Timer(TimeSpan.FromMilliseconds(100 * v)))
                .Subscribe(b => Console.WriteLine(String.Join(", ", b)));

            //1
            //2, 3
            //3, 4, 5
            //4, 5, 6, 7
            //5, 6, 7, 8, 9
            //...
        }
    }
}
