using Microsoft.Reactive.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Concurrency;
using System.Reactive.Linq;

namespace IntroToRx.Examples.Chapter4.Testing
{
    class TestSchedulerExamples
    {
        public void Example()
        {
            var scheduler = new TestScheduler();
            var wasExecuted = false;
            scheduler.Schedule(() => wasExecuted = true);
            Assert.IsFalse(wasExecuted);
            scheduler.AdvanceBy(1); //execute 1 tick of queued actions
            Assert.IsTrue(wasExecuted);
        }

        public void ExampleAdvanceTo()
        {
            var scheduler = new TestScheduler();
            scheduler.Schedule(() => Console.WriteLine("A")); //Schedule immediately
            scheduler.Schedule(TimeSpan.FromTicks(10), () => Console.WriteLine("B"));
            scheduler.Schedule(TimeSpan.FromTicks(20), () => Console.WriteLine("C"));
            Console.WriteLine("scheduler.AdvanceTo(1);");
            scheduler.AdvanceTo(1);
            Console.WriteLine("scheduler.AdvanceTo(10);");
            scheduler.AdvanceTo(10);
            Console.WriteLine("scheduler.AdvanceTo(15);");
            scheduler.AdvanceTo(15);
            Console.WriteLine("scheduler.AdvanceTo(20);");
            scheduler.AdvanceTo(20);

            //scheduler.AdvanceTo(1);
            //A
            //scheduler.AdvanceTo(10);
            //B
            //scheduler.AdvanceTo(15);
            //scheduler.AdvanceTo(20);
            //C
        }

        public void ExampleAdvanceBy()
        {
            var scheduler = new TestScheduler();
            scheduler.Schedule(() => Console.WriteLine("A")); //Schedule immediately
            scheduler.Schedule(TimeSpan.FromTicks(10), () => Console.WriteLine("B"));
            scheduler.Schedule(TimeSpan.FromTicks(20), () => Console.WriteLine("C"));
            Console.WriteLine("scheduler.AdvanceBy(1);");
            scheduler.AdvanceBy(1);
            Console.WriteLine("scheduler.AdvanceBy(9);");
            scheduler.AdvanceBy(9);
            Console.WriteLine("scheduler.AdvanceBy(5);");
            scheduler.AdvanceBy(5);
            Console.WriteLine("scheduler.AdvanceBy(5);");
            scheduler.AdvanceBy(5);

            //scheduler.AdvanceBy(1);
            //A
            //scheduler.AdvanceBy(9);
            //B
            //scheduler.AdvanceBy(5);
            //scheduler.AdvanceBy(5);
            //C
        }

        public void ExampleStart()
        {
            var scheduler = new TestScheduler();
            scheduler.Schedule(() => Console.WriteLine("A")); //Schedule immediately
            scheduler.Schedule(TimeSpan.FromTicks(10), () => Console.WriteLine("B"));
            scheduler.Schedule(TimeSpan.FromTicks(20), () => Console.WriteLine("C"));
            Console.WriteLine("scheduler.Start();");
            scheduler.Start();
            Console.WriteLine("scheduler.Clock:{0}", scheduler.Clock);
            scheduler.Schedule(() => Console.WriteLine("D"));

            //scheduler.Start();
            //A
            //B
            //C
            //scheduler.Clock:20
        }

        public void ExampleStop()
        {
            var scheduler = new TestScheduler();
            scheduler.Schedule(() => Console.WriteLine("A"));
            scheduler.Schedule(TimeSpan.FromTicks(10), () => Console.WriteLine("B"));
            scheduler.Schedule(TimeSpan.FromTicks(15), scheduler.Stop);
            scheduler.Schedule(TimeSpan.FromTicks(20), () => Console.WriteLine("C"));
            Console.WriteLine("scheduler.Start();");
            scheduler.Start();
            Console.WriteLine("scheduler.Clock:{0}", scheduler.Clock);

            //scheduler.Start();
            //A
            //B
            //scheduler.Clock:15
        }

        public void ExampleCollision()
        {
            var scheduler = new TestScheduler();
            scheduler.Schedule(TimeSpan.FromTicks(10), () => Console.WriteLine("A"));
            scheduler.Schedule(TimeSpan.FromTicks(10), () => Console.WriteLine("B"));
            scheduler.Schedule(TimeSpan.FromTicks(10), () => Console.WriteLine("C"));
            Console.WriteLine("scheduler.Start();");
            scheduler.Start();
            Console.WriteLine("scheduler.Clock:{0}", scheduler.Clock);

            //scheduler.AdvanceTo(10);
            //A
            //B
            //C
            //scheduler.Clock:10
        }

        [TestMethod]
        public void Testing_with_test_scheduler()
        {
            var expectedValues = new long[] { 0, 1, 2, 3, 4 };
            var actualValues = new List<long>();
            var scheduler = new TestScheduler();
            var interval = Observable
                .Interval(TimeSpan.FromSeconds(1), scheduler)
                .Take(5);
            interval.Subscribe(actualValues.Add);
            scheduler.Start();
            CollectionAssert.AreEqual(expectedValues, actualValues);
            //Executes in less than 0.01s "on my machine"

            //
        }
    }
}
