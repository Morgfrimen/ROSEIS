using System;

namespace ModelsDataLib
{
    public class PurchaseEIS
    {
        public string UrlJournal { get; set; } //ссылка на журнал закупки
        public long Number { get; set; } //номер закупки

        public string[] MessageJournal { get; set; }//информация в журнале о закупке
    }
}
