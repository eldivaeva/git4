asdasdusing System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OracleClient;
using System.Data.OleDb;
using System.Data.Odbc;

namespace design
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /*public static int num_of_conc = 0;
        public static double[,] w;
        public static double[,] R;
        public static double[,] v;
        public static double[,] v_;*/

        public static int a = 30;
        public static int b = 30;
        int num = 0;
        int dx = 0, dy = 0;
        bool stop = false;
        class Concept
        {
            public int x, y;
            public int number;
            public Concept(int xx, int yy)
            {
                x = xx;
                y = yy;
            }
            public int a = 30;
            public int b = 30;

            public void draw(Graphics g)
            {
                Pen blackPen = new Pen(Color.Black, 2);
                Pen pen2 = new Pen(Color.Black, 1);
                SolidBrush br = new SolidBrush(Color.Black);
                Font f = new Font(FontFamily.GenericSansSerif, 12.0F, FontStyle.Bold);
                pen2.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                if (selected)
                    g.DrawRectangle(pen2, x - a / 2, y - b / 2, a, b);
                g.DrawEllipse(blackPen, x - a / 2, y - b / 2, a, b);
                g.DrawString(number.ToString(), f, br, x - 3 * b / 7, y - b / 4);

            }
            public bool selected = false;
        }

        class Arrow
        {
            public Concept start, end;
            public int x1, x2, y1, y2;
            public void _Arrow()
            {
                x1 = start.x;
                y1 = start.y;
                x2 = end.x;
                y2 = end.y;
            }
            public bool pos;
            public void draw(Graphics g)
            {
                _Arrow();
                Pen Pen_ = new Pen(Color.Black, 2);
                Pen Pen1 = new Pen(Color.Red, 2);
                Pen Pen2 = new Pen(Color.Green, 2);
                System.Drawing.Drawing2D.AdjustableArrowCap arr = new System.Drawing.Drawing2D.AdjustableArrowCap(6, 6);
                Pen1.CustomEndCap = arr;
                Pen2.CustomEndCap = arr;
                if (pos)
                    Pen_ = Pen2;
                else
                    Pen_ = Pen1;
                if (x2 > x1)
                    g.DrawLine(Pen_, x1 + a / 2, y1, x2 - a / 2, y2);
                if (x2 <= x1)
                    g.DrawLine(Pen_, x1 - a / 2, y1, x2 + a / 2, y2);
            }

        }
        List<Concept> concepts = new List<Concept>();
        List<Arrow> arrows = new List<Arrow>();
        private void Form1_Load(object sender, EventArgs e)
        {
            OleDbConnection conn = new OleDbConnection();
            conn.ConnectionString = "Provider=MSDAORA; SERVICE_NAME=XE; user ID = elvira; password = elvira123";
        }

        private void navBarItem1_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            tabPane1.SelectedPage = tabNavigationPage1;
        }

        private void navBarItem2_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            tabPane1.SelectedPage = tabNavigationPage3;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //listBoxControl1.Items.Add("dsfsdf");
            addThreat f3 = new addThreat();
            f3.Owner = this;
            f3.Show();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            dataGridView2.Rows.Remove(dataGridView2.SelectedRows[0]);
        }

        private void navBarItem3_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            tabPane1.SelectedPage = tabNavigationPage2;
        }

        private void navBarItem4_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            tabPane1.SelectedPage = tabNavigationPage4;
        }

        private void navBarItem5_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            tabPane1.SelectedPage = tabNavigationPage5;
        }

        private void navBarItem6_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            tabPane1.SelectedPage = tabNavigationPage7;
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            addResource f2 = new addResource();
            f2.Owner = this;
            f2.Show();
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            Form4 f4 = new Form4();
            f4.Owner = this;
            f4.Show();
        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex!= -1)
            {
                listBox2.Items.Add(listBox1.SelectedItem);
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);
            }
        }

        private void simpleButton8_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex != -1)
            {
                listBox1.Items.Add(listBox2.SelectedItem);
                listBox2.Items.RemoveAt(listBox2.SelectedIndex);
            }
        }

        private void simpleButton9_Click(object sender, EventArgs e)
        {
            Globals.MyGlobals.num_of_conc = listBox2.Items.Count;
            for (int i = 0; i < Globals.MyGlobals.num_of_conc; i++)
            {
                dataGridView1.Columns.Add((i + 1).ToString(), (i + 1).ToString());
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].HeaderCell.Value = (i + 1).ToString();
            }

        }

        public void createR(double[,] w, double[,] R)
        {
            int N = Globals.MyGlobals.num_of_conc;
            for (int i = 0; i < (2 * N); i++)
                for (int j = 0; j < (2 * N); j++)
                    R[i, j] = 0;
            for (int i = 0; i < N; i++)
                for (int j = 0; j < N; j++)
                {
                    if (w[i, j] > 0)
                    {
                        R[2 * i, 2 * j] = w[i, j];
                        R[2 * i + 1, 2 * j + 1] = w[i, j];
                    }
                    if (w[i, j] < 0)
                    {
                        R[2 * i, 2 * j + 1] = (-w[i, j]);
                        R[2 * i + 1, 2 * j] = (-w[i, j]);
                    }
                }
        }

        public double[,] zamykanie(double[,] A, double[,] B)
        {
            int N = Globals.MyGlobals.num_of_conc;
            for (int x = 0; x < N; x++)
            {
                for (int z = 0; z < N; z++)
                {
                    double max = 0;
                    double tmp;
                    for (int y = 0; y < N; y++)
                    {
                        tmp = Math.Min(B[x, y], B[y, z]);
                        if (tmp > max)
                            max = tmp;
                    }
                    B[x, z] = max;
                }
            }
            return B;
        }
        public double[,] tranz(double[,] A, double[,] B, int N)
        {
            double[,] C = new double[N, N];
            for (int i = 0; i < N; i++)
                for (int j = 0; j < N; j++)
                    C[i, j] = A[i, j];
            do
            {
                B = zamykanie(B, A);
                for (int i = 0; i < N; i++)
                    for (int j = 0; j < N; j++)
                        C[i, j] = Math.Max(C[i, j], B[i, j]);
            } while (B != A);
            return C;
        }

        private void barSubItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int N = Globals.MyGlobals.num_of_conc;
            int n = 2 * N;
            Globals.MyGlobals.R = new double[n, n];
            Globals.MyGlobals.v = new double[N, N];
            Globals.MyGlobals.v_ = new double[N, N];
            createR(Globals.MyGlobals.w, Globals.MyGlobals.R);
            Globals.MyGlobals.R = tranz(Globals.MyGlobals.R, Globals.MyGlobals.R, n);
            for (int i = 0; i < N; i++)
                for (int j = 0; j < N; j++)
                {
                    Globals.MyGlobals.v[i, j] = Math.Max(Globals.MyGlobals.R[2 * i, 2 * j], Globals.MyGlobals.R[2 * i + 1, 2 * j + 1]);
                    Globals.MyGlobals.v_[i, j] = Math.Max(Globals.MyGlobals.R[2 * i, 2 * j + 1], Globals.MyGlobals.R[2 * i + 1, 2 * j]);
                    dataGridView1.Rows[i].Cells[j].Value = Globals.MyGlobals.v[i, j];
                }
            MessageBox.Show("Расчеты проведены успешно!");
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Indicators f5 = new Indicators();
            f5.Owner = this;
            f5.Show();
        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows != null)
                simpleButton2.Enabled = true;
        }

        private void simpleButton2_Click_1(object sender, EventArgs e)
        {

        }

        private void simpleButton10_Click(object sender, EventArgs e)
        {
            bool fl = false;
            int x1 = 30; int y1 = 30;
            int d = 30;
            int r = 50;
            int N = Globals.MyGlobals.num_of_conc;
            Globals.MyGlobals.w = new double[N, N];
            for (int i = 0; i < N; i++)
                for (int j = 0; j < N; j++)
                    Globals.MyGlobals.w[i, j] = System.Convert.ToDouble(dataGridView1.Rows[i].Cells[j].Value);
            int length = pictureBox1.Width;
            double l = (length - 2 * x1 + r) / (d + r);
            int N1 = (int)l;
            for (int i = 0; i < N; i++)
            {
                int x = x1 + (i % N1) * (r + d);
                int y = y1 + (i / N1) * (r + d);
                Concept c1 = new Concept(x, y);
                c1.number = ++num;
                concepts.Add(c1);
            }
            for (int i=0; i<N; i++)
                for (int j = 0; j < N; j++)
                {
                    if (Math.Abs(Globals.MyGlobals.w[i, j]) > 1)
                        fl = true;
                }
            if (!fl)
                for (int i=0; i<N; i++)
                    for (int j = 0; j < N; j++)
                    {
                        if ((Globals.MyGlobals.w[i, j] != null)&&(Globals.MyGlobals.w[i, j] != 0))
                        {
                            Arrow a1 = new Arrow();
                            a1.start = concepts[i];
                            a1.end = concepts[j];
                            if (Globals.MyGlobals.w[i, j] > 0)
                                a1.pos = true;
                            else
                                a1.pos = false;
                            a1._Arrow();
                            arrows.Add(a1);
                        }
                    }
            if (fl)
                MessageBox.Show("Значения должны лежать в пределах от -1 до 1", "Ошибка!");
            pictureBox1.Refresh();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            foreach (Concept con in concepts)
            {
                con.draw(e.Graphics);
            }
            foreach (Arrow ar in arrows)
            {
                if (ar.start != null && ar.end != null)
                    ar.draw(e.Graphics);
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            stop = false;
            foreach (Concept con in concepts)
            {
                if (con.selected)
                {
                    con.x = e.X - dx;
                    con.y = e.Y - dy;
                    stop = true;
                }
            }
            Refresh();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            bool flag = false;
            foreach (Concept con in concepts)
            {
                if (4 * (e.X - con.x) * (e.X - con.x) / (con.a * con.a) + 4 * (e.Y - con.y) * (e.Y - con.y) / (con.b * con.b) <= 1)
                {
                    dx = e.X - con.x;
                    dy = e.Y - con.y;
                    if ((con.selected) && (dx == 0) && (dy == 0))
                        con.selected = false;
                    else
                    {
                        foreach (Concept con2 in concepts)
                            con2.selected = false;
                        con.selected = true;
                    }
                    flag = true;
                }
            }
            if (!flag)
                foreach (Concept con2 in concepts)
                    con2.selected = false;
            Refresh();
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            RiskEval f6 = new RiskEval();
            f6.Owner = this;
            f6.Show();
        }

        private void dataGridView3_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView3.SelectedRows != null)
                simpleButton5.Enabled = true;
        }

        private void dataGridView4_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView4.SelectedRows != null)
                simpleButton3.Enabled = true;
        }

        private void barSubItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            MessageBox.Show("Программный продукт для оценки финормационных рисков с помощью нечетких когнитивных карт. Эльвира Диваева. 2016", "О программе");
        }
    }
}
