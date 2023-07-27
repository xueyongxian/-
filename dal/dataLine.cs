using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 窗口
{
    class dataLine
    {
        public static string dataName = "sqltablebuild";//数据库名称
        public static string tableName = "testTable";//表名称
        public static string password = "123";//数据库密码
       


        /// <summary>
        /// 获得master数据库连接
        /// </summary>
        /// <returns></returns>
        public string DataMasterLine()
        {
            string DataMasterLine = @"server=" + Environment.MachineName + ";database=master;uid=sa;pwd="+password+";Connection Timeout=5;";
            return DataMasterLine;
        }

        /// <summary>
        /// 获得数据库连接
        /// </summary>
        /// <param name="dataName"></param>
        /// <returns></returns>
        public string DataLine(string dataName)
        {
            //"Data Source={Environment.MachineName};Initial Catalog=XYXtable;User ID = sa;Password=hb123456;Connect Timeout=30");
            string dataBaseLink = @"server=" + Environment.MachineName + ";database="+ dataName + ";uid=sa;pwd=" + password + ";Connection Timeout=5;";
            return dataBaseLink;
        }
    }
}
