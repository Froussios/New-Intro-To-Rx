using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IntroToRx.Examples.Chapter4.Scheduling
{
    class Schedulers
    {
        public void Example1()
        {
            var myName = "Lee";
            new NewThreadScheduler().Schedule(
                () => Console.WriteLine("myName = {0}", myName));

            // myName = Lee
        }

        public void Example2()
        {
            var scheduler = Scheduler.Default;
            var myName = "Lee";
            scheduler.Schedule(myName,
                (_, state) =>
                {
                    Console.WriteLine(state);
                    return Disposable.Empty;
                });
            myName = "John";

            // Lee
        }

        public void Example3()
        {
            var scheduler = Scheduler.Default;
            var list = new List<int>();
            scheduler.Schedule(list,
                (innerScheduler, state) =>
                {
                    Console.WriteLine(state.Count);
                    return Disposable.Empty;
                });
            list.Add(1);

            // 1
            // or (race condition)
            // 0
        }

        public void ExampleFuture()
        {
            var scheduler = Scheduler.Default;
            var delay = TimeSpan.FromSeconds(1);
            Console.WriteLine("Before schedule at {0:o}", DateTime.Now);
            scheduler.Schedule(delay,
                () => Console.WriteLine("Inside schedule at {0:o}", DateTime.Now));
            Console.WriteLine("After schedule at {0:o}", DateTime.Now);

            //Before schedule at 2012 - 01 - 01T12: 00:00.000000 + 00:00
            //After schedule at 2012 - 01 - 01T12: 00:00.058000 + 00:00
            //Inside schedule at 2012 - 01 - 01T12: 00:01.044000 + 00:00
        }

        public void ExampleCancelation()
        {
            var scheduler = Scheduler.Default;
            var delay = TimeSpan.FromSeconds(1);
            Console.WriteLine("Before schedule at {0:o}", DateTime.Now);
            var token = scheduler.Schedule(delay,
                () => Console.WriteLine("Inside schedule at {0:o}", DateTime.Now));
            Console.WriteLine("After schedule at {0:o}", DateTime.Now);
            token.Dispose();

            //Before schedule at 2012 - 01 - 01T12: 00:00.000000 + 00:00
            //After schedule at 2012 - 01 - 01T12: 00:00.058000 + 00:00
        }

        public IDisposable Work(IScheduler scheduler, List<int> list)
        {
            var tokenSource = new CancellationTokenSource();
            var cancelToken = tokenSource.Token;
            var task = new Task(() =>
            {
                Console.WriteLine();
                for (int i = 0; i < 1000; i++)
                {
                    var sw = new SpinWait();
                    for (int j = 0; j < 3000; j++) sw.SpinOnce();
                    Console.Write(".");
                    list.Add(i);
                    if (cancelToken.IsCancellationRequested)
                    {
                        Console.WriteLine("Cancelation requested");
                        //cancelToken.ThrowIfCancellationRequested();
                        return;
                    }
                }
            }, cancelToken);
            task.Start();
            return Disposable.Create(tokenSource.Cancel);
        }

        public void ExampleCancelationToken()
        {
            var scheduler = Scheduler.Default;
            var list = new List<int>();
            Console.WriteLine("Enter to quit:");
            var token = scheduler.Schedule(list, Work);
            Console.ReadLine();
            Console.WriteLine("Cancelling...");
            token.Dispose();
            Console.WriteLine("Cancelled");

            //Enter to quit:
            //........
            //Cancelling...
            //Cancelled
            //Cancelation requested
        }

        public void ExampleCancellingRecursion()
        {
            Action<Action> work = (Action self) =>
            {
                Console.WriteLine("Running");
                self();
            };
            var s = Scheduler.Default;
            var token = s.Schedule(work);
            Console.ReadLine();
            Console.WriteLine("Cancelling");
            token.Dispose();
            Console.WriteLine("Cancelled");

            //Enter to quit:
            //Running
            //Running
            //Running
            //Running
            //Cancelling
            //Cancelled
            //Running
        }
    }
}
