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
        [MinLength(1)]
        [MaxLength(10)]
        [DefaultValue(null)]
        public int Priority { get; set; }
        [DefaultValue(null)]
        public DateTime? Deadline { get; set; }
        [DefaultValue(null)]
        public DateTime? Started { get; set; }
        [DefaultValue(null)]
        public DateTime Completed { get; set; }
        [DefaultValue(0)]
        public byte Finished { get; set; }
        public ICollection<UserModel> Users { get; set; } = new List<UserModel>();
        [Required]
        public int ColumnId { get; set; }
        
        [DefaultValue(null)]
        public int? StatusId { get; set; }
        [DefaultValue(null)]
        public StatusModel? StatusModel { get; set; }
    }
}
