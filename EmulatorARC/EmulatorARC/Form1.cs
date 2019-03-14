using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Collections;
namespace EmulatorARC
{

    public partial class Form1 : Form
    {
        public StreamReader f;
        public static int lungy = 0;
        public static string[,] ruri = new string[33, 3];
        public static string[,] arc = new string[100, 100];
        public static int[] arcl = new int[100];
        public static int[] arci = new int[100];
        public static int[] indexuri = new int[100];
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            for (int i=0;i<=31;i++)
            {
                ruri[i, 0] = "%r" + i.ToString();
            }
            ruri[0, 1] = Convert.ToString(0);
            ruri[15, 1] = ruri[0, 1];
            Rescrie();
           
        }

        private void Rescrie()
        {
            richTextBox2.Clear();
            richTextBox4.Clear();
            for (int i = 0; i <= 31; i++)
            {
                richTextBox2.Text = richTextBox2.Text + ruri[i, 0] + ": " + ruri[i, 1] ;
                richTextBox2.Text = richTextBox2.Text + "\n";
            }
            for (int i = 0; i < lungy; i++)
            {
                richTextBox4.Text = richTextBox4.Text + arci[i] + ":  ";
                for (int j = 0; j < arcl[i]; j++)
                {
                    richTextBox4.Text = richTextBox4.Text + arc[i, j]+"        ";
                }
                richTextBox4.Text = richTextBox4.Text + "\n";

            }
        }

