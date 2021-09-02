using System.Collections.Generic;
using Inflow.Shared.Infrastructure.Logging.Options;

namespace Inflow.Shared.Infrastructure.Logging
{
    public class LoggerOptions
    {
        public string Level { get; set; }
        public ConsoleOptions Console { get; set; }
        public FileOptions File { get; set; }
        public SeqOptions Seq { get; set; }
        public IDictionary<string, string> Overrides { get; set; }
        public IEnumerable<string> ExcludePaths { get; set; }
        public IEnumerable<string> ExcludeProperties { get; set; }
        public IDictionary<string, object> Tags { get; set; }
    }
}