using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace IntroToRx.Examples
{
    class Create
    {
        private IObservable<string> BlockingMethod()
        {
            var subject = new ReplaySubject<string>();
            subject.OnNext("a");
            subject.OnNext("b");
            subject.OnCompleted();
            Thread.Sleep(1000);
            return subject;
        }

        private IObservable<string> NonBlocking()
        {
            return Observable.Create<string>(
                (IObserver<string> observer) =>
                {
                    observer.OnNext("a");
                    observer.OnNext("b");
                    observer.OnCompleted();
                    Thread.Sleep(1000);
                    return Disposable.Create(() => Console.WriteLine("Observer has unsubscribed"));
                    //or can return an Action like
                    //return () => Console.WriteLine("Observer has unsubscribed");
                });
        }

        public void ExampleBlockingNonblocking()
        {
            var sw = new Stopwatch();

            sw.Restart();
            Console.WriteLine("Calling blocking @" + sw.ElapsedMilliseconds);
            var source1 = BlockingMethod();
            Console.WriteLine("Subscribing to blocking @" + sw.ElapsedMilliseconds);
            source1.Subscribe(Console.WriteLine);
            Console.WriteLine("Completed @" + sw.ElapsedMilliseconds);
            Console.WriteLine();
            sw.Restart();
            Console.WriteLine("Calling non-blocking @" + sw.ElapsedMilliseconds);
            var source2 = NonBlocking();
            Console.WriteLine("Subscribing to non-blocking @" + sw.ElapsedMilliseconds);
            source2.Subscribe(Console.WriteLine);
            Console.WriteLine("Completed @" + sw.ElapsedMilliseconds);

            //Calling blocking @0
            //Subscribing to blocking @1024
            //a
            //b
            //Completed @1028
            //
            //Calling non-blocking @0
            //Subscribing to non - blocking @80
            //a
            //b
            //Observer has unsubscribed
            //Completed @1093

        }

        public void NonBlocking_event_driven()
        {
            var ob = Observable.Create<string>(
                observer =>
                {
                    var timer = new System.Timers.Timer();
                    timer.Interval = 1000;
                    timer.Elapsed += (s, e) => observer.OnNext("tick");
                    timer.Elapsed += OnTimerElapsed;
                    timer.Start();
                    return timer;
                });
            var subscription = ob.Subscribe(Console.WriteLine);
            Console.ReadLine();
            subscription.Dispose();

            //tick
            //01/01/2012 12:00:00
            //tick
            //01/01/2012 12:00:01
            //tick
            //01/01/2012 12:00:02
        }

        public void NonBlocking_event_driven2()
        {
            var ob = Observable.Create<string>(
                observer =>
                {
                    var timer = new System.Timers.Timer();
                    timer.Enabled = true;
                    timer.Interval = 100;
                    timer.Elapsed += OnTimerElapsed;
                    timer.Start();
                    return () => {
                        timer.Elapsed -= OnTimerElapsed;
                        timer.Dispose();
                    };
                });
            var subscription = ob.Subscribe(Console.WriteLine);
            Console.ReadLine();
            subscription.Dispose();

            //tick
            //01/01/2012 12:00:00
            //tick
            //01/01/2012 12:00:01
            //tick
            //01/01/2012 12:00:02
        }

        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine(e.SignalTime);
        }


    }
}
