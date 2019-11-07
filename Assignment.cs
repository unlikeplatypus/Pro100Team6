using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TOM
{
    public class Assignment
    {
        private string title;
        private string dueDate;

        public Assignment(string title, string dueDate)
        {
            this.title = title;
            this.dueDate = dueDate;
        }

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        public string DueDate
        {
            get { return dueDate; }
            set { dueDate = value; }
        }


    }
}
