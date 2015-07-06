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
    class SchedulersInDepth
    {
        private static void ScheduleTasks(IScheduler scheduler)
        {
            Action leafAction = () => Console.WriteLine("----leafAction.");
            Action innerAction = () =>
            {
                Console.WriteLine("--innerAction start.");
                scheduler.Schedule(leafAction);
                Console.WriteLine("--innerAction end.");
            };
            Action outerAction = () =>
            {
                Console.WriteLine("outer start.");
                scheduler.Schedule(innerAction);
                Console.WriteLine("outer end.");
            };
            scheduler.Schedule(outerAction);
        }

        public void ExampleCurrentThread()
        {
            ScheduleTasks(Scheduler.CurrentThread);

            //outer start.
            //outer end.
            //--innerAction start.
            //--innerAction end.
            //----leafAction.
        }

        public void ExampleImmediate()
        {
            ScheduleTasks(Scheduler.Immediate);

            //outer start.
            //--innerAction start.
            //----leafAction.
            //--innerAction end.
            //outer end.
        }

        private static IDisposable OuterAction(IScheduler scheduler, string state)
        {
            Console.WriteLine("{0} start. ThreadId:{1}",
                state,
                Thread.CurrentThread.ManagedThreadId);
            scheduler.Schedule(state + ".inner", InnerAction);
            Console.WriteLine("{0} end. ThreadId:{1}",
                state,
                Thread.CurrentThread.ManagedThreadId);
            return Disposable.Empty;
        }
        private static IDisposable InnerAction(IScheduler scheduler, string state)
        {
            Console.WriteLine("{0} start. ThreadId:{1}",
                state,
                Thread.CurrentThread.ManagedThreadId);
            scheduler.Schedule(state + ".Leaf", LeafAction);
            Console.WriteLine("{0} end. ThreadId:{1}",
                state,
                Thread.CurrentThread.ManagedThreadId);
            return Disposable.Empty;
        }
        private static IDisposable LeafAction(IScheduler scheduler, string state)
        {
            Console.WriteLine("{0}. ThreadId:{1}",
                state,
                Thread.CurrentThread.ManagedThreadId);
            return Disposable.Empty;
        }

        public void ExampleNewThread()
        {
            Console.WriteLine("Starting on thread :{0}",
                Thread.CurrentThread.ManagedThreadId);
            new NewThreadScheduler().Schedule("A", OuterAction);

            //Starting on thread:8
            //A start. ThreadId:9
            //A end. ThreadId:9
            //A.inner start. ThreadId:9
            //A.inner end. ThreadId:9
            //A.inner.Leaf.ThreadId:9
        }

        public void ExampleNewThreadSecondTask()
        {
            IScheduler scheduler = new NewThreadScheduler();
            Console.WriteLine("Starting on thread :{0}",
                Thread.CurrentThread.ManagedThreadId);
            scheduler.Schedule("A", OuterAction);
            scheduler.Schedule("B", OuterAction);

            //Starting on thread :9
            //A start. ThreadId:10
            //A end. ThreadId:10
            //A.inner start . ThreadId:10
            //A.inner end. ThreadId:10
            //A.inner.Leaf. ThreadId:10
            //B start. ThreadId:11
            //B end. ThreadId:11
            //B.inner start . ThreadId:11
            //B.inner end. ThreadId:11
            //B.inner.Leaf. ThreadId:11
        }

        public void ExampleThreadPool()
        {
            Console.WriteLine("Starting on thread :{0}",
                Thread.CurrentThread.ManagedThreadId);
            var scheduler = ThreadPoolScheduler.Instance;
            scheduler.Schedule("A", OuterAction);
            scheduler.Schedule("B", OuterAction);

            //Starting on thread :9
            //A start. ThreadId:10
            //A end. ThreadId:10
            //A.inner start . ThreadId:10
            //A.inner end. ThreadId:10
            //A.inner.Leaf. ThreadId:10
            //B start. ThreadId:11
            //B end. ThreadId:11
            //B.inner start . ThreadId:10
            //B.inner end. ThreadId:10
            //B.inner.Leaf. ThreadId:11
        }
    }
}
