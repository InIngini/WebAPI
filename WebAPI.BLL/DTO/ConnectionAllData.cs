using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.BLL.DTO
{
    public class ConnectionAllData
    {
        public int IdConnection { get; set; }
        public int IdCharacter1 { get; set; }
        public int IdCharacter2 { get; set; }
        public int TypeConnection { get; set; }
    }
}
