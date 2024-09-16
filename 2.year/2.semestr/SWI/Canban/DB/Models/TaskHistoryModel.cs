using System;
using System.Collections.Generic;
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
        public virtual TaskModel OldTaskInfo {  get; set; }
        public int OldTaskInfoId { get; set; }
        public virtual TaskModel NewTaskInfo { get; set; }
        public int NewTaskInfoId { get; set; }
        public DateTime MoveAt { get; set; }
        public virtual UserModel MoveBy { get; set; }
        public int MoveById { get; set; }
    }
}
