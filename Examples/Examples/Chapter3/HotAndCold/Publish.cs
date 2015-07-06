using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IntroToRx.Examples.Chapter3.HotAndCold
{
    class Publish
    {
        public void ExampleConnectBeforeSubscribe()
        {
            var period = TimeSpan.FromSeconds(1);
            var observable = Observable.Interval(period).Publish();
            observable.Connect();
            observable.Subscribe(i => Console.WriteLine("first subscription : {0}", i));
            Thread.Sleep(period + TimeSpan.FromMilliseconds(50));
            observable.Subscribe(i => Console.WriteLine("second subscription : {0}", i));

            //first subscription : 0
            //first subscription : 1
            //second subscription : 1
            //first subscription : 2
            //second subscription : 2
            //...
        }

        public void ExampleSubscribeBeforeConnect()
        {
            var period = TimeSpan.FromSeconds(1);
            var observable = Observable.Interval(period).Publish();
            observable.Subscribe(i => Console.WriteLine("first subscription : {0}", i));
            Thread.Sleep(period + TimeSpan.FromMilliseconds(50));
            observable.Subscribe(i => Console.WriteLine("second subscription : {0}", i));
            observable.Connect();

            //first subscription : 0
            //second subscription : 0
            //first subscription : 1
            //second subscription : 1
            //first subscription : 2
            //second subscription : 2
        }

        public void ExampleInteractiveDisconnect()
        {
            var period = TimeSpan.FromSeconds(1);
            var observable = Observable.Interval(period).Publish();
            observable.Subscribe(i => Console.WriteLine("subscription : {0}", i));
            var exit = false;
            while (!exit)
            {
                Console.WriteLine("Press enter to connect, esc to exit.");
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                {
                    var connection = observable.Connect(); //--Connects here--
                    Console.WriteLine("Press any key to dispose of connection.");
                    Console.ReadKey();
                    connection.Dispose(); //--Disconnects here--
                }
                if (key.Key == ConsoleKey.Escape)
                {
                    exit = true;
                }
            }

            //Press enter to connect, esc to exit.
            //Press any key to dispose of connection.
            //subscription : 0
            //subscription: 1
            //subscription: 2
            //Press enter to connect, esc to exit.
            //Press any key to dispose of connection.
            //subscription : 0
            //subscription: 1
            //subscription: 2
            //Press enter to connect, esc to exit.
        }

        public void ExampleAutomaticDisconnect()
        {
            var period = TimeSpan.FromSeconds(1);
            var observable = Observable.Interval(period)
                .Do(l => Console.WriteLine("Publishing {0}", l)) //Side effect to show it is running
                .Publish();
            observable.Connect();
            Console.WriteLine("Press any key to subscribe");
            Console.ReadKey();
            var subscription = observable.Subscribe(i => Console.WriteLine("subscription : {0}", i));
            Console.WriteLine("Press any key to unsubscribe.");
            Console.ReadKey();
            subscription.Dispose();
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();

            //Press any key to subscribe
            //Publishing 0
            //Publishing 1
            //Press any key to unsubscribe.
            //Publishing 2
            //subscription: 2
            //Publishing 3
            //subscription: 3
            //Press any key to exit.
            //Publishing 4
            //Publishing 5
        }
    }
}
