using System;

namespace TaskManager.EntityLayer.Models
{
    public class TaskViewModel
    {

        public string Task { get; set; }
        public string ParentTask { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public long? Priority { get; set; }
        public  string TaskId { get; set; }
        public long? ParentId { get; set; }
        public long? ProjectId { get; set; }
        public string ProjectName { get; set; }
        public long? UserId { get; set; }
        public string UserName { get; set; }
        public bool? Status { get; set; }
    }
}