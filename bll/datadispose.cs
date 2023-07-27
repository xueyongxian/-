using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 窗口
{
    internal class datadispose
    {

        private DataTable dt = new DataTable();//数据库拿值
        public DataTable dt2 = new DataTable();//返回界面值
        private  database database =new database();
        /// <summary>
        /// 得到数据库中的值
        /// </summary>
        /// <returns></returns>
         public DataTable dbt()
        {
            dt = database.selectTable();
            return dt;
        }
        /// <summary>
        /// 提取一列内容返回
        /// </summary>
        /// <param name="ColumnName"></param>
        /// <returns></returns>
        public List<string > columdata(string ColumnName)
        {
            // 假设您已经在 dbt() 方法中填充了 DataTable
            dt = dbt();

            // 提取去掉重复的指定列
            var columnData = dt.AsEnumerable().Select(selector: row => row.Field<string>(ColumnName)).Distinct().ToList();

            // 将去重后的数据返回
            return columnData;
        }

        public DataTable FilterDataByBob(string uname, string power, string explain, string data1, string data2)
        {
            dt = dbt();
            DataRow[] filteredRows = dt.Select($"uname = '{uname}' and " +
                                               $"power = '{power}' and " +
                                               $"explain like '{explain}' ");//and ");// +
                                               //$"data > '{data1}' and " +
                                              // $"data < '{data2}'");

            if (filteredRows.Length > 0)
            {
                // 使用 CopyToDataTable 方法将 DataRow[] 转换为 DataTable
                DataTable filteredDataTable = filteredRows.CopyToDataTable();
                return filteredDataTable;
            }
            else
            {
                // 如果没有符合条件的行，可以返回一个空的 DataTable 或者 null
                return new DataTable();
            }
        }
        



    }
}
