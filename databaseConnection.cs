using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sagment5
{
    public class databaseConnection
    {
        public string DBConnectionStr;

        public databaseConnection()
        {
            string connection = System.Configuration.ConfigurationSettings.AppSettings.Get("DatabaseConnection");
            this.DBConnectionStr = connection;
        }

        #region Open/Close DB Connections

        private SqlConnection OpenConnection()
        {
            SqlConnection cn = new SqlConnection(DBConnectionStr);
            cn.Open();
            return cn;
        }

        private void CloseConnection(SqlConnection cn)
        {
            cn.Close();
            cn.Dispose();
        }
        #endregion

        #region  Execute General SQL statemenets
        public DataTable Return(string sQuery)
        {
            try
            {
                SqlConnection mySqlConnection = this.OpenConnection();
                SqlCommand mySqlCommand = mySqlConnection.CreateCommand();
                mySqlCommand.CommandTimeout = 2147483647;
                mySqlCommand.CommandText = sQuery;
                SqlDataAdapter mySqlDataAdapter = new SqlDataAdapter();
                mySqlDataAdapter.SelectCommand = mySqlCommand;
                DataSet myDataSet = new DataSet();
                mySqlDataAdapter.Fill(myDataSet, "Table1");
                this.CloseConnection(mySqlConnection);

                return myDataSet.Tables[0];
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public void ExecuteCommand(string sCommand)
        {
            SqlConnection cn = this.OpenConnection();
            SqlCommand cmd = new SqlCommand(sCommand, cn);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            this.CloseConnection(cn);
        }

        public DataTable ExecuteQuery(string sQuery)
        {

            DataTable dt = null;

            SqlConnection cn = this.OpenConnection();
            SqlDataAdapter da = new SqlDataAdapter(sQuery, cn);
            dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            this.CloseConnection(cn);

            return dt;
        }

        public object ExecuteScalar(string sQuery)
        {

            object o = null;
            SqlConnection cn = this.OpenConnection();
            SqlCommand cmd = new SqlCommand(sQuery, cn);
            o = cmd.ExecuteScalar();
            cmd.Dispose();
            this.CloseConnection(cn);
            if (o == DBNull.Value)
                return null;
            else
                return o;
        }

        #endregion
    }
}
