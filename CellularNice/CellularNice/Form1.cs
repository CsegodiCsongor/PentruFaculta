using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CellularNice
{
    public partial class Form1 : Form
    {
        public int[] rules;
        public Color[] colors;

        public int n = 200;
        public int size;
        public int penWidth = 1;

        public int[,] cel;

        public Bitmap bmp;
        public Graphics grp;

        public Pen gridPen;

        public CellAlg cellAlg;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            gridPen = new Pen(Color.Red, penWidth);

            rules = new int[] { 2, 3, 5, 7 };
            colors = new Color[] { Color.Black, Color.Blue, Color.Orange, Color.Red };

            DrawInittialGrid();
            cellAlg = new CellAlg();
            cel = new int[n, n];

            cel[n / 2, n / 2] = 2;
            cel[n / 2 - 1, n / 2 - 1] = 2;
            cel[n / 2 - 1, n / 2] = 2;
            cel[n / 2, n / 2 - 1] = 2;
            DrawCell();

            for (int i = 0; i < colors.Length; i++)
            {
                comboBox1.Items.Add(colors[i]);
            }
            comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cel = cellAlg.RunAlg(cel);
            DrawCell();
        }

        private void DrawInittialGrid()
        {
            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            grp = Graphics.FromImage(bmp);

            size = pictureBox1.Width / n;

            for (int i = 0; i < n - 1; i++)
            {
                grp.DrawLine(gridPen, (i + 1) * size, 0, (i + 1) * size, pictureBox1.Height);
                grp.DrawLine(gridPen, 0, (i + 1) * size, pictureBox1.Width, (i + 1) * size);
            }

            pictureBox1.Image = bmp;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Point clicked = new Point(((MouseEventArgs)e).X, ((MouseEventArgs)e).Y);

            clicked = new Point(clicked.X / size, clicked.Y / size);
            grp.FillRectangle(new SolidBrush(colors[comboBox1.SelectedIndex]), clicked.X * size, clicked.Y * size, size, size);
            pictureBox1.Image = bmp;
            //MessageBox.Show(clicked.Y + "    " + clicked.X);
            cel[clicked.Y, clicked.X] = rules[comboBox1.SelectedIndex];
        }

        private void DrawCell()
        {
            DrawInittialGrid();

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    for (int q = 0; q < rules.Length; q++)
                    {
                        if (cel[i, j] != 0 && cel[i, j] % rules[q] == 0)
                        {
                            grp.FillRectangle(new SolidBrush(colors[q]), j * size, i * size, size + penWidth / 2, size + penWidth / 2);
                            //break;
                        }
                    }
                }
            }

            pictureBox1.Image = bmp;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cel = new int[n, n];
            DrawInittialGrid();
            timer1.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            timer1.Enabled = !timer1.Enabled;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            cel = cellAlg.RunAlg(cel);
            DrawCell();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
