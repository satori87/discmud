using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUDEdit {
    public class Room {
        public string name;
        public string displayName;
        public string desc;
        public int[] exit = new int[6];
        public int id = 1;
        public int height = 0;
        public int x = 0;
        public int y = 0;
        public String linkTo = "";

        public Room(int id) {
            this.id = id;
        }
    }
}
