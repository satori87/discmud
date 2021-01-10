using Microsoft.VisualBasic;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MUDEdit {
    public partial class FormArea : Form {

        private Dictionary<int, Room> rooms = new Dictionary<int, Room>();
        private Dictionary<int, Panel> panels = new Dictionary<int, Panel>();
        private Dictionary<int, Label> labels = new Dictionary<int, Label>();

        private int curDragRoom;
        private int curSelRoom;
        private int curHeight = 0;

        bool[] mdown = new bool[2];
        bool linking = false;

        private bool shifting;

        int mDownX = 0;
        int mDownY = 0;

        public FormArea() {
            Font = new Font(Font.Name, 8.25f * 96f / CreateGraphics().DpiX, Font.Style, Font.Unit, Font.GdiCharSet, Font.GdiVerticalFont);
            this.AutoScaleMode = AutoScaleMode.None;
            InitializeComponent();
        }


        private void Form_Area_Load(object sender, EventArgs e) {
            over.Owner = this;
            foreach (Control c in Controls) {
                if (!(c is TextBox)) {
                    c.KeyDown += new System.Windows.Forms.KeyEventHandler(this.keyDown);
                    c.KeyUp += new System.Windows.Forms.KeyEventHandler(this.keyUp);
                }
            }
            FixFocus(sender, e);
        }


        private void FormArea_Shown(object sender, EventArgs e) {
            FixFocus(sender, e);
            Area a = MUDEdit.curArea;
            //rooms = a.rooms;
            txtAreaName.Text = a.name;
            txtDisplayName.Text = a.displayName;
            roomPanel.Visible = false;
            foreach (Room r in a.rooms.Values) {
                roomPanel.Visible = true;
                addRoom(r, r.id, r.x, r.y);
                curHeight = r.height;
                lblHeight.Text = curHeight + "";
            }
        }


        private void FixFocus(object sender, EventArgs e) {
            this.lbl.Focus();
        }


        void ScrollArea(int bx, int by) {
            foreach (Room r in rooms.Values) {
                panels[r.id].Location = new Point(panels[r.id].Location.X + bx, panels[r.id].Location.Y + by);
                r.x = panels[r.id].Location.X + bx;
                r.y = panels[r.id].Location.Y + by;
            }
            redraw();
        }

        void desel() {
            curSelRoom = 0;
            roomPanel.Visible = false;
            btnDelete.Enabled = false;
            foreach (Panel panel in panels.Values) {
                panel.BackColor = Color.Black;
            }
        }

        void clearExits(Room src) {
            int sid = src.id;
            int did;
            Room dest;
            for (int i = 0; i < 6; i++) {
                if (src.exit[i] > 0) {
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
            lblHeight.Text = curHeight + "";
            if (curDragRoom > 0) {
                rooms[curDragRoom].height = curHeight;
                clearExits(rooms[curDragRoom]);
            } else if (!linking) {
                desel();
            }
            foreach (Room r in rooms.Values) {
                if (r.height == curHeight) {
                    panels[r.id].Visible = true;
                } else {
                    panels[r.id].Visible = false;
                }
            }
            redraw();
        }

        void heightDown() {
            curHeight--;
            lblHeight.Text = curHeight + "";
            if (curDragRoom > 0) {
                rooms[curDragRoom].height = curHeight;
                clearExits(rooms[curDragRoom]);
            } else if (!linking) {
                desel();
            }
            foreach (Room r in rooms.Values) {
                if (r.height == curHeight) {
                    panels[r.id].Visible = true;
                } else {
                    panels[r.id].Visible = false;
                }
            }
            redraw();
        }

        private void keyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.LShiftKey || e.KeyCode == Keys.RShiftKey || e.KeyCode == Keys.Shift || e.KeyCode == Keys.ShiftKey) {
                shifting = true;
            } else if (e.KeyCode == Keys.Delete) {
                deleteRoom();
            } else if (e.KeyCode == Keys.Add || e.KeyCode == Keys.Oemplus) {
                heightUp();
            } else if (e.KeyCode == Keys.Subtract || e.KeyCode == Keys.OemMinus) {
                heightDown();
            } else if (e.KeyCode == Keys.N) {
                newRoom();
            } else if (e.KeyCode == Keys.L) {
                if (curSelRoom > 0) {
                    roomLink();
                }
            } else if (e.KeyCode == Keys.P) {
                autoLink();
            }
            if (e.KeyCode == Keys.W) {
                ScrollArea(0, -10);
            } else if (e.KeyCode == Keys.S) {
                ScrollArea(0, 10);
            }
            if (e.KeyCode == Keys.A) {
                ScrollArea(-10, 0);
            } else if (e.KeyCode == Keys.D) {
                ScrollArea(10, 0);
            }
        }

        private void keyUp(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.LShiftKey || e.KeyCode == Keys.RShiftKey || e.KeyCode == Keys.Shift || e.KeyCode == Keys.ShiftKey) {
                shifting = false;
            }
        }

        int freeRoom() {
            int i = 0;
            do {
                i++;
                if (!rooms.ContainsKey(i)) {
                    return i;
                }
            } while (i > 0);
            return i;
        }

        private void button1_Click_1(object sender, EventArgs e) {
            newRoom();
            FixFocus(sender, e);
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
                txtLinkArea.Text = room.linkTo;
            }
        }

        void updateRoom() {
            if (curSelRoom > 0) {
                Room room;
                if (rooms.TryGetValue(curSelRoom, out room)) {
                    room.displayName = txtDisplayName.Text;
                    room.desc = txtDescription.Text;
                    room.name = txtName.Text;
                    labels[room.id].Text = txtName.Text;
                    room.linkTo = txtLinkArea.Text;
                    if(room.linkTo.Length > 0) {
                        labels[room.id].BackColor = Color.Purple;
                    } else {
                        labels[room.id].BackColor = Color.White;
                    }
                    
                }
            }
        }

        void newRoom() {
            int id = freeRoom();
            int x = graph.Size.Width / 2, y = graph.Size.Height / 2;
            Room r = new Room(id);
            r.x = x - 50;
            r.y = y - 50;
            addRoom(r, id, x, y);
            selRoom(id);
        }

        void addRoom(Room r, int id, int x, int y) {

            desel();

            r.height = curHeight;
            rooms[id] = r;

            Panel panel = new Panel {
                Location = new System.Drawing.Point(x - 50, y - 50),
                Size = new System.Drawing.Size(100, 100),
                AccessibleName = id + "",
                BorderStyle = BorderStyle.None,
                BackColor = Color.Yellow
            };
            panel.Click += new System.EventHandler(FixFocus);
            graph.Controls.Add(panel);
            panels[id] = panel;
            panel.BringToFront();

            Label lbl = new Label {
                Location = new System.Drawing.Point(2, 2),
                BackColor = Color.White,
                Size = new System.Drawing.Size(96, 96),
                AccessibleName = id + "",
                BorderStyle = BorderStyle.None,
                Text = id + ": " + r.name
            };
            if (r.linkTo.Length > 0) {
                lbl.BackColor = Color.Purple;
            } else {
                lbl.BackColor = Color.White;
            }
            lbl.MouseUp += new System.Windows.Forms.MouseEventHandler(nodeMouseUp);
            lbl.MouseDown += new System.Windows.Forms.MouseEventHandler(nodeMouseDown);
            lbl.MouseMove += new System.Windows.Forms.MouseEventHandler(nodeMouseMove);
            lbl.Click += new System.EventHandler(FixFocus);
            labels[id] = lbl;
            panel.Controls.Add(lbl);
            selRoom(id);
        }

        private void panelMouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {

            if (linking && curSelRoom > 0) {
                panels[curSelRoom].BackColor = Color.Yellow;
            }
            linking = false;
        }

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
                        panels[curSelRoom].BackColor = Color.Black;
                    }
                    curDragRoom = id;
                    selRoom(id);
                    if (rooms.TryGetValue(id, out r)) {
                        panels[id].BackColor = Color.Yellow;
                    }
                }

            } else if (e.Button == MouseButtons.Right && !mdown[0]) {
                mdown[1] = true;
                if (!linking) {
                    if (int.TryParse(label.AccessibleName, out id)) {
                        if (curSelRoom > 0) {
                            panels[curSelRoom].BackColor = Color.Black;
                        }
                        selRoom(id);
                        if (rooms.TryGetValue(id, out r)) {
                            panels[id].BackColor = Color.Blue;
                        }
                        linking = true;
                    }
                } else {
                    if (int.TryParse(label.AccessibleName, out id)) {
                        if (rooms.TryGetValue(id, out r)) {
                            if (id == curSelRoom) {
                                linking = false;
                                panels[id].BackColor = Color.Yellow;
                            } else {
                                panels[curSelRoom].BackColor = Color.Yellow;
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
            int sx = panels[sid].Location.X + 50;
            int sy = panels[sid].Location.Y + 50;
            int dx = panels[did].Location.X + 50;
            int dy = panels[did].Location.Y + 50;
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
                    if (src.height < dest.height) {
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
                c.Invalidate();
            }
            foreach (Label lbl in labels.Values) {
                lbl.Invalidate();
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
                        //panels[r.id].BackColor = Color.Yellow;
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
                        panels[r.id].Location = new System.Drawing.Point(e.X - (mDownX - 48) + panels[r.id].Location.X - 50 + 2, e.Y - (mDownY - 48) + panels[r.id].Location.Y - 50 + 2);
                        r.x = e.X - (mDownX - 48) + panels[r.id].Location.X - 50 + 2;
                        r.y = e.Y - (mDownY - 48) + panels[r.id].Location.Y - 50 + 2;
                        redraw();
                    }
                }
            }
        }


        void over_paint(object sender, PaintEventArgs e) {
            Graphics g = e.Graphics;
            Pen pen = new Pen(Color.FromArgb(64, 10, 10, 230), 7);
            Pen dpen = new Pen(Color.FromArgb(64, 230, 10, 10), 9);
            Pen curPen = pen;
            System.Drawing.SolidBrush brush = new System.Drawing.SolidBrush(System.Drawing.Color.Red);
            g.PageUnit = GraphicsUnit.Pixel; //convenient for working with form width and height
            Room dest;
            foreach (Room src in rooms.Values) {
                int sx, dx, sy, dy, mx, my;
                if (src.height == curHeight) {
                    for (int i = 0; i < 6; i++) {
                        if (src.exit[i] > 0) {
                            if (rooms.TryGetValue(src.exit[i], out dest)) {
                                if (src.height == dest.height) {
                                    curPen = pen;
                                } else {
                                    curPen = dpen;
                                }
                                sx = graph.Location.X + panels[src.id].Location.X + 50 + getExitX(i) + 4;
                                sy = graph.Location.Y + panels[src.id].Location.Y + 50 + getExitY(i) + 4;
                                dx = graph.Location.X + panels[dest.id].Location.X + 50 + getExitX(reverseDir(i)) + 4;
                                dy = graph.Location.Y + panels[dest.id].Location.Y + 50 + getExitY(reverseDir(i)) + 4;
                                if (i < 4) {
                                    g.DrawLine(curPen, sx, sy, dx, dy);
                                    g.FillRectangle(brush, graph.Location.X + panels[src.id].Location.X + 50 + getExitX(i), graph.Location.Y + panels[src.id].Location.Y + 50 + getExitY(i), 10, 10);
                                    g.FillRectangle(brush, graph.Location.X + panels[dest.id].Location.X + 50 + getExitX(reverseDir(i)), graph.Location.Y + panels[dest.id].Location.Y + 50 + getExitY(reverseDir(i)), 10, 10);
                                } else {
                                    mx = (int)((float)(sx + dx) / 2f);
                                    my = (int)((float)(sy + dy) / 2f);
                                    if (src.height == curHeight) {
                                        g.DrawLine(curPen, sx, sy, mx, my);
                                        g.FillRectangle(brush, graph.Location.X + panels[src.id].Location.X + 50 + getExitX(i), graph.Location.Y + panels[src.id].Location.Y + 50 + getExitY(i), 10, 10);
                                    } else {
                                        g.DrawLine(curPen, dx, dy, mx, my);
                                        g.FillRectangle(brush, graph.Location.X + panels[dest.id].Location.X + 50 + getExitX(reverseDir(i)), graph.Location.Y + panels[dest.id].Location.Y + 50 + getExitY(reverseDir(i)), 10, 10);
                                    }
                                }
                            }
                        }
                    }
                }
            }

        }

        private void button2_Click(object sender, EventArgs e) {
            deleteRoom();
            FixFocus(sender, e);
        }

        void deleteRoom() {
            if (curSelRoom > 0) {
                graph.Controls.Remove(panels[curSelRoom]);
                panels.Remove(curSelRoom);
                labels.Remove(curSelRoom);
                rooms.Remove(curSelRoom);
                desel();
                redraw();
            }
        }

        private void txtUpdate_Click(object sender, EventArgs e) {
            updateRoom();
            FixFocus(sender, e);
        }


        private void FrmArea_Click(object sender, EventArgs e) {
            FixFocus(sender, e);
        }

        private void button2_Click_2(object sender, EventArgs e) {
            autoLink();
        }

        void autoLink() {
            foreach (Room r in rooms.Values) {
                foreach (Room r2 in rooms.Values) {
                    if (r != r2 && r.height == r2.height && GetDistance(panels[r.id].Location.X, panels[r.id].Location.Y, panels[r2.id].Location.X, panels[r2.id].Location.Y) <= 200) {
                        link(r, r2);
                    }
                }
            }
        }

        double GetDistance(double x1, double y1, double x2, double y2) {
            return Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2));
        }

        void roomLink() {
            Room r = rooms[curSelRoom];
            foreach (Room r2 in rooms.Values) {
                if (r != r2 && r.height == r2.height && GetDistance(panels[r.id].Location.X, panels[r.id].Location.Y, panels[r2.id].Location.X, panels[r2.id].Location.Y) <= 200) {
                    link(r, r2);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e) {
            roomLink();
        }

        private void btnClearExit_Click(object sender, EventArgs e) {
            clearExits(rooms[curSelRoom]);
        }

        private void graph_Paint(object sender, PaintEventArgs e) {

        }

        private void roomPanel_Paint(object sender, PaintEventArgs e) {

        }

        private void FormArea_FormClosing(object sender, FormClosingEventArgs e) {
            MUDEdit.formEditor.Show();
        }

        private void btnCancel_Click(object sender, EventArgs e) {
            this.Hide();
            MUDEdit.formEditor.Show();
        }

        private void btnSave_Click(object sender, EventArgs e) {
            Area a = new Area(txtAreaName.Text);
            a.displayName = txtDisplayName.Text;
            a.rooms = rooms;
            String cs = @"server=18.223.190.165;port=3306;userid=bear;password=%Pb?fYW@ydP9RLqeTnfSW-u!23c$f=%#;database=mud";
            var con = new MySqlConnection(cs);
            con.Open();
            String stm = "UPDATE area SET json='" + a.GetJSON() + "' WHERE name='" + a.name + "'";
            var cmd = new MySql.Data.MySqlClient.MySqlCommand(stm, con);
            if (cmd.ExecuteNonQuery() < 1) {
                Interaction.MsgBox("Save area failed SQL");
            } else {
                this.Hide();
                MUDEdit.formEditor.Show();
                MUDEdit.formEditor.fetchArea();
            }
            con.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e) {
            deleteRoom();
        }

    }

}
