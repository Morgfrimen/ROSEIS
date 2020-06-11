using Microsoft.Extensions.Logging;
using ModelsDataLib;
using ParserEIC;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace WorkersDataParserLib
{
    public class Workers
    {
        private readonly DateTime timeCheck;
        private readonly ILogger _logger;
        private List<PurchaseEIS> purchaseEises = new List<PurchaseEIS>();

        public bool IsParse { get; set; }

        public Workers(DateTime checkValueDateTime, ILogger logger)
        {
            this.timeCheck = checkValueDateTime;
            _logger = logger;
        }

        public async void RunWork(object obg = default)
        {
            _logger.LogInformation("Run parse {time}",DateTime.Now);
            DateTime now = DateTime.Now;
            //парсинг данных
            var result = ParserSite.GetStringArray("32009180292");
            purchaseEises.Add(new PurchaseEIS() { MessageJournal = result, Number = 32009180292 });
            using (FileStream fs = new FileStream(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + "\\EIC.json", FileMode.OpenOrCreate))
            {
                string outCache = JsonSerializer.Serialize<PurchaseEIS>(purchaseEises[0]);
                await JsonSerializer.SerializeAsync<PurchaseEIS>(utf8Json: fs, value: purchaseEises[0],
                    options: new JsonSerializerOptions()
                    {
                        WriteIndented = true,
                        AllowTrailingCommas = true
                    });
            }

            //созранение данных
            IsParse = true;
            //if (now.DayOfWeek == timeCheck.DayOfWeek && !IsParse)
            //{
            //    //парсинг данных
            //    var result = FSharp.ParserSite.GetStringArray("32009180292");
            //    purchaseEises.Add(new PurchaseEIS(){MessageJournal = result,Number = 32009180292});
            //    using (FileStream fs = new FileStream(System.Environment.GetFolderPath(System.Environment.SpecialFolder.UserProfile)+"\\EIC.json",FileMode.OpenOrCreate))
            //    {
            //        string outCache = JsonSerializer.Serialize<PurchaseEIS>(purchaseEises[0]);
            //        await JsonSerializer.SerializeAsync<PurchaseEIS>(utf8Json: fs, value: purchaseEises[0],
            //            options: new JsonSerializerOptions()
            //            {
            //                WriteIndented = true,
            //                AllowTrailingCommas = true
            //            });
            //    }

            //    //созранение данных
            //    IsParse = true;
            //}
            //else
            //{
            //    IsParse = false;
            //}
        }

        private void Parser_OnNewData(object arg1, string[] arg2)
        {
            var testresult = arg2;
        }
    }
}