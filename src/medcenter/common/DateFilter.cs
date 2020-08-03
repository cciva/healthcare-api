using System;

namespace MedCenter
{
    public class DateFilter
    {
        private DateTime _from = DateTime.MinValue;
        private DateTime _to = DateTime.Now;

        public DateFilter()
        {

        }

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