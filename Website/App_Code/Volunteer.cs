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
/// Summary description for Volunteer
/// </summary>
namespace GFRC
{
    public class Volunteer
    {
        #region Other Data
        public OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|gfrc_database.accdb");
        public OleDbCommand cmd = new OleDbCommand();
        public OleDbDataReader rdr;
        #endregion
        #region Private Data
        private uint _gvoID;
        private string _Name;
        private string _Address;
        private string _PostalAddress;
        private string _Email;
        private string _HomePh;
        private string _MobilePh;
        private DateTime _DOB;
        private string _Status;
        private string _Referred;
        private string _ReferredDoc;
        private bool _Police;
        private string _PoliceDoc;
        private bool _Induction;
        private string _Application;
        private DateTime _DateModified;
        private uint _ModifiedBy;
        #endregion
        #region Public Properties
        public uint gvoID
        {
            get { return _gvoID; }
            set { _gvoID = value; }
        }
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        public string Address
        {
            get { return _Address; }
            set { _Address = value; }
        }
        public string PostalAddress
        {
            get { return _PostalAddress; }
            set { _PostalAddress = value; }
        }
        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }
        public string HomePh
        {
            get { return _HomePh; }
            set { _HomePh = value; }
        }
        public string MobilePh
        {
            get { return _MobilePh; }
            set { _MobilePh = value; }
        }
        public DateTime DOB
        {
            get { return _DOB; }
            set { _DOB = value; }
        }
        public string Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        public string Referred
        {
            get { return _Referred; }
            set { _Referred = value; }
        }
        public string ReferredDoc
        {
            get { return _ReferredDoc; }
            set { _ReferredDoc = value; }
        }
        public bool Police
        {
            get { return _Police; }
            set { _Police = value; }
        }
        public string PoliceDoc
        {
            get { return _PoliceDoc; }
            set { _PoliceDoc = value; }
        }
        public bool Induction
        {
            get { return _Induction; }
            set { _Induction = value; }
        }
        public string Application
        {
            get { return _Application; }
            set { _Application = value; }
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
        public Volunteer()
        {
            _gvoID = 0;
            _Name = null;
            _Address = null;
            _PostalAddress = null;
            _Email = null;
            _HomePh = null;
            _MobilePh = null;
            _DOB = new DateTime(1901, 1, 1);
            _Status = null;
            _Referred = null;
            _ReferredDoc = null;
            _Police = false;
            _PoliceDoc = null;
            _Induction = false;
            _Application = null;
            _DateModified = new DateTime(1901, 1, 1);
            _ModifiedBy = 0;
        }
        // Constructor for searched volunteer
        public Volunteer(uint gvoid)
        {
            // Declarations
            uint modifiedby = 0;
            string name = null, address = null, postal = null, email = null, home = null, mobile = null;
            string status = null, referred = null, refdoc = null, poldoc = null, application = null;
            bool police = false, induction = false;
            DateTime datemodified = new DateTime(1901, 1, 1), dob = new DateTime(1901, 1, 1);

            // Command
            string query = string.Format("SELECT gvo_name, gvo_address, gvo_postal_address, gvo_email, gvo_home_ph, gvo_mobile_ph, gvo_dob, gvo_status, gvo_referred, gvo_referred_doc, " +
                                            "gvo_police, gvo_police_doc, gvo_induction, gvo_application, date_modified, modified_by FROM gfrc_volunteer" +
                                            " WHERE (gvo_id = {0})", gvoid);

            try
            {
                using (conn)
                {
                    conn.Open();
                    cmd = new OleDbCommand(query, conn);
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        name = rdr.GetString(0);
                        address = rdr.GetString(1);
                        postal = rdr.GetString(2);
                        email = rdr.GetString(3);
                        home = rdr.GetString(4);
                        mobile = rdr.GetString(5);
                        dob = rdr.GetDateTime(6);
                        status = rdr.GetString(7);
                        referred = rdr.GetString(8);
                        refdoc = rdr.GetString(9);
                        police = rdr.GetBoolean(10);
                        poldoc = rdr.GetString(11);
                        induction = rdr.GetBoolean(12);
                        application = rdr.GetString(13);
                        if (!DateTime.TryParse(rdr.GetValue(14).ToString(), out datemodified))
                            datemodified = new DateTime(1901, 1, 1);
                        if (!UInt32.TryParse(rdr.GetValue(15).ToString(), out modifiedby))
                            modifiedby = 0;
                    }
                }
            }
            catch (Exception e)
            {
                name = "";
                address = "";
                postal = "";
                email = "";
                home = "";
                mobile = "";
                dob = new DateTime(1901, 1, 1);
                status = "";
                referred = "";
                refdoc = "";
                police = false;
                poldoc = "";
                induction = false;
                application = "";
                datemodified = new DateTime(1901, 1, 1);
                modifiedby = 0;
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


            _gvoID = gvoid;
            _Name = name;
            _Address = address;
            _PostalAddress = postal;
            _Email = email;
            _HomePh = home;
            _MobilePh = mobile;
            _DOB = dob;
            _Status = status;
            _Referred = referred;
            _ReferredDoc = refdoc;
            _Police = police;
            _PoliceDoc = poldoc;
            _Induction = induction;
            _Application = application;
            _DateModified = datemodified;
            _ModifiedBy = modifiedby;
        }
        // Constructor to create new volunteer
        public Volunteer(string name, string address, string postal, string email, string home, string mobile, DateTime dob, string status, string referred, string refdoc, bool police, string poldoc, bool induction, string application)
        {
            _gvoID = 0;
            _Name = name;
            _Address = address;
            _PostalAddress = postal;
            _Email = email;
            _HomePh = home;
            _MobilePh = mobile;
            _DOB = dob;
            _Status = status;
            _Referred = referred;
            _ReferredDoc = refdoc;
            _Police = police;
            _PoliceDoc = poldoc;
            _Induction = induction;
            _Application = application;
            _DateModified = new DateTime(1901, 1, 1);
            _ModifiedBy = 0;
        }
        // Constructor to edit existing volunteer
        public Volunteer(uint gvoid, string name, string address, string postal, string email, string home, string mobile, DateTime dob, string status, string referred, string refdoc, bool police, string poldoc, bool induction, string application, DateTime datemodified, uint modifiedby)
        {
            _gvoID = gvoid;
            _Name = name;
            _Address = address;
            _PostalAddress = postal;
            _Email = email;
            _HomePh = home;
            _MobilePh = mobile;
            _DOB = dob;
            _Status = status;
            _Referred = referred;
            _ReferredDoc = refdoc;
            _Police = police;
            _PoliceDoc = poldoc;
            _Induction = induction;
            _Application = application;
            _DateModified = datemodified;
            _ModifiedBy = modifiedby;
        }
        #endregion

        #region Public Methods
        public bool createVolunteer(Volunteer create)
        {
            bool result = false;
            // Command
            string query = String.Format(@"INSERT INTO gfrc_volunteer (gvo_name, gvo_address, gvo_postal_address, gvo_email, gvo_home_ph, gvo_mobile_ph, gvo_dob, gvo_status, gvo_referred, " +
                                            "gvo_referred_doc, gvo_police, gvo_police_doc, gvo_induction, gvo_application) " +
                                            "VALUES('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', {10}, '{11}', {12}, '{13}')",
                                            create.Name, create.Address, create.PostalAddress, create.Email, create.HomePh, create.MobilePh, create.DOB, create.Status,
                                            create.Referred, create.ReferredDoc, create.Police, create.PoliceDoc, create.Induction, create.Application);

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

                        query = String.Format(@"SELECT gvo_id FROM gfrc_volunteer WHERE gvo_id = (SELECT MAX(gvo_id) FROM gfrc_volunteer)");
                        cmd = new OleDbCommand(query, conn);
                        rdr = cmd.ExecuteReader();

                        while (rdr.Read())
                        {
                            // Set gloID
                            _gvoID = Convert.ToUInt32(rdr.GetInt32(0));
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
        public bool editVolunteer(Volunteer edit)
        {
            bool result = false;
            // Command
            string query = String.Format(@"UPDATE gfrc_volunteer SET gvo_name = '{0}', gvo_address= '{1}', gvo_postal_address = '{2}', gvo_email = '{3}', gvo_home_ph = '{4}', gvo_mobile_ph ='{5}', gvo_dob = '{6}', gvo_status = '{7}', gvo_referred = '{8}', " +
                                            "gvo_referred_doc = '{9}', gvo_police = {10}, gvo_police_doc = '{11}', gvo_induction = {12}, gvo_application = '{13}', date_modified = '{14}', modified_by = {15} " +
                                            "WHERE gvo_id = {16}",
                                            edit.Name, edit.Address, edit.PostalAddress, edit.Email, edit.HomePh, edit.MobilePh, edit.DOB, edit.Status, edit.Referred,
                                            edit.ReferredDoc, edit.Police, edit.PoliceDoc, edit.Induction, edit.Application, edit.DateModified, edit.ModifiedBy, edit.gvoID);

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
        public bool dereactivateVolunteer(uint gvoid, string status, DateTime datemodified, uint modifiedby)
        {
            bool result = false;

            // Command
            string query = String.Format(@"UPDATE gfrc_volunteer SET gvo_active = '{0}', date_modified = '{1}', modified_by = {2} WHERE gvo_id = {3}", status, datemodified, modifiedby, gvoid);
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
        public override string ToString()
        {
            string result = null;

            result = string.Format("#{0}: {1}", _gvoID, _Name);

            return result;
        }
        #endregion
    }
}