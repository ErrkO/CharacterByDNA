using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CharByDNA
{

    public class Race
    {

        //string conn_string = "Data Source=C:/Users/Eric/Documents/GitHub/Rpg/Database/RPG.mdb";
        //string conn_string = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:/Users/Eric/Documents/GitHub/Rpg/Database/RPG.mdb;Jet OLEDB:Database Password=Shadow13;Persist Security Info=True";

        private int int_mod, str_mod, agi_mod, con_mod, wis_mod, luk_mod, cha_mod;
        private string racename;
        private List<Race> races;
        private string rconn = "C:/Users/erico/Documents/Github/Rpg/RPGTester/Races.txt";

        public int Int_mod { get; set; }

        public int Str_mod { get; set; }

        public int Agi_mod { get; set; }

        public int Con_mod { get; set; }

        public int Wis_mod { get; set; }

        public int Luk_mod { get; set; }

        public int Cha_mod { get; set; }

        public string Racename { get; set; }

        public List<Race> Races { get; set; }

        public Race()
        {
            
            Races = GetRaces();

        }

        public Race(string name,int s, int i, int a, int c, int w, int l, int ch)
        {

            Int_mod = i;
            Str_mod = s;
            Agi_mod = a;
            Con_mod = c;
            Wis_mod = w;
            Luk_mod = l;
            Cha_mod = ch;
            Racename = name;

        }

        //public Race()

        // text file get races
        private List<Race> GetRaces()
        {

            List<Race> races = new List<Race>();

            string line = "";

            StreamReader file = new StreamReader(rconn);

            while ((line = file.ReadLine()) != null)
            {

                string name;
                int s, i, a, w, c, l, ch;
                string[] parts = line.Split(',');
                name = parts[0];
                s = Convert.ToInt32(parts[1]);
                i = Convert.ToInt32(parts[2]);
                a = Convert.ToInt32(parts[3]);
                c = Convert.ToInt32(parts[4]);
                w = Convert.ToInt32(parts[5]);
                l = Convert.ToInt32(parts[6]);
                ch = Convert.ToInt32(parts[7]);

                races.Add(new Race(name, s, i, a, c, w, l, ch));

            }

            return races;

        }

        //database getraces
        /* private List<Race> GetRaces()
        {

            List<Race> races = new List<Race>();

            using (OleDbConnection cn = new OleDbConnection(conn_string))
            {
                cn.Open();
                OleDbCommand sqlCommand = new OleDbCommand("SELECT * FROM RACE", cn);
                OleDbDataReader reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {

                    Race r = new Race();
                    r.Race_id = (int)reader["RACE_ID"];
                    r.Str_mod = (int)reader["STR_MOD"];
                    r.Int_mod = (int)reader["INT_MOD"];
                    r.Agi_mod = (int)reader["AGI_MOD"];

                    r.Racename = (string)reader["RACE"];

                    races.Add(r);

                }

                cn.Close();

            }

            return races;

        } */

        public override string ToString()
        {

            string str = string.Format("{0} str: {1} int: {2} agi: {3} con: {4} wis: {5} luk: {6} cha: {7}",this.Racename,this.Str_mod,this.Int_mod,this.Agi_mod,this.Con_mod,this.Wis_mod,this.Luk_mod,this.Cha_mod );

            return str;

        }

    }

}
