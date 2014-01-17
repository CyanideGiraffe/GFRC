using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Security.Cryptography;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using GFRC;

/// <summary>
/// The login class assists with storing information for logging in.
/// </summary>
namespace GFRC
{
    public class Login
    {
        #region Other Data
        public OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|gfrc_database.accdb");
        public OleDbCommand cmd = new OleDbCommand();
        public OleDbDataReader rdr;
        #endregion
        #region Private Data
        private uint _gloID;
        private string _Username;
        private string _Password;
        private string _Note;
        private bool _Active;
        private string _Status;
        private uint _gvoID;
        private DateTime _DateModified;
        private uint _ModifiedBy;
        #endregion
        #region Public Properties
        public uint gloID
        {
            get { return _gloID; }
            set { _gloID = value; }
        }
        public string Username
        {
            get { return _Username; }
            set { _Username = value; }
        }
        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }
        public string Note
        {
            get { return _Note; }
            set { _Note = value; }
        }
        public bool Active
        {
            get { return _Active; }
            set { _Active = value; }
        }
        public string Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        public uint gvoID
        {
            get { return _gvoID; }
            set { _gvoID = value; }
        }
        public DateTime DateModified
        {
            get { return _DateModified; }
            set { _DateModified = value; }
        }
        public uint ModifiedBy
        {
            get { return _ModifiedBy; }
            set { _ModifiedBy = value; }
        }
        #endregion
        #region Constructors
        // Blank constructor
        public Login()
        {
            _gloID = 0;
            _Username = null;
            _Password = null;
            _Note = null;
            _Active = false;
            _Status = null;
            _gvoID = 0;
            _DateModified = new DateTime(1901, 1, 1);
            _ModifiedBy = 0;
        }
        // Constructor for searched login
        public Login(uint gloid)
        {
            // Declarations
            uint gvoid = 0, modifiedby = 0;
            string username = null, password = null, note = null, status = null;
            bool active = false;
            DateTime datemodified = new DateTime(1901, 1, 1);

            // Command
            string query = string.Format("SELECT glo_username, glo_password, glo_note, glo_active, glo_status, gvo_id, date_modified, modified_by FROM gfrc_login" +
                                            " WHERE glo_id = {0}", gloid);

            try
            {
                using (conn)
                {
                    conn.Open();
                    cmd = new OleDbCommand(query, conn);
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        username = rdr.GetString(0);
                        password = rdr.GetString(1);
                        note = rdr.GetString(2);
                        active = rdr.GetBoolean(3);
                        status = rdr.GetString(4);
                        if (!UInt32.TryParse(rdr.GetValue(5).ToString(), out gvoid))
                            gvoid = 0;
                        if (!DateTime.TryParse(rdr.GetValue(6).ToString(), out datemodified))
                            datemodified = new DateTime(1901, 1, 1);
                        if (!UInt32.TryParse(rdr.GetValue(7).ToString(), out modifiedby))
                            modifiedby = 0;
                    }
                }
            }
            finally
            {
                if (rdr != null)
                    rdr.Close();
            }
            if (conn != null)
            {
                conn.Close();
            }


