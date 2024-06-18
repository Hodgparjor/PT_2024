using Data.Interfaces;


namespace Data
{
    internal class Event : IEvent
    {
        private DateTime date;

        public DateTime Date { get => date; set => date = value; }
    }
}
