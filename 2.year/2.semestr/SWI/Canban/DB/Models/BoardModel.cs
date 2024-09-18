using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Threading.Tasks;
using System.Drawing;

namespace Canban.DB.Models
{
    public class BoardModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Color { get; set; }
        public int UserId { get; set; }
        public virtual UserModel CreatedBy { get; set; }
    }
}
