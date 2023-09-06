using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Main.EventBus.Events
{
    public readonly struct OnOpenMenu : IEvenet
    {
        public readonly bool CanInput;

        public OnOpenMenu(bool canInput)
        {
            CanInput = canInput;
        }
    }
}
