using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharByDNA
{

    public class GTime
    {

        public int Year { get; private set; }

        public int Month { get; private set; }

        public int Day { get; private set; }

        public int Hour { get; private set; }

        public int Minute { get; private set; }

        public GTime(int year, int month, int day, int hour, int minute)
        {

            this.Year = year;
            this.Month = month;
            this.Day = day;
            this.Hour = hour;
            this.Minute = minute;

        }

        public GTime(int year, int month, int day, int hour) : this(year,month,day,hour,00)
        {

            

        }

        public GTime(int year, int month, int day) : this(year,month,day,00,00)
        {



        }

        public GTime(int year, int month) : this(year, month, 01, 00, 00)
        {



        }

        public GTime(int year) : this(year, 01, 01, 00, 00)
        {



        }

        public GTime() : this(0001, 01, 01, 00, 00)
        {



        }

        public GTime(string timestring)
        {

            char[] seperators = {'-',' ',':'};

            List<string> splits = timestring.Split(seperators).ToList<string>();

            this.Year = Convert.ToInt32(splits[0]);
            this.Month = Convert.ToInt32(splits[1]);
            this.Day = Convert.ToInt32(splits[2]);
            this.Hour = Convert.ToInt32(splits[3]);
            this.Minute = Convert.ToInt32(splits[4]);

        }

        public static GTime operator ++(GTime gtime)
        {

            gtime.Minute++;

            if (gtime.Minute > 59)
            {

                gtime.Minute = 0;

                gtime.Hour++;

            }

            if (gtime.Hour > 23)
            {

                gtime.Hour = 0;

                gtime.Day++;

            }

            if (gtime.Day > 30)
            {

                gtime.Day = 1;

                gtime.Month++;

            }

            if (gtime.Month > 12)
            {

                gtime.Month = 1;

                gtime.Year++;

            }

            return gtime;

        }

        /*
        public static GTime operator -(GTime gt1, GTime gt2)
        {

            GTime newtime = new GTime();

            newtime.Year = gt1.Year - gt2.Year;
            newtime.Month = gt1.Month - gt2.Month;
            newtime.Day = gt1.Day - gt2.Day;
            newtime.Hour = gt1.Hour - gt2.Hour;
            newtime.Minute = gt1.Minute - gt2.Minute;

        }
        */

        public override string ToString()
        {

            return string.Format("{0:d4}-{1:d2}-{2:d2} {3:d2}:{4:d2}",this.Year,this.Month,this.Day,this.Hour,this.Minute);

        }

    }

}
