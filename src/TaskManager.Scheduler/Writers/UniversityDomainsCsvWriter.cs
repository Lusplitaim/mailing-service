using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Core.Models;

namespace TaskManager.Scheduler.Writers
{
    public class UniversityDomainsCsvWriter : ICsvWriter<IEnumerable<University>>
    {
        public string Write(IEnumerable<University> universities)
        {
            string uniqueFileName = Path.GetTempPath() + Guid.NewGuid().ToString() + ".csv";
            using var writer = new StreamWriter(uniqueFileName);
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);

            csv.WriteHeader<University>();
            csv.NextRecord();
            foreach (var university in universities)
            {
                csv.WriteRecord(university);
                csv.NextRecord();
            }

            return uniqueFileName;
        }
    }
}
