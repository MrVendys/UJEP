using Microsoft.EntityFrameworkCore;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Canban.View.Windows;
using System.Threading.Tasks;
namespace Canban.DB.Models
{
    class DatabaseContext : DbContext
    {
        public DbSet<TaskModel> Tasks { set; get; }
        public DbSet<StatusModel> Statuses { get; set; }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<BoardModel> Boards { get; set; }
        public DbSet<ColumnModel> Columns { get; set; }
        public DbSet<TaskHistoryModel> TaskHistories { get; set; }

        private string DbPath { get; set; }
        public DatabaseContext()
        {
            SetContext();
        }
        private void SetContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "blogging.db");
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            optionsBuilder.UseSqlite($"Data Source={DbPath}");
        }
    }
}
