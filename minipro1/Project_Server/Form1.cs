using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Project_Server
{
    public partial class mo : Form
    {

        OracleCommand cmd = new OracleCommand();
        OracleDataReader rdr;
        OracleConnection conn = new OracleConnection(strConn);
        static string strConn = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=xe)));User Id=system ;Password=20161268;";
        OracleDataAdapter adapt = new OracleDataAdapter();
        private Thread serverThread;
        private Thread receiveThread;
        private Socket clnSocket;

        public mo()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            // 오라클 연결
            conn.Open();
            cmd.Connection = conn;
                
            // 클라이언트 대기 시작
            serverThread = new Thread(serverFunc);
            serverThread.IsBackground = true;
            serverThread.Start();

            // 당일 주문 목록 초기화
            cmd.CommandText = $"DELETE FROM ORDER_T";
            cmd.ExecuteNonQuery();

            // 모든 패널 오프
            panel1.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
        }
        private void serverFunc(object obj)
        {
            // 서버
            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            EndPoint endPoint = new IPEndPoint(IPAddress.Any, 11200);
            serverSocket.Bind(endPoint);
            serverSocket.Listen(10);

            clnSocket = serverSocket.Accept();

        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 패널 온오프
            panel1.Visible = true;
            panel3.Visible = false;
            panel4.Visible = false;

            // 주문데이터 받기
            receiveThread = new Thread(Receive);
            receiveThread.IsBackground = true;
            receiveThread.Start();

            // 주문 테이블 시각화
            string B = "SELECT * FROM ORDER_T";
            adapt.SelectCommand = new OracleCommand(B, conn);
            DataSet ds = new DataSet();
            adapt.Fill(ds);
            dataGridView2.DataSource = ds.Tables[0].DefaultView;
            oraclesearch();

            try
            {
                cmd.CommandText = $"SELECT 수량 FROM ORDER_T WHERE ROWNUM = 1";
                cmd.ExecuteNonQuery();

                rdr = cmd.ExecuteReader();
                rdr.Read();
                int bring = rdr.GetInt32(0);
            }
            catch
            {
                MessageBox.Show("주문이 없습니다.", "알림");
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            // 재고 테이블 시각화
            panel1.Visible = false;
            panel3.Visible = true;
            panel4.Visible = false;
            string A = "SELECT * FROM STOCK";
            
            adapt.SelectCommand = new OracleCommand(A, conn);
            DataSet ds = new DataSet();
            adapt.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0].DefaultView;
            chart();
        }
        private void chart()
        {
            // 재고 차트 시각화
            chart2.Series["Series1"].Points.Clear();
            cmd.CommandText = "SELECT DROGBA FROM STOCK WHERE NAME = '검정'";
            cmd.ExecuteNonQuery();
            rdr = cmd.ExecuteReader();
            rdr.Read();
            int item1 = rdr.GetInt32(0);
            chart2.Series[0].Points.AddXY("검정", item1);
            chart2.Series[0].Points[0].Color = Color.Black;

            cmd.CommandText = "SELECT DROGBA FROM STOCK WHERE NAME = '빨강'";
            cmd.ExecuteNonQuery();
            rdr = cmd.ExecuteReader();
            rdr.Read();
            int item2 = rdr.GetInt32(0);
            chart2.Series[0].Points.AddXY("빨강", item2);
            chart2.Series[0].Points[1].Color = Color.Red;

            cmd.CommandText = "SELECT DROGBA FROM STOCK WHERE NAME = '흰색'";
            cmd.ExecuteNonQuery();
            rdr = cmd.ExecuteReader();
            rdr.Read();
            int item3 = rdr.GetInt32(0);
            chart2.Series[0].Points.AddXY("흰색", item3);
            chart2.Series[0].Points[2].Color = Color.Silver;

            cmd.CommandText = "SELECT DROGBA FROM STOCK WHERE NAME = '면'";
            cmd.ExecuteNonQuery();
            rdr = cmd.ExecuteReader();
            rdr.Read();
            int item4 = rdr.GetInt32(0);
            chart2.Series[0].Points.AddXY("면", item4);
            chart2.Series[0].Points[3].Color = Color.Green;

            cmd.CommandText = "SELECT DROGBA FROM STOCK WHERE NAME = '청'";
            cmd.ExecuteNonQuery();
            rdr = cmd.ExecuteReader();
            rdr.Read();
            int item5 = rdr.GetInt32(0);
            chart2.Series[0].Points.AddXY("청", item5);
            chart2.Series[0].Points[4].Color = Color.Blue;

            cmd.CommandText = "SELECT DROGBA FROM STOCK WHERE NAME = '폴리'";
            cmd.ExecuteNonQuery();
            rdr = cmd.ExecuteReader();
            rdr.Read();
            int item6 = rdr.GetInt32(0);
            chart2.Series[0].Points.AddXY("폴리", item6);
            chart2.Series[0].Points[5].Color = Color.Purple;

            chart2.Series[0].Points[0].Label = $"{item1}";
            chart2.Series[0].Points[1].Label = $"{item2}";
            chart2.Series[0].Points[2].Label = $"{item3}";
            chart2.Series[0].Points[3].Label = $"{item4}";
            chart2.Series[0].Points[4].Label = $"{item5}";
            chart2.Series[0].Points[5].Label = $"{item6}";
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void Receive()
        {
            try
            {
                while (true)
                {

                    byte[] recvBytes1 = new byte[1024];
                    String day = DateTime.Now.ToString();
                    string date = day.Substring(0, 10);
                    clnSocket.Receive(recvBytes1);
                    string txt = Encoding.UTF8.GetString(recvBytes1, 0, recvBytes1.Length);
                    string[] txt_1 = txt.Split('/');
                
                    cmd.CommandText = $"INSERT INTO ORDER_T VALUES(ODD_SEQ.NEXTVAL,'{txt_1[0]}','{txt_1[1]}','{txt_1[2]}','{txt_1[3]}',{txt_1[4]},to_date('{date}','YY-MM-DD'))";
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("주문이 들어왔습니다.", "알림");
                }
            }
            catch
            {
                
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnBuy1_Click(object sender, EventArgs e)
        {

            //int num1 = Int32.Parse(textBox2)
            if (comboBox1.SelectedIndex == 0)
            {
                try
                {
                    cmd.CommandText = $"UPDATE STOCK SET DROGBA = DROGBA + '{textBox2.Text}' WHERE NAME = '검정'";
                    cmd.ExecuteNonQuery();
                    MessageBox.Show($"검정색 도료 {textBox2.Text}개를 구매하셨습니다.", "알림");
                    cmd.CommandText = "Commit";
                    cmd.ExecuteNonQuery();
                    comboBox1.SelectedIndex = -1;
                    textBox2.Clear();
                    chart();
                }
                catch (Exception x)
                {
                    MessageBox.Show("도료의 갯수를 입력하지 않았습니다.", "오류");
                }
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                try
                {
                    cmd.CommandText = $"UPDATE STOCK SET DROGBA = DROGBA + '{textBox2.Text}' WHERE NAME = '빨강'";
                    cmd.ExecuteNonQuery();
                    MessageBox.Show($"도료의 빨강색 도료 {textBox2.Text}개를 구매하셨습니다.", "알림");
                    cmd.CommandText = "Commit";
                    cmd.ExecuteNonQuery();
                    comboBox1.SelectedIndex = -1;
                    textBox2.Clear();
                    chart();
                }
                catch (Exception x)
                {
                    MessageBox.Show("도료의 갯수를 입력하지 않았습니다.", "오류");
                }
            }
            else if (comboBox1.SelectedIndex == 2)
            {
                try
                {
                    cmd.CommandText = $"UPDATE STOCK SET DROGBA= DROGBA + '{textBox2.Text}' WHERE NAME = '흰색'";
                    cmd.ExecuteNonQuery();
                    MessageBox.Show($"흰색 도료 {textBox2.Text}개를 구매하셨습니다.", "알림");
                    cmd.CommandText = "Commit";
                    cmd.ExecuteNonQuery();
                    comboBox1.SelectedIndex = -1;
                    textBox2.Clear();
                    chart();
                }
                catch (Exception x)
                {
                    MessageBox.Show("도료의 갯수를 입력하지 않았습니다.", "오류");
                }
            }

            if (comboBox2.SelectedIndex == 0)
            {
                try
                {
                    cmd.CommandText = $"UPDATE STOCK SET DROGBA = DROGBA + '{textBox3.Text}' WHERE NAME = '면'";
                    cmd.ExecuteNonQuery();
                    MessageBox.Show($"면 옷감 {textBox3.Text}개를 구매하셨습니다.", "알림");
                    cmd.CommandText = "Commit";
                    cmd.ExecuteNonQuery();
                    comboBox2.SelectedIndex = -1;
                    textBox3.Clear();
                    chart();
                }
                catch (Exception x)
                {
                    MessageBox.Show("옷감의 갯수를 입력하지 않았습니다.", "오류");
                }
            }
            else if (comboBox2.SelectedIndex == 1)
            {
                try
                {
                    cmd.CommandText = $"UPDATE STOCK SET DROGBA = DROGBA + '{textBox3.Text}' WHERE NAME = '청'";
                    cmd.ExecuteNonQuery();
                    MessageBox.Show($"청 옷감 {textBox3.Text}개를 구매하셨습니다.", "알림");
                    cmd.CommandText = "Commit";
                    cmd.ExecuteNonQuery();
                    comboBox2.SelectedIndex = -1;
                    textBox3.Clear();
                    chart();
                }
                catch (Exception x)
                {
                    MessageBox.Show("옷감의 갯수를 입력하지 않았습니다.", "오류");
                }
            }
            else if (comboBox2.SelectedIndex == 2)
            {
                try
                {
                    cmd.CommandText = $"UPDATE STOCK SET DROGBA = DROGBA + '{textBox3.Text}' WHERE NAME = '폴리'";
                    cmd.ExecuteNonQuery();
                    MessageBox.Show($"폴리 옷감 {textBox3.Text}개를 구매하셨습니다.", "알림");
                    cmd.CommandText = "Commit";
                    cmd.ExecuteNonQuery();
                    comboBox2.SelectedIndex = -1;
                    textBox3.Clear();
                    chart();
                }
                catch (Exception x)
                {
                    MessageBox.Show("옷감의 갯수를 입력하지 않았습니다.", "오류");
                }
            }
            

            string A = "SELECT * FROM STOCK";

            OracleDataAdapter adapt = new OracleDataAdapter();
            adapt.SelectCommand = new OracleCommand(A, conn);
            DataSet ds = new DataSet();
            adapt.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0].DefaultView;

        }

        private void btnBuy2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel3.Visible = false;
            panel4.Visible = true;
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cth_up_Click(object sender, EventArgs e)
        {
            // 오라클 DB데이터 정보 합산
            cmd.CommandText = "select 수량 from ORDER_ALL WHERE 옷종류 = '상의' AND 사이즈 = 'S' ";
            rdr = cmd.ExecuteReader();
            int num_us = 0;
            while (rdr.Read())
            {
                num_us += Int32.Parse(rdr["수량"].ToString());
            }
            cmd.CommandText = "select 수량 from ORDER_ALL WHERE 옷종류 = '상의' AND 사이즈 = 'M' ";
            rdr = cmd.ExecuteReader();
            int num_um = 0;
            while (rdr.Read())
            {
                num_um += Int32.Parse(rdr["수량"].ToString());
            }
            cmd.CommandText = "select 수량 from ORDER_ALL WHERE 옷종류 = '상의' AND 사이즈 = 'L' ";
            rdr = cmd.ExecuteReader();
            int num_ul = 0;
            while (rdr.Read())
            {
                num_ul += Int32.Parse(rdr["수량"].ToString());
            }
            // 사이즈 그래프 생성
            DrawPieChart_S(chart1, "사이즈별", num_us, num_um, num_ul);
            // 오라클 DB데이터 정보 합산
            cmd.CommandText = "select 수량 from ORDER_ALL WHERE 옷종류 = '상의' AND 색상 = '검정' ";
            rdr = cmd.ExecuteReader();
            int num_ub = 0;
            while (rdr.Read())
            {
                num_ub += Int32.Parse(rdr["수량"].ToString());
            }
            cmd.CommandText = "select 수량 from ORDER_ALL WHERE 옷종류 = '상의' AND 색상 = '빨강' ";
            rdr = cmd.ExecuteReader();
            int num_ur = 0;
            while (rdr.Read())
            {
                num_ur += Int32.Parse(rdr["수량"].ToString());
            }
            cmd.CommandText = "select 수량 from ORDER_ALL WHERE 옷종류 = '상의' AND 색상 = '흰색' ";
            rdr = cmd.ExecuteReader();
            int num_uw = 0;
            while (rdr.Read())
            {
                num_uw += Int32.Parse(rdr["수량"].ToString());
            }
            // 색상 그래프 생성
            DrawPieChart_C(chart3, "색상별", num_ub, num_ur, num_uw);


            // 오라클 DB데이터 정보 합산
            cmd.CommandText = "select 수량 from ORDER_ALL WHERE 옷종류 = '상의' AND 재질 = '면' ";
            rdr = cmd.ExecuteReader();
            int num_uc = 0;
            while (rdr.Read())
            {
                num_uc += Int32.Parse(rdr["수량"].ToString());
            }
            cmd.CommandText = "select 수량 from ORDER_ALL WHERE 옷종류 = '상의' AND 재질 = '청' ";
            rdr = cmd.ExecuteReader();
            int num_uj = 0;
            while (rdr.Read())
            {
                num_uj += Int32.Parse(rdr["수량"].ToString());
            }
            cmd.CommandText = "select 수량 from ORDER_ALL WHERE 옷종류 = '상의' AND 재질 = '폴리' ";
            rdr = cmd.ExecuteReader();
            int num_up = 0;
            while (rdr.Read())
            {
                num_up += Int32.Parse(rdr["수량"].ToString());
            }
            //재질 그래프 생성
            DrawPieChart_M(chart4, "소재별", num_uc, num_uj, num_up);
        }

        private void cth_do_Click(object sender, EventArgs e)
        {
            // 오라클 DB데이터 정보 합산
            cmd.CommandText = "select 수량 from ORDER_ALL WHERE 옷종류 = '하의' AND 사이즈 = 'S' ";
            rdr = cmd.ExecuteReader();
            int num_ds = 0;
            while (rdr.Read())
            {
                num_ds += Int32.Parse(rdr["수량"].ToString());
            }
            cmd.CommandText = "select 수량 from ORDER_ALL WHERE 옷종류 = '하의' AND 사이즈 = 'M' ";
            rdr = cmd.ExecuteReader();
            int num_dm = 0;
            while (rdr.Read())
            {
                num_dm += Int32.Parse(rdr["수량"].ToString());
            }
            cmd.CommandText = "select 수량 from ORDER_ALL WHERE 옷종류 = '하의' AND 사이즈 = 'L' ";
            rdr = cmd.ExecuteReader();
            int num_dl = 0;
            while (rdr.Read())
            {
                num_dl += Int32.Parse(rdr["수량"].ToString());
            }
            // 사이즈 그래프 생성
            DrawPieChart_S(chart1, "사이즈별", num_ds, num_dm, num_dl);

            // 오라클 DB데이터 정보 합산
            cmd.CommandText = "select 수량 from ORDER_ALL WHERE 옷종류 = '하의' AND 색상 = '검정' ";
            rdr = cmd.ExecuteReader();
            int num_db = 0;
            while (rdr.Read())
            {
                num_db += Int32.Parse(rdr["수량"].ToString());
            }
            cmd.CommandText = "select 수량 from ORDER_ALL WHERE 옷종류 = '하의' AND 색상 = '빨강' ";
            rdr = cmd.ExecuteReader();
            int num_dr = 0;
            while (rdr.Read())
            {
                num_dr += Int32.Parse(rdr["수량"].ToString());
            }
            cmd.CommandText = "select 수량 from ORDER_ALL WHERE 옷종류 = '하의' AND 색상 = '흰색' ";
            rdr = cmd.ExecuteReader();
            int num_dw = 0;
            while (rdr.Read())
            {
                num_dw += Int32.Parse(rdr["수량"].ToString());
            }
            // 색상 그래프 생성
            DrawPieChart_C(chart3, "색상별", num_db, num_dr, num_dw);


            // 오라클 DB데이터 정보 합산
            cmd.CommandText = "select 수량 from ORDER_ALL WHERE 옷종류 = '하의' AND 재질 = '면' ";
            rdr = cmd.ExecuteReader();
            int num_dc = 0;
            while (rdr.Read())
            {
                num_dc += Int32.Parse(rdr["수량"].ToString());
            }
            cmd.CommandText = "select 수량 from ORDER_ALL WHERE 옷종류 = '하의' AND 재질 = '청' ";
            rdr = cmd.ExecuteReader();
            int num_dj = 0;
            while (rdr.Read())
            {
                num_dj += Int32.Parse(rdr["수량"].ToString());
            }
            cmd.CommandText = "select 수량 from ORDER_ALL WHERE 옷종류 = '하의' AND 재질 = '폴리' ";
            rdr = cmd.ExecuteReader();
            int num_dp = 0;
            while (rdr.Read())
            {
                num_dp += Int32.Parse(rdr["수량"].ToString());
            }
            // 재질 그래프 생성
            DrawPieChart_M(chart4, "소재별", num_dc, num_dj, num_dp);

        }

        private void cth_out_Click(object sender, EventArgs e)
        {
            // 오라클 DB데이터 정보 합산
            cmd.CommandText = "select 수량 from ORDER_ALL WHERE 옷종류 = '외투' AND 사이즈 = 'S' ";
            rdr = cmd.ExecuteReader();
            int num_os = 0;
            while (rdr.Read())
            {
                num_os += Int32.Parse(rdr["수량"].ToString());
            }
            cmd.CommandText = "select 수량 from ORDER_ALL WHERE 옷종류 = '외투' AND 사이즈 = 'M' ";
            rdr = cmd.ExecuteReader();
            int num_om = 0;
            while (rdr.Read())
            {
                num_om += Int32.Parse(rdr["수량"].ToString());
            }
            cmd.CommandText = "select 수량 from ORDER_ALL WHERE 옷종류 = '외투' AND 사이즈 = 'L' ";
            rdr = cmd.ExecuteReader();
            int num_ol = 0;
            while (rdr.Read())
            {
                num_ol += Int32.Parse(rdr["수량"].ToString());
            }
            // 색상 그래프 생성
            DrawPieChart_S(chart1, "사이즈별", num_os, num_om, num_ol);

            // 오라클 DB데이터 정보 합산
            cmd.CommandText = "select 수량 from ORDER_ALL WHERE 옷종류 = '외투' AND 색상 = '검정' ";
            rdr = cmd.ExecuteReader();
            int num_ob = 0;
            while (rdr.Read())
            {
                num_ob += Int32.Parse(rdr["수량"].ToString());
            }
            cmd.CommandText = "select 수량 from ORDER_ALL WHERE 옷종류 = '외투' AND 색상 = '빨강' ";
            rdr = cmd.ExecuteReader();
            int num_or = 0;
            while (rdr.Read())
            {
                num_or += Int32.Parse(rdr["수량"].ToString());
            }
            cmd.CommandText = "select 수량 from ORDER_ALL WHERE 옷종류 = '외투' AND 색상 = '흰색' ";
            rdr = cmd.ExecuteReader();
            int num_ow = 0;
            while (rdr.Read())
            {
                num_ow += Int32.Parse(rdr["수량"].ToString());
            }
            // 색상 그래프 생성
            DrawPieChart_C(chart3, "색상별", num_ob, num_or, num_ow);


            // 오라클 DB데이터 정보 합산
            cmd.CommandText = "select 수량 from ORDER_ALL WHERE 옷종류 = '외투' AND 재질 = '면' ";
            rdr = cmd.ExecuteReader();
            int num_oc = 0;
            while (rdr.Read())
            {
                num_oc += Int32.Parse(rdr["수량"].ToString());
            }
            cmd.CommandText = "select 수량 from ORDER_ALL WHERE 옷종류 = '외투' AND 재질 = '청' ";
            rdr = cmd.ExecuteReader();
            int num_oj = 0;
            while (rdr.Read())
            {
                num_oj += Int32.Parse(rdr["수량"].ToString());
            }
            cmd.CommandText = "select 수량 from ORDER_ALL WHERE 옷종류 = '외투' AND 재질 = '폴리' ";
            rdr = cmd.ExecuteReader();
            int num_op = 0;
            while (rdr.Read())
            {
                num_op += Int32.Parse(rdr["수량"].ToString());
            }
            // 재질 그래프 생성
            DrawPieChart_M(chart4, "소재별", num_oc, num_oj, num_op);

        }
        //색깔 원 그래프 생성함수
        private void DrawPieChart_C(Chart chart, string title, int value1, int value2, int value3)
        {
            chart.Series.Clear();
            chart.Legends.Clear();
            chart.Legends.Add("NEWLegend");
            chart.Legends[0].LegendStyle = LegendStyle.Table;
            chart.Legends[0].Docking = Docking.Bottom;
            chart.Legends[0].Alignment = StringAlignment.Center;
            chart.Legends[0].Title = title;


            string seriesname = "MySeriesName";
            chart.Series.Add(seriesname);
            chart.Series[seriesname].ChartType = SeriesChartType.Pie;
            chart.Series[seriesname].Label = "#PERCENT{P1}";
            chart.Series[seriesname].Points.AddXY("검정", value1);
            chart.Series[seriesname].Points.AddXY("빨강", value2);
            chart.Series[seriesname].Points.AddXY("하양", value3);
            chart.Series[0].Points[0].LegendText = "검정";
            chart.Series[0].Points[1].LegendText = "빨강";
            chart.Series[0].Points[2].LegendText = "하양";
            chart.Series[0].Points[0].Color = Color.Gray;
            chart.Series[0].Points[1].Color = Color.Red;
            chart.Series[0].Points[2].Color = Color.LightGray;
        }
        //재질 원 그래프 생성함수
        private void DrawPieChart_M(Chart chart, string title, int value1, int value2, int value3)
        {
            chart.Series.Clear();
            chart.Legends.Clear();
            chart.Legends.Add("NEWLegend");
            chart.Legends[0].LegendStyle = LegendStyle.Table;
            chart.Legends[0].Docking = Docking.Bottom;
            chart.Legends[0].Alignment = StringAlignment.Center;
            chart.Legends[0].Title = title;
            string seriesname = "MySeriesName";
            chart.Series.Add(seriesname);
            chart.Series[seriesname].ChartType = SeriesChartType.Pie;
            chart.Series[seriesname].Points.AddXY("면", value1);
            chart.Series[seriesname].Points.AddXY("천", value2);
            chart.Series[seriesname].Points.AddXY("폴리에스테르", value3);
            chart.Series[seriesname].Label = "#PERCENT{P1}";
            chart.Series[0].Points[0].LegendText = "면";
            chart.Series[0].Points[1].LegendText = "청";
            chart.Series[0].Points[2].LegendText = "폴리에스테르";
            chart.Series[0].Points[0].Color = Color.LightGray;
            chart.Series[0].Points[1].Color = Color.Blue;
            chart.Series[0].Points[2].Color = Color.Orange;
        }
        //사이즈 원 그래프 생성함수
        private void DrawPieChart_S(Chart chart, string title, int value1, int value2, int value3)
        {
            chart.Series.Clear();
            chart.Legends.Clear();
            chart.Legends.Add("NEWLegend");
            chart.Legends[0].LegendStyle = LegendStyle.Table;
            chart.Legends[0].Docking = Docking.Bottom;
            chart.Legends[0].Alignment = StringAlignment.Center;
            chart.Legends[0].Title = title;
            string seriesname = "MySeriesName";
            chart.Series.Add(seriesname);
            chart.Series[seriesname].ChartType = SeriesChartType.Pie;
            chart.Series[seriesname].Label = "#PERCENT{P1}";
            chart.Series[seriesname].Points.AddXY("S", value1);
            chart.Series[seriesname].Points.AddXY("M", value2);
            chart.Series[seriesname].Points.AddXY("L", value3);
            chart.Series[0].Points[0].LegendText = "S";
            chart.Series[0].Points[1].LegendText = "M";
            chart.Series[0].Points[2].LegendText = "L";

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {       
            try
            {
                Fac_Start();
            }
            catch
            {
                MessageBox.Show("모든 주문이 완료되었거나 주문이 없습니다.", "알림");
            }
        }

        private void Fac_Start()
        {
            string order_color = string.Empty;
            string order_name = string.Empty;
            string order_size = string.Empty;
            string order_tex = string.Empty;
            
            // 주문 테이블 데이터 읽기
            cmd.CommandText = "SELECT * FROM ORDER_T WHERE ROWNUM = 1";
            cmd.ExecuteNonQuery();
            rdr = cmd.ExecuteReader();
            rdr.Read();
            order_name = rdr["옷종류"] as string;
            order_size = rdr["사이즈"] as string;
            order_color = rdr["색상"] as string;
            order_tex = rdr["재질"] as string;

            // 읽은 데이터에 대한 재고 업데이트
            try
            {
                cmd.CommandText = $"UPDATE STOCK SET DROGBA = (SELECT DROGBA FROM STOCK WHERE NAME = '{order_color}') - (SELECT 필요수량 * 수량 FROM PRODUCT_RH2 P, ORDER_T O WHERE P.옷종류 = '{order_name}' AND P.사이즈 = '{order_size}' AND ROWNUM = 1) WHERE NAME = '{order_color}'";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "COMMIT";
                cmd.ExecuteNonQuery();
                cmd.CommandText = $"UPDATE STOCK SET DROGBA = (SELECT DROGBA FROM STOCK WHERE NAME = '{order_tex}') - (SELECT 필요수량 * 수량 FROM PRODUCT_RH2 P, ORDER_T O WHERE P.옷종류 = '{order_name}' AND P.사이즈 = '{order_size}' AND ROWNUM = 1) WHERE NAME = '{order_tex}'";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "COMMIT";
                cmd.ExecuteNonQuery();

                // 공정 시작 (애니메이션)
                progressBar7.Value = 0;
                int p = 0;
                while (p < 1)
                {
                    pictureBox8.Visible = true;
                    pictureBox9.Visible = true;
                    pictureBox10.Visible = true;
                    pictureBox11.Visible = true;
                    pictureBox12.Visible = true;
                    pictureBox13.Visible = true;
                    p++;
                }
                int i, j;
                i = 381;
                j = 180;
                pictureBox17.Visible = true;
                pictureBox17.Location = new Point(i, j);

                while (i < 620)
                {
                    i += 1;
                    j += 0;
                    Delay(9);
                    pictureBox17.Location = new Point(i, j);
                }
                pictureBox17.Visible = false;
                progressBar7.Value = 10;
                p = 0;
                while (p < 1)
                {
                    pictureBox11.Visible = false;
                    pictureBox5.Visible = false;
                    move.Visible = true;
                    p++;
                    Delay(6000);
                }
                move.Visible = false;
                progressBar7.Value = 30;
                pictureBox5.Visible = true;
                i = 965;
                j = 180;
                pictureBox18.Visible = true;
                pictureBox18.Location = new Point(i, j);

                while (i < 1270)//230
                {
                    i += 1;
                    j += 0;
                    Delay(6);
                    pictureBox18.Location = new Point(i, j);
                }
                while (j < 315)//230
                {
                    j += 1;
                    Delay(6);
                    pictureBox18.Location = new Point(i, j);
                }
                pictureBox18.Visible = false;
                progressBar7.Value = 40;
                p = 0;

                while (p < 1)
                {
                    pictureBox12.Visible = false;
                    pictureBox15.Visible = true;
                    p++;
                    Delay(6000);
                }
                pictureBox15.Visible = false;
                progressBar7.Value = 60;
                pictureBox2.Visible = true;

                i = 1283;
                j = 504;
                pictureBox19.Visible = true;
                pictureBox19.Location = new Point(i, j);

                while (j < 618)
                {
                    j += 1;
                    Delay(7);
                    pictureBox19.Location = new Point(i, j);
                }
                while (i > 971)//230
                {
                    i -= 1;
                    Delay(7);
                    pictureBox19.Location = new Point(i, j);
                }
                pictureBox19.Visible = false;
                progressBar7.Value = 70;
                p = 0;

                while (p < 1)
                {
                    pictureBox13.Visible = false;
                    pictureBox16.Visible = true;
                    p++;
                    Delay(6000);
                }
                pictureBox16.Visible = false;
                progressBar7.Value = 90;
                i = 620;
                j = 618;
                ch.Visible = true;
                ch.Location = new Point(i, j);

                while (i > 381)//230
                {
                    i -= 1;
                    Delay(12);
                    ch.Location = new Point(i, j);
                }
                ch.Visible = false;
                pictureBox8.Visible = false;
                pictureBox9.Visible = false;
                pictureBox10.Visible = false;
                progressBar7.Value = 100;

                // 애니메이션 끝난 후 끝난 주문 삭제
                cmd.CommandText = $"DELETE FROM ORDER_T WHERE ROWNUM = 1";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "COMMIT";
                MessageBox.Show("제작 완료!");

            }
            catch (Exception x)
            {
                MessageBox.Show("재고가 없습니다.");
            }
        }

        // 딜레이 함수
        private static DateTime Delay(int MS)
        {
            DateTime ThisMoment = DateTime.Now;
            TimeSpan duration = new TimeSpan(0, 0, 0, 0, MS);
            DateTime AfterWards = ThisMoment.Add(duration);

            while (AfterWards >= ThisMoment)
            {
                Application.DoEvents();
                ThisMoment = DateTime.Now;
            }
            return DateTime.Now;
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel2_Paint_1(object sender, PaintEventArgs e)
        {

        }

        // 프로그레스 바
        void oraclesearch()
        {
            cmd.CommandText = "select DROGBA from STOCK where NAME = '검정'";
            OracleDataReader rdr1 = cmd.ExecuteReader();

            while (rdr1.Read())
            {  
                int DROGBA = rdr1.GetInt32(0);
                progressBar1.Value = DROGBA;
                if (DROGBA < 2000)
                {
                    ModifyProgressBarColor.SetState(progressBar1, 2);
                }
                else if (DROGBA < 4000)
                {
                    ModifyProgressBarColor.SetState(progressBar1, 3);
                }
                else
                {
                    ModifyProgressBarColor.SetState(progressBar1, 1);
                }
                 
            }

            cmd.CommandText = "select DROGBA from STOCK where NAME = '빨강'";
            OracleDataReader rdr2 = cmd.ExecuteReader();

            while (rdr2.Read())
            {
                int DROGBA = rdr2.GetInt32(0);
                progressBar2.Value = DROGBA;
                if (DROGBA < 2000)
                {
                    ModifyProgressBarColor.SetState(progressBar2, 2);
                }
                else if (DROGBA < 4000)
                {
                    ModifyProgressBarColor.SetState(progressBar2, 3);
                }
                else
                {
                    ModifyProgressBarColor.SetState(progressBar2, 1);
                }

            }
            cmd.CommandText = "select DROGBA from STOCK where NAME = '흰색'";
            OracleDataReader rdr3 = cmd.ExecuteReader();

            while (rdr3.Read())
            {
                int DROGBA = rdr3.GetInt32(0);
                progressBar3.Value = DROGBA;
                if (DROGBA < 2000)
                {
                    ModifyProgressBarColor.SetState(progressBar3, 2);
                }
                else if (DROGBA < 4000)
                {
                    ModifyProgressBarColor.SetState(progressBar3, 3);
                }
                else
                {
                    ModifyProgressBarColor.SetState(progressBar3, 1);
                }

            }
            cmd.CommandText = "select DROGBA from STOCK where NAME = '면'";
            OracleDataReader rdr4 = cmd.ExecuteReader();

            while (rdr4.Read())
            {
                int DROGBA = rdr4.GetInt32(0);
                progressBar4.Value = DROGBA;
                if (DROGBA < 2000)
                {
                    ModifyProgressBarColor.SetState(progressBar4, 2);
                }
                else if (DROGBA < 4000)
                {
                    ModifyProgressBarColor.SetState(progressBar4, 3);
                }
                else
                {
                    ModifyProgressBarColor.SetState(progressBar4, 1);
                }

            }
            cmd.CommandText = "select DROGBA from STOCK where NAME = '청'";
            OracleDataReader rdr5 = cmd.ExecuteReader();

            while (rdr5.Read())
            {
                int DROGBA = rdr5.GetInt32(0);
                progressBar5.Value = DROGBA;
                if (DROGBA < 2000)
                {
                    ModifyProgressBarColor.SetState(progressBar5, 2);
                }
                else if (DROGBA < 4000)
                {
                    ModifyProgressBarColor.SetState(progressBar5, 3);
                }
                else
                {
                    ModifyProgressBarColor.SetState(progressBar5, 1);
                }

            }
            cmd.CommandText = "select DROGBA from STOCK where NAME = '폴리'";
            OracleDataReader rdr6 = cmd.ExecuteReader();

            while (rdr6.Read())
            {
                int DROGBA = rdr6.GetInt32(0);
                progressBar6.Value = DROGBA;
                if (DROGBA < 2000)
                {
                    ModifyProgressBarColor.SetState(progressBar6, 2);
                }
                else if (DROGBA < 4000)
                {
                    ModifyProgressBarColor.SetState(progressBar6, 3);
                }
                else
                {
                    ModifyProgressBarColor.SetState(progressBar6, 1);
                }

            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox17_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox18_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }
    }

    // 프로그레스 바 색상 변환 클래스
    public static class ModifyProgressBarColor
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr w, IntPtr l);
        public static void SetState(this ProgressBar gbBar, int state)
        {
            SendMessage(gbBar.Handle, 1040, (IntPtr)state, IntPtr.Zero);
        }
    }
}
