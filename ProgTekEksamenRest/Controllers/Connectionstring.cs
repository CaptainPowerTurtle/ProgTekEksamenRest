using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgTekEksamenRest.Controllers
{
    public static class Connectionstring
    {
        public static string GetConnectionString()
        {
            return
                "Server=tcp:progtek.database.windows.net,1433;Initial Catalog=ProgTekEksamen;Persist Security Info=False;User ID=PowerTurtle;Password=ProgTek1234;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        }
    }
}