        public void Selectare(int i)
        {
            richTextBox3.SelectAll();
            richTextBox3.SelectionBackColor = DefaultBackColor;
            int selectindex = 0;
            for (int q = 0; q < i; q++)
            {
                selectindex = selectindex + indexuri[q];
            }
            richTextBox3.Select(selectindex, indexuri[i]);
            richTextBox3.SelectionBackColor = Color.Red;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                f = File.OpenText(richTextBox1.Text + ".txt");
            }
            catch (Exception q)
            {
                MessageBox.Show(q.Message);
                return;
            }
            Emulare();
        }

        public void Indexare()
        {
            int lung = 0;
            for (int i = 0; i < lungy; i++)
            {
                for (int j = 0; j < arcl[i]; j++)
                {
                    lung = lung + arc[i,j].Length;
                    
                }
                lung = lung + arcl[i];
                lung = lung+1;
                lung = lung + arci[i].ToString().Length;
                indexuri[i] = lung;
                lung = 0;
            }

        }

        public void Scriere()
        {
            for(int i=0;i<lungy;i++)
            {
                richTextBox3.Text = richTextBox3.Text + arci[i]+": ";
                for(int j=0;j<arcl[i];j++)
                {
                    richTextBox3.Text = richTextBox3.Text + " " + arc[i, j];
                }
                richTextBox3.Text = richTextBox3.Text + "\n";
            }
        }

        private void Emulare()
        {
            
            Despartire();
            Enumerare();
           // Scriere();
           // Indexare();

            Incerc();
            

            for (int i = 0; i < lungy; i++)
            {
                Selectare(i);
                
                for (int j = 0; j < arcl[i]; j++)
                {
                   
                    string a = arc[i, j];
                   
                    switch(a)
                    {
                        case "ld":
                            ld(i, j);
                            break;
                        case "addcc":
                            addcc(i, j);
                            break;
                        case "st":
                            st(i, j);
                            break;
                        case "andcc":
                            andcc(ref i, ref j);
                            break;
                        case "orcc":
                            orcc(ref i, ref j);
                            break;
                        case "orncc":
                            orncc(ref i, ref j);
                            break;
                        case "ba":
                            ba(ref i, ref j);
                            break;
                        case "srl":
                            srl(i, j);
                            break;
                        case "sethi":
                            sethi(i, j);
                            break;
                        case "call":
                            call(ref i, ref j);
                            break;
                        case "jmpl":
                            jmpl(ref i, ref j);
                            break;
                        default:break;
                    }
                }
            }
        }

        public void jmpl(ref int i,ref int j)
        {
            if(Convert.ToInt32(ruri[15,1])==0)
            {
                i = lungy - 1;
                j = arcl[i];
            }
            else
            {
                string semn = arc[i, j + 2];
                int nr = Convert.ToInt32(arc[i, j + 3].Trim(','));
                nr = nr / 4;
                if(semn=="-")
                {
                    i = Convert.ToInt32(ruri[15, 1]) - nr;
                    j = -1;
                }
                else
                {
                    i = Convert.ToInt32(ruri[15, 1]) + nr;
                    j = -1;
                }
                ruri[15, 1] = 0.ToString();
            }
        }

        public void call (ref int i,ref int j)
        {
            string caut = arc[i, j + 1];
            ruri[15,1] = i.ToString();
            for(int q=0;q<lungy;q++)
            {
                if(arc[q,0]==caut+":")
                {
                    i = q;
                    j = 0;
                }
            }
        }

        public void sethi(int i,int j)
        {

        }

        public void srl(int i,int j)
        {
            int r1 = Convert.ToInt32(arc[i, j + 1].Trim('%', 'r', ','));
            int r2 = Convert.ToInt32(arc[i, j + 2].Trim('%', 'r', ','));
            int r3 = Convert.ToInt32(arc[i, j + 3].Trim('%', 'r', ','));
            int e1 = Convert.ToInt32(ruri[r1, 1]);
            int e2 = Convert.ToInt32(ruri[r2, 1]);
            int e3 = e1 >> e2;
            ruri[r3, 1] = e3.ToString();
            Rescrie();
        }

        public void Enumerare()
        {
            int c = 0;
            bool found = false;
            for (int i = 0; i < lungy; i++)
            {
                arci[i] = c;
                c = c + 4;
                if (arc[i, 0] == ".org")
                {
                    
                    for (int j = 0; j < lungy; j++)
                    {

                        if (arc[j, 0] == arc[i, 1])
                        {
                            c = Convert.ToInt32(arc[j, 2]);
                            found = true;
                            break;
                        }
                    }
                    if (found == false)
                    {
                        c = Convert.ToInt32(arc[i, 1]);
                    }
                    found = false;
                    
                }

            }
        }

        public void ba(ref int i,ref int j)
        {
            string caut = arc[i, j + 1];
            for(int q=0;q<lungy;q++)
            {
                for (int w = 0; w < arcl[q]; w++)
                {
                    if(arc[q,w]==caut+":")
                    {
                        i = q;
                        j = w;
                        
                    }
                }
            }
        }



        public bool dacaIfand(ref int i,ref int j)
        {
            for (int q = 0; q < arcl[i + 1]; q++)
            {
                string a = arc[i + 1, q];
                switch (a)
                {
                    case "be":
                        beandcc(ref i,ref j);
                        return true;
                    case "bneg":
                        bnegandcc(ref i, ref j);
                        return true;
                        
                    default: break;
                }
            }
            return false;
        }

        public void bnegandcc(ref int i,ref int u)
        {
            for (int j = 0; j < arcl[i]; j++)
            {
                if (arc[i, j] == "andcc")
                {
                    int r1 = Convert.ToInt32(arc[i, j + 1].Trim('%', 'r', ','));
                    int r2 = Convert.ToInt32(arc[i, j + 2].Trim('%', 'r', ','));
                    int r3 = Convert.ToInt32(arc[i, j + 3].Trim('%', 'r', ','));
                    int e1 = Convert.ToInt32(ruri[r1, 1]);
                    int e2 = Convert.ToInt32(ruri[r2, 1]);
                    int e3 = e1 & e2;
                    if (e3 != Convert.ToInt32(ruri[r3, 1]))
                    {
                        string caut = arc[i + 1, 1];
                        for (int q = 0; q < lungy; q++)
                        {
                            for (int y = 0; y < arcl[q]; y++)
                            {
                                if (arc[q, y] == caut + ":")
                                {
                                    i = q;
                                    u = y;
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        i = i + 1;
                        u = -1;
                        break;
                    }

                }
            }
        }    

        public void beandcc(ref int i,ref int u)
        {
            for(int j=0;j<arcl[i];j++)
            {
                if(arc[i,j]=="andcc")
                {
                    int r1 = Convert.ToInt32(arc[i, j + 1].Trim('%', 'r', ','));
                    int r2 = Convert.ToInt32(arc[i, j + 2].Trim('%', 'r', ','));
                    int r3 = Convert.ToInt32(arc[i, j + 3].Trim('%', 'r', ','));
                    int e1 = Convert.ToInt32(ruri[r1, 1]);
                    int e2 = Convert.ToInt32(ruri[r2, 1]);
                    int e3 = e1 & e2;
                    if(e3==Convert.ToInt32( ruri[r3,1]))
                    {
                        string caut = arc[i + 1, 1];
                        for(int q=0;q<lungy;q++)
                        {
                            for(int y=0;y<arcl[q];y++)
                            {
                                if(arc[q,y]==caut+":")
                                {
                                    i = q;
                                    u = y;
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        i = i + 1;
                        u = -1;
                        break;
                    }

                }
            }
        }

        public void andcc(ref int i,ref int j)
        {
            if (dacaIfand(ref i,ref j) == false)
            {
                int r1 = Convert.ToInt32(arc[i, j + 1].Trim('%', 'r', ','));
                int r2 = Convert.ToInt32(arc[i, j + 2].Trim('%', 'r', ','));
                int r3 = Convert.ToInt32(arc[i, j + 3].Trim('%', 'r', ','));
                int e1 = Convert.ToInt32(ruri[r1, 1]);
                int e2 = Convert.ToInt32(ruri[r2, 1]);
                int e3 = e1 & e2;
                ruri[r3, 1] = e3.ToString();
                Rescrie();
            }
        }



        public void orcc(ref int i, ref int j)
        {
            if (dacaIfor(ref i, ref j) == false)
            {
                int r1 = Convert.ToInt32(arc[i, j + 1].Trim('%', 'r', ','));
                int r2 = Convert.ToInt32(arc[i, j + 2].Trim('%', 'r', ','));
                int r3 = Convert.ToInt32(arc[i, j + 3].Trim('%', 'r', ','));
                int e1 = Convert.ToInt32(ruri[r1, 1]);
                int e2 = Convert.ToInt32(ruri[r2, 1]);
                int e3 = e1 | e2;
                ruri[r3, 1] = e3.ToString();
                Rescrie();
            }
        }

        public bool dacaIfor(ref int i, ref int j)
        {
            for (int q = 0; q < arcl[i + 1]; q++)
            {
                string a = arc[i + 1, q];
                switch (a)
                {
                    case "be":
                        beorcc(ref i, ref j);
                        return true;
                    case "bneg":
                        bnegorcc(ref i, ref j);
                        return true;

                    default: break;
                }
            }
            return false;
        }

        public void beorcc(ref int i, ref int u)
        {
            for (int j = 0; j < arcl[i]; j++)
            {
                if (arc[i, j] == "orcc")
                {
                    int r1 = Convert.ToInt32(arc[i, j + 1].Trim('%', 'r', ','));
                    int r2 = Convert.ToInt32(arc[i, j + 2].Trim('%', 'r', ','));
                    int r3 = Convert.ToInt32(arc[i, j + 3].Trim('%', 'r', ','));
                    int e1 = Convert.ToInt32(ruri[r1, 1]);
                    int e2 = Convert.ToInt32(ruri[r2, 1]);
                    int e3 = e1 | e2;
                    if (e3 == Convert.ToInt32(ruri[r3, 1]))
                    {
                        string caut = arc[i + 1, 1];
                        for (int q = 0; q < lungy; q++)
                        {
                            for (int y = 0; y < arcl[q]; y++)
                            {
                                if (arc[q, y] == caut + ":")
                                {
                                    i = q;
                                    u = y;
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        i = i + 1;
                        u = -1;
                        break;
                    }

                }
            }
        }

        public void bnegorcc(ref int i, ref int u)
        {
            for (int j = 0; j < arcl[i]; j++)
            {
                if (arc[i, j] == "orcc")
                {
                    int r1 = Convert.ToInt32(arc[i, j + 1].Trim('%', 'r', ','));
                    int r2 = Convert.ToInt32(arc[i, j + 2].Trim('%', 'r', ','));
                    int r3 = Convert.ToInt32(arc[i, j + 3].Trim('%', 'r', ','));
                    int e1 = Convert.ToInt32(ruri[r1, 1]);
                    int e2 = Convert.ToInt32(ruri[r2, 1]);
                    int e3 = e1 | e2;
                    if (e3 != Convert.ToInt32(ruri[r3, 1]))
                    {
                        string caut = arc[i + 1, 1];
                        for (int q = 0; q < lungy; q++)
                        {
                            for (int y = 0; y < arcl[q]; y++)
                            {
                                if (arc[q, y] == caut + ":")
                                {
                                    i = q;
                                    u = y;
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        i = i + 1;
                        u = -1;
                        break;
                    }

                }
            }
        }



        public void orncc(ref int i, ref int j)
        {
            if (dacaIfnor(ref i, ref j) == false)
            {
                int r1 = Convert.ToInt32(arc[i, j + 1].Trim('%', 'r', ','));
                int r2 = Convert.ToInt32(arc[i, j + 2].Trim('%', 'r', ','));
                int r3 = Convert.ToInt32(arc[i, j + 3].Trim('%', 'r', ','));
                int e1 = Convert.ToInt32(ruri[r1, 1]);
                int e2 = Convert.ToInt32(ruri[r2, 1]);
                int e3 = e1 |~ e2;
                ruri[r3, 1] = e3.ToString();
                Rescrie();
            }
        }

        public bool dacaIfnor(ref int i, ref int j)
        {
            for (int q = 0; q < arcl[i + 1]; q++)
            {
                string a = arc[i + 1, q];
                switch (a)
                {
                    case "be":
                        beorncc(ref i, ref j);
                        return true;
                    case "bneg":
                        bnegorncc(ref i, ref j);
                        return true;

                    default: break;
                }
            }
            return false;
        }

        public void beorncc(ref int i, ref int u)
        {
            for (int j = 0; j < arcl[i]; j++)
            {
                if (arc[i, j] == "orncc")
                {
                    int r1 = Convert.ToInt32(arc[i, j + 1].Trim('%', 'r', ','));
                    int r2 = Convert.ToInt32(arc[i, j + 2].Trim('%', 'r', ','));
                    int r3 = Convert.ToInt32(arc[i, j + 3].Trim('%', 'r', ','));
                    int e1 = Convert.ToInt32(ruri[r1, 1]);
                    int e2 = Convert.ToInt32(ruri[r2, 1]);
                    int e3 = e1 |~ e2;
                    if (e3 == Convert.ToInt32(ruri[r3, 1]))
                    {
                        string caut = arc[i + 1, 1];
                        for (int q = 0; q < lungy; q++)
                        {
                            for (int y = 0; y < arcl[q]; y++)
                            {
                                if (arc[q, y] == caut + ":")
                                {
                                    i = q;
                                    u = y;
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        i = i + 1;
                        u = -1;
                        break;
                    }

                }
            }
        }

        public void bnegorncc(ref int i, ref int u)
        {
            for (int j = 0; j < arcl[i]; j++)
            {
                if (arc[i, j] == "orncc")
                {
                    int r1 = Convert.ToInt32(arc[i, j + 1].Trim('%', 'r', ','));
                    int r2 = Convert.ToInt32(arc[i, j + 2].Trim('%', 'r', ','));
                    int r3 = Convert.ToInt32(arc[i, j + 3].Trim('%', 'r', ','));
                    int e1 = Convert.ToInt32(ruri[r1, 1]);
                    int e2 = Convert.ToInt32(ruri[r2, 1]);
                    int e3 = e1 |~ e2;
                    if (e3 != Convert.ToInt32(ruri[r3, 1]))
                    {
                        string caut = arc[i + 1, 1];
                        for (int q = 0; q < lungy; q++)
                        {
                            for (int y = 0; y < arcl[q]; y++)
                            {
                                if (arc[q, y] == caut + ":")
                                {
                                    i = q;
                                    u = y;
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        i = i + 1;
                        u = -1;
                        break;
                    }

                }
            }
        }



        public void st(int i,int j)
        {
            int r = Convert.ToInt32(arc[i, j + 1].Trim('%', ',', 'r'));
            r = Convert.ToInt32( ruri[r, 1]);
            string z = arc[i, j + 2].Trim('[', ']');
            for (int a = 0; a < lungy; a++)
            {
                if (z + ":" == arc[a, 0])
                {
                    arc[a, 1] = r.ToString();
                }
            }
            Rescrie();
        }

        public  void ld(int i,int j)
        {
            j = j + 1;
            bool var = false;
            string h = arc[i, j].Trim('[',']',',');
            for (int k = 0; k < lungy; k++)
            {
                if (arc[k, 0] == h + ":")
                {
                    for (int t = 0; t<32; t++)
                    {
                        if(ruri[t,0]==arc[i,j+1])
                        {
                            ruri[t, 1] = arc[k, 1];
                            var = true;
                        }
                    }
                }
            }
            if (var == false)
            {
                for(int u=0;u<32;u++)
                {
                    if(ruri[u,0]==h)
                    {
                        int lat = Convert.ToInt32(ruri[u, 1]);
                        for(int y=0;y<lungy; y++)
                        {
                            if (lat == arci[y])
                            {
                                string z = arc[y, 0];
                                if (Char.IsLetter(z[0]))
                                {
                                    int yu = Convert.ToInt32(arc[i, j + 1].Trim(',', '%', 'r'));
                                    ruri[yu, 1] = arc[y, 1];
                                }
                                else
                                {
                                    int yu = Convert.ToInt32(arc[i, j + 1].Trim(',', '%', 'r'));
                                    ruri[yu, 1] = arc[y, 0];
                                }
                                
                            }
                        }
                    }
                }
            }
            DefinVar();
            Rescrie();
        }

        public void DefinVar()
        {
            for(int i=0;i<32;i++)
            {
                string var = ruri[i, 1];
                bool dd = true;
                if (!String.IsNullOrEmpty(var))
                {
                    for (int j = 0; j < var.Length; j++)
                    {
                        
                        if (Char.IsLetter(var[j]))
                        {
                            dd = false;
                            break;
                        }
                    }
                }
                if(dd==false)
                {
                    for (int q = 0; q < lungy; q++)
                    {
                        if(arc[q,0]==var)
                        {
                            if (arc[q, 1] != ".equ")
                            {
                                ruri[i, 1] = arc[q, 1];
                                DefinVar();
                            }
                            else
                            {
                                ruri[i, 1] = arc[q, 2];
                                DefinVar();
                            }
                        }
                    }

                }
            }
        }

        public void addcc(int i,int j)
        {
            bool var = false;
            string r1 = arc[i, j + 1].Trim(',');
            int r11=0;
            string r2 = arc[i, j + 2].Trim(',');
            int r22=0;
            string r3 = arc[i, j + 3].Trim(',');
            int r33=0;
            for(int y=0;y<r2.Length;y++)
            {
                if(r2[y]=='%')
                {
                    var = true;
                }
            }
            if (var == true)
            {
                for (int k = 0; k < 32; k++)
                {
                    if (ruri[k, 0] == r1)
                    {
                        r11 = Convert.ToInt32(ruri[k, 1]);
                    }
                    if (ruri[k, 0] == r2)
                    {
                        r22 = Convert.ToInt32(ruri[k, 1]);
                    }
                }
                r33 = r11 + r22;
                r3 = r3.Trim('%', ',', 'r');
                ruri[Convert.ToInt32(r3), 1] = r33.ToString();
            }
            else
            {
                for (int k = 0; k < 32; k++)
                {
                    if (ruri[k, 0] == r1)
                    {
                        r11 = Convert.ToInt32(ruri[k, 1]);
                    }
                }
                r22 = Convert.ToInt32(r2);
                r33 = r11 + r22;
                r3 = r3.Trim('%', ',', 'r');
                ruri[Convert.ToInt32(r3), 1] = r33.ToString();
            }
            Rescrie();
        }

        private void Despartire()
        {
            
            string g;
            while ((g=f.ReadLine()) != null)
            {
                string q = g.Trim(',');
                string[] z = q.Split(' ');
                for(int j=0;j<z.Length;j++)
                {
                    arc[lungy, j] = z[j];
                    
                }
                arcl[lungy] = z.Length;
                lungy++;
            }
            
        }

        private void Incerc()
        {
            for(int i=0;i<lungy;i++)
            {
                richTextBox4.Text = richTextBox4.Text + arci[i] + "  ";
                for (int j=0;j<arcl[i];j++)
                {
                    richTextBox4.Text = richTextBox4.Text  + arc[i, j]+"     ";
                    
                }
                richTextBox4.Text = richTextBox4.Text + "\n";
                
            }
           

        }
    }
}



