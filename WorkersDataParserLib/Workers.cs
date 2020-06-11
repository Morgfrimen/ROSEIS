using Microsoft.Extensions.Logging;
using System;

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
                var result = FSharp.ParserSite.GetStringArray("32009180292");

                //созранение данных
                IsParse = true;
            }
            else
            {
                IsParse = false;
            }
        }

        private void Parser_OnNewData(object arg1, string[] arg2)
        {
            var testresult = arg2;
        }
    }
}