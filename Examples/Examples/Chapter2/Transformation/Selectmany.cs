using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroToRx.Examples.Chapter2.Transformation
{
    class SelectMany
    {
        public void Example()
        {
            Observable.Return(3)
                .SelectMany(i => Observable.Range(1, i))
                .Dump("SelectMany");

            //SelectMany-->1
            //SelectMany-->2
            //SelectMany-->3
            //SelectMany completed
        }

        public void Example2()
        {
            Observable.Range(1, 3)
                .SelectMany(i => Observable.Range(1, i))
                .Dump("SelectMany");

            //SelectMany-->1
            //SelectMany-->1
            //SelectMany-->2
            //SelectMany-->1
            //SelectMany-->2
            //SelectMany-->3
            //SelectMany completed
        }

        public void Example3()
        {
            Func<int, char> letter = i => (char)(i + 64);
            Observable.Return(1)
                .SelectMany(i => Observable.Return(letter(i)))
                .Dump("SelectMany");

            //SelectMany-->A
            //SelectMany completed
        }

        public void Example4()
        {
            Func<int, char> letter = i => (char)(i + 64);
            Observable.Range(1, 3)
                .SelectMany(i => Observable.Return(letter(i)))
                .Dump("SelectMany");

            //SelectMany-->A
            //SelectMany-->B
            //SelectMany-->C
        }

        public void ExampleAlphabet()
        {
            Func<int, char> letter = i => (char)(i + 64);
            Observable.Range(1, 30)
                .SelectMany(
                    i =>
                    {
                        if (0 < i && i < 27)
                        {
                            return Observable.Return(letter(i));
                        }
                        else
                        {
                            return Observable.Empty<char>();
                        }
                    })
                .Dump("SelectMany");

            //SelectMany-->A
            //SelectMany-->B
            //SelectMany-->C
            //SelectMany-->D
            //SelectMany-->E
            //SelectMany-->F
            //SelectMany-->G
            //SelectMany-->H
            //SelectMany-->I
            //SelectMany-->J
            //SelectMany-->K
            //SelectMany-->L
            //SelectMany-->M
            //SelectMany-->N
            //SelectMany-->O
            //SelectMany-->P
            //SelectMany-->Q
            //SelectMany-->R
            //SelectMany-->S
            //SelectMany-->T
            //SelectMany-->U
            //SelectMany-->V
            //SelectMany-->W
            //SelectMany-->X
            //SelectMany-->Y
            //SelectMany-->Z
            //SelectMany completed

        }

        public void ExampleAsynchronous()
        {
            // Values [1,2,3] 3 seconds apart.
            Observable.Interval(TimeSpan.FromSeconds(3))
                .Select(i => i + 1) //Values start at 0, so add 1.
                .Take(3) //We only want 3 values
                .SelectMany(GetSubValues) //project into child sequences
                .Dump("SelectMany");
        }

        private IObservable<long> GetSubValues(long offset)
        {
            //Produce values [x*10, (x*10)+1, (x*10)+2] 4 seconds apart, but starting immediately.
            return Observable.Timer(TimeSpan.Zero, TimeSpan.FromSeconds(4))
                .Select(x => (offset * 10) + x)
                .Take(3);

            //SelectMany-->10
            //SelectMany-->20
            //SelectMany-->11
            //SelectMany-->30
            //SelectMany-->21
            //SelectMany-->12
            //SelectMany-->31
            //SelectMany-->22
            //SelectMany-->32
            //SelectMany completed
        }

        public void ExampleQCSyntax()
        {
            var query = from i in Observable.Range(1, 5)
                        where i % 2 == 0
                        from j in GetSubValues(i)
                        select new { i, j };
            query.Dump("SelectMany");

            //SelectMany-->{ i = 2, j = 20 }
            //SelectMany-->{ i = 4, j = 40 }
            //SelectMany-->{ i = 2, j = 21 }
            //SelectMany-->{ i = 4, j = 41 }
            //SelectMany-->{ i = 2, j = 22 }
            //SelectMany-->{ i = 4, j = 42 }
            //SelectMany completed
        }
    }
}
