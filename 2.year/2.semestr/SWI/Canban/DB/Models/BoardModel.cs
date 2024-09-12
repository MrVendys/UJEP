using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Canban.DB.Models
{
    internal class BoardModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //public ICollection<Collum>
        public int UserId { get; set; }
        public UserModel CreatedBy { get; set; }
    }
}
