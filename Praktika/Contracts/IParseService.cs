using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Praktika.Contracts
{
    public interface IParseService
    {
        public List<string> Parse(Uri ursl, List<string> selectors, string selectorsType);
        
        

    }
}
