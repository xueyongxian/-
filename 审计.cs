using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace 窗口
{
    public partial class 审计 : UserControl
    {
        private datadispose datadispose = new datadispose();
        public 审计()
        {
            InitializeComponent();
            comboBox1.DataSource = datadispose.columdata("power");
            comboBox1.DisplayMember = "power";
            comboBox2.DataSource = datadispose.columdata("uname");
            comboBox2.DisplayMember = "uname";
            dataGridView1.DataSource = datadispose.dbt();
            numericUpDown1.Minimum = 1;
            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;





        }
        /// <summary>
        /// 查询按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            datadispose.dt2 = datadispose.FilterDataByBob(comboBox2.Text,
                                                                   comboBox1.Text,
                                                                   textBox1.Text,
                                                                   dateTimePicker1.Text,
                                                                   dateTimePicker1.Text);
            LoadPageData(currentPage);
        }
        private int pageSize = 3; // 每页显示的记录数
        private int currentPage = 1; // 当前页码
         
        /// <summary>
        /// 显示页数
        /// </summary>
        /// <param name="page"></param>
        private void LoadPageData(int page)
        {
            // 计算起始索引和结束索引
            int startIndex = (page - 1) * pageSize;
           
            int endIndex = Math.Min(startIndex + pageSize, datadispose.dt2.Rows.Count);
            

            // 创建一个新的 DataTable 用于存放分页数据
            DataTable pageData = datadispose.dt2.Clone();

            // 将分页数据复制到新的 DataTable
            for (int i = startIndex; i < endIndex; i++)
            {
                pageData.ImportRow(datadispose.dt2.Rows[i]);
            }
            // 更新当前页码
            currentPage = page;
            // 绑定分页数据到 DataGridView 控件
            dataGridView1.DataSource = pageData;
            int totalPages = (int)Math.Ceiling((double)datadispose.dt2.Rows.Count / pageSize);//计算总页数
            label6.Text = $"显示{startIndex + 1}-{endIndex}条，共{totalPages}页，总计{datadispose.dt2.Rows.Count}条";
            comboBox3.Text = pageSize.ToString();
            numericUpDown1.Maximum = totalPages;
            numericUpDown1.Value = currentPage;

        }
        /// <summary>
        /// 下一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            int totalPages = (int)Math.Ceiling((double)datadispose.dt2.Rows.Count / pageSize);
            if (currentPage < totalPages)
            {
                LoadPageData(currentPage + 1);
            }
        }
        /// <summary>
        /// 上一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                LoadPageData(currentPage - 1);
            }
        }
        /// <summary>
        /// 每页显示条数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            pageSize = int.Parse(comboBox3.Text);
            currentPage = 1;
            LoadPageData(currentPage);
        }
        /// <summary>
        /// 跳转到指定页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            currentPage= (int)numericUpDown1.Value;
            LoadPageData(currentPage);
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 尾页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            int totalPages = (int)Math.Ceiling((double)datadispose.dt2.Rows.Count / pageSize);
            
            LoadPageData(totalPages);
            


        }
        /// <summary>
        /// 首页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            LoadPageData(1);
        }
    }


    
}
