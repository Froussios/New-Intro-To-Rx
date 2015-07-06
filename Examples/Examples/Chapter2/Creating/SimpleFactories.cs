using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace IntroToRx.Examples
{
    class SimpleFactories
    {
        public void ExampleReturn()
        {
            var singleValue = Observable.Return<string>("Value");

            singleValue.Subscribe(
                Console.WriteLine,
                Console.WriteLine,
                () => Console.WriteLine("Completed"));

            //Value
            //Completed
        }

        public void ExampleEmpty()
        {
            var empty = Observable.Empty<string>();

            empty.Subscribe(
                Console.WriteLine,
                Console.WriteLine,
                () => Console.WriteLine("Completed"));

            //Completed
        }

        public void ExampleNever()
        {
            var never = Observable.Never<string>();

            never.Subscribe(
                Console.WriteLine,
                Console.WriteLine,
                () => Console.WriteLine("Completed"));

            //
        }

        public void ExampleThrow()
        {
            var throws = Observable.Throw<string>(new Exception());

            throws.Subscribe(
                Console.WriteLine,
                Console.WriteLine,
                () => Console.WriteLine("Completed"));

            //System.Exception: Exception of type 'System.Exception' was thrown.
        }
    }
}
