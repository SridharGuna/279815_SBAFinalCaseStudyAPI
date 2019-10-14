using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NBench;
using TaskManager.EntityLayer;

namespace TaskManager.NBenchPerformanceTesting
{
    public class User_GarbageCollectionTests : PerformanceTestStuite<User_GarbageCollectionTests>
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

        public readonly List<EntityLayer.User> _dataCache = new List<EntityLayer.User>();

        private void RunTest()
        {
            for (var i = 0; i < 10000; i++)
            {
                _dataCache.Add(new EntityLayer.User
                {TaskId = 1010,
                    ProjectId = 521,
                    UserId = 2351,
                    EmployeeId = "1020",
                    FirstName = "FirstName",
                    LastName = "LastName"

                });

              
            }
            _dataCache.Clear();
        }
    }
}
