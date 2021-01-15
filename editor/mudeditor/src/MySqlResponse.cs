using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUDEdit {
    public class MySqlResponse {

        public MySqlDataReader rdr;
        public MySqlConnection con;

        public MySqlResponse(MySqlDataReader rdr, MySqlConnection con) {
            this.rdr = rdr;
            this.con = con;
        }

    }
    
}
