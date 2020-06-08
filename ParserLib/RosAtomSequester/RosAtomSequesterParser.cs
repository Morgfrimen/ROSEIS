using System.Linq;
using AngleSharp.Dom.Html;

namespace ParserLib.RosAtomSequester
{
    public class RosAtomSequesterParser : IParser<string[]>
    {
        public string[] Parse(IHtmlDocument document)
        {
            var item = document.QuerySelectorAll("td").Where(item => item.ClassName != null && item.ClassName.Contains("journalEvent")).Select(item=>item.TextContent).ToArray();
            return item;
        }
    }
}