using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NBench;
using TaskManager.EntityLayer;
using Task = TaskManager.EntityLayer.Task;

namespace TaskManager.NBenchPerformanceTesting 
{
   public class Projects_MemoryTests : PerformanceTestStuite<Projects_MemoryTests>
    {
        private const int NumberOfAdds = 1000000;
        private const int DictionaryEntrySize = 62;
        private const int MaxExpectedMemory = NumberOfAdds * DictionaryEntrySize;
      //  62,563,504.40 
        [PerfBenchmark(RunMode = RunMode.Iterations, TestMode = TestMode.Measurement)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        public void Project_AddMemoryMeasurement()
        {
            var dictionary = new List<Project>();

            Project_Populate(dictionary, NumberOfAdds);
        }

        [PerfBenchmark(RunMode = RunMode.Iterations, TestMode = TestMode.Test, Description = "Dictionary without capacity, add memory test.")]
        [MemoryAssertion(MemoryMetric.TotalBytesAllocated, MustBe.GreaterThan, MaxExpectedMemory)]
        public void Project_AddMemory_FailingTest()
        {
            var dictionary = new List<Project>(); ;

            Project_Populate(dictionary, NumberOfAdds);
        }

        [PerfBenchmark(RunMode = RunMode.Iterations, TestMode = TestMode.Test)]
        [MemoryAssertion(MemoryMetric.TotalBytesAllocated, MustBe.GreaterThan, MaxExpectedMemory)]
        public void Project_AddMemory_PassingTest()
        {
            var dictionary = new List<Project>();

            Project_Populate(dictionary, NumberOfAdds);
        }

        public void Project_Populate(List<Project> dictionary, int n)
        {
            for (var i = 0; i < n; i++)
            {
                dictionary.Add(new Project
                {
                  StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(1),
                    ProjectId = 142,
                    ManagerId = 21,
                    Priority = 2,
                    ProjectName = "Project Name"
                });
            }
        }
    }
}
