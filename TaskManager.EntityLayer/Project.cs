//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TaskManager.EntityLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class Project
    {
        public long ProjectId { get; set; }
        public string ProjectName { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public Nullable<long> Priority { get; set; }
        public Nullable<long> ManagerId { get; set; }
        public Nullable<bool> IsCompleted { get; set; }
        public Nullable<long> TaskCount { get; set; }
    }
}