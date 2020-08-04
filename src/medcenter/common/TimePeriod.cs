using System;

namespace MedCenter
{
    public class TimePeriod
    {
        private DateTime _from = DateTime.MinValue;
        private DateTime _to = DateTime.Now;
        
        public DateTime From
        {
            get { return _from; }
            set { _from = value; }
        }

        public DateTime To
        {
            get { return _to; }
            set { _to = value; }
        }
    }
}