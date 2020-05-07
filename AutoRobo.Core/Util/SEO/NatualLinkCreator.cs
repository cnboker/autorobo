using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web.Caching;
using AutoRobo.WebHelper.StringSearchAlgorithm;
using Newtonsoft.Json;
using Util;
using Newtonsoft.Json.Linq;

namespace AutoRobo.Core.SEO
{
    /// <summary>
    /// 将输入文本增加自然链接
    /// </summary>
    public class NatualLinkCreator : INatualLinkCreator
    {
        private log4net.ILog logger = log4net.LogManager.GetLogger("NatualLinkCreator");
        static private NatualLinkCreator creator = null;

        public static NatualLinkCreator Instance {
            get {
                if (creator == null) {
                    creator = new NatualLinkCreator();
                }
                return creator;
            }
        }

        private NatualLinkCreator() { }
     

        public string Create(string text) {
            return Create(text, LinkStyle.Standard).OutputText;
        }
        /// <summary>
        /// 通过MetaSEO内容输出文本自然链接
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public NLResult Create(string text, LinkStyle ls)
        {
            return CacheExtensions.Data(text, () =>
             {
                 List<SeoMeta> metas = GetMetas();
                 var arrayWords = (from c in metas select c.Keywords)
                   .SelectMany(c => c.Split(',')).Distinct().Where(c => !string.IsNullOrEmpty(c)).ToArray();
                 List<string> matchWords = Match(text, arrayWords);
                 // Console.WriteLine("Keyword='{0}', Index={1}", r.Keyword, r.Index);
                 //logger.Info("find match meta:" + r.Keyword);
                 StringBuilder sb = new StringBuilder(text);
                 matchWords = matchWords.Distinct().ToList();
                 foreach (var x in matchWords)
                 {
                     int firstOccurrence = sb.ToString().IndexOf(x);
                     SeoMeta match = metas.FirstOrDefault(c => c.Keywords.Contains(x));
                     //只替换匹配的第一个
                     if (firstOccurrence > -1)
                     {
                         sb.Replace(x,
                             string.Format("<a href=\"{2}{0}\">{1}</a>", match.Url, x, StringHelper.Domain),
                             firstOccurrence,
                             x.Length
                             );
                     }
                 }
                 NLResult result = new NLResult();
                 result.MatchWords = matchWords;
                 result.OutputText = sb.ToString();                
                 return result;
             });
        }

        /// <summary>
        /// 在host浏览器查找文本中是否包括关键字
        /// </summary>
        /// <param name="fulltext"></param>
        /// <returns></returns>
        private List<string> Match(string fulltext, string[] words)
        {
            //logger.Info(arrayWords);
            // Create search algorithm instance
            IStringSearchAlgorithm searchAlg = new StringSearch();
            searchAlg.Keywords = words;
            StringSearchResult[] results = searchAlg.FindAll(fulltext);           
            List<string> matchWords = new List<string>();
            // Write all results  
            foreach (StringSearchResult r in results)
            {
                matchWords.Add(r.Keyword);                
            }
            return matchWords;
        }

        /// <summary>
        /// 获取网站SEO内容列表
        /// </summary>
        /// <returns></returns>
        private List<SeoMeta> GetMetas()
        {
            List<SeoMeta> seoMetas = new List<SeoMeta>();
            JArray result = CacheExtensions.Data("GetMetas", () =>
            {
                return HttpRequestWapper.GetJsonArray(ConfigurationManager.AppSettings["root"] + "/jsonApi/" + "get_seo_keywords");
            });
            foreach (JObject o in result)
            {
                SeoMeta seo = new SeoMeta();
                seo.Url = o["url"] == null ? "" : o["url"].ToString();
                seo.Keywords = o["seoKeyword"] == null ? "" : o["seoKeyword"].ToString();
                seoMetas.Add(seo);
            }
            return seoMetas;
        }


    }
}
