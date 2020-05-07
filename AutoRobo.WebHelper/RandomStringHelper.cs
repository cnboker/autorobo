using System;
using System.Collections.Generic;
using System.Text;

namespace Util
{
    public enum RandomType
    {
        //手机号
        Mobile = 0,
        //公司名称
        CompanyName,
        //公司地址
        Address,
        //姓名
        Name,
        //数字
        Digit,
        //字符
        Chars
    }

   

    public class RandomStringHelper
    {
        static private readonly Random _rng = new Random();
        private const string _chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string _digits = "1234567890";
        private const string _zh = @"泰辉新禄公生复东春优庆聚同圣台金鼎全春百顺大东全谦和亚庆顺富鼎旺庆盈捷辉金协捷盈干新禄公公生复东春优庆祥福晶恒隆宝泰洪公厚皇正旺盈复浩进祥通广富泰耀金利干生干优晶升安光盈鼎信祥广春复昌皇谦信禄国富复大合兴宏干全公复盈康吉通进发协昌广台复益千生台永浩台复同福鼎弘顺瑞正如耀盛正进东辉协永多合长恒万晶安多兴元顺厚寿昌兴千本聚元多润庆洪隆盈安宏金大";
        private const string _city = @"厦门 澳门 青岛 大连 南京 杭州 济南 高雄 宁波 苏州 唐山 汕头 长沙 珠海 烟台 东莞 南昌 重庆 福州 成都 长春 西安 沈阳 海口 南宁 太原 郑州 合肥 贵阳 昆明 拉萨 兰州 西宁 银川 乌鲁木齐 呼和浩特 哈尔滨 石家庄";
        private const string _street = @"春风路 幸福街35号 创业路 同志路 北京路 沈阳路 南通广场 上川路";

      

        public static string RandomString(RandomType type, int size) {
            string val = "";
            switch (type) { 
                case RandomType.Address:
                    val = RandomAddress();
                    break;
                case RandomType.Chars:
                    val = RandomString(size);
                    break;
                case RandomType.CompanyName:
                    val = RandomCompanyName(size);
                    break;
                case RandomType.Digit:
                    val = RandomDigits(size);
                    break;
                case RandomType.Mobile:
                    val = RandomMobile();
                    break;
                case RandomType.Name:
                    val = RandomName();
                    break;
                default:
                    break;
            }
            return val;
        }

        public static string RandomString(int size)
        {
            return RandomString(size, _chars);
        }

        public static string RandomDigits(int size)
        {
            return RandomString(size, _digits);
        }

        public static string RandomChinese(int size)
        {
            return RandomString(size, _zh);
        }

        public static string RandomCompanyName(int size) {
            return RandomChinese(size) + "发展有限公司";
        }

        public static string RandomMobile() {
            return "130" + RandomDigits(8);
        }

        public static string RandomAddress()
        {
            var city = _city.Split(" ".ToCharArray());
            var street = _street.Split(" ".ToCharArray());
            return city[_rng.Next(city.Length)] + street[_rng.Next(street.Length)];
        }

        public static string RandomName()
        {
            string xing = "张王李郭宋和田赵黄";
            return xing[_rng.Next(xing.Length)] + RandomString(2, _zh);
        }

        private static string RandomString(int size, string charString)
        {
            char[] buffer = new char[size];

            for (int i = 0; i < size; i++)
            {
                buffer[i] = charString[_rng.Next(charString.Length)];
            }
            return new string(buffer);
        }

    }
}
