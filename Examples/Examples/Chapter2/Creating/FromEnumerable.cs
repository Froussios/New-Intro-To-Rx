using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Linq;

namespace IntroToRx.Examples
{ 
    class FromEnumerable
    {
        private IEnumerable<int> MyEnumerable(int start)
        {
            yield return start;
            yield return start + 1;
            yield return start + 2;
        }

        public void Example()
        {
            MyEnumerable(2)
                .ToObservable()
                .Subscribe(Console.WriteLine);

            //2
            //3
            //4
        }
    }
}
