using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sagment5
{
    public class ConnectwithDatabase
    {
        databaseConnection dbcon = new databaseConnection();

        public DataTable getStudentDtl()
        {
            string query = "getStudent_detail";
            DataTable dt = dbcon.Return(query);
            return dt;
        }

        public void Insertdata(string Name,DateTime DOB,double avg,bool status)
        {
            string query = "Insert into tbStudent (Name,DOB,GradeAVGPoint,Active) Values "+
                            "('" + Name + "' ,'" + DOB + "','" + avg + "','" + status + "')";
            dbcon.ExecuteCommand(query);
        }
        public int getID()
        {
            string query = "select top(1) Id from tbStudent order by Id desc";
            object o = dbcon.ExecuteScalar(query);
            if (o != DBNull.Value)
                return Convert.ToInt32(o) + 1;
            else
                return 1;
        }
        public void Updatedata(int Id, string name, DateTime DOB, double avg, bool status)
        {
            string query = "Update tbStudent set Name='"+ name +"',DOB='"+ DOB +"',GradeAVGPoint='"+ avg +"',Active='"+ status+"'" +
                                       "where(Id="+Id +")";
            dbcon.ExecuteCommand(query);
        }
         
    }
}
