using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroToRx.Examples.Chapter2.Reducing
{
    class Where
    {
        public void Example()
        {
            var oddNumbers = Observable.Range(0, 10)
                .Where(i => i % 2 == 0)
                .Subscribe(
                    Console.WriteLine,
                    () => Console.WriteLine("Completed"));

            // 0
            // 2
            // 4
            // 6
            // 8
            // Completed            
        }
    }
}
