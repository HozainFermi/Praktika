namespace Praktika.Contracts
{
    public interface IParseService
    {
        public List<string> Parse(string ursl, List<string> selectors,string selectorsType , out int numoflines);

    }
}
