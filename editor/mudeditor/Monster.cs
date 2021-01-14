using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUDEdit {
   
    class Monster {

        public Dictionary<string, Object> fields = new Dictionary<string, Object>();

        public Monster(String name) {
            fields[name] = name;
        }
    }


}
