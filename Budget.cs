using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TOM
{
    public class Budget
    {
        private string name;
        private int max;
        private int current;

       


        public Budget(string name, int max, int current)
        {
            this.name = name;
            this.max = max;
            this.current = current;
        }
        public string BudgetString
        {
            get { return "$" + current + " / $" + max; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int Max
        {
            get { return max; }
            set { max = value; }
        }

        public int Current
        {
            get { return current; }
            set { current = value; }
        }


    }
}
