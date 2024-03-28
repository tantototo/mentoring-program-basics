using NLog;
using System;
using System.Diagnostics;

namespace MvcMusicStore.Infra
{
    public static class Counter
    {
        public static PerformanceCounter LogIn { get; private set; }
        public static PerformanceCounter LogOut { get; private set; }
        public static PerformanceCounter Error { get; private set; }

        private enum Categories
        {
            BaseCategory
        }

        public static void SetupCounters()
        {
            var categoryName = nameof(Categories.BaseCategory);
            if (!PerformanceCounterCategory.Exists(categoryName))
            {
                var userCounters = new CounterCreationDataCollection
                {
                    new CounterCreationData
                    {
                        CounterType = PerformanceCounterType.NumberOfItems32,
                        CounterName = nameof(LogIn)
                    },
                    new CounterCreationData
                    {
                        CounterType = PerformanceCounterType.NumberOfItems32,
                        CounterName = nameof(LogOut)
                    },
                    new CounterCreationData
                    {
                        CounterType = PerformanceCounterType.NumberOfItems32,
                        CounterName = nameof(Error)
                    }
                };
                try
                {
                    PerformanceCounterCategory.Create(categoryName, "",
                        PerformanceCounterCategoryType.SingleInstance, userCounters);
                } catch (Exception ex)
                {
                    LogManager.GetCurrentClassLogger().Error(ex, "Processing failed.");
                    throw;
                }
            }

            LogIn = new PerformanceCounter(categoryName, nameof(LogIn), false);
            LogOut = new PerformanceCounter(categoryName, nameof(LogOut), false);
            Error = new PerformanceCounter(categoryName, nameof(Error), false);

            LogIn.RawValue = 0;
            LogOut.RawValue = 0;
            Error.RawValue = 0;
        }

        public static void OutputSample(CounterSample s)
        {
            Console.WriteLine("\r\n+++++++++++");
            Console.WriteLine("Sample values - \r\n");
            Console.WriteLine("   BaseValue        = " + s.BaseValue);
            Console.WriteLine("   CounterFrequency = " + s.CounterFrequency);
            Console.WriteLine("   CounterTimeStamp = " + s.CounterTimeStamp);
            Console.WriteLine("   CounterType      = " + s.CounterType);
            Console.WriteLine("   RawValue         = " + s.RawValue);
            Console.WriteLine("   SystemFrequency  = " + s.SystemFrequency);
            Console.WriteLine("   TimeStamp        = " + s.TimeStamp);
            Console.WriteLine("   TimeStamp100nSec = " + s.TimeStamp100nSec);
            Console.WriteLine("++++++++++++++++++++++");
        }

        private static void DeleteCategories()
        {
            var categoryName = nameof(Categories.BaseCategory);
            if (!PerformanceCounterCategory.Exists(categoryName))
                PerformanceCounterCategory.Delete(categoryName);
        }
    }
}