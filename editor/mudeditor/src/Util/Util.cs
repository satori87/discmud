using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUDEdit {
    class Util {

        public static Object JSONToObject(String s, Type c) {
            return JsonConvert.DeserializeObject(s,c);
        }

        public static String ObjectToJSON(Object o) {
            return JsonConvert.SerializeObject(o);
        }

    }

}
