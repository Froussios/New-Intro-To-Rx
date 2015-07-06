using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroToRx.Examples.Chapter2.Reducing
{
    class SkipTake
    {
        public void ExampleSkip()
        {
            Observable.Range(0, 10)
               .Skip(3)
               .Subscribe(Console.WriteLine, () => Console.WriteLine("Completed"));

            //3
            //4
            //5
            //6
            //7
            //8
            //9
            //Completed
        }

        public void ExampleTake()
        {
            Observable.Range(0, 10)
               .Take(3)
               .Subscribe(Console.WriteLine, () => Console.WriteLine("Completed"));

            //0
            //1
            //2
            //Completed
        }
    }
}
