using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Canban.DB.Models
{
    internal class ColumnModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BoardId { get; set; }
        public BoardModel Board { get; set; }

    }
}
