using System;
using System.Collections.Generic;
using System.Text;

namespace Eirpoint.Mobile.Datasource.Helpers
{
    public class ContentRangeHeaderHelper
    {
        private string _units;
        private int _start;
        private int _end;
        private int _total;

        public ContentRangeHeaderHelper(string headerValue)
        {
            string[] unitSplit = headerValue.Split(' ');
            this._units = unitSplit[0];
            string[] totalSplit = unitSplit[1].Split('/');
            if (totalSplit[1] != "*")
            {
                this._total = Convert.ToInt32(totalSplit[1]);
            }
            string[] rangeSplit = totalSplit[0].Split('-');
            this._start = Convert.ToInt32(rangeSplit[0]);
            this._end = Convert.ToInt32(rangeSplit[1]);
        }

        public string getUnits()
        {
            return _units;
        }
        public int getStart()
        {
            return _start;
        }
        public int getEnd()
        {
            return _end;
        }
        public int getTotal()
        {
            return _total;
        }

        public bool isFinal()
        {
            if (_total == 0)
                return false;
            return (_end == _total - 1);
        }
        public bool isFirst()
        {
            return _start == 0;
        }
    }
}
