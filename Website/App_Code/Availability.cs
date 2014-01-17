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
/// Volunteer availabilities
/// </summary>
namespace GFRC
{
    public class Availability
    {
        #region Other Data
        public OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|gfrc_database.accdb");
        public OleDbCommand cmd = new OleDbCommand();
        public OleDbDataReader rdr;
        #endregion
        #region Private Data
        private uint _gvaID;
        private uint _gvoID;
        private DateTime _Start;
        private DateTime _End;
        private uint _ReqHours;
        private bool _Mon;
        private bool _Tues;
        private bool _Wed;
        private bool _Thur;
        private bool _Fri;
        private bool _FillIn;
        private DateTime _DateModified;
        private uint _ModifiedBy;
        #endregion
        #region Public Properties
        public uint gvaID
        {
            get { return _gvaID; }
            set { _gvaID = value; }
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
        public uint ReqHours
        {
            get { return _ReqHours; }
            set { _ReqHours = value; }
        }
        public bool Mon
        {
            get { return _Mon; }
            set { _Mon = value; }
        }
        public bool Tues
        {
            get { return _Tues; }
            set { _Tues = value; }
        }
        public bool Wed
        {
            get { return _Wed; }
            set { _Wed = value; }
        }
        public bool Thur
        {
            get { return _Thur; }
            set { _Thur = value; }
        }
        public bool Fri
        {
            get { return _Fri; }
            set { _Fri = value; }
        }
        public bool FillIn
        {
            get { return _FillIn; }
            set { _FillIn = value; }
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
        public Availability()
        {
            _gvaID = 0;
            _gvoID = 0;
            _Start = new DateTime(1901, 1, 1);
            _End = new DateTime(1901, 1, 1);
            _ReqHours = 0;
            _Mon = false;
            _Tues = false;
            _Wed = false;
            _Thur = false;
            _Fri = false;
            _FillIn = false;
            _DateModified = new DateTime(1901, 1, 1);
            _ModifiedBy = 0;
        }
        // Constructor for searched volunteer
        public Availability(uint gvoid)
        {
            // Declarations
            uint gvaid = 0, reqhours = 0, modifiedby = 0;
            bool mon = false, tues = false, wed = false, thur = false, fri = false, fillin = false;
            DateTime datemodified = new DateTime(1901, 1, 1), start = new DateTime(1901, 1, 1), end = new DateTime(1901, 1, 1);

            // Command
            string query = string.Format("SELECT gva_id, gva_start, gva_end, gva_req_hours, gva_mon, gva_tues, gva_wed, gva_thur, gva_fri, gva_fill_in, date_modified, modified_by " +
                                            "FROM gfrc_volunteer_avail WHERE gvo_id = {0}", gvoid);

            try
            {
                using (conn)
                {
                    conn.Open();
                    cmd = new OleDbCommand(query, conn);
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        UInt32.TryParse(rdr.GetValue(0).ToString(), out gvaid);
                        start = rdr.GetDateTime(1);
                        if (!DateTime.TryParse(rdr.GetValue(2).ToString(), out end))
                            end = new DateTime(1901, 1, 1);
                        UInt32.TryParse(rdr.GetValue(3).ToString(), out reqhours);
                        mon = rdr.GetBoolean(4);
                        tues = rdr.GetBoolean(5);
                        wed = rdr.GetBoolean(6);
                        thur = rdr.GetBoolean(7);
                        fri = rdr.GetBoolean(8);
                        fillin = rdr.GetBoolean(9);
                        if (!DateTime.TryParse(rdr.GetValue(10).ToString(), out datemodified))
                            datemodified = new DateTime(1901, 1, 1);
                        if (!UInt32.TryParse(rdr.GetValue(11).ToString(), out modifiedby))
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


            _gvaID = gvaid;
            _gvoID = gvoid;
            _Start = start;
            _End = end;
            _ReqHours = reqhours;
            _Mon = mon;
            _Tues = tues;
            _Wed = wed;
            _Thur = thur;
            _Fri = fri;
            _FillIn = fillin;
            _DateModified = datemodified;
            _ModifiedBy = modifiedby;
        }
        // Constructor to create new volunteer availabilities
        public Availability(uint gvoid, DateTime start, DateTime end, uint reqhours, bool mon, bool tues, bool wed, bool thur, bool fri, bool fillin)
        {
            _gvaID = 0;
            _gvoID = gvoid;
            _Start = start;
            _End = end;
            _ReqHours = reqhours;
            _Mon = mon;
            _Tues = tues;
            _Wed = wed;
            _Thur = thur;
            _Fri = fri;
            _FillIn = fillin;
            _DateModified = new DateTime(1901, 1, 1);
            _ModifiedBy = 0;
        }
        // Constructor to edit existing volunteer availabilities
        public Availability(uint gvaid, uint gvoid, DateTime start, DateTime end, uint reqhours, bool mon, bool tues, bool wed, bool thur, bool fri, bool fillin, DateTime datemodified, uint modifiedby)
        {
            _gvaID = gvaid;
            _gvoID = gvoid;
            _Start = start;
            _End = end;
            _ReqHours = reqhours;
            _Mon = mon;
            _Tues = tues;
            _Wed = wed;
            _Thur = thur;
            _Fri = fri;
            _FillIn = fillin;
            _DateModified = datemodified;
            _ModifiedBy = modifiedby;
        }
        #endregion

        #region Public Methods
        public bool createAvailability(Availability create)
        {
            bool result = false;
            // Command
            string query = String.Format(@"INSERT INTO gfrc_volunteer_avail (gvo_id, gva_start, gva_end, gva_req_hours, gva_mon, gva_tues, gva_wed, gva_thur, gva_fri, gva_fill_in) " +
                                            "VALUES({0}, '{1}', '{2}', {3}, {4}, {5}, {6}, {7}, {8}, {9})",
                                            create.gvoID, create.Start, create.End, create.ReqHours, create.Mon, create.Tues, create.Wed, create.Thur, create.Fri, create.FillIn);

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

                        query = String.Format(@"SELECT gva_id FROM gfrc_volunteer_avail WHERE gva_id = (SELECT MAX(gva_id) FROM gfrc_volunteer_avail)");
                        cmd = new OleDbCommand(query, conn);
                        rdr = cmd.ExecuteReader();

                        while (rdr.Read())
                        {
                            // Set gloID
                            UInt32.TryParse(rdr.GetValue(0).ToString(), out _gvaID);
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
        public bool editAvailability(Availability edit)
        {
            bool result = false;
            // Command
            string query = String.Format(@"UPDATE gfrc_volunteer_avail SET gva_start = '{0}', gva_end = '{1}', gva_req_hours = {2}, gva_mon = {3}, gva_tues = {4}, gva_wed = {5}, gva_thur = {6}, gva_fri = {7}, gva_fill_in = {8}, date_modified = '{9}', modified_by = {10} " +
                                            "WHERE gvo_id = {11}", edit.Start, edit.End, edit.ReqHours, edit.Mon, edit.Tues, edit.Wed, edit.Thur, edit.Fri, edit.FillIn, edit.DateModified, edit.ModifiedBy, edit.gvoID);

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