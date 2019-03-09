using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharByDNA
{

    public class GDate
    {

        public char[] seperators = { '-' };

        public int Year { get; private set; }

        public int Month { get; private set; }

        public int Day { get; private set; }

        public GDate(bool isnegative)
        {

            if (isnegative)
            {

                this.Year = -1;
                this.Month = -1;
                this.Day = -1;

            }

        }

        public GDate(int year, int month, int day)
        {

            this.Year = year;
            this.Month = month;
            this.Day = day;

        }

        public GDate(int year, int month) : this(year, month, 01)
        {
        }

        public GDate(int year) : this(year, 01, 01)
        {
        }

        public GDate() : this(0000, 01, 01)
        {
        }

        public GDate(GDate date)
        {

            this.Year = date.Year;
            this.Month = date.Month;
            this.Day = date.Day;

        }

        public GDate(string datestring)
        {

            //char[] seperators = {'-',' ',':'};

            List<string> splits = datestring.Split(seperators).ToList<string>();

            this.Year = Convert.ToInt32(splits[0]);
            this.Month = Convert.ToInt32(splits[1]);
            this.Day = Convert.ToInt32(splits[2]);

        }

        public GDate(double datecode)
        {

            double dcodemonth = (datecode % 10000);
            double dcodeday = (datecode % 100);

            double dyear = (datecode - dcodemonth) / 10000;
            double dmonth = (dcodemonth - dcodeday) / 100;
            double dday = dcodeday;

            this.Year = Convert.ToInt32(dyear);
            this.Month = Convert.ToInt32(dmonth);
            this.Day = Convert.ToInt32(dday);

        }

        public override bool Equals(object obj)
        {

            return this.Year == obj.Year && this.Month == obj.Month && this.Day == obj.Day; 

        }

        public static GDate operator ++(GDate GDate)
        {

            GDate.Day++;

            if (GDate.Day > 30)
            {

                GDate.Day = 1;

                GDate.Month++;

            }

            if (GDate.Month > 12)
            {

                GDate.Month = 1;

                GDate.Year++;

            }

            return GDate;

        }

        public static GDate operator -(GDate gd1, GDate gd2)
        {

            GDate newtime = new GDate();

            newtime.Year = gd1.Year - gd2.Year;
            newtime.Month = gd1.Month - gd2.Month;
            newtime.Day = gd1.Day - gd2.Day;

            return newtime;

        }

        public static bool operator >(GDate gd1, GDate gd2)
        {

            if (gd1.Year > gd2.Year)
            {

                return true;

            }

            else
            {

                if (gd1.Month > gd2.Month)
                {

                    return true;

                }

                else
                {

                    if (gd1.Day > gd2.Day)
                    {

                        return true;

                    }

                }

            }

            return false;

        }

        public static bool operator <(GDate gd1, GDate gd2)
        {

            if (gd1.Year < gd2.Year)
            {

                return true;

            }

            else
            {

                if (gd1.Month < gd2.Month)
                {

                    return true;

                }

                else
                {

                    if (gd1.Day < gd2.Day)
                    {

                        return true;

                    }

                }

            }

            return false;

        }

        public static bool operator >=(GDate gd1, GDate gd2)
        {

            if (gd1.Year >= gd2.Year)
            {

                return true;

            }

            else
            {

                if (gd1.Month >= gd2.Month)
                {

                    return true;

                }

                else
                {

                    if (gd1.Day >= gd2.Day)
                    {

                        return true;

                    }

                }

            }

            return false;

        }

        public static bool operator <=(GDate gd1, GDate gd2)
        {

            if (gd1.Year <= gd2.Year)
            {

                return true;

            }

            else
            {

                if (gd1.Month <= gd2.Month)
                {

                    return true;

                }

                else
                {

                    if (gd1.Day <= gd2.Day)
                    {

                        return true;

                    }

                }

            }

            return false;

        }

        public static bool operator ==(GDate gd1, GDate gd2)
        {

            if (gd1.Year == gd2.Year && gd1.Month == gd2.Month && gd1.Day == gd2.Day)
            {

                return true;

            }

            return false;

        }

        public static bool operator !=(GDate gd1, GDate gd2)
        {

            if (gd1.Year == gd2.Year && gd1.Month == gd2.Month && gd1.Day == gd2.Day)
            {

                return false;

            }

            return true;

        }

        public static bool CheckForValidDate(GDate time)
        {

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

        public static GDate AddDays(GDate date, int days)
        {

            //double cdate = ToDouble(date);

            GDate newdate = new GDate(date);

            newdate.Day += days;

            while (!CheckForValidDate(newdate))
            {

                if (date.Day > 30)
                {

                    date.Day -= 30;
                    date.Month += 1;

                    if (date.Month > 12)
                    {

                        date.Month = 1;
                        date.Year += 1;

                    }

                }

            }

            return newdate;

        }

        public static GDate AddDays(double date, int days)
        {

            return AddDays(new GDate(date),days);

        }

        public override string ToString()
        {

            return string.Format("{0:d4}-{1:d2}-{2:d2}",this.Year,this.Month,this.Day);

        }

        public double ToDouble()
        {

            double tyear = this.Year * 10000;
            double tmonth = this.Month * 100;
            double tday = this.Day * 1;

            return tyear + tmonth + tday;

        }

    }

}
