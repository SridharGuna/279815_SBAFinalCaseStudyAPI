using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NBench;
using Task = TaskManager.EntityLayer.Task;

namespace TaskManager.NBenchPerformanceTesting
{
    public class Task_ThroughputTests : PerformanceTestStuite<Task_ThroughputTests>
    {
        private readonly Dictionary<int, Task> taskDictionary = new Dictionary<int, Task>();

        private const string AddCounterName = "AddCounter";
        private Counter addCounter;
        private int key;

        private const int AcceptableMinAddThroughput = 20000000;

        [PerfSetup]
        public void Setup(BenchmarkContext context)
        {
            addCounter = context.GetCounter(AddCounterName);
            key = 0;
        }

        [PerfBenchmark(RunMode = RunMode.Throughput, TestMode = TestMode.Test)]
        [CounterThroughputAssertion(AddCounterName, MustBe.LessThanOrEqualTo, AcceptableMinAddThroughput)]
        public void AddThroughput_ThroughputMode(BenchmarkContext context)
        {
            taskDictionary.Add(key++, new Task{TaskId = key++, StartDate = DateTime.Now,EndDate = DateTime.Now,TaskName = "test task"+ key++, Priority = key++, ParentId = 0});
            addCounter.Increment();
        }

        [PerfBenchmark(RunMode = RunMode.Iterations, TestMode = TestMode.Test)]
        [CounterThroughputAssertion(AddCounterName, MustBe.LessThanOrEqualTo, AcceptableMinAddThroughput)]
        public void AddThroughput_IterationsMode(BenchmarkContext context)
        {
            for (var i = 0; i < AcceptableMinAddThroughput; i++)
            {
                taskDictionary.Add(i, new Task { TaskId = i, StartDate = DateTime.Now, EndDate = DateTime.Now, TaskName = "test task"+i, Priority = i, ParentId = 0 });
                addCounter.Increment();
            }
        }

        [PerfCleanup]
        public void Cleanup(BenchmarkContext context)
        {
            taskDictionary.Clear();
        }
    }
}
