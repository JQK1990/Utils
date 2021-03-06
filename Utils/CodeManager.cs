﻿using System;
using System.Drawing;
using System.Security.Cryptography;
using System.Web;

namespace Utils
{
    
    /// <summary>
    /// 验证码类
    /// </summary>
    public class Rand
    {
        #region 生成随机数字

        /// <summary>
        /// 生成随机数字
        /// </summary>
        public static string Number(int length)
        {
            return Number(length, false);
        }

        /// <summary>
        /// 生成随机数字
        /// </summary>
        /// <param name="length">生成长度</param>
        /// <param name="sleep">是否要在生成前将当前线程阻止以避免重复</param>
        public static string Number(int length, bool sleep)
        {
            if (sleep) System.Threading.Thread.Sleep(3);
            string result = "";
            var random = new Random();
            for (int i = 0; i < length; i++)
            {
                result += random.Next(10).ToString();
            }
            return result;
        }
        #endregion

        #region 生成随机字母与数字

        /// <summary>
        /// 生成随机字母与数字
        /// </summary>
        /// <param name="length"></param>
        public static string Str(int length)
        {
            return Str(length, false);
        }

        /// <summary>
        /// 生成随机字母与数字
        /// </summary>
        /// <param name="length">生成长度</param>
        /// <param name="sleep">是否要在生成前将当前线程阻止以避免重复</param>
        public static string Str(int length, bool sleep)
        {
            if (sleep) System.Threading.Thread.Sleep(3);
            char[] pattern = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            string result = "";
            int n = pattern.Length;
            var random = new Random(~unchecked((int)DateTime.Now.Ticks));
            for (int i = 0; i < length; i++)
            {
                int rnd = random.Next(0, n);
                result += pattern[rnd];
            }
            return result;
        }
        #endregion

        #region 生成随机纯字母随机数

        /// <summary>
        /// 生成随机纯字母随机数
        /// </summary>
        /// <param name="length"></param>
        public static string Str_char(int length)
        {
            return Str_char(length, false);
        }

        /// <summary>
        /// 生成随机纯字母随机数
        /// </summary>
        /// <param name="length">生成长度</param>
        /// <param name="sleep">是否要在生成前将当前线程阻止以避免重复</param>
        public static string Str_char(int length, bool sleep)
        {
            if (sleep) System.Threading.Thread.Sleep(3);
            char[] pattern = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            string result = "";
            int n = pattern.Length;
            var random = new Random(~unchecked((int)DateTime.Now.Ticks));
            for (int i = 0; i < length; i++)
            {
                int rnd = random.Next(0, n);
                result += pattern[rnd];
            }
            return result;
        }
        #endregion
    }

    /// <summary>
    /// 验证图片类
    /// </summary>
    public class YzmHelper
    {
        #region 私有字段
        private readonly string _text;
        private Bitmap _image;
        private int _letterCount = 4;   //验证码位数
        private int letterWidth = 16;  //单个字体的宽度范围
        private int letterHeight = 20; //单个字体的高度范围
        private static readonly byte[] Randb = new byte[4];
        private static readonly RNGCryptoServiceProvider Rand = new RNGCryptoServiceProvider();
        private readonly Font[] _fonts = 
    {
       new Font(new FontFamily("Times New Roman"),10 +Next(1),FontStyle.Regular),
       new Font(new FontFamily("Georgia"), 10 + Next(1),FontStyle.Regular),
       new Font(new FontFamily("Arial"), 10 + Next(1),FontStyle.Regular),
       new Font(new FontFamily("Comic Sans MS"), 10 + Next(1),FontStyle.Regular)
    };
        #endregion

        #region 公有属性
        /// <summary>
        /// 验证码
        /// </summary>
        public string Text
        {
            get { return _text; }
        }

        /// <summary>
        /// 验证码图片
        /// </summary>
        public Bitmap Image
        {
            get { return _image; }
        }
        #endregion