            _gloID = gloid;
            _Username = username;
            _Password = password;
            _Note = note;
            _Active = active;
            _Status = status;
            _gvoID = gvoid;
            _DateModified = datemodified;
            _ModifiedBy = modifiedby;
        }
        // Constructor to login
        public Login(string username, string password)
        {
            // Declarations
            uint gloid = 0, gvoid = 0, modifiedby = 0;
            string note = null, status = null;
            bool active = false;
            DateTime datemodified = new DateTime(1901, 1, 1);

            //md5
            password = CalculateMD5(password);

            // Command
            string query = string.Format("SELECT glo_id, glo_note, glo_active, glo_status, gvo_id, date_modified, modified_by FROM gfrc_login" +
                                            " WHERE glo_username = '{0}' AND glo_password = '{1}'", username, password);

            try
            {
                using (conn)
                {
                    conn.Open();
                    cmd = new OleDbCommand(query, conn);
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        gloid = Convert.ToUInt32(rdr.GetInt32(0));
                        note = rdr.GetString(1);
                        active = rdr.GetBoolean(2);
                        status = rdr.GetString(3);
                        if (!UInt32.TryParse(rdr.GetValue(4).ToString(), out gvoid))
                            gvoid = 0;
                        if (!DateTime.TryParse(rdr.GetValue(5).ToString(), out datemodified))
                            datemodified = new DateTime(1901, 1, 1);
                        if (!UInt32.TryParse(rdr.GetValue(6).ToString(), out modifiedby))
                            modifiedby = 0;
                    }
                }
            }
            finally
            {
                if (rdr != null)
                    rdr.Close();
            }
            if (conn != null)
            {
                conn.Close();
            }
            

            _gloID = gloid;
            _Username = username;
            _Password = password;
            _Note = note;
            _Active = active;
            _Status = status;
            _gvoID = gvoid;
            _DateModified = datemodified;
            _ModifiedBy = modifiedby;
        }
        // Constructor to create new login
        public Login(string username, string password, string note, string status, uint gvoid)
        {
            _gloID = 0;
            _Username = username;
            _Password = CalculateMD5(password);
            _Note = note;
            _Active = true;
            _Status = status;
            _gvoID = gvoid;
            _DateModified = new DateTime(1901, 1, 1);
            _ModifiedBy = 0;
        }
        // Constructor to edit existing login
        public Login(uint gloid, string username, string password, string note, bool active, string status, uint gvoid, DateTime datemodified, uint modifiedby)
        {
            _gloID = gloid;
            _Username = username;
            _Password = CalculateMD5(password);
            _Note = note;
            _Active = active;
            _Status = status;
            _gvoID = gvoid;
            _DateModified = datemodified;
            _ModifiedBy = modifiedby;
        }
        #endregion

        #region Private Methods
        private string CalculateMD5(string input)
        {
            //Declarations
            Byte[] originalBytes;
            Byte[] encodedBytes;
            MD5 md5;
            string result;

            //Step 1, salt input
            input += "gfrc";

            //Step 2, calculate MD5 hash from input
            md5 = new MD5CryptoServiceProvider();
            originalBytes = ASCIIEncoding.Default.GetBytes(input);
            encodedBytes = md5.ComputeHash(originalBytes);

            //Step 3 remove hyphens
            result = BitConverter.ToString(encodedBytes);
            result = result.Replace("-", "");

            //Step 3, return encoded password as string
            return result;
        }
        #endregion
        #region Public Methods
        public bool createLogin(string username, string password, string note, bool active, string status, uint gvoid)
        {
            bool result = false;
            string sGvoid;
            if (gvoid == 0)
                sGvoid = "null";
            else
                sGvoid = gvoid.ToString();
            // Command
            string query = String.Format(@"INSERT INTO gfrc_login (glo_username, glo_password, glo_note, glo_active, glo_status, gvo_id)" +
                                            "VALUES ('{0}','{1}','{2}',{3},'{4}',{5})", username, password, note, active, status, sGvoid);
            int affected = 0;

            try
            {
                using (conn)
                {
                    conn.Open();
                    cmd = new OleDbCommand(query, conn);
                    affected = cmd.ExecuteNonQuery();

                    // checks if rows were affected by the insert query
                    if (affected > 0)
                    {
                        result = true;

                        query = String.Format(@"SELECT glo_id FROM gfrc_login WHERE glo_username = '{0}'", username);
                        cmd = new OleDbCommand(query, conn);
                        rdr = cmd.ExecuteReader();

                        while (rdr.Read())
                        {
                            // Set gloID
                            _gloID = Convert.ToUInt32(rdr.GetInt32(0));
                        }
                    }

                }
            }
            catch (Exception e)
            {
                result = false;
            }
            finally
            {
                if (rdr != null)
                    rdr.Close();
            }
            if (conn != null)
            {
                conn.Close();
            }

            return result;
        }
        public bool editLogin(uint gloid, string password, string note, bool active, string status, uint gvoid, DateTime datemodified, uint modifiedby)
        {
            bool result = false;
            string sGvoid;
            if (gvoid == 0)
                sGvoid = "null";
            else
                sGvoid = gvoid.ToString();
            // Command
            string query = String.Format(@"UPDATE gfrc_login SET glo_password = '{0}', glo_note = '{1}', glo_active = {2}, glo_status = '{3}', gvo_id = '{4}', date_modified = {5}, modified_by = {6} " +
                                            "WHERE glo_id = {7}", password, note, active, status, sGvoid, datemodified, modifiedby, gloid);
            int affected = 0;

            try
            {
                using (conn)
                {
                    conn.Open();
                    cmd = new OleDbCommand(query, conn);
                    affected = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                result = false;
            }
            finally
            {
                if (rdr != null)
                    rdr.Close();
            }
            if (conn != null)
            {
                conn.Close();
            }

            // checks if rows were affected by the insert query
            if (affected > 0)
            {
                result = true;
            }

            return result;
        }
        public bool dereactivateLogin(uint gloid, bool active, DateTime datemodified, uint modifiedby)
        {
            bool result = false;

            // Command
            string query = String.Format(@"UPDATE gfrc_login SET glo_active = {0}, date_modified = {1}, modified_by = {2} WHERE glo_id = {3}", active, datemodified, modifiedby, gloid);
            int affected = 0;

            try
            {
                using (conn)
                {
                    conn.Open();
                    cmd = new OleDbCommand(query, conn);
                    affected = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                result = false;
            }
            finally
            {
                if (rdr != null)
                    rdr.Close();
            }
            if (conn != null)
            {
                conn.Close();
            }

            // checks if rows were affected by the insert query
            if (affected > 0)
            {
                result = true;
            }

            return result;
        }
        public bool deleteLogin(uint gloid)
        {
            bool result = false;

            // Command
            string query = String.Format(@"DELETE FROM gfrc_login WHERE glo_id = {0}", gloid);
            int affected = 0;

            try
            {
                using (conn)
                {
                    conn.Open();
                    cmd = new OleDbCommand(query, conn);
                    affected = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                result = false;
            }
            finally
            {
                if (rdr != null)
                    rdr.Close();
            }
            if (conn != null)
            {
                conn.Close();
            }

            // checks if rows were affected by the insert query
            if (affected > 0)
            {
                result = true;
            }

            return result;
        }
        #endregion

    }
}