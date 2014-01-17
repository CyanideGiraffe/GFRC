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
using Vector;

/// <summary>
/// Volunteer availabilities
/// </summary>
namespace GFRC
{
    public class Holiday
    {
        #region Other Data
        public OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|gfrc_database.accdb");
        public OleDbCommand cmd = new OleDbCommand();
        public OleDbDataReader rdr;
        #endregion
        #region Private Data
        private uint _gvhID;
        private uint _gvoID;
        private DateTime _Start;
        private DateTime _End;
        private DateTime _DateModified;
        private uint _ModifiedBy;
        #endregion
        #region Public Properties
        public uint gvhID
        {
            get { return _gvhID; }
            set { _gvhID = value; }
        }
        public uint gvoID
        {
            get { return _gvoID; }
            set { _gvoID = value; }
        }
        public DateTime Start
        {
            get { return _Start; }
            set { _Start = value; }
        }
        public DateTime End
        {
            get { return _End; }
            set { _End = value; }
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
        public Holiday()
        {
            _gvhID = 0;
            _gvoID = 0;
            _Start = new DateTime(1901, 1, 1);
            _End = new DateTime(1901, 1, 1);
            _DateModified = new DateTime(1901, 1, 1);
            _ModifiedBy = 0;
        }
        // Constructor for searched volunteer holiday
        public Holiday(uint gvhid)
        {
            // Declarations
            uint gvoid = 0, modifiedby = 0;
            DateTime datemodified = new DateTime(1901, 1, 1), start = new DateTime(1901, 1, 1), end = new DateTime(1901, 1, 1);

            // Command
            string query = string.Format("SELECT gvo_id, gvh_start, gvh_end, date_modified, modified_by " +
                                            "FROM gfrc_volunteer_holiday WHERE gvh_id = {0}", gvhid);

            try
            {
                using (conn)
                {
                    conn.Open();
                    cmd = new OleDbCommand(query, conn);
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        gvoid = Convert.ToUInt32(rdr.GetInt32(0));
                        start = rdr.GetDateTime(1);
                        if (!DateTime.TryParse(rdr.GetValue(2).ToString(), out end))
                            end = new DateTime(1901, 1, 1);
                        if (!DateTime.TryParse(rdr.GetValue(14).ToString(), out datemodified))
                            datemodified = new DateTime(1901, 1, 1);
                        if (!UInt32.TryParse(rdr.GetValue(15).ToString(), out modifiedby))
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


            _gvhID = gvhid;
            _gvoID = gvoid;
            _Start = start;
            _End = end;
            _DateModified = datemodified;
            _ModifiedBy = modifiedby;
        }
        // Constructor to create new volunteer holiday
        public Holiday(uint gvoid, DateTime start, DateTime end)
        {
            _gvhID = 0;
            _gvoID = gvoid;
            _Start = start;
            _End = end;
            _DateModified = new DateTime(1990, 1, 1);
            _ModifiedBy = 0;
        }
        // Constructor to edit existing volunteer holiday
        public Holiday(uint gvhid, uint gvoid, DateTime start, DateTime end, DateTime datemodified, uint modifiedby)
        {
            _gvhID = gvhid;
            _gvoID = gvoid;
            _Start = start;
            _End = end;
            _DateModified = datemodified;
            _ModifiedBy = modifiedby;
        }
        #endregion

        #region Public Methods
        public bool createHoliday(Holiday create)
        {
            bool result = false;
            // Command
            string query = String.Format(@"INSERT INTO gfrc_volunteer_holiday (gvo_id, gvh_start, gvh_end) " +
                                            "VALUES({0}, {1}, {2})",
                                            create.gvoID, create.Start, create.End);

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

                        query = String.Format(@"SELECT gvh_id FROM gfrc_volunteer_holiday WHERE gvh_id = (SELECT MAX(gvh_id) FROM gfrc_volunteer_holiday)");
                        cmd = new OleDbCommand(query, conn);
                        rdr = cmd.ExecuteReader();

                        while (rdr.Read())
                        {
                            // Set gloID
                            _gvhID = Convert.ToUInt32(rdr.GetInt32(0));
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
        public bool editHoliday(Holiday edit)
        {
            bool result = false;
            // Command
            string query = String.Format(@"UPDATE gfrc_volunteer_holiday SET gvh_start = {0}, gvh_end = {1}, date_modified = {2}, modified_by = {3} " +
                                            "WHERE gvo_id = {4}", edit.Start, edit.End, edit.DateModified, edit.ModifiedBy, edit.gvoID);

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
        public bool deleteHoliday(uint gvhid)
        {
            bool result = false;

            // Command
            string query = String.Format(@"DELETE FROM gfrc_volunteer_holiday WHERE gvh_id = {0}", gvhid);
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
        public Vector<Holiday> listHolidays(uint gvoid)
        {
            // Declarations
            Vector<Holiday> quals = new Vector<Holiday>();
            uint gvhid = 0, modifiedby = 0;
            DateTime datemodified = new DateTime(1901, 1, 1), start = new DateTime(1901, 1, 1), end = new DateTime(1901, 1, 1);
            Holiday input = new Holiday();

            // Declarations
            

            // Command
            string query = string.Format("SELECT gvh_id, gvh_start, gvh_end, date_modified, modified_by " +
                                            "FROM gfrc_volunteer_holiday WHERE gvo_id = {0}", gvoid);

            try
            {
                using (conn)
                {
                    conn.Open();
                    cmd = new OleDbCommand(query, conn);
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        if (UInt32.TryParse(rdr.GetValue(0).ToString(), out gvhid))
                        {
                            start = rdr.GetDateTime(1);
                            if (!DateTime.TryParse(rdr.GetValue(2).ToString(), out end))
                                end = new DateTime(1901, 1, 1);
                            if (!DateTime.TryParse(rdr.GetValue(14).ToString(), out datemodified))
                                datemodified = new DateTime(1901, 1, 1);
                            if (!UInt32.TryParse(rdr.GetValue(15).ToString(), out modifiedby))
                                modifiedby = 0;

                            input = new Holiday(gvhid, gvoid, start, end, datemodified, modifiedby);
                            quals.Add(input);
                        }
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

            return quals;
        }
        #endregion
    }
}