using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IntroToRx.Examples.Chapter4.Scheduling
{
    class SubscribeOn
    {
        public void Example()
        {
            Console.WriteLine("Starting on threadId:{0}", Thread.CurrentThread.ManagedThreadId);
            var source = Observable.Create<int>(
                o =>
                {
                    Console.WriteLine("Invoked on threadId:{0}", Thread.CurrentThread.ManagedThreadId);
                    o.OnNext(1);
                    o.OnNext(2);
                    o.OnNext(3);
                    o.OnCompleted();
                    Console.WriteLine("Finished on threadId:{0}",
                        Thread.CurrentThread.ManagedThreadId);
                    return Disposable.Empty;
                });
            source
                .SubscribeOn(Scheduler.Default)
                .Subscribe(
                    o => Console.WriteLine("Received {1} on threadId:{0}",
                            Thread.CurrentThread.ManagedThreadId,
                            o),
                    () => Console.WriteLine("OnCompleted on threadId:{0}",
                            Thread.CurrentThread.ManagedThreadId));
            Console.WriteLine("Subscribed on threadId:{0}", Thread.CurrentThread.ManagedThreadId);

            //Starting on threadId:9
            //Subscribed on threadId:9
            //Invoked on threadId:10
            //Received 1 on threadId:10
            //Received 2 on threadId:10
            //Received 3 on threadId:10
            //OnCompleted on threadId:10
            //Finished on threadId:10
        }
    }
}
