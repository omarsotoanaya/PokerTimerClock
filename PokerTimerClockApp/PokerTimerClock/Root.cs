using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerTimerClock
{
    public class Root
    {
        public TimeSpan RoundTime { get; set; }
        public List<Blind> BlindList { get; set; }
    }
}
