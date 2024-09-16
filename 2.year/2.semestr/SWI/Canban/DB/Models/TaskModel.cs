using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
namespace Canban.DB.Models
{
    
    public class TaskModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [DefaultValue(null)]
        public string? Desc { get; set; }
        [DefaultValue(null)]
        public DateTime? Deadline { get; set; }
        [DefaultValue(null)]
        public DateTime? Started { get; set; }
        [DefaultValue(null)]
        public DateTime? Completed { get; set; }
        public int? test {  get; set; }
        public virtual ICollection<UserModel> Users { get; set; }
        [Required]
        public int? ColumnId { get; set; }

    }
}
