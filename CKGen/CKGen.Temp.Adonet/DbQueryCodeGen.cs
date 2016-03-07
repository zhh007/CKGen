using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CKGen.Temp.Adonet
{
    public class DbQueryCodeGen
    {
        public string Gen(string query, DataSet ds, string connstr)
        {

            if(ds != null && ds.Tables != null && ds.Tables.Count > 0)
            {
                foreach (DataTable dt in ds.Tables)
                {

                    foreach (DataColumn dc in dt.Columns)
                    {
                        
                    }
                }
            }

            return "";
        }
    }
}
