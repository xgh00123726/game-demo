using System.Text.RegularExpressions;
using GameBase.XCard;


namespace GameBase.XCard
{
    /* @FUNC : 解析游戏中的文本，并根据语言替换版本
     * @FEATURE : 可以通过<keyword-{keyword_name}/></keyword-{keyword_name}>的方式将带有关键字标签的文本替换为对应语言的关键字名
     * @FEATURE : 可以通过<content-{num}/>的方式将字符表中对应行的内容嵌入字符串
     */
    public class XCDescribeParser
    {
        static XCDescribeParser()
        {

        }

        public static string ToTextMeshContent(string s)
        {
            /* @FUNC : 解析文本中的关键字，将关键字标签替换为对应语言版本的文本
             * 
             */
            string result = Regex.Replace(s, @"<(keyword-[a-zA-Z_]+[a-zA-Z0-9]*)>([\s\S]*)</\1>", new MatchEvaluator((Match match) =>
            {
                string tag = match.Groups[1].Value;
                string content = match.Groups[2].Value;
                if (content != null && content.Length > 0)
                {
                    return content;
                }
                return XCKeyWord.Content(tag, Settings.Lang);
            }));

            result = Regex.Replace(result, @"<(content-)([0-9]+)>", new MatchEvaluator((Match match) =>
            {
                return XCContent.Get(int.Parse(match.Groups[2].Value));
            }));

            return result;
        }
    }

}