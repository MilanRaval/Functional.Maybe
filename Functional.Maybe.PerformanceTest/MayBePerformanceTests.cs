using Functional.Maybe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MayBePerformanceTests
{
    public class MayBePerformanceTests
    {
        public static void TestFirstMayBe()
        {
            int totalItems = 10000;
            var collection = new List<Data>();
            for (int i = 0; i < totalItems; i++)
            {
                collection.Add(new Data()
                {
                    Id = i,
                    Name = i.ToString(),
                    Address = string.Format("Address {0}", i)
                });
            }

            var watch1 = System.Diagnostics.Stopwatch.StartNew();
            for (int i = 0; i < totalItems; i++)
            {
                var v = collection.FirstOrDefault(item => item.Id == i);
                if (v != null)
                {
                    v.Address = v.Address + " Checked";
                    //Console.WriteLine(i.ToString());
                }
            }
            watch1.Stop();
          

            var watch2 = System.Diagnostics.Stopwatch.StartNew();
            for (int i = 0; i < totalItems; i++)
            {
                var v = collection.FirstMaybe(item => item.Id == i);
                v.Do(item => item.Address = item.Address + " Checked");
                //Console.WriteLine(i.ToString());
            }
            watch2.Stop();


            Console.WriteLine("Time taken by FirstOrDefault and Null check {0}", watch1.ElapsedMilliseconds);
            Console.WriteLine("Time taken by FirstMaybe {0}", watch2.ElapsedMilliseconds);
        }
    }

    public class Data
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
