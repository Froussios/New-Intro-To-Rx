using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace IntroToRx.Examples.Chapter3.SideEffects
{
    class SharedState
    {
        public class Account
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        public void Example()
        {
            var source = new Subject<Account>();
            //Evil code. It modifies the Account object.
            source.Subscribe(account => account.Name = "Garbage");
            //unassuming well behaved code
            source.Subscribe(
                account => Console.WriteLine("{0} {1}", account.Id, account.Name),
                () => Console.WriteLine("completed"));
            source.OnNext(new Account { Id = 1, Name = "Microsoft" });
            source.OnNext(new Account { Id = 2, Name = "Google" });
            source.OnNext(new Account { Id = 3, Name = "IBM" });
            source.OnCompleted();

            //1 Garbage
            //2 Garbage
            //3 Garbage
            //completed

        }
    }
}
