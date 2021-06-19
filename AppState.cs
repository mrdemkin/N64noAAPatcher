using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N64noAAPatcher
{
    public class AppState
    {
        enum States : short { Launch, FilesReady, AllReady, Running, Stopped }

        States CurrentState;

        public AppState() {
            this.CurrentState = States.Launch;
        }
    }
}
