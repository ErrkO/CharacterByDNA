using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharByDNA
{

    class CharacterDB
    { 

        public Database DB { get; set; }

        private NameDB NDB { get; set; }

        public int CID { get; private set; }

        public string Fname { get; private set; }

        public string Lname { get; private set; }

        public DNA Dna { get; private set; }

        public bool Gender { get; set; }

        public GTime BirthTime { get; private set; }

        public GTime DueDate { get; set; }

        public bool Dead { get; set; }

        public RaceDB Racee { get; set; }

        public int Strength
        {
            get
            {

                return (int)Math.Floor((double)(this.Dna.Genes[12].ToValue() + this.Dna.Genes[13].ToValue()) / 2);

            }

        }

        public int StrMod
        {

            get
            {

                return GetMod(this.Strength);

            }

        }
        
        public int Intelligence
        {

            get
            {

                return (int)Math.Floor((double)(this.Dna.Genes[14].ToValue() + this.Dna.Genes[15].ToValue()) / 2);

            }

        }

        public int IntMod
        {

            get
            {

                return GetMod(this.Intelligence);

            }

        }

        public int Dexterity
        {

            get
            {

                return (int)Math.Floor((double)(this.Dna.Genes[16].ToValue() + this.Dna.Genes[17].ToValue()) / 2);

            }

        }

        public int DexMod
        {

            get
            {

                return GetMod(this.Dexterity);

            }

        }

        public int Constitution
        {

            get
            {

                return (int)Math.Floor((double)(this.Dna.Genes[18].ToValue() + this.Dna.Genes[19].ToValue()) / 2);

            }

        }

        public int ConMod
        {

            get
            {

                return GetMod(this.Constitution);

            }

        }

        public int Wisdom
        {

            get
            {

                return (int)Math.Floor((double)(this.Dna.Genes[20].ToValue() + this.Dna.Genes[21].ToValue()) / 2);

            }

        }

        public int WisMod
        {

            get
            {

                return GetMod(this.Wisdom);

            }

        }

        public int Luck
        {

            get
            {

                return (int)Math.Floor((double)(this.Dna.Genes[22].ToValue() + this.Dna.Genes[23].ToValue()) / 2);

            }

        }

        public int LukMod
        {

            get
            {

                return GetMod(this.Luck);

            }

        }

        public int Charisma
        {

            get
            {

                return (int)Math.Floor((double)(this.Dna.Genes[24].ToValue() + this.Dna.Genes[25].ToValue()) / 2);

            }

        }

        public int ChaMod
        {

            get
            {

                return GetMod(this.Charisma);

            }

        }

        public int Height
        {

            get
            {

                if (this.Gender)
                {

                    return this.Racee.MHeightBase + this.Dna.Genes[4].ToValue() + this.Dna.Genes[5].ToValue();

                }

                else
                {

                    return this.Racee.FHeightBase + this.Dna.Genes[4].ToValue() + this.Dna.Genes[5].ToValue();

                }

            }

        }

        public string HairColor
        {

            get
            {

                return GetProperty(this.Dna.Genes[6].ToValue(), this.Dna.Genes[7].ToValue(), this.Racee.HairColors);

            }

        }

        public string EyeColor
        {

            get
            {

                return GetProperty(this.Dna.Genes[8].ToValue(), this.Dna.Genes[9].ToValue(), this.Racee.EyeColors);

            }

        }

        public string SkinColor
        {

            get
            {

                return GetProperty(this.Dna.Genes[10].ToValue(), this.Dna.Genes[11].ToValue(), this.Racee.SkinColors);

            }

        }

        // Desktop Conn
        private string sqlcconn = "URI=file:D:\\Users\\erico\\Code_Projects\\CharacterByDNA\\Database\\Game.db;Version=3";

        private SQLiteConnection sqlConn;

        public CharacterDB(Database db)
        {

            this.DB = db;
            this.sqlConn = db.SQLCONN;
            this.NDB = new NameDB(db);

        }

        public CharacterDB(Database db, int id, string fname, string lname, string dna, bool gender, double btime, double dtime, bool dead) : this(db)
        {
            
            this.CID = id;
            this.Fname = fname;
            this.Lname = lname;
            this.Dna = new DNA(dna);
            this.Gender = gender;
            this.BirthTime = new GTime(btime);
            this.DueDate = new GTime(dtime);
            this.Dead = dead;
            this.Racee = new RaceDB(db,1);

        }

        public CharacterDB(Character character, GTime time)
        {

            this.sqlConn = new SQLiteConnection(sqlcconn);
            this.CID = character.ID;
            this.Fname = character.FirstName;
            this.Lname = character.LastName;
            this.Dna = character.Dna;
            this.BirthTime = time;
            this.DueDate = character.DueDate;
            this.Dead = character.Dead;

        }

        public CharacterDB(Database db, DNA dna, GTime time, int id) : this(db)
        {

            List<Gene> genes = dna.Genes;
            this.CID = id;
            this.BirthTime = time;
            this.Dead = false;
            this.DueDate = new GTime(true);
            this.Dna = dna;

            // Gene Pair 1
            int gone = genes[0].ToInt() % 2;
            int gtwo = genes[1].ToInt() % 2;

            if (gone + gtwo == 1)
            {

                this.Gender = true;

            }

            else if (gone + gtwo == 0)
            {

                this.Gender = false;

            }

            else
            {

                Console.WriteLine("{0} has two y chromosomes",id);

            }

            this.Fname = NDB.GenFname(this.Gender);

            this.Lname = NDB.GenLname();

        }

        public CharacterDB(Database db, CharacterDB dad, CharacterDB mom, GTime time, int id) : this(db, new DNA(dad.Dna.Miosis(), mom.Dna.Miosis()),time, id)
        {

            this.Fname = NDB.GenFname(this.Gender);
            this.Lname = dad.Lname;

        }

        public void SaveCharacter()
        {

            this.sqlConn.Open();

            string query = string.Format("INSERT INTO Characters VALUES ({0},{1},{2},{3},{4},{5},{6},{7})", this.CID, this.Fname, this.Lname, this.Dna.ToString(),
                this.Gender, this.BirthTime.ToDouble(), this.DueDate.ToDouble(), this.Dead);

            SQLiteCommand command = new SQLiteCommand(query, sqlConn);
            command.ExecuteNonQuery();

        }

        public void SaveCharacter(CharacterDB character)
        {

            this.sqlConn.Open();

            string query = string.Format("INSERT INTO Characters VALUES ({0},{1},{2},{3},{4},{5},{6},{7})",character.CID,character.Fname,character.Lname,character.Dna.ToString(),
                character.Gender, character.BirthTime.ToDouble(),character.DueDate.ToDouble(),character.Dead);

            SQLiteCommand command = new SQLiteCommand(query, sqlConn);
            command.ExecuteNonQuery();

        }

        public void SaveListOfCharacters(List<CharacterDB> chars)
        {

            this.sqlConn.Open();

            foreach (CharacterDB c in chars)
            {

                string query = string.Format("INSERT INTO Characters VALUES ({0},{1},{2},{3},{4},{5},{6},{7})", c.CID, c.Fname, c.Lname, c.Dna.ToString(),
                    c.Gender, c.BirthTime.ToDouble(), c.DueDate.ToDouble(), c.Dead);

                SQLiteCommand command = new SQLiteCommand(query, sqlConn);
                command.ExecuteNonQuery();

            }

            sqlConn.Close();

        }

        private List<CharacterDB> Query(string query)
        {

            List<CharacterDB> characters = new List<CharacterDB>();

            this.sqlConn.Open();

            SQLiteCommand command = new SQLiteCommand(query, sqlConn);
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {

                int cid = reader.GetInt32(0);
                string fname = reader.GetString(1);
                string lname = reader.GetString(2);
                string dna = reader.GetString(3);
                bool gender = reader.GetBoolean(4);
                double btime = reader.GetDouble(5);
                double dtime = reader.GetDouble(6);
                bool dead = reader.GetBoolean(7);

                characters.Add(new CharacterDB(this.DB, cid, fname, lname, dna, gender, btime, dtime, dead));

            }

            sqlConn.Close();

            return characters;

        }

        public void SaveAllCharacters(List<CharacterDB> characters)
        {

            foreach(CharacterDB c in characters)
            {

                SaveCharacter(c);

            }

        }

        public List<CharacterDB> GetAllCharacters()
        {

            string query = "SELECT * FROM CharacterDB";

            return Query(query);

        }

        public List<CharacterDB> GetAllLivingCharacters()
        {

            string query = "SELECT * FROM CharacterDB WHERE Dead = false";

            return Query(query);

        }

        public List<CharacterDB> GetAllOfGender(bool gender)
        {

            string query = string.Format("SELECT * FROM CharacterDB WHERE Gender = {0} AND Dead = false",gender);

            return Query(query);

        }

        ///<summary>
        ///
        ///</summary>
        public string GetProperty(int value, int valuetwo, List<string> properties)
        {

            int vused;

            if (value == 16 || valuetwo == 16)
            {

                if (value < valuetwo)
                {

                    vused = value;

                }

                else if (valuetwo < value)
                {

                    vused = valuetwo;

                }

                else
                {

                    vused = 16;

                }

            }

            else if (value >= valuetwo)
            {

                vused = value;

            }

            else
            {

                vused = valuetwo;

            }

            if (vused >= 1 && vused <= 3)
            {

                return properties[1];

            }

            else if (vused >= 4 && vused <= 6)
            {

                return properties[2];

            }

            else if (vused >= 5 && vused <= 9)
            {

                return properties[3];

            }

            else if (vused >= 10 && vused <= 12)
            {

                return properties[4];

            }

            else if (vused >= 13 && vused <= 15)
            {

                return properties[5];

            }

            else if (vused == 16)
            {

                return properties[0];

            }

            return null;

        }

        public int GetMod(int score)
        {

            double temp = (score / 2);

            int temp2 = Convert.ToInt32(Math.Floor(temp));

            return temp2 - 5;

        }

    }

}