        #region 构造函数
        public YzmHelper(int letterCount)
        {
            _letterCount = letterCount;
            HttpContext.Current.Response.Expires = 0;
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
            HttpContext.Current.Response.AddHeader("pragma", "no-cache");
            HttpContext.Current.Response.CacheControl = "no-cache";
            _text = Utils.Rand.Number(4);
            CreateImage();
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 获得下一个随机数
        /// </summary>
        /// <param name="max">最大值</param>
        private static int Next(int max)
        {
            Rand.GetBytes(Randb);
            int value = BitConverter.ToInt32(Randb, 0);
            value = value % (max + 1);
            if (value < 0) value = -value;
            return value;
        }

        /// <summary>
        /// 获得下一个随机数
        /// </summary>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        private static int Next(int min, int max)
        {
            int value = Next(max - min) + min;
            return value;
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 绘制验证码
        /// </summary>
        public void CreateImage()
        {
            int intImageWidth = _text.Length * letterWidth;
            Bitmap image = new Bitmap(intImageWidth, letterHeight);
            Graphics g = Graphics.FromImage(image);
            g.Clear(Color.White);
            for (int i = 0; i < 2; i++)
            {
                int x1 = Next(image.Width - 1);
                int x2 = Next(image.Width - 1);
                int y1 = Next(image.Height - 1);
                int y2 = Next(image.Height - 1);
                g.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
            }
            var _x = -12;
            for (int intIndex = 0; intIndex < _text.Length; intIndex++)
            {
                _x += Next(12, 16);
                var y = Next(-2, 2);
                string strChar = _text.Substring(intIndex, 1);
                strChar = Next(1) == 1 ? strChar.ToLower() : strChar.ToUpper();
                Brush newBrush = new SolidBrush(GetRandomColor());
                Point thePos = new Point(_x, y);
                g.DrawString(strChar, _fonts[Next(_fonts.Length - 1)], newBrush, thePos);
            }
            for (int i = 0; i < 10; i++)
            {
                var x = Next(image.Width - 1);
                var y = Next(image.Height - 1);
                image.SetPixel(x, y, Color.FromArgb(Next(0, 255), Next(0, 255), Next(0, 255)));
            }
            image = TwistImage(image, true, Next(1, 3), Next(4, 6));
            g.DrawRectangle(new Pen(Color.LightGray, 1), 0, 0, intImageWidth - 1, (letterHeight - 1));
            _image = image;
        }

        /// <summary>
        /// 字体随机颜色
        /// </summary>
        public Color GetRandomColor()
        {
            Random randomNumFirst = new Random((int)DateTime.Now.Ticks);
            System.Threading.Thread.Sleep(randomNumFirst.Next(50));
            Random randomNumSencond = new Random((int)DateTime.Now.Ticks);
            int intRed = randomNumFirst.Next(180);
            int intGreen = randomNumSencond.Next(180);
            int intBlue = (intRed + intGreen > 300) ? 0 : 400 - intRed - intGreen;
            intBlue = (intBlue > 255) ? 255 : intBlue;
            return Color.FromArgb(intRed, intGreen, intBlue);
        }

        /// <summary>
        /// 正弦曲线Wave扭曲图片
        /// </summary>
        /// <param name="srcBmp">图片路径</param>
        /// <param name="bXDir">如果扭曲则选择为True</param>
        /// <param name="dMultValue"></param>
        /// <param name="dPhase">波形的起始相位,取值区间[0-2*PI)</param>
        public Bitmap TwistImage(Bitmap srcBmp, bool bXDir, double dMultValue, double dPhase)
        {
            double PI = 6.283185307179586476925286766559;
            Bitmap destBmp = new Bitmap(srcBmp.Width, srcBmp.Height);
            Graphics graph = Graphics.FromImage(destBmp);
            graph.FillRectangle(new SolidBrush(Color.White), 0, 0, destBmp.Width, destBmp.Height);
            graph.Dispose();
            double dBaseAxisLen = bXDir ? destBmp.Height : destBmp.Width;
            for (int i = 0; i < destBmp.Width; i++)
            {
                for (int j = 0; j < destBmp.Height; j++)
                {
                    var dx = bXDir ? (PI * j) / dBaseAxisLen : (PI * i) / dBaseAxisLen;
                    dx += dPhase;
                    double dy = Math.Sin(dx);
                    var nOldX = bXDir ? i + (int)(dy * dMultValue) : i;
                    var nOldY = bXDir ? j : j + (int)(dy * dMultValue);

                    Color color = srcBmp.GetPixel(i, j);
                    if (nOldX >= 0 && nOldX < destBmp.Width
                     && nOldY >= 0 && nOldY < destBmp.Height)
                    {
                        destBmp.SetPixel(nOldX, nOldY, color);
                    }
                }
            }
            srcBmp.Dispose();
            return destBmp;
        }
        #endregion
    }

}
