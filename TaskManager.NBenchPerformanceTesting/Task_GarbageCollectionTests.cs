using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NBench;
using TaskManager.EntityLayer;

namespace TaskManager.NBenchPerformanceTesting
{
    public class Task_GarbageCollectionTests : PerformanceTestStuite<Task_GarbageCollectionTests>
    {
        [PerfBenchmark(RunMode = RunMode.Iterations, TestMode = TestMode.Measurement)]
        [GcMeasurement(GcMetric.TotalCollections, GcGeneration.AllGc)]
        public void MeasureGarbageCollections()
        {
            RunTest();
        }

        [PerfBenchmark(RunMode = RunMode.Iterations, TestMode = TestMode.Test)]
        [GcThroughputAssertion(GcMetric.TotalCollections, GcGeneration.Gen0, MustBe.LessThan, 300)]
        [GcThroughputAssertion(GcMetric.TotalCollections, GcGeneration.Gen1, MustBe.LessThan, 150)]
        [GcThroughputAssertion(GcMetric.TotalCollections, GcGeneration.Gen2, MustBe.LessThan, 20)]
        [GcTotalAssertion(GcMetric.TotalCollections, GcGeneration.Gen2, MustBe.LessThan, 50)]
        public void TestGarbageCollections()
        {
            RunTest();
        }

        private readonly List<EntityLayer.Task> _dataCache = new List<EntityLayer.Task>();

        private void RunTest()
        {
            for (var i = 0; i < 10000; i++)
            {
                _dataCache.Add(new EntityLayer.Task{TaskId = i,TaskName = "Task"+i,StartDate = DateTime.Now,EndDate = DateTime.Now,Priority = i,ParentId = 0});

              
            }
            _dataCache.Clear();
        }
    }
}
