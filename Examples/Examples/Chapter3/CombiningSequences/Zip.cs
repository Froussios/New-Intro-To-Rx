using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace IntroToRx.Examples.Chapter3.CombiningSequences
{
    class Zip
    {
        public void Example()
        {
            //Generate values 0,1,2
            var nums = Observable.Interval(TimeSpan.FromMilliseconds(250))
                .Take(3);
            //Generate values a,b,c,d,e,f
            var chars = Observable.Interval(TimeSpan.FromMilliseconds(150))
                .Take(6)
                .Select(i => Char.ConvertFromUtf32((int)i + 97));
            //Zip values together
            nums.Zip(chars, (lhs, rhs) => new { Left = lhs, Right = rhs })
                .Dump("Zip");

            //Zip-->{ Left = 0, Right = a }
            //Zip-->{ Left = 1, Right = b }
            //Zip-->{ Left = 2, Right = c }
            //Zip completed
        }

        public class Coord
        {
            public int X { get; set; }
            public int Y { get; set; }
            public override string ToString()
            {
                return string.Format("{0},{1}", X, Y);
            }
        }

        public void ExampleCoords()
        {
            var mm = new Subject<Coord>();
            var s1 = mm.Skip(1);
            var delta = mm.Zip(s1,
                (prev, curr) => new Coord
                {
                    X = curr.X - prev.X,
                    Y = curr.Y - prev.Y
                });
            delta.Subscribe(
                Console.WriteLine,
                () => Console.WriteLine("Completed"));
            mm.OnNext(new Coord { X = 0, Y = 0 });
            mm.OnNext(new Coord { X = 1, Y = 0 }); //Move across 1
            mm.OnNext(new Coord { X = 3, Y = 2 }); //Diagonally up 2
            mm.OnNext(new Coord { X = 0, Y = 0 }); //Back to 0,0
            mm.OnCompleted();

            //1,0
            //2,2
            //-3,-2
            //Completed

        }
    }
}
