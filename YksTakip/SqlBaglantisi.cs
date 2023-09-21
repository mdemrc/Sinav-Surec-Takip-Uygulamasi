using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace YksTakip
{
    class SqlBaglantisi
    {
        public SqlConnection baglanti()
        {
            SqlConnection baglan = new SqlConnection("Data Source=GRANGER\\SQLEXPRESS;Initial Catalog=SınavSurecTakip;Integrated Security=True");
            baglan.Open();
            return baglan;
        }
    }
}
