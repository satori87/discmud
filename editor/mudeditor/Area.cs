using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUDEdit {
    class Area {

        public String name = "";
        public String displayName = "";

        public Dictionary<int, Room> rooms = new Dictionary<int, Room>();

        public Area() {

        }

        public Area(String name) {
            this.name = name;
            
        }

        public String GetJSON() {
            return Util.ObjectToJSON(this);
        }
    }
}
