using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ParserLib;
using ParserLib.RosAtomSequester;

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
                var first = new RosAtomSequesterParser();
                var second = new RosAtomSequesterParserSettings();
                second.PostFix = "32009180292";//для теста пока так
                ParserLib.ParserWorker<string[]> parser = new ParserWorker<string[]>(first,second);
                parser.OnNewData += Parser_OnNewData;
                parser.Start();
                
                //созранение данных
                IsParse = true;
            }
            else
            {
                IsParse = false;
                RunWork();
            }
        }

        private void Parser_OnNewData(object arg1, string[] arg2)
        {
            var testresult = arg2;
        }
    }
}