using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Core.Models;
using CsvHelper;
using System.Globalization;

namespace TaskManager.Scheduler.Writers
{
    internal class DogFactsCsvWriter : ICsvWriter<IEnumerable<DogFact>>
    {
        public string Write(IEnumerable<DogFact> dogFacts)
        {
            string uniqueFileName = Path.GetTempPath() + Guid.NewGuid().ToString() + ".csv";
            using var writer = new StreamWriter(uniqueFileName);
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);

            csv.WriteHeader<DogFact>();
            csv.NextRecord();
            foreach (var dogFact in dogFacts)
            {
                csv.WriteRecord(dogFact);
                csv.NextRecord();
            }

            return uniqueFileName;
        }
    }
}
