
using System.ComponentModel;

namespace Canban.DB.Models
{
    public class ColumnModel
    {
        public int Id { get; set; }
        [DefaultValue("Novy Sloupec")]
        public string Name { get; set; }
        [DefaultValue("White")]
        public string? Color { get; set; }
        public int BoardId { get; set; }
        public virtual BoardModel Board { get; set; }

    }
}
