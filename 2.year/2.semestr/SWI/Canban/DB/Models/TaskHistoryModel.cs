using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Canban.DB.Models
{
    public class TaskHistoryModel
    {
        [Key]
        public int Id { get; set; }
        public int TaskId { get; set; }
        public string? OldName {  get; set; }
        public string? NewName { get; set; }
        public string? OldDesc { get; set; }
        public string? NewDesc { get; set; }
        [DefaultValue(null)]
        public DateTime OldDeadline { get; set; }
        [DefaultValue(null)]
        public DateTime NewDeadline { get; set; }
        [DefaultValue(null)]
        public DateTime OldStarted { get; set; }
        [DefaultValue(null)]
        public DateTime NewStarted { get; set; }
        [DefaultValue(null)]
        public DateTime OldCompleted { get; set; }
        [DefaultValue(null)]
        public DateTime NewCompleted { get; set; }
        public List<string>? OldUsersNames { get; set; }
        public List<string>? NewUsersNames { get; set; }
        public int? OldColumnId { get; set; }
        public int? NewColumnId { get; set; }

        [DefaultValue(null)]
        public DateTime MoveAt { get; set; }
        public string MoveBy { get; set; }
    }
}
