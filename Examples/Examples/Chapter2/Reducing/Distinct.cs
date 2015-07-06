using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace IntroToRx.Examples.Chapter2.Reducing
{
    class Distinct
    {
        public void ExampleDistinct()
        {
            var subject = new Subject<int>();
            var distinct = subject.Distinct();
            subject.Subscribe(
                i => Console.WriteLine("{0}", i),
                () => Console.WriteLine("subject.OnCompleted()"));
            distinct.Subscribe(
                i => Console.WriteLine("distinct.OnNext({0})", i),
                () => Console.WriteLine("distinct.OnCompleted()"));
            subject.OnNext(1);
            subject.OnNext(2);
            subject.OnNext(3);
            subject.OnNext(1);
            subject.OnNext(1);
            subject.OnNext(4);
            subject.OnCompleted();

            //1
            //distinct.OnNext(1)
            //2
            //distinct.OnNext(2)
            //3
            //distinct.OnNext(3)
            //1
            //1
            //4
            //distinct.OnNext(4)
            //subject.OnCompleted()
            //distinct.OnCompleted()
        }

        public void ExampleDistinctUntilChanged()
        {
            var subject = new Subject<int>();
            var distinct = subject.DistinctUntilChanged();
            subject.Subscribe(
                i => Console.WriteLine("{0}", i),
                () => Console.WriteLine("subject.OnCompleted()"));
            distinct.Subscribe(
                i => Console.WriteLine("distinct.OnNext({0})", i),
                () => Console.WriteLine("distinct.OnCompleted()"));
            subject.OnNext(1);
            subject.OnNext(2);
            subject.OnNext(3);
            subject.OnNext(1);
            subject.OnNext(1);
            subject.OnNext(4);
            subject.OnCompleted();

            //1
            //distinct.OnNext(1)
            //2
            //distinct.OnNext(2)
            //3
            //distinct.OnNext(3)
            //1
            //distinct.OnNext(1)
            //1
            //4
            //distinct.OnNext(4)
            //subject.OnCompleted()
            //distinct.OnCompleted()
        }
    }
}
