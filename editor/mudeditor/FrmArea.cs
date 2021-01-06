using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1 {
    public partial class FrmArea : Form {

        public class Room {
            public Panel panel;
            public Label lbl;
            public string name;
            public string desc;
            public int[] exit = new int[6];
            public int id = 1;
            
            public Room(int id) {
                this.id = id;
            }
        }

        int nextID = 1;
        Dictionary<int, Room> rooms = new Dictionary<int, Room>();
        int curDragRoom;
        int curSelRoom;
       
        public FrmArea() {
            InitializeComponent();
            this.graph.MouseDown += new System.Windows.Forms.MouseEventHandler(panelMouseDown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(keyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(keyUp);
            this.over.Paint += new System.EventHandler<System.Windows.Forms.PaintEventArgs>(this.over_paint);
        }

     
        private void Form1_Load(object sender, EventArgs e) {
            over.Owner = this;

        }
        bool shifting;



        private void keyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.LShiftKey || e.KeyCode == Keys.RShiftKey || e.KeyCode == Keys.Shift || e.KeyCode == Keys.ShiftKey) {
                shifting = true;
            } else {
                Console.WriteLine(e.KeyCode);
            }
        }

        private void keyUp(object sender, KeyEventArgs e) {

            if (e.KeyCode == Keys.LShiftKey || e.KeyCode == Keys.RShiftKey || e.KeyCode == Keys.Shift || e.KeyCode == Keys.ShiftKey) {
                shifting = false;
            }
        }

        void addRoom(int x, int y) {
            Panel panel = new Panel();
            panel.Location = new System.Drawing.Point(x - 50, y - 50);
            
            panel.Size = new System.Drawing.Size(100, 100);
            this.graph.Controls.Add(panel);
            Room r = new Room(nextID);
            rooms[nextID] = r;
            r.panel = panel;
            Label lbl = new Label();
            lbl.Location = new System.Drawing.Point(2, 2);
            lbl.BackColor = Color.White;
            lbl.Size = new System.Drawing.Size(96, 96);
            lbl.MouseUp += new System.Windows.Forms.MouseEventHandler(nodeMouseUp);
            lbl.MouseDown += new System.Windows.Forms.MouseEventHandler(nodeMouseDown);
            lbl.MouseMove += new System.Windows.Forms.MouseEventHandler(nodeMouseMove);
            lbl.AccessibleName = nextID + "";
            panel.AccessibleName = nextID + "";
            r.panel.Controls.Add(lbl);

            panel.BorderStyle = BorderStyle.None;
            lbl.BorderStyle = BorderStyle.None;
            panel.BackColor = Color.Black;
            r.lbl = lbl;
            lbl.Text = "" + nextID;
            nextID++;
        }

        bool[] mdown = new bool[2];
        bool linking = false;

        private void panelMouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
            
            if(linking && curSelRoom > 0) {
                rooms[curSelRoom].panel.BackColor = Color.Yellow;
            }
            linking = false;
        }

        int mDownX = 0;
        int mDownY = 0;

        private void nodeMouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
            Label label = (Label)sender;
            int id;
            Room r;
            mDownX = e.X;
            mDownY = e.Y;
            if (e.Button == MouseButtons.Left && !mdown[1]) {
                mdown[0] = true;
                
                if (int.TryParse(label.AccessibleName, out id)) {
                    linking = false;
                    if (curSelRoom > 0) {
                        rooms[curSelRoom].panel.BackColor = Color.Black;
                    }
                    curDragRoom = id;
                    curSelRoom = id;               
                    if (rooms.TryGetValue(id, out r)) {
                        r.panel.BackColor = Color.Yellow;
                    }
                }

            } else if (e.Button == MouseButtons.Right && !mdown[0]) {
                mdown[1] = true;
                if (!linking) {
                    if (int.TryParse(label.AccessibleName, out id)) {
                        if (curSelRoom > 0) {
                            rooms[curSelRoom].panel.BackColor = Color.Black;
                        }
                        curSelRoom = id;
                        if (rooms.TryGetValue(id, out r)) {
                            r.panel.BackColor = Color.Blue;
                        }
                        linking = true;
                    }
                } else {                    
                    if (int.TryParse(label.AccessibleName, out id)) {
                        if (rooms.TryGetValue(id, out r)) {
                            if(id == curSelRoom) {
                                linking = false;
                                rooms[id].panel.BackColor = Color.Yellow;
                            } else {
                                rooms[curSelRoom].panel.BackColor = Color.Yellow;
                                Room source = rooms[curSelRoom];
                                Room dest = r;
                                link(source, dest);
                                linking = false;
                            }
                        }
                    }
                }
            }
        }

        int getExitDir(double angle) {
            if (angle > Math.PI * 0.25 && angle <= Math.PI * .75) {
                return 1;
            } else if (angle > Math.PI * 0.75 || angle <= Math.PI * -.75) {
                return 2;
            } else if (angle > Math.PI * -0.75 && angle <= Math.PI * -.25) {
                return 0;
            } else if (angle > Math.PI * -0.25 && angle <= Math.PI * .25) {
                return 3;
            }
            Console.WriteLine("heyuhoh");
            return 0;
        }

        void link(Room src, Room dest) {
            int sid = src.id;
            int did = dest.id;
            int sx = src.panel.Location.X + 50;
            int sy = src.panel.Location.Y + 50;
            int dx = dest.panel.Location.X + 50;
            int dy = dest.panel.Location.Y + 50;
            for (int i = 0; i < 4; i++) {
                if (src.exit[i] == did) {
                    src.exit[i] = 0;
                }
                if (dest.exit[i] == sid) {
                    dest.exit[i] = 0;
                }
            }
            if (!shifting) {
                //calc our angle
                double a = Math.Atan2(dy - sy, dx - sx);
                int e = getExitDir(a);
                Room other;
                if (rooms.TryGetValue(dest.exit[reverseDir(e)], out other)) {
                    for (int i = 0; i < 4; i++) {
                        if(other.exit[i] == did) {
                            other.exit[i] = 0;
                        }
                    }
                }
                src.exit[e] = did;
                dest.exit[reverseDir(e)] = sid;
            }
            redraw();
            
        }

        void redraw() {
            graph.Invalidate();
            foreach(Control c in Controls) {
                if(c is Label) {
                    c.Invalidate();
                }
            }
            foreach(Room r in rooms.Values) {
                r.lbl.Invalidate();
            }
        }

        int getExitX(int d) {
            switch (d) {
                case 0:
                    return -8;
                case 1:
                    return -8;
                case 2:
                    return -60;
                case 3:
                    return 50;
            }
            Console.WriteLine("uhoh");
            return 5;
        }

        int getExitY(int d) {
            switch (d) {
                case 0:
                    return -60;
                case 1:
                    return 50;
                case 2:
                    return 0;
                case 3:
                    return 0;
            }
            Console.WriteLine("uhoh");
            return 5;
        }

        int reverseDir(int d) {
            switch(d) {
                case 0:
                    return 1;
                case 1:
                    return 0;
                case 2:
                    return 3;
                case 3:
                    return 2;
            }
            Console.WriteLine("uhoh");
            return 5;
        }

        private void nodeMouseUp(object sender, System.Windows.Forms.MouseEventArgs e) {
            Label label = (Label)sender;            
            int id;
            Room r;
            if (e.Button == MouseButtons.Left && !mdown[1]) {
                mdown[0] = false;
                curDragRoom = -1;
                if (int.TryParse(label.AccessibleName, out id)) {
                    if (rooms.TryGetValue(id, out r)) {
                        //r.panel.BackColor = Color.Yellow;
                    }
                }
            } else if (e.Button == MouseButtons.Right && !mdown[0]) {
                mdown[1] = false;
            }
        }

        private void nodeMouseMove(object sender, System.Windows.Forms.MouseEventArgs e) {
            Label label = (Label)sender;
            //curDragRoom = -1;
            int id;
            Room r;
            if (int.TryParse(label.AccessibleName, out id)) {
                if (rooms.TryGetValue(id, out r)) {
                    if (curDragRoom == id) {
                        r.panel.Location = new System.Drawing.Point(e.X - (mDownX-48) + r.panel.Location.X - 50 + 2, e.Y - (mDownY-48) + r.panel.Location.Y - 50 + 2);
                        redraw();
                    }
                }
            }
        }


        void over_paint(object sender, PaintEventArgs e) {
            Graphics g = e.Graphics;

            //Let's make some pens
            Pen pen = new Pen(Color.FromArgb(64,10,10,230), 5);
            System.Drawing.SolidBrush brush = new System.Drawing.SolidBrush(System.Drawing.Color.Red);
            Pen pen2 = new Pen(brush);
            //Pen blackPen = new Pen(Color.Black, 1 / g.DpiX);

            g.PageUnit = GraphicsUnit.Pixel; //convenient for working with form width and height

            //g.DrawLine(pen, 100, 100, 300, 300);
            Room dest;
            foreach(Room src in rooms.Values) {
                for(int i = 0; i < 4; i++) {
                    if(src.exit[i] > 0) {
                        if(rooms.TryGetValue(src.exit[i], out dest)) {
                            g.DrawLine(pen, 20+src.panel.Location.X + 50 + getExitX(i), 20+src.panel.Location.Y + 50 + getExitY(i), 20+dest.panel.Location.X + 50 + getExitX(reverseDir(i)), 20+dest.panel.Location.Y + 50 + getExitY(reverseDir(i)));
                            g.FillRectangle(brush, 20+src.panel.Location.X + 50 + getExitX(i) - 10, 20 + src.panel.Location.Y + 50 + getExitY(i) - 10, 20,20);
                            g.FillRectangle(brush, 20 + dest.panel.Location.X + 50 + getExitX(reverseDir(i)) - 10, 20 + dest.panel.Location.Y + 50 + getExitY(reverseDir(i))- 10,20,20);
                        }
                    }
                }
            }
            //Draw axes (for chopping down trees to make graph paper)
            //g.DrawLine(blackPen, 0, pic.Height / 2, pic.Width, pic.Height / 2);

            //for (int i = 1; i < pic.Width; i++) { //start at 1 because we use the i before to draw the line to current i. didnt seem to matter as Microsoft doesn't seem to mind out of bounds drawing and handles clipping nicely

               // g.DrawLine(bluePen, i - 1, (float)-Math.Sin(GetRelativeX(i - 1, min, max)) * pic.Height / 2 + pic.Height / 2, i, (float)-Math.Sin(GetRelativeX(i, min, max)) * pic.Height / 2 + pic.Height / 2);

           // }

        }

        private void button1_Click_1(object sender, EventArgs e) {
            addRoom(600, 600);
        }

        private void graph_Paint(object sender, PaintEventArgs e) {

        }

    }
}
