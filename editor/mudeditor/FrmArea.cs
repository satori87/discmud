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
            public string displayName;
            public string desc;
            public int[] exit = new int[6];
            public int id = 1;
            public int height = 0;

            public Room(int id) {
                this.id = id;
            }
        }

        int nextID = 1;
        Dictionary<int, Room> rooms = new Dictionary<int, Room>();
        int curDragRoom;
        int curSelRoom;
        bool[] down = new bool[256];
        int sx = 0;
        int sy = 0;

        public FrmArea() {
            InitializeComponent();
            this.graph.MouseDown += new System.Windows.Forms.MouseEventHandler(panelMouseDown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(keyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(keyUp);
            graph.Click += new System.EventHandler(fixFocus);
            this.over.Paint += new System.EventHandler<System.Windows.Forms.PaintEventArgs>(this.over_paint);
            lblName.KeyDown += new System.Windows.Forms.KeyEventHandler(keyDown);
            lblName.KeyUp += new System.Windows.Forms.KeyEventHandler(keyUp);

        }


        private void Form1_Load(object sender, EventArgs e) {
            over.Owner = this;
            foreach (Control c in Controls) {
                //if(c is TextBox || c is Button) {
                c.KeyDown += new System.Windows.Forms.KeyEventHandler(this.keyDown);
                c.KeyUp += new System.Windows.Forms.KeyEventHandler(this.keyUp);
                
                //}
            }
            fixFocus(sender, e);

        }
        bool shifting;

        void fixFocus(object sender, EventArgs e) {
            this.lbl.Focus();
        }

        int curHeight = 0;

        void scroll(int bx, int by) {
            sx += bx;
            sy += by;
            foreach (Room r in rooms.Values) {
                r.panel.Location = new Point(r.panel.Location.X + bx, r.panel.Location.Y + by);
            }
            redraw();
        }

        void desel() {
            curSelRoom = 0;
            roomPanel.Visible = false;
            btnDelete.Enabled = false;
            foreach (Room r in rooms.Values) {
                r.panel.BackColor = Color.Black;
            }
        }


        void clearExits(Room src) {
            int sid = src.id;
            int did;
            Room dest;
            for(int i = 0; i < 6; i++) {
                if(src.exit[i] > 0) {
                    did = src.exit[i];
                    dest = rooms[did];
                    src.exit[i] = 0;
                    dest.exit[reverseDir(i)] = 0;
                }
            }
            redraw();
        }

        void heightUp() {
            curHeight++;
            lblHeight.Text = curHeight+"";
            if (curDragRoom > 0) {
                rooms[curDragRoom].height = curHeight;
                clearExits(rooms[curDragRoom]);
            } else if(!linking) {
                desel();
            }
            foreach (Room r in rooms.Values) {                
                if (r.height == curHeight) {
                    r.panel.Visible = true;
                } else {
                    r.panel.Visible = false;
                }
            }
            redraw();
        }

        void heightDown() {
            curHeight--;
            lblHeight.Text = curHeight+"";
            if (curDragRoom > 0) {
                rooms[curDragRoom].height = curHeight;
                clearExits(rooms[curDragRoom]);
            } else if (!linking) {
                desel();
            }
            foreach (Room r in rooms.Values) {                
                if (r.height == curHeight) {
                    r.panel.Visible = true;
                } else {
                    r.panel.Visible = false;
                }
            }
            redraw();
        }

        private void keyDown(object sender, KeyEventArgs e) {
            down[e.KeyValue] = true;
            if (e.KeyCode == Keys.LShiftKey || e.KeyCode == Keys.RShiftKey || e.KeyCode == Keys.Shift || e.KeyCode == Keys.ShiftKey) {
                shifting = true;
            } else if (e.KeyCode == Keys.Delete) {
                deleteRoom();
            } else if (e.KeyCode == Keys.Add || e.KeyCode == Keys.Oemplus) {
                heightUp();
            } else if (e.KeyCode == Keys.Subtract || e.KeyCode == Keys.OemMinus) {
                heightDown();
            } else if (e.KeyCode == Keys.N) {
                addRoom();
            } else if (e.KeyCode == Keys.L) {
                if(curSelRoom > 0) {
                    roomLink();
                }
            } else if (e.KeyCode == Keys.A) {
                autoLink();
            }
            if (e.KeyCode == Keys.W) {
                scroll(0, -10);
            } else if (e.KeyCode == Keys.S) {
                scroll(0, 10);
            }
            if (e.KeyCode == Keys.A) {
                scroll(-10, 0);
            } else if (e.KeyCode == Keys.D) {
                scroll(10, 0);
            }
        }

        private void keyUp(object sender, KeyEventArgs e) {
            down[e.KeyValue] = false;
            if (e.KeyCode == Keys.LShiftKey || e.KeyCode == Keys.RShiftKey || e.KeyCode == Keys.Shift || e.KeyCode == Keys.ShiftKey) {
                shifting = false;
            }
        }

        private void button1_Click_1(object sender, EventArgs e) {
            addRoom();
            fixFocus(sender, e);
        }

        void selRoom(int id) {
            curSelRoom = id;
            roomPanel.Visible = true;
            btnDelete.Enabled = true;
            Room room;
            if (rooms.TryGetValue(id, out room)) {
                txtDisplayName.Text = room.displayName;
                txtDescription.Text = room.desc;
                txtName.Text = room.name;
            }
        }

        void updateRoom() {
            if (curSelRoom > 0) {
                Room room;
                if (rooms.TryGetValue(curSelRoom, out room)) {
                    room.displayName = txtDisplayName.Text;
                    room.desc = txtDescription.Text;
                    room.name = txtName.Text;
                    room.lbl.Text = txtName.Text;
                }
            }
        }

        void addRoom() {            
            foreach (Room room in rooms.Values) {
                room.panel.BackColor = Color.Black;
            }
            Panel panel = new Panel();
            panel.Location = new System.Drawing.Point(graph.Size.Width/2 - 50, graph.Size.Width/2 - 50);
            panel.Size = new System.Drawing.Size(100, 100);
            this.graph.Controls.Add(panel);
            Room r = new Room(nextID);
            rooms[nextID] = r;
            selRoom(nextID);
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
            panel.BackColor = Color.Yellow;
            r.lbl = lbl;
            r.height = curHeight;
            lbl.Text = "" + nextID;
            panel.BringToFront();
            lbl.Click += new System.EventHandler(fixFocus);
            panel.Click += new System.EventHandler(fixFocus);
            nextID++;
        }

        bool[] mdown = new bool[2];
        bool linking = false;

        private void panelMouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {

            if (linking && curSelRoom > 0) {
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
                    selRoom(id);
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
                        selRoom(id);
                        if (rooms.TryGetValue(id, out r)) {
                            r.panel.BackColor = Color.Blue;
                        }
                        linking = true;
                    }
                } else {
                    if (int.TryParse(label.AccessibleName, out id)) {
                        if (rooms.TryGetValue(id, out r)) {
                            if (id == curSelRoom) {
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
            if (angle > Math.PI * 0.35 && angle <= Math.PI * .65) {
                Console.WriteLine("1");
                return 1;
            } else if (angle > Math.PI * 0.85 || angle <= Math.PI * -.85) {
                Console.WriteLine("2"); return 2;
            } else if (angle > Math.PI * -0.65 && angle <= Math.PI * -.35) {
                Console.WriteLine("0"); return 0;
            } else if (angle > Math.PI * -0.15 && angle <= Math.PI * .15) {
                Console.WriteLine("3"); return 3;
            }
            return -1;
        }

        void link(Room src, Room dest) {
            int sid = src.id;
            int did = dest.id;
            int sx = src.panel.Location.X + 50;
            int sy = src.panel.Location.Y + 50;
            int dx = dest.panel.Location.X + 50;
            int dy = dest.panel.Location.Y + 50;
            for (int i = 0; i < 6; i++) {
                if (src.exit[i] == did) {
                    src.exit[i] = 0;
                }
                if (dest.exit[i] == sid) {
                    dest.exit[i] = 0;
                }
            }
            if (!shifting) {

                if (src.height == dest.height) {
                    //calc our angle
                    double a = Math.Atan2(dy - sy, dx - sx);
                    int e = getExitDir(a);
                    if (e >= 0) {
                        Room other;
                        if (rooms.TryGetValue(dest.exit[reverseDir(e)], out other)) {
                            for (int i = 0; i < 6; i++) {
                                if (other.exit[i] == did) {
                                    other.exit[i] = 0;
                                }
                            }
                        }
                        if (rooms.TryGetValue(src.exit[e], out other)) {
                            for (int i = 0; i < 6; i++) {
                                if (other.exit[i] == sid) {
                                    other.exit[i] = 0;
                                }
                            }
                        }
                        src.exit[e] = did;
                        dest.exit[reverseDir(e)] = sid;
                    }
                } else {
                    Room top = src, bottom = dest;
                    int tid = sid, bid = did;
                    if(src.height < dest.height) {
                        top = dest;
                        tid = did;
                        bottom = src;
                        bid = sid;
                    }
                    bottom.exit[4] = tid;
                    top.exit[5] = bid;
                }
            }
            redraw();

        }

        void redraw() {
            graph.Invalidate();
            foreach (Control c in Controls) {
                if (c is Label) {
                    c.Invalidate();
                }
            }
            foreach (Room r in rooms.Values) {
                r.lbl.Invalidate();
            }
        }

        int getExitX(int d) {
            switch (d) {
                case 0:
                    return -5;
                case 1:
                    return -5;
                case 2:
                    return -60;
                case 3:
                    return 50;
                case 4:
                    return 0;
                case 5:
                    return 0;
            }
            return 5;
        }

        int getExitY(int d) {
            switch (d) {
                case 0:
                    return -60;
                case 1:
                    return 50;
                case 2:
                    return -5;
                case 3:
                    return -5;
                case 4:
                    return -30;
                case 5:
                    return 30;
            }
            return 5;
        }

        int reverseDir(int d) {
            switch (d) {
                case 0:
                    return 1;
                case 1:
                    return 0;
                case 2:
                    return 3;
                case 3:
                    return 2;
                case 4:
                    return 5;
                case 5:
                    return 4;
            }
            return 5;
        }

        private void nodeMouseUp(object sender, System.Windows.Forms.MouseEventArgs e) {
            Label label = (Label)sender;
            int id;
            Room r;
            if (e.Button == MouseButtons.Left && !mdown[1]) {
                mdown[0] = false;
                curDragRoom = 0;
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
                        r.panel.Location = new System.Drawing.Point(e.X - (mDownX - 48) + r.panel.Location.X - 50 + 2, e.Y - (mDownY - 48) + r.panel.Location.Y - 50 + 2);
                        redraw();
                    }
                }
            }
        }


        void over_paint(object sender, PaintEventArgs e) {
            Graphics g = e.Graphics;

            //Let's make some pens
            Pen pen = new Pen(Color.FromArgb(64, 10, 10, 230), 7);
            Pen dpen = new Pen(Color.FromArgb(64, 230, 10, 10), 9);
            Pen curPen = pen;
            System.Drawing.SolidBrush brush = new System.Drawing.SolidBrush(System.Drawing.Color.Red);
            Pen pen2 = new Pen(brush);
            //Pen blackPen = new Pen(Color.Black, 1 / g.DpiX);

            g.PageUnit = GraphicsUnit.Pixel; //convenient for working with form width and height

            //g.DrawLine(pen, 100, 100, 300, 300);
            Room dest;
            foreach (Room src in rooms.Values) {
                if (src.height == curHeight) {
                    for (int i = 0; i < 6; i++) {
                        if (src.exit[i] > 0) {
                            if (rooms.TryGetValue(src.exit[i], out dest)) {
                                if(src.height == dest.height) {
                                    curPen = pen;
                                } else {
                                    curPen = dpen;
                                }
                                g.DrawLine(curPen, graph.Location.X + src.panel.Location.X + 50 + getExitX(i) + 4, graph.Location.Y + src.panel.Location.Y + 50 + getExitY(i) + 4, graph.Location.X + dest.panel.Location.X + 50 + getExitX(reverseDir(i)) + 4, graph.Location.Y + dest.panel.Location.Y + 50 + getExitY(reverseDir(i)) + 4);
                                g.FillRectangle(brush, graph.Location.X + src.panel.Location.X + 50 + getExitX(i), graph.Location.Y + src.panel.Location.Y + 50 + getExitY(i), 10, 10);
                                g.FillRectangle(brush, graph.Location.X + dest.panel.Location.X + 50 + getExitX(reverseDir(i)), graph.Location.Y + dest.panel.Location.Y + 50 + getExitY(reverseDir(i)), 10, 10);
                            }
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

        private void graph_Paint(object sender, PaintEventArgs e) {

        }

        private void button2_Click(object sender, EventArgs e) {
            deleteRoom();
            fixFocus(sender, e);
        }

        void deleteRoom() {
            if (curSelRoom > 0) {
                graph.Controls.Remove(rooms[curSelRoom].panel);
                rooms.Remove(curSelRoom);
                desel();
                redraw();
            }
        }

        private void button2_Click_1(object sender, EventArgs e) {

        }

        private void txtUpdate_Click(object sender, EventArgs e) {
            updateRoom();
            fixFocus(sender, e);
        }

        private void btnSouth_Click(object sender, EventArgs e) {

        }


        private void FrmArea_Click(object sender, EventArgs e) {
            fixFocus(sender, e);
        }

        private void button2_Click_2(object sender, EventArgs e) {
            autoLink();
        }

        void autoLink() {
            foreach (Room r in rooms.Values) {
                foreach (Room r2 in rooms.Values) {
                    if (r != r2 && r.height == r2.height && GetDistance(r.panel.Location.X, r.panel.Location.Y, r2.panel.Location.X, r2.panel.Location.Y) <= 200) {
                        link(r, r2);
                    }
                }
            }
        }

        double GetDistance(double x1, double y1, double x2, double y2) {
            return Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2));
        }

        private void graph_Paint_1(object sender, PaintEventArgs e) {

        }

        private void roomPanel_Paint(object sender, PaintEventArgs e) {

        }

        void roomLink() {
            Room r = rooms[curSelRoom];
            foreach (Room r2 in rooms.Values) {
                if (r != r2 && r.height == r2.height && GetDistance(r.panel.Location.X, r.panel.Location.Y, r2.panel.Location.X, r2.panel.Location.Y) <= 200) {
                    link(r, r2);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e) {
            roomLink();
        }

        private void lbl_Click(object sender, EventArgs e) {

        }

        private void btnClearExit_Click(object sender, EventArgs e) {
            clearExits(rooms[curSelRoom]);
        }

        private void btnDelete_Click(object sender, EventArgs e) {

        }

        private void label5_Click(object sender, EventArgs e) {

        }

        private void txtAreaName_TextChanged(object sender, EventArgs e) {

        }

        private void label4_Click(object sender, EventArgs e) {

        }

        private void lblHeight_Click(object sender, EventArgs e) {

        }

        private void txtAreaDisplayName_TextChanged(object sender, EventArgs e) {

        }
    }
}
