using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManagerApiService.Models
{
    /// <summary>
    /// this is task view model
    /// </summary>
    public class TaskViewModel
    {

        public string Task { get; set; }
        public string ParentTask { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public long? Priority { get; set; }
        public  string TaskId { get; set; }
    }
}