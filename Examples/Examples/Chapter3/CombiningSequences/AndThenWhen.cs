using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroToRx.Examples.Chapter3.CombiningSequences
{
    class AndThenWhen
    {
        public void ExampleUglyZip()
        {
            var one = Observable.Interval(TimeSpan.FromSeconds(1)).Take(5);
            var two = Observable.Interval(TimeSpan.FromMilliseconds(250)).Take(10);
            var three = Observable.Interval(TimeSpan.FromMilliseconds(150)).Take(14);
            //lhs represents 'Left Hand Side'
            //rhs represents 'Right Hand Side'
            var zippedSequence = one
                .Zip(two, (lhs, rhs) => new { One = lhs, Two = rhs })
                .Zip(three, (lhs, rhs) => new { One = lhs.One, Two = lhs.Two, Three = rhs });
            zippedSequence.Subscribe(
                Console.WriteLine,
                () => Console.WriteLine("Completed"));

            //{ One = 0, Two = 0, Three = 0 }
            //{ One = 1, Two = 1, Three = 1 }
            //{ One = 2, Two = 2, Three = 2 }
            //{ One = 3, Two = 3, Three = 3 }
            //{ One = 4, Two = 4, Three = 4 }
            //Completed
        }

        public void Example()
        {
            var one = Observable.Interval(TimeSpan.FromSeconds(1)).Take(5);
            var two = Observable.Interval(TimeSpan.FromMilliseconds(250)).Take(10);
            var three = Observable.Interval(TimeSpan.FromMilliseconds(150)).Take(14);
            var pattern = one.And(two).And(three);
            var plan = pattern.Then((first, second, third) => new { One = first, Two = second, Three = third });
            var zippedSequence = Observable.When(plan);
            zippedSequence.Subscribe(
                Console.WriteLine,
                () => Console.WriteLine("Completed"));

            //{ One = 0, Two = 0, Three = 0 }
            //{ One = 1, Two = 1, Three = 1 }
            //{ One = 2, Two = 2, Three = 2 }
            //{ One = 3, Two = 3, Three = 3 }
            //{ One = 4, Two = 4, Three = 4 }
            //Completed
        }

        public void ExampleAlternative()
        {
            var one = Observable.Interval(TimeSpan.FromSeconds(1)).Take(5);
            var two = Observable.Interval(TimeSpan.FromMilliseconds(250)).Take(10);
            var three = Observable.Interval(TimeSpan.FromMilliseconds(150)).Take(14);
            var zippedSequence = Observable.When(
                one.And(two)
                    .And(three)
                    .Then((first, second, third) =>
                        new {
                            One = first,
                            Two = second,
                            Three = third
                        })
                );
            zippedSequence.Subscribe(
                Console.WriteLine,
                () => Console.WriteLine("Completed"));

            //{ One = 0, Two = 0, Three = 0 }
            //{ One = 1, Two = 1, Three = 1 }
            //{ One = 2, Two = 2, Three = 2 }
            //{ One = 3, Two = 3, Three = 3 }
            //{ One = 4, Two = 4, Three = 4 }
            //Completed
        }
    }
}
