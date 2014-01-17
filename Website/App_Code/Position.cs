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
/// Volunteer position
/// </summary>
namespace GFRC
{
    public class Position
    {
        #region Other Data
        public OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|gfrc_database.accdb");
        public OleDbCommand cmd = new OleDbCommand();
        public OleDbDataReader rdr;
        #endregion
        #region Private Data
        private uint _gvpID;
        private uint _gvoID;
        private string _Position;
        private string _DriverCat;
        private string _DriverCatDoc;
        private string _DriverTrans;
        #endregion
        #region Public Properties
        public uint gvpID
        {
            get { return _gvpID; }
            set { _gvpID = value; }
        }
        public uint gvoID
        {
            get { return _gvoID; }
            set { _gvoID = value; }
        }
        public string PositionA
        {
            get { return _Position; }
            set { _Position = value; }
        }
        public string DriverCat
        {
            get { return _DriverCat; }
            set { _DriverCat = value; }
        }
        public string DriverCatDoc
        {
            get { return _DriverCatDoc; }
            set { _DriverCatDoc = value; }
        }
        public string DriverTrans
        {
            get { return _DriverTrans; }
            set { _DriverTrans = value; }
        }
        #endregion
        #region Constructors
        // Blank constructor
        public Position()
        {
            _gvpID = 0;
            _gvoID = 0;
            _Position = null;
            _DriverCat = null;
            _DriverCatDoc = null;
            _DriverTrans = null;
        }
        // Constructor for searched volunteer position
        public Position(uint gvid, char gvo_gvp)
        {
            // Declarations
            uint gvoid = 0, gvpid = 0;
            string position = null, drivercat = null, drivercatdoc = null, drivertrans = null;
            string query = "";

            // Command
            if (gvo_gvp == 'o')
                query = string.Format("SELECT gvo_id, gvp_id, gvp_position, gvp_driver_cat, gvp_driver_cat_doc, gvp_driver_trans " +
                                            "FROM gfrc_volunteer_position WHERE gvo_id = {0}", gvid);
            else if (gvo_gvp == 'p')
                query = string.Format("SELECT gvo_id, gvp_id, gvp_position, gvp_driver_cat, gvp_driver_cat_doc, gvp_driver_trans " +
                                            "FROM gfrc_volunteer_position WHERE gvp_id = {0}", gvid);

            try
            {
                using (conn)
                {
                    conn.Open();
                    cmd = new OleDbCommand(query, conn);
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        UInt32.TryParse(rdr.GetValue(0).ToString(), out gvoid);
                        UInt32.TryParse(rdr.GetValue(1).ToString(), out gvpid);
                        position = rdr.GetString(2);
                        drivercat = rdr.GetString(3);
                        drivercatdoc = rdr.GetString(4);
                        drivertrans = rdr.GetString(5);
                    }
                }
            }
            catch (Exception e)
            {
                string exception = e.ToString();
                gvoid = 0;
                gvpid = 0;
                position = "";
                drivercat = "";
                drivercatdoc = "";
                drivertrans = "";
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

            _gvpID = gvpid;
            _gvoID = gvoid;
            _Position = position;
            _DriverCat = drivercat;
            _DriverCatDoc = drivercatdoc;
            _DriverTrans = drivertrans;
        }
        // Constructor to create new volunteer position
        public Position(uint gvoid, string position, string drivercat, string drivercatdoc, string drivertrans)
        {
            _gvpID = 0;
            _gvoID = gvoid;
            _Position = position;
            _DriverCat = drivercat;
            _DriverCatDoc = drivercatdoc;
            _DriverTrans = drivertrans;
        }
        // Constructor to edit existing volunteer holiday
        public Position(uint gvpid, uint gvoid, string position, string drivercat, string drivercatdoc, string drivertrans)
        {
            _gvpID = gvpid;
            _gvoID = gvoid;
            _Position = position;
            _DriverCat = drivercat;
            _DriverCatDoc = drivercatdoc;
            _DriverTrans = drivertrans;
        }
        #endregion

        #region Public Methods
        public bool createPosition(Position create)
        {
            bool result = false;
            // Command
            string query = String.Format(@"INSERT INTO gfrc_volunteer_position (gvo_id, gvp_position, gvp_driver_cat, gvp_driver_cat_doc, gvp_driver_trans) " +
                                            "VALUES({0}, '{1}', '{2}', '{3}', '{4}')",
                                            create.gvoID, create.PositionA, create.DriverCat, create.DriverCatDoc, create.DriverTrans);

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

                        query = String.Format(@"SELECT gvp_id FROM gfrc_volunteer_position WHERE gvp_id = (SELECT MAX(gvp_id) FROM gfrc_volunteer_position)");
                        cmd = new OleDbCommand(query, conn);
                        rdr = cmd.ExecuteReader();

                        while (rdr.Read())
                        {
                            // Set gloID
                            _gvpID = Convert.ToUInt32(rdr.GetInt32(0));
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
        public bool editPosition(Position edit)
        {
            bool result = false;
            // Command
            string query = String.Format(@"UPDATE gfrc_volunteer_position SET gvp_position = '{0}', gvp_driver_cat = '{1}', gvp_driver_cat_doc = '{2}', gvp_driver_trans = '{3}' " +
                                            "WHERE gvo_id = {4} AND gvp_id = {5}", edit.PositionA, edit.DriverCat, edit.DriverCatDoc, edit.DriverTrans, edit.gvoID, edit.gvpID);

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