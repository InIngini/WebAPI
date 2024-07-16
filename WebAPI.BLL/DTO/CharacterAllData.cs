using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.BLL.DTO
{
    public class CharacterAllData
    {
        public int IdCharacter { get; set; }

        public string Name { get; set; }

        public byte[]? Picture1 { get; set; }
    }
}
