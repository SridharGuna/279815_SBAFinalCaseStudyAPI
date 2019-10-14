using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.EntityLayer.Models
{
   public class ProjectViewModel
    {
        
            public long ProjectId { get; set; }
            public string ProjectName { get; set; }
            public Nullable<System.DateTime> StartDate { get; set; }
            public Nullable<System.DateTime> EndDate { get; set; }
            public Nullable<long> Priority { get; set; }
            public Nullable<long> ManagerId { get; set; }
            public Nullable<bool> IsCompleted { get; set; }
            public Nullable<long> TaskCount { get; set; }

            public string UserName { get; set; }
            public Nullable<long> UserId { get; set; }

       
    }
}
