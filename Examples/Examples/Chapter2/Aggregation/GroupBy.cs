using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroToRx.Examples.Chapter2.Aggregation
{
    class GroupBy
    {
        public void Example()
        {
            var source = Observable.Interval(TimeSpan.FromSeconds(0.1)).Take(10);
            var group = source.GroupBy(i => i % 3);
            group
                .SelectMany(grp =>
                    grp.Max()
                    .Select(value => new { grp.Key, value }))
                .Dump("group");

            //group-- >{ Key = 0, value = 9 }
            //group-- >{ Key = 1, value = 7 }
            //group-- >{ Key = 2, value = 8 }
            //group completed

        }
    }
}
