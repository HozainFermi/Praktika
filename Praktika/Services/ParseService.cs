using Praktika.Contracts;
using HtmlAgilityPack;
using System;
using System.Reflection.Metadata;
using System.Text;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;

namespace Praktika.Services
{
    public class ParseService : IParseService
    {
        public List<string> Parse(Uri url, List<string> selectors,string selectorsType)
        {
            //numoflines = 0;
            List<string> result = new List<string>();
            HtmlDocument document=new HtmlDocument();

            StringBuilder strbuilder = new StringBuilder();

            //int counter = 0;

            List<HtmlNodeCollection> collections = new List<HtmlNodeCollection>();
            List<List<HtmlNode>> csscollection = new List<List<HtmlNode>>();


            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--headless");
            options.AddArgument("--no-sandbox");
            options.AddArgument("--disable-dev-shm-usage");
            options.AddArgument("--window-size=1920,1080");


            var web = new HtmlWeb();
            try
            {
                // Создаем экземпляр браузера Selenium
                var driver = new ChromeDriver(options);

                // Загружаем страницу
                driver.Navigate().GoToUrl(url);

                // Ждем, пока страница полностью загрузится
                driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(3);
                

                // Получаем HTML-код страницы
                string html = driver.PageSource;
                document.LoadHtml(html);  //web.Load(url);
                driver.Quit();
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
                            //return result;
                        }
                    }
                    
                    for (int i = 0; i < collections[0].Count; i++)
                    {
                        for (int j = 0; j < collections.Count; j++)
                        {
                            //counter = j;
                            HtmlNode? node=null;
                            try
                            {
                                 node = collections[j][i];
                            }
                            catch (IndexOutOfRangeException ex) { break;/*strbuilder.Append("Null").Append(" , ");*/ }

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
                        //strbuilder.AppendLine();
                        result.Add(strbuilder.ToString());
                        strbuilder.Clear();
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
