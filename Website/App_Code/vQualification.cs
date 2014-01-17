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
    public class vQualification
    {
        #region Other Data
        public OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|gfrc_database.accdb");
        public OleDbCommand cmd = new OleDbCommand();
        public OleDbDataReader rdr;
        #endregion
        #region Private Data
        private uint _gvqID;
        private uint _gvoID;
        private string _Qualification;
        #endregion
        #region Public Properties
        public uint gvqID
        {
            get { return _gvqID; }
            set { _gvqID = value; }
        }
        public uint gvoID
        {
            get { return _gvoID; }
            set { _gvoID = value; }
        }
        public string Qualification
        {
            get { return _Qualification; }
            set { _Qualification = value; }
        }
        #endregion
        #region Constructors
        // Blank constructor
        public vQualification()
        {
            _gvqID = 0;
            _gvoID = 0;
            _Qualification = null;
        }
        // Constructor for searched volunteer qualification
        public vQualification(uint gvqid)
        {
            // Declarations
            uint gvoid = 0;
            string qualification = null;

            // Command
            string query = string.Format("SELECT gvo_id, gvq_qual " +
                                            "FROM gfrc_volunteer_qualification WHERE gvq_id = {0}", gvqid);

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
                        qualification = rdr.GetString(1);
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


            _gvqID = gvqid;
            _gvoID = gvoid;
            _Qualification = qualification;
        }
        // Constructor to create new volunteer qualification
        public vQualification(uint gvoid, string qualification)
        {
            _gvqID = 0;
            _gvoID = gvoid;
            _Qualification = qualification;
        }
        // Constructor to edit existing volunteer qualification
        public vQualification(uint gvqid, uint gvoid, string qualification)
        {
            _gvqID = gvqid;
            _gvoID = gvoid;
            _Qualification = qualification;
        }
        #endregion

        #region Public Methods
        public bool createQualification(vQualification create)
        {
            bool result = false;
            // Command
            string query = String.Format(@"INSERT INTO gfrc_volunteer_qualification (gvo_id, gvq_qual) " +
                                            "VALUES({0}, '{1}')",
                                            create.gvoID, create.Qualification);

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

                        query = String.Format(@"SELECT gvq_id FROM gfrc_volunteer_qualification WHERE gvq_id = (SELECT MAX(gvq_id) FROM gfrc_volunteer_qualification)");
                        cmd = new OleDbCommand(query, conn);
                        rdr = cmd.ExecuteReader();

                        while (rdr.Read())
                        {
                            // Set gloID
                            UInt32.TryParse(rdr.GetValue(0).ToString(), out _gvqID);
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
        public bool editQualification(vQualification edit)
        {
            bool result = false;
            // Command
            string query = String.Format(@"UPDATE gfrc_volunteer_qualification SET gvq_qual = '{0}' " +
                                            "WHERE gvq_id = {1}", edit.Qualification, edit.gvqID);

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
        public bool deleteQualification(uint gvqid)
        {
            bool result = false;

            // Command
            string query = String.Format(@"DELETE FROM gfrc_volunteer_qualification WHERE (gvq_id = {0})", gvqid);
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
        public Vector<vQualification> listQualifications(uint gvoid)
        {
            // Declarations
            Vector<vQualification> quals = new Vector<vQualification>();
            uint gvqid = 0;
            string qualification = null;
            vQualification input = new vQualification();

            // Command
            string query = string.Format("SELECT gvq_id, gvq_qual " +
                                            "FROM gfrc_volunteer_qualification WHERE gvo_id = {0}", gvoid);

            try
            {
                using (conn)
                {
                    conn.Open();
                    cmd = new OleDbCommand(query, conn);
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        if (!UInt32.TryParse(rdr.GetValue(0).ToString(), out gvqid))
                            gvqid = 0;
                        qualification = rdr.GetString(1);
                        if (gvqid != 0)
                        {
                            input = new vQualification(gvqid, gvoid, qualification);
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