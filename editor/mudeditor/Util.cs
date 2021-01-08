using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mudeditor {
    class Util {

        public static Object JSONToObject(String s) {
            return JsonConvert.DeserializeObject(s);
        }

        public static String ObjectToJSON(Object o) {
            return JsonConvert.SerializeObject(o);
        }

    }

}
