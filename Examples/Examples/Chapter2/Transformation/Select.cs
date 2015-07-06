using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroToRx.Examples.Chapter2.Transformation
{
    class Select
    {
        public void Example1()
        {
            var source = Observable.Range(0, 5);
            source.Select(i => i + 3)
                .Dump("+3");

            //+3-->3
            //+3-->4
            //+3-->5
            //+3-->6
            //+3-->7
            //+3 completed
        }

        public void Example2()
        {
            Observable.Range(1, 5)
                .Select(i => (char)(i + 64))
                .Dump("char");

            //char-->A
            //char-->B
            //char-->C
            //char-->D
            //char-->E
            //char completed
        }

        public void ExampleAnon()
        {
            Observable.Range(1, 5)
                .Select(
                    i => new { Number = i, Character = (char)(i + 64) })
                .Dump("anon");

            //anon-->{ Number = 1, Character = A }
            //anon-->{ Number = 2, Character = B }
            //anon-->{ Number = 3, Character = C }
            //anon-->{ Number = 4, Character = D }
            //anon-->{ Number = 5, Character = E }
            //anon completed
        }

        public void ExampleQCSyntax()
        {
            var query = from i in Observable.Range(1, 5)
                        select new { Number = i, Character = (char)(i + 64) };
            query.Dump("anon");

            //anon-->{ Number = 1, Character = A }
            //anon-->{ Number = 2, Character = B }
            //anon-->{ Number = 3, Character = C }
            //anon-->{ Number = 4, Character = D }
            //anon-->{ Number = 5, Character = E }
            //anon completed
        }
    }
}
