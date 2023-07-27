using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 窗口
{
    class database
    {
        
        dataLine dataLine = new dataLine();
        public string dataName = dataLine.dataName;
        public string tableName = dataLine.tableName;
        public string password = dataLine.password;
        public string FillName = @"D:\work\RShDB_Data\";
        /// <summary>
        /// 创建数据库
        /// </summary>
        public void createData()
        {

            SqlConnection conn = new SqlConnection(dataLine.DataMasterLine());
            try
            {
                conn.Open();
               
                string sql = "if not exists(select * from master.dbo.sysdatabases where name='" + dataName + "')"
                    + "create database " + dataName + " on primary"
                    + "(name=" + dataName + ",filename='" + FillName + dataName + ".mdf',size=5mb,filegrowth=1mb)"
                    + "log on(name=" + dataName + "_log,filename='" + FillName + dataName + "_log.ldf',size=5mb,filegrowth=10%) COLLATE Chinese_PRC_CS_AS"
                    + " alter database " + dataName  + " set recovery simple"; 
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.ExecuteNonQuery();
                conn.Close();

            }
            catch (Exception e)
            {
                conn.Close();
                MessageBox.Show(e.Message);

            }

        }

        /// <summary>
        /// 创建数据表
        /// </summary>
        /// <param name="tableName"></param>
        public void createTable()
        {
            SqlConnection conn = new SqlConnection(dataLine.DataLine(dataName));
            try
            {
                conn.Open();

                string sql = @" CREATE TABLE [dbo].[" + tableName + "] (                      " +
                                "     [id] INT  IDENTITY (100, 1) NOT NULL,                    " +
                                "     [uname]    VARCHAR (100) NOT NULL,                       " +
                                "     [password] VARCHAR (100) NOT NULL,                       " +
                                "     [power]    VARCHAR (100) NOT NULL,                       " +
                                "     [data]     VARCHAR (100) NOT NULL,                       " +
                                "     [explain]  VARCHAR (100) NOT NULL,                       " +
                                "     PRIMARY KEY CLUSTERED ([id] ASC)                         " +
                                " );                                                           " +
                                " INSERT INTO " + tableName + " (uname,password,power,data,explain)VALUES(" +
                                "     'admin',                                                 " +
                                "     'admin',                                                 " +
                                "     '管理员',                                                " +
                                "     '2023-07-01',                                            " +
                                "     '无'                                                     " +
                                " );";



                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                conn.Close();

            }
            catch (Exception e)
            {
                conn.Close();
                MessageBox.Show(e.Message);

            }
        }

        /// <summary>
        /// 遍历数据库
        /// </summary>
        /// <returns></returns>
        public DataTable selectTable()
        {
            SqlConnection conn = new SqlConnection(dataLine.DataLine(dataName));
            try
            {
                conn.Open();
                string sql = $"select * from {tableName} ";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                SqlDataAdapter sda = new SqlDataAdapter(sql, conn);
                
                DataSet ds = new DataSet();
                sda.Fill(ds);
                conn.Close();
                return ds.Tables[0];
            }
            catch (Exception e)
            {
                conn.Close();
                MessageBox.Show(e.Message);
                throw;
            }


        }
        /// <summary>
        /// 提取列数据
        /// </summary>
        /// <param name="columnName">列名称</param>
        /// <returns></returns>
        public DataTable selectColumTable(string columnName)
        {
            SqlConnection conn = new SqlConnection(dataLine.DataLine(dataName));
            try
            {
                conn.Open();
                string sql = $"select {columnName} from {tableName} ";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                SqlDataAdapter sda = new SqlDataAdapter(sql, conn);

                DataSet ds = new DataSet();
                sda.Fill(ds);
                conn.Close();
                return ds.Tables[0];
            }
            catch (Exception e)
            {
                conn.Close();
                MessageBox.Show(e.Message);
                throw;
            }


        }

        /// <summary>
        /// 删除数据库内容
        /// </summary>
        /// <param name="id">一般为id</param>
        /// <param name="dataid">id的值</param>
        public void deleteDb(string id, string dataid)//
        {
            SqlConnection conn = new SqlConnection(dataLine.DataLine(dataName));
            try
            {
                conn.Open();
                string sql = $"delete from {tableName}" +
                                $"where                  " +
                                $"{id}= '{dataid}';";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("信息");
                conn.Close();
            }
            catch (Exception e)
            {
                conn.Close();
                MessageBox.Show(e.Message);
            }
        }

        /// <summary>
        /// 修改数据库资源
        /// </summary>
        /// <param name="rankname">所在行</param>
        /// <param name="data">数据</param>
        /// /// <param name="dataid">id行数据</param>
        public void changeDb(string rankname, string data, string dataid)
        {
            SqlConnection conn = new SqlConnection(dataLine.DataLine(dataName));
            try
            {
                conn.Open();
                string time = DateTime.Now.ToString("yyyy-MM-dd"); ;
                string sql = $"update {tableName}           " +
                                $"   set {rankname}  = '{data}'" +
                                $"   where id  ={dataid} ;";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("信息新增成功！！");
                conn.Close();
            }
            catch (Exception e)
            {
                conn.Close();
                MessageBox.Show(e.Message);
            }

        }

        /// <summary>
        /// 增加数据信息
        /// </summary>
        /// <param name="tablename"></param>
        /// <param name="textBoxAddUseName"></param>
        /// <param name="textBoxAddpassword"></param>
        /// <param name="textBoxAddExplain"></param>
        /// <param name="textBoxAddEnsurePassword"></param>
        /// <param name="power1"></param>
        public void addDb(string textBoxAddUseName, string textBoxAddpassword, string textBoxAddExplain, string textBoxAddEnsurePassword, string power1)
        {
            SqlConnection conn = new SqlConnection(dataLine.DataLine(dataName));
            try
            {
                conn.Open();
                string time = DateTime.Now.ToString("yyyy-MM-dd"); ;
                string sql = $"INSERT INTO {tableName}                         " +
                                "(uname,password,power,data,explain)VALUES       " +
                                "('" + textBoxAddUseName + "',                   " +
                                " '" + textBoxAddpassword + "',                  " +
                                " '" + power1 + "',                              " +
                                " '" + time + "',                                " +
                                " '" + textBoxAddExplain + "');";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("信息新增成功！！");
                conn.Close();
            }
            catch (Exception e)
            {
                conn.Close();
                MessageBox.Show(e.Message);
            }
        }
#if false
        /// <summary>
        /// 判断是否数据库和表是否存在
        /// </summary>
        public void chackDB()
        {
            if (!ExistFileFolder())
            {
                CreateDirectory(FillName);
            }
            //string databaseName = "XYXtable";
            //string tableName = "Table2";
            if (CheckIfDatabaseExists(dataName))
            {
                if (!CheckIfTableExists(tableName))
                    createTable(tableName);//创建表                
            }
            else
            {
                //创建数据库
                createData();
                if (!CheckIfTableExists(tableName))
                    createTable(tableName);//创建表 
            }
        }
        /// <summary>
        /// 检查数据库是否存在
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="databaseName"></param>
        /// <returns></returns>
        private bool CheckIfDatabaseExists(string databaseName)
        {
            SqlConnection connection = new SqlConnection($"Data Source={Environment.MachineName};Initial Catalog=master;User ID = sa;Password={password};Connect Timeout=5");
            connection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = $"SELECT COUNT(*) FROM sys.databases WHERE name = '{databaseName}'";
            cmd.ExecuteNonQuery();
            int count = (int)cmd.ExecuteScalar();
            connection.Close();
            return count > 0;
        }
        /// <summary>
        /// 检查表是否存在
        /// </summary>        
        /// <param name="databaseName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        private bool CheckIfTableExists(string tableName)
        {
            return true;
            SqlConnection conn = new SqlConnection($"Data Source={Environment.MachineName};Initial Catalog={dataName};User ID = sa;Password={password};Connect Timeout=30");
            conn.Open();
            //string query = ;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = $"SELECT count(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = '{tableName}'";
            cmd.ExecuteNonQuery();
            int count = (int)cmd.ExecuteScalar();
            conn.Close();
            return count > 0;
        }
        /// <summary>
        /// 检查数据库文件夹是否存在
        /// </summary>
        /// <returns></returns>
        public bool ExistFileFolder()
        {
            return Directory.Exists(FillName);
        }

        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="FillName"></param>
        private void CreateDirectory(string FillName)
        {
            if (!Directory.Exists(FillName))
            {
                Directory.CreateDirectory(FillName);
            }
        }
#endif
    }

}
