
namespace Canban.DB.Models
{
    public class ColumnModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Color { get; set; }
        public int BoardId { get; set; }
        public virtual BoardModel Board { get; set; }

    }
}
