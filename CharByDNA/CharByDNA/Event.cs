using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharByDNA
{

    class Event
    {

        public GTime Time { get; private set; }

        public string EventText { get; private set; }

        private string sqlconnstring = "URI=file:D:\\Users\\erico\\Code_Projects\\CharacterByDNA\\Database\\Game.db;Version=3";

        private SQLiteConnection sqlConn;

        public Event()
        {

            this.sqlConn = new SQLiteConnection(sqlconnstring);

        }

        public Event(GTime time, string evntxt)
        {

            this.Time = time;
            this.EventText = evntxt;

        }

        public Event(GTime time) : this(time, "")
        {



        }

        public Event(string evnttxt) : this(null,evnttxt)
        {



        }

        public void SaveEvent()
        {

            this.sqlConn.Open();

            string query = string.Format("INSERT INTO EventLog VALUES ({0},{1})", this.Time,this.EventText);

            SQLiteCommand command = new SQLiteCommand(query, sqlConn);
            command.ExecuteNonQuery();

        }

    }

}
