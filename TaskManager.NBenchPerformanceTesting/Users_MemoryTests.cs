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
   public class Users_MemoryTests : PerformanceTestStuite<Users_MemoryTests>
    {
        private const int NumberOfAdds = 1000000;
        private const int DictionaryEntrySize = 62;
        private const int MaxExpectedMemory = NumberOfAdds * DictionaryEntrySize;
      //  62,563,504.40 
        [PerfBenchmark(RunMode = RunMode.Iterations, TestMode = TestMode.Measurement)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        public void Users_AddMemoryMeasurement()
        {
            var dictionary = new List<User>();

            Users_Populate(dictionary, NumberOfAdds);
        }

        [PerfBenchmark(RunMode = RunMode.Iterations, TestMode = TestMode.Test, Description = "Dictionary without capacity, add memory test.")]
        [MemoryAssertion(MemoryMetric.TotalBytesAllocated, MustBe.GreaterThan, MaxExpectedMemory)]
        public void Users_AddMemory_FailingTest()
        {
            var dictionary = new List<User>(); ;

            Users_Populate(dictionary, NumberOfAdds);
        }

        [PerfBenchmark(RunMode = RunMode.Iterations, TestMode = TestMode.Test)]
        [MemoryAssertion(MemoryMetric.TotalBytesAllocated, MustBe.GreaterThan, MaxExpectedMemory)]
        public void Users_AddMemory_PassingTest()
        {
            var dictionary = new List<User>();

            Users_Populate(dictionary, NumberOfAdds);
        }

        public void Users_Populate(List<User> dictionary, int n)
        {
            for (var i = 0; i < n; i++)
            {
                dictionary.Add(new User
                {TaskId = 1020,
                    ProjectId = 235,
                    UserId = 52,
                    EmployeeId = "Xdorf5",
                    FirstName = "FirstName",
                    LastName = "LastName"
                });
            }
        }
    }
}
