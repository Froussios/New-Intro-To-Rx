using Microsoft.Reactive.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroToRx.Examples.Chapter4.Testing
{
    class AdvancedFeatures
    {
        public void ExampleITestableObserver()
        {
            var scheduler = new TestScheduler();
            var source = Observable.Interval(TimeSpan.FromSeconds(1), scheduler)
                .Take(4);
            var testObserver = scheduler.Start(
                () => source,
                0,
                0,
                TimeSpan.FromSeconds(5).Ticks);
            Console.WriteLine("Time is {0} ticks", scheduler.Clock);
            Console.WriteLine("Received {0} notifications", testObserver.Messages.Count);
            foreach (Recorded<Notification<long>> message in testObserver.Messages)
            {
                Console.WriteLine("{0} @ {1}", message.Value, message.Time);
            }

            //Time is 50000000 ticks
            //Received 5 notifications
            //OnNext(0) @ 10000001
            //OnNext(1) @ 20000001
            //OnNext(2) @ 30000001
            //OnNext(3) @ 40000001
            //OnCompleted() @ 40000001
        }

        public void ExampleLateITestableObserver()
        {
            var scheduler = new TestScheduler();
            var source = Observable.Interval(TimeSpan.FromSeconds(1), scheduler)
                .Take(4);
            var testObserver = scheduler.Start(
                () => Observable.Interval(TimeSpan.FromSeconds(1), scheduler).Take(4),
                0,
                TimeSpan.FromSeconds(2).Ticks,
                TimeSpan.FromSeconds(5).Ticks);
            Console.WriteLine("Time is {0} ticks", scheduler.Clock);
            Console.WriteLine("Received {0} notifications", testObserver.Messages.Count);
            foreach (Recorded<Notification<long>> message in testObserver.Messages)
            {
                Console.WriteLine("{0} @ {1}", message.Value, message.Time);
            }

            //Time is 50000000 ticks
            //Received 2 notifications
            //OnNext(0) @ 30000000
            //OnNext(1) @ 40000000
        }

        public void ExampleCreateColdObservable()
        {
            var scheduler = new TestScheduler();
            var source = scheduler.CreateColdObservable(
                new Recorded<Notification<long>>(10000000, Notification.CreateOnNext(0L)),
                new Recorded<Notification<long>>(20000000, Notification.CreateOnNext(1L)),
                new Recorded<Notification<long>>(30000000, Notification.CreateOnNext(2L)),
                new Recorded<Notification<long>>(40000000, Notification.CreateOnNext(3L)),
                new Recorded<Notification<long>>(40000000, Notification.CreateOnCompleted<long>())
            );
            var testObserver = scheduler.Start(
                () => source,
                0,
                0,
                TimeSpan.FromSeconds(5).Ticks);
            Console.WriteLine("Time is {0} ticks", scheduler.Clock);
            Console.WriteLine("Received {0} notifications", testObserver.Messages.Count);
            foreach (Recorded<Notification<long>> message in testObserver.Messages)
            {
                Console.WriteLine(" {0} @ {1}", message.Value, message.Time);
            }

            //Time is 50000000 ticks
            //Received 5 notifications
            //OnNext(0) @ 10000001
            //OnNext(1) @ 20000001
            //OnNext(2) @ 30000001
            //OnNext(3) @ 40000001
            //OnCompleted() @ 40000001
        }

        public void ExampleCreateHotObservable()
        {
            var scheduler = new TestScheduler();
            var source = scheduler.CreateHotObservable(
                new Recorded<Notification<long>>(10000000, Notification.CreateOnNext(0L)),
                new Recorded<Notification<long>>(20000000, Notification.CreateOnNext(1L)),
                new Recorded<Notification<long>>(30000000, Notification.CreateOnNext(2L)),
                new Recorded<Notification<long>>(40000000, Notification.CreateOnNext(3L)),
                new Recorded<Notification<long>>(40000000, Notification.CreateOnCompleted<long>())
            );
            var testObserver = scheduler.Start(
                () => source,
                0,
                0,
                TimeSpan.FromSeconds(5).Ticks);
            Console.WriteLine("Time is {0} ticks", scheduler.Clock);
            Console.WriteLine("Received {0} notifications", testObserver.Messages.Count);
            foreach (Recorded<Notification<long>> message in testObserver.Messages)
            {
                Console.WriteLine(" {0} @ {1}", message.Value, message.Time);
            }

            //Time is 50000000 ticks
            //Received 5 notifications
            //OnNext(0) @ 10000000
            //OnNext(1) @ 20000000
            //OnNext(2) @ 30000000
            //OnNext(3) @ 40000000
            //OnCompleted() @ 40000000
        }

        public void ExampleCreateHotObservableLate()
        {
            var scheduler = new TestScheduler();
            var source = scheduler.CreateHotObservable(
                        new Recorded<Notification<long>>(10000000, Notification.CreateOnNext(0L)),
            new Recorded<Notification<long>>(20000000, Notification.CreateOnNext(1L)),
            new Recorded<Notification<long>>(30000000, Notification.CreateOnNext(2L)),
            new Recorded<Notification<long>>(40000000, Notification.CreateOnNext(3L)),
            new Recorded<Notification<long>>(40000000, Notification.CreateOnCompleted<long>())
            );
            var testObserver = scheduler.Start(
                () => source,
                0,
                TimeSpan.FromSeconds(1).Ticks,
                TimeSpan.FromSeconds(5).Ticks);
            Console.WriteLine("Time is {0} ticks", scheduler.Clock);
            Console.WriteLine("Received {0} notifications", testObserver.Messages.Count);
            foreach (Recorded<Notification<long>> message in testObserver.Messages)
            {
                Console.WriteLine(" {0} @ {1}", message.Value, message.Time);
            }

            //Time is 50000000 ticks
            //Received 4 notifications
            //OnNext(1) @ 20000000
            //OnNext(2) @ 30000000
            //OnNext(3) @ 40000000
            //OnCompleted() @ 40000000
        }
    }
}
