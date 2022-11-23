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
    public class JokesCsvWriter : ICsvWriter<Joke>
    {
        public string Write(Joke joke)
        {
            string uniqueFileName = Path.GetTempPath() + Guid.NewGuid().ToString() + ".csv";
            using var writer = new StreamWriter(uniqueFileName);
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);

            csv.WriteHeader<Joke>();
            csv.NextRecord();
            csv.WriteRecord(joke);

            return uniqueFileName;
        }
    }
}
