using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    internal class Event : IEvent
    {
        private DateTime date;

        public DateTime Date { get => date; set => date = value; }
    }
}
