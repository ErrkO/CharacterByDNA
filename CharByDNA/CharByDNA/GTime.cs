using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharByDNA
{

    public class GTime
    {

        public char[] seperators = { '-', ' ', ':' };

        public int Year { get; private set; }

        public int Month { get; private set; }

        public int Day { get; private set; }

        public int Hour { get; private set; }

        public int Minute { get; private set; }

        public GTime(bool isnegative)
        {

            if (isnegative)
            {

                this.Year = -1;
                this.Month = -1;
                this.Day = -1;
                this.Hour = -1;
                this.Minute = -1;

            }

        }

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

        public GTime() : this(0000, 01, 01, 00, 00)
        {
        }

        public GTime(string timestring)
        {

            //char[] seperators = {'-',' ',':'};

            List<string> splits = timestring.Split(seperators).ToList<string>();

            this.Year = Convert.ToInt32(splits[0]);
            this.Month = Convert.ToInt32(splits[1]);
            this.Day = Convert.ToInt32(splits[2]);
            this.Hour = Convert.ToInt32(splits[3]);
            this.Minute = Convert.ToInt32(splits[4]);

        }

        public GTime(double timecode)
        {

            double tcodemonth = (timecode % 10000);
            double tcodeday = (timecode % 100);
            double tcodehour = (timecode % 1);
            double tcodemin = (timecode % .01);

            double tyear = (timecode - tcodemonth) / 10000;
            double tmonth = (tcodemonth - tcodeday) / 100;
            double tday = (tcodeday - tcodehour);
            double thour = (tcodehour - tcodemin) * 100;
            double tmin = tcodemin * 10000;

            this.Year = Convert.ToInt32(tyear);
            this.Month = Convert.ToInt32(tmonth);
            this.Day = Convert.ToInt32(tday);
            this.Hour = Convert.ToInt32(thour);
            this.Minute = Convert.ToInt32(tmin);

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

        public static GTime operator -(GTime gt1, GTime gt2)
        {

            GTime newtime = new GTime();

            newtime.Year = gt1.Year - gt2.Year;
            newtime.Month = gt1.Month - gt2.Month;
            newtime.Day = gt1.Day - gt2.Day;
            newtime.Hour = gt1.Hour - gt2.Hour;
            newtime.Minute = gt1.Minute - gt2.Minute;

            return newtime;

        }

        public static bool operator >(GTime gt1, GTime gt2)
        {

            if (gt1.Year > gt2.Year)
            {

                return true;

            }

            else if (gt1.Month > gt2.Month)
            {

                return true;

            }

            else if (gt1.Day > gt2.Day)
            {

                return true;

            }

            else if (gt1.Hour > gt2.Hour)
            {

                return true;

            }

            else if (gt1.Day > gt2.Day)
            {

                return true;

            }

            return false;

        }

        public static bool operator <(GTime gt1, GTime gt2)
        {

            if (gt1.Year < gt2.Year)
            {

                return true;

            }

            else if (gt1.Month < gt2.Month)
            {

                return true;

            }

            else if (gt1.Day < gt2.Day)
            {

                return true;

            }

            else if (gt1.Hour < gt2.Hour)
            {

                return true;

            }

            else if (gt1.Day < gt2.Day)
            {

                return true;

            }

            return false;

        }

        public static bool operator >=(GTime gt1, GTime gt2)
        {

            if (gt1.Year >= gt2.Year)
            {

                return true;

            }

            else if (gt1.Month >= gt2.Month)
            {

                return true;

            }

            else if (gt1.Day >= gt2.Day)
            {

                return true;

            }

            else if (gt1.Hour >= gt2.Hour)
            {

                return true;

            }

            else if (gt1.Day >= gt2.Day)
            {

                return true;

            }

            return false;

        }

        public static bool operator <=(GTime gt1, GTime gt2)
        {

            if (gt1.Year <= gt2.Year)
            {

                return true;

            }

            else if (gt1.Month <= gt2.Month)
            {

                return true;

            }

            else if (gt1.Day <= gt2.Day)
            {

                return true;

            }

            else if (gt1.Hour <= gt2.Hour)
            {

                return true;

            }

            else if (gt1.Day <= gt2.Day)
            {

                return true;

            }

            return false;

        }

        public static bool operator ==(GTime gt1, GTime gt2)
        {

            if (gt1.Year == gt2.Year && gt1.Month == gt2.Month && gt1.Day == gt2.Day && gt1.Hour == gt2.Hour && gt1.Minute == gt2.Minute)
            {

                return true;

            }

            return false;

        }

        public static bool operator !=(GTime gt1, GTime gt2)
        {

            if (gt1.Year == gt2.Year && gt1.Month == gt2.Month && gt1.Day == gt2.Day && gt1.Hour == gt2.Hour && gt1.Minute == gt2.Minute)
            {

                return false;

            }

            return true;

        }

        public static GTime IncrementByDays(GTime time)
        {

            time.Day++;

            if (time.Day > 30)
            {

                time.Day = 1;

                time.Month++;

            }

            if (time.Month > 12)
            {

                time.Month = 1;

                time.Year++;

            }

            return time;

        }

        public static bool CheckForValidDate(GTime time)
        {

            if (time.Minute > 59 || time.Minute < 0)
            {

                return false;

            }

            if (time.Hour > 23 || time.Hour < 0)
            {

                return false;

            }

            if (time.Day > 30 || time.Day < 0)
            {

                return false;

            }

            if (time.Month > 12 || time.Month < 0)
            {

                return false;

            }

            return true;

        }

        public GTime AddDays(GTime time, int days)
        {

            GTime newtime = time;

            newtime.Day += days;

            while (!CheckForValidDate(newtime))
            {

                if (Day >= 30)
                {

                    newtime.Month += 1;

                    newtime.Day -= 30;

                }

                if (Month >= 12)
                {

                    newtime.Year += 1;

                    newtime.Month -= 12;

                }

            }

            return newtime;

        }

        public static string AddDays(string timecode, int days)
        {

            //List<string> splits = timecode.Split(seperators).ToList<string>();

            GTime newtime = new GTime(timecode);

            //int newdays = Convert.ToInt32(splits[2]);

            newtime.Day += days;

            while (!CheckForValidDate(newtime))
            {

                if (newtime.Day >= 30)
                {

                    newtime.Month += 1;

                    newtime.Day -= 30;

                }

                if (newtime.Month >= 12)
                {

                    newtime.Year += 1;

                    newtime.Month -= 12;

                }

            }

            return newtime.ToString();

        }

        public GTime FutureDateDays(int days)
        {

            GTime newtime = this;

            for (int i = 0; i < days*1440; i++)
            {

                newtime = newtime++;

            }

            return newtime;

        }

        public override string ToString()
        {

            return string.Format("{0:d4}-{1:d2}-{2:d2} {3:d2}:{4:d2}",this.Year,this.Month,this.Day,this.Hour,this.Minute);

        }

        public double ToDouble()
        {

            double tyear = this.Year * 10000;
            double tmonth = this.Month * 100;
            double tday = this.Day * 1;
            double thour = this.Hour * .01;
            double tmin = this.Minute * .0001;

            return tyear + tmonth + tday + thour + tmin;

        }

    }

}
