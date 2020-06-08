using System;
using System.Threading.Tasks;
using AngleSharp.Parser.Html;

namespace ParserLib
{
    public class ParserWorker<T> where T : class
    {
        private IParser<T> parser;
        private IParserSettings parserSettings;
        private HtmlLoaderRosAtom loader;

        private bool _isActibe;

        public IParser<T> Parser
        {
            get => parser;
            set => parser = value;
        }

        public event Action<object, T> OnNewData;
        public event Action<object> OnCompleted; 

        public IParserSettings ParserSettings
        {
            get => parserSettings;
            set
            {
                parserSettings = value;
                loader = new HtmlLoaderRosAtom(value);
            }
        }

        public bool IsActibe => _isActibe;


        public ParserWorker(IParser<T> parser)
        {
            this.Parser = parser;
        }

        public ParserWorker(IParser<T> parser, IParserSettings parserSettings) : this(parser)
        {
            this.ParserSettings = parserSettings;
        }

        public void Start()
        {
            _isActibe = true;
            Worker();
        }

        public void Abort()
        {
            _isActibe = false;
        }


        private async void Worker()
        {
            if (!IsActibe)
            {
                OnCompleted?.Invoke(this);
                return;
            }
                
            var source = await loader.GetSourseByPage(parserSettings.PostFix);
            var domParser = new HtmlParser();
            var document = await domParser.ParseAsync(source);

            var result = parser.Parse(document);

            OnNewData?.Invoke(this,result);

            OnCompleted?.Invoke(this);
            _isActibe = false;
        }
    }
}