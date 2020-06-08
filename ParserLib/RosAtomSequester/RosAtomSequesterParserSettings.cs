namespace ParserLib.RosAtomSequester
{
    public class RosAtomSequesterParserSettings : IParserSettings
    {
        //Ссылки под конкретную организацию одни и те же, меняется лишь номер договора
        public string BaseUrl { get; set; } =
            "https://zakupki.gov.ru/223/purchase/public/purchase/info/journal.html?regNumber={num}";

        //Номер закупки
        public string PostFix { get; set; }//тест
    }
}