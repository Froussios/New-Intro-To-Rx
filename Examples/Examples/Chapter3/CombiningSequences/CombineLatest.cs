using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace IntroToRx.Examples.Chapter3.CombiningSequences
{
    class CombineLatest
    {
        public void Example()
        {
            var webServerStatus = new Subject<bool>();
            var databaseStatus = new Subject<bool>();

            //Yields true when both systems are up, and only on change of status
            var systemStatus = webServerStatus
                .CombineLatest(
                    databaseStatus,
                    (webStatus, dbStatus) => webStatus && dbStatus)
                .StartWith(false)
                .DistinctUntilChanged();
            systemStatus.Dump("System status");

            webServerStatus.OnNext(true);
            webServerStatus.OnNext(false);
            databaseStatus.OnNext(true);
            webServerStatus.OnNext(true); // Now they are both on
            webServerStatus.OnNext(false); // and not anymore

            //System status-->False
            //System status-->True
            //System status-->False
        }

    }
}
