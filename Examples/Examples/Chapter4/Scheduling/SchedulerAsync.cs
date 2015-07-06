using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroToRx.Examples.Chapter4.Scheduling
{
    class SchedulerAsync
    {
        public IObservable<int> RangeRecursive(int start, int count, IScheduler scheduler)
        {
            return Observable.Create<int>(observer =>
            {
                return scheduler.Schedule(0, (i, self) =>
                {
                    if (i < count)
                    {
                        observer.OnNext(start + i);
                        self(i + 1); /* Here is the recursive call */
                    }
                    else
                    {
                        observer.OnCompleted();
                    }
                });
            });
        }

        public void ExampleRecursive()
        {
            var scheduler = Scheduler.Default;
            RangeRecursive(0, 3, scheduler)
                .Subscribe(i => Console.WriteLine("First " + i));
            RangeRecursive(0, 3, scheduler)
                .Subscribe(i => Console.WriteLine("Second " + i));
            Console.WriteLine("Subscribed");

            //Subscribed
            //First 0
            //Second 0
            //Second 1
            //First 1
            //Second 2
            //First 2
        }

        public IObservable<int> RangeAsync(int start, int count, IScheduler scheduler)
        {
            return Observable.Create<int>(observer =>
            {
                return scheduler.ScheduleAsync(async (ctrl, ct) =>
                {
                    for (int i = 0; i < count; i++)
                    {
                        observer.OnNext(start + i);
                        await ctrl.Yield(); /* Use a task continuation to schedule next event */
                    }
                    observer.OnCompleted();

                    return Disposable.Empty;
                });
            });
        }

        public void ExampleAsync()
        {
            var scheduler = Scheduler.Default;
            RangeAsync(0, 3, scheduler)
                .Subscribe(i => Console.WriteLine("First " + i));
            RangeAsync(0, 3, scheduler)
                .Subscribe(i => Console.WriteLine("Second " + i));
            Console.WriteLine("Subscribed");

            //Subscribed
            //First 0
            //Second 0
            //First 1
            //Second 1
            //First 2
            //Second 2
        }
    }
}
