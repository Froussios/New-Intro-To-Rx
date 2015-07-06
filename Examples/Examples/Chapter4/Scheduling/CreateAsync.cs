using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IntroToRx.Examples.Chapter4.Scheduling
{
    class CreateAsync
    {
        public void Example()
        {
            Console.WriteLine("Starting on threadId:{0}", Thread.CurrentThread.ManagedThreadId);
            Observable.Create<int>(async o =>
                {
                    Console.WriteLine("Invoked on threadId:{0}", Thread.CurrentThread.ManagedThreadId);
                    await Task.Delay(500);
                    o.OnNext(1);
                    await Task.Delay(500);
                    o.OnNext(2);
                    o.OnCompleted();

                    Console.WriteLine("Finished on threadId:{0}", Thread.CurrentThread.ManagedThreadId);
                })
                .Subscribe(
                    o => Console.WriteLine("Received {1} on threadId:{0}",
                            Thread.CurrentThread.ManagedThreadId,
                            o),
                    () => Console.WriteLine("OnCompleted on threadId:{0}",
                            Thread.CurrentThread.ManagedThreadId));
            Console.WriteLine("Subscribed on threadId:{0}", Thread.CurrentThread.ManagedThreadId);

            //Starting on threadId:9
            //Invoked on threadId:9
            //Subscribed on threadId:9
            //Received 1 on threadId:11
            //Received 2 on threadId:11
            //OnCompleted on threadId:11
            //Finished on threadId:11
        }
    }
}
