using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 武汉大学试题
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        #region 读取矩阵
        /// <summary>
        /// 读取txt文件中的矩阵
        /// </summary>
        /// <param name="pth"></param>

        public double[,] Read_arry(string path) {

            StreamReader sr = new StreamReader(path, Encoding.Default);
            StreamReader sr1 = new StreamReader(path, Encoding.Default);
            String line;
            int n = 0;
            //get the dimession
            while ((line = sr.ReadLine()) != null) {
                string[] ls = line.Split('\t');
                n = ls.Length;
               // MessageBox.Show(ls[0]);
            }
            sr.Close();
            int row = 0;
            double[,] Matrix=new double[n,n];
            //According read data through the line.
            while ((line = sr1.ReadLine()) != null)
            {
                string[] ls = line.Split('\t');
                //MessageBox.Show(ls[0]);
                //get the cols of the matrix
                int cols = ls.Length;
                for (int i = 0; i < ls.Length; i++) {
                    //MessageBox.Show(Matrix[row,i].ToString());
                    Matrix[row, i] = Convert.ToDouble(ls[i]);
                    //MessageBox.Show(Matrix[row, i].ToString());
                }
                row++;
            }
            sr1.Close();
            return Matrix;
        }

        #endregion

        #region 矩阵运算
        /// <summary>
        /// 初步定义m1为小矩阵 m2为大矩阵
        /// 后期可以对其进行优化
        /// </summary>
        /// <param name="m1"></param>
        /// <param name="m2"></param>

        public double[,] Cal_1(double[,] m1,double[,] m2) {
            //get the 
            int l1 = m1.GetLength(0);
            int l2 = m2.GetLength(0);
            double[,] Result = new double[10,10];

            //规定m1为M矩阵 m2为N矩阵
            for (int i = 0; i < 10; i++) {
                for (int j = 0; j < 10; j++) {

                    //计算Mi,j *N I-i-1,J-j-1

                    double sm_1=0.0, sm_2=0.0;//第一个除数 第二个被除数

                    for (int x = 0; x < 3; x++) {
                        for (int y = 0; y < 3; y++) {
                            //判断I-i-1<0或者J-j-1<0..
                            if (i - x - 1 < 0 || j - y - 1 < 0 || i - x - 1 > 9 || j - y - 1 > 9)
                            {
                                Result[i, j] = 0.0;

                                continue;
                            }
                            else {
                                sm_1 += m1[x, y] * m2[i - x - 1, j - y - 1];
                                sm_2 += m1[x, y];

                            }
                        }
                    }
                    if (sm_2 == 0)
                    {
                        Result[i, j] = 0.0;

                    }
                    else {
                        double st = sm_1 / sm_2;
                        Result[i, j] = st;

                    }
                }
            }


            return Result;
        }
        #endregion

        #region 写入文件
        /// <summary>
        /// 对矩阵写入txt
        /// 可以做判断实现判断txt是否存在不存在自动建立
        /// </summary>
        /// <param name="m"></param>

        public void Write_Rst(double[,] m)
        {
            //get the cols
            int cols = m.GetLength(0);
            string sm="";
            for (int i = 0; i < cols; i++) {
                for (int j = 0; j < cols; j++) {
                    sm=sm+ m[i, j].ToString() + '\t';
                }
                sm += Environment.NewLine;
            }
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string Names = openFileDialog1.FileName;
                FileStream fs = new FileStream(Names, FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);
                //开始写入
                sw.Write(sm);
                //清空缓冲区
                sw.Flush();
                //关闭流
                sw.Close();
                fs.Close();
                MessageBox.Show("finish");
            }
        }

        #endregion

        #region 按钮事件
        /// <summary>
        /// 事件机制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            double[,] a = Read_arry(textBox1.Text.ToString());
            double[,] b = Read_arry(textBox2.Text.ToString());
            Write_Rst(Cal_1(a, b));

        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK) {
                string Name = openFileDialog.FileName;
                textBox1.Text = Name;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string Name = openFileDialog.FileName;
                textBox2.Text = Name;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(-1);
        }
        #endregion
    }
}
