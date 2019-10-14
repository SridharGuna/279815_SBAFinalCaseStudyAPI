using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NBench;
using Task = TaskManager.EntityLayer.Task;

namespace TaskManager.NBenchPerformanceTesting 
{
   public class Task_MemoryTests : PerformanceTestStuite<Task_MemoryTests>
    {
        private const int NumberOfAdds = 1000000;
        private const int DictionaryEntrySize = 62;
        private const int MaxExpectedMemory = NumberOfAdds * DictionaryEntrySize;
      //  62,563,504.40 
        [PerfBenchmark(RunMode = RunMode.Iterations, TestMode = TestMode.Measurement)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        public void Task_AddMemoryMeasurement()
        {
            var dictionary = new List<Task>();

            Task_Populate(dictionary, NumberOfAdds);
        }

        [PerfBenchmark(RunMode = RunMode.Iterations, TestMode = TestMode.Test, Description = "Dictionary without capacity, add memory test.")]
        [MemoryAssertion(MemoryMetric.TotalBytesAllocated, MustBe.GreaterThan, MaxExpectedMemory)]
        public void Task_AddMemory_FailingTest()
        {
            var dictionary = new List<Task>(); ;

            Task_Populate(dictionary, NumberOfAdds);
        }

        [PerfBenchmark(RunMode = RunMode.Iterations, TestMode = TestMode.Test)]
        [MemoryAssertion(MemoryMetric.TotalBytesAllocated, MustBe.GreaterThan, MaxExpectedMemory)]
        public void Task_AddMemory_PassingTest()
        {
            var dictionary = new List<Task>();

            Task_Populate(dictionary, NumberOfAdds);
        }

        public void Task_Populate(List<Task> dictionary, int n)
        {
            for (var i = 0; i < n; i++)
            {
                dictionary.Add(new Task { TaskId = i++, StartDate = DateTime.Now, EndDate = DateTime.Now, TaskName = "test task" + i, Priority = i, ParentId = 0 });
            }
        }
    }
}
