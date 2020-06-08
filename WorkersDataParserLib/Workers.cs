using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace WorkersDataParserLib
{
    public class Workers
    {
        private readonly DateTime timeCheck;
        private readonly ILogger _logger;

        public bool IsParse { get; set; }

        public Workers(DateTime checkValueDateTime, ILogger logger)
        {
            this.timeCheck = checkValueDateTime;
            _logger = logger;
        }

        public void RunWork(object obg = default)
        {
            _logger.LogInformation("Run parse {time}",DateTime.Now);
            DateTime now = DateTime.Now;
            if (now.DayOfWeek == timeCheck.DayOfWeek && !IsParse)
            {
                //парсинг данных
                //созранение данных
                IsParse = true;
            }
            else
            {
                IsParse = false;
                RunWork();
            }
        }
    }
}