using Praktika.Contracts;
using HtmlAgilityPack;
using System;
using System.Reflection.Metadata;
using System.Text;

namespace Praktika.Services
{
    public class ParseService : IParseService
    {
        public List<string> Parse(string url, List<string> selectors,string selectorsType, out int numoflines)
        {
            numoflines = 0;
            List<string> result = new List<string>();
            HtmlDocument document;
            StringBuilder strbuilder = new StringBuilder();
            

            List<HtmlNodeCollection> collections = new List<HtmlNodeCollection>();
            List<List<HtmlNode>> csscollection = new List<List<HtmlNode>>();

            var web = new HtmlWeb();
            try
            {
                document = web.Load(url);
            }
            catch (HtmlWebException ex) {
                result.Add(ex.Message);
                return result;
            }



            switch (selectorsType){

                case "XPath":
                    for (int s = 0; s < selectors.Count; s++)
                    {
                        var elements = document.DocumentNode.SelectNodes(selectors[s]);
                        collections.Add(elements);

                        if (elements == null)
                        {
                            result.Add("Элементов с таким селектором не найдено");
                            return result;
                        }
                    }

                    for (int i = 0; i < collections[0].Count; i++)
                    {
                        for (int j = 0; j < collections.Count; j++)
                        {
                            HtmlNode? node=null;
                            try
                            {
                                 node = collections[j][i];
                            }
                            catch (IndexOutOfRangeException ex) { strbuilder.Append("Null").Append(" , "); }

                            string contentofnode = HtmlEntity.DeEntitize(node.InnerText);

                            if (contentofnode == String.Empty)
                            {
                                strbuilder.Append("Нет InnerText'а").Append(" , ");
                                //return result;
                            }
                            else
                            {
                                strbuilder.Append(contentofnode).Append(" , ");
                            }
                        }
                        strbuilder.AppendLine();
                        result.Add(strbuilder.ToString());
                    }
                    return result;
                    break;

                case "CSSselector":
                    for (int s = 0; s < selectors.Count; s++)
                    {
                        List<HtmlNode> elements =(List<HtmlNode>)document.DocumentNode.QuerySelectorAll(selectors[s]);
                        csscollection.Add(elements);

                        if (elements == null)
                        {
                            result.Add("Элементов с таким селектором не найдено");
                            return result;
                        }
                    }

                    for (int i = 0; i < csscollection[0].Count; i++)
                    {
                        for (int j = 0; j < csscollection.Count; j++)
                        {
                            HtmlNode? node = null;
                            try
                            {
                                node = collections[j][i];
                            }
                            catch (IndexOutOfRangeException ex) { strbuilder.Append("Null").Append(" , "); }

                            string contentofnode = HtmlEntity.DeEntitize(node.InnerText);

                            if (contentofnode == String.Empty  )//Условие поменять два раза возможно будет добавляться строка для одного и того же нода
                            {
                                strbuilder.Append("Нет InnerText'а").Append(" , ");
                                //return result;
                            }
                            else
                            {
                                strbuilder.Append(contentofnode).Append(" , ");
                            }
                        }
                        //strbuilder.AppendLine();
                        result.Add(strbuilder.ToString());
                        strbuilder.Clear();
                    }
                    return result;


                    break;


            }
            return result;

            

            



            
        }
    }
}
