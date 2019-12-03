using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimerPractice.Model
{
    class Clock
    {
           private int seconds;
        private int minutes;
        private int hours;

        public int Seconds
        {
            get { return seconds; }
            set { seconds = value; }
        }


        public int Minutes
        {
            get { return minutes; }
            set { minutes = value; }
        }


        public int Hours
        {
            get { return hours; }
            set { hours = value; }
        }
    }
}
