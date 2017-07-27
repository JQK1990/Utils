using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Utils.DateManager
{
    public class DateHelper
    {
        #region 当前时间
        /// <summary>
        /// 返回当前时间的标准日期格式
        /// </summary>
        /// <returns>yyyy-MM-dd</returns>
        public static string GetDate()
        {
            return DateTime.Now.ToString("yyyy-MM-dd");
        }
        /// <summary>
        /// 返回当前时间的标准时间格式string
        /// </summary>
        /// <returns>HH:mm:ss</returns>
        public static string GetTime()
        {
            return DateTime.Now.ToString("HH:mm:ss");
        }
        /// <summary>
        /// 返回当前时间的标准时间格式string
        /// </summary>
        /// <returns>yyyy-MM-dd HH:mm:ss</returns>
        public static string GetDateTime()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
        /// <summary>
        /// 返回相对于当前时间的相对天数
        /// </summary>
        /// <param name="relativeday">增加的天数</param>
        /// <returns>相对天数</returns>
        public static string GetDateTimeOfDay(int relativeday)
        {
            return DateTime.Now.AddDays(relativeday).ToString("yyyy-MM-dd HH:mm:ss");
        }
        /// <summary>
        /// 返回相对于当前时间的相对分钟数
        /// </summary>
        /// <param name="relativeday">增加的分钟数</param>
        /// <returns>相对分钟数</returns>
        public static string GetDateTimeOfMinutes(int relativeday)
        {
            return DateTime.Now.AddMinutes(relativeday).ToString("yyyy-MM-dd HH:mm:ss");
        }
        /// <summary>
        /// 返回当前时间的标准时间格式
        /// </summary>
        /// <returns>yyyy-MM-dd HH:mm:ss:fffffff</returns>
        public static string GetDateTimeF()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fffffff");
        }
        #endregion

        #region 时间转换


        /// <summary>
        /// 返回指定日期格式
        /// </summary>
        /// <param name="datetimestr">需要转换的时间</param>
        /// <param name="replacestr">指定格式</param>
        /// <returns>转换后的时间</returns>
        public static string GetDate(string datetimestr, string replacestr)
        {
            if (datetimestr == null) return replacestr;
            if (datetimestr.Equals("")) return replacestr;
            try
            {
                datetimestr = Convert.ToDateTime(datetimestr).ToString("yyyy-MM-dd").Replace("1900-01-01", replacestr);
            }
            catch
            {
                return replacestr;
            }
            return datetimestr;
        }
        /// <summary>
        /// 转换时间为unix时间戳
        /// </summary>
        /// <param name="date">需要传递UTC时间,避免时区误差,例:DataTime.UTCNow</param>
        /// <returns></returns>
        public static double ConvertToUnixTimestamp(DateTime date)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            TimeSpan diff = date - origin;
            return Math.Floor(diff.TotalSeconds);
        }
        /// <summary>
        /// 将8位日期型整型数据转换为日期字符串数据
        /// 默认为英文格式
        /// </summary>
        /// <param name="date">整型日期</param>
        /// <returns></returns>
        public static string FormatDate(int date)
        {
            return FormatDate(date, false);
        }
        /// <summary>
        /// 将8位日期型整型数据转换为日期字符串数据
        /// </summary>
        /// <param name="date">整型日期</param>
        /// <param name="chnType">是否以中文年月日输出</param>
        /// <returns></returns>
        public static string FormatDate(int date, bool chnType)
        {
            string dateStr = date.ToString();
            if (date <= 0 || dateStr.Length != 8) return dateStr;
            if (chnType) return dateStr.Substring(0, 4) + "年" + dateStr.Substring(4, 2) + "月" + dateStr.Substring(6) + "日";
            return dateStr.Substring(0, 4) + "-" + dateStr.Substring(4, 2) + "-" + dateStr.Substring(6);
        }

        #endregion

        #region 标准时间

        /// <summary>
        /// 返回标准时间
        /// </summary>
        /// <param name="fDateTime">转换时间</param>
        /// <param name="formatStr">转换格式</param>
        /// <returns>转换后的时间</returns>
        public static string GetStandardDateTime(string fDateTime, string formatStr)
        {
            if (fDateTime == "0000-0-0 0:00:00") return fDateTime;
            DateTime time;
            if (DateTime.TryParse(fDateTime, out time))
            {
                return time.ToString(formatStr);
            }
            else
            {
                return "N/A";
            }
        }
        /// <summary>
        /// 返回标准时间 yyyy-MM-dd HH:mm:ss
        /// </summary>
        /// <param name="fDateTime">转换时间</param>
        /// <returns>yyyy-MM-dd HH:mm:ss</returns>
        public static string GetStandardDateTime(string fDateTime)
        {
            return GetStandardDateTime(fDateTime, "yyyy-MM-dd HH:mm:ss");
        }
        /// <summary>
        /// 返回标准时间 yyyy-MM-dd
        /// </summary>
        /// <param name="fDate">转换时间</param>
        /// <returns>yyyy-MM-dd</returns>
        public static string GetStandardDate(string fDate)
        {
            return GetStandardDateTime(fDate, "yyyy-MM-dd");
        }

        #endregion

        #region 返回时间差
        public static string DateDiff(DateTime dateTime1, DateTime dateTime2)
        {
            string dateDiff = null;
            try
            {
                //TimeSpan ts1 = new TimeSpan(DateTime1.Ticks);
                //TimeSpan ts2 = new TimeSpan(DateTime2.Ticks);
                //TimeSpan ts = ts1.Subtract(ts2).Duration();
                TimeSpan ts = dateTime2 - dateTime1;
                if (ts.Days >= 1)
                {
                    dateDiff = dateTime1.Month + "月" + dateTime1.Day + "日";
                }
                else
                {
                    if (ts.Hours > 1)
                    {
                        dateDiff = ts.Hours + "小时前";
                    }
                    else
                    {
                        dateDiff = ts.Minutes + "分钟前";
                    }
                }
            }
            catch
            {
                // ignored
            }
            return dateDiff;
        }
        #endregion

        #region 获得两个日期的间隔
        /// <summary>
        /// 获得两个日期的间隔
        /// </summary>
        /// <param name="dateTime1">日期一。</param>
        /// <param name="dateTime2">日期二。</param>
        /// <returns>日期间隔TimeSpan。</returns>
        public static TimeSpan DateDiff2(DateTime dateTime1, DateTime dateTime2)
        {
            TimeSpan ts1 = new TimeSpan(dateTime1.Ticks);
            TimeSpan ts2 = new TimeSpan(dateTime2.Ticks);
            TimeSpan ts = ts1.Subtract(ts2).Duration();
            return ts;
        }
        #endregion

        #region 得到随机日期
        /// <summary>
        /// 得到随机日期
        /// </summary>
        /// <param name="time1">起始日期</param>
        /// <param name="time2">结束日期</param>
        /// <returns>间隔日期之间的 随机日期</returns>
        public static DateTime GetRandomTime(DateTime time1, DateTime time2)
        {
            Random random = new Random();
            DateTime minTime;
            DateTime maxTime = new DateTime();

            TimeSpan ts = new TimeSpan(time1.Ticks - time2.Ticks);

            // 获取两个时间相隔的秒数
            double dTotalSecontds = ts.TotalSeconds;
            int iTotalSecontds;

            if (dTotalSecontds > int.MaxValue)
            {
                iTotalSecontds = Int32.MaxValue;
            }
            else if (dTotalSecontds < int.MinValue)
            {
                iTotalSecontds = Int32.MinValue;
            }
            else
            {
                iTotalSecontds = (int)dTotalSecontds;
            }

            if (iTotalSecontds > 0)
            {
                minTime = time2;
                maxTime = time1;
            }
            else if (iTotalSecontds < 0)
            {
                minTime = time1;
                maxTime = time2;
            }
            else
            {
                return time1;
            }

            int maxValue = iTotalSecontds;

            if (iTotalSecontds <= Int32.MinValue)
                maxValue = Int32.MinValue + 1;

            int i = random.Next(Math.Abs(maxValue));

            return minTime.AddSeconds(i);
        }
        #endregion

        #region 时间格式检测

        /// <summary>
        /// 是否常规时间
        /// </summary>
        public static bool IsTime(string timeval)
        {
            return Regex.IsMatch(timeval, @"^((([0-1]?[0-9])|(2[0-3])):([0-5]?[0-9])(:[0-5]?[0-9])?)$");
        }
        /// <summary>
        /// 判断字符串是否是yy-mm-dd字符串
        /// </summary>
        /// <param name="str">待判断字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsDateString(string str)
        {
            return Regex.IsMatch(str, @"(\d{4})-(\d{1,2})-(\d{1,2})");
        }

        #endregion

        #region 年

        /// <summary>
        /// 判断某年是否为闰年
        /// </summary>
        /// <param name="year">需要计算的年份</param>
        /// <returns>是否为闰年</returns>
        public static bool YearIsLeap(int year)
        {
            if (year <= 0001 || year >= 9999) return false;
            return DateTime.IsLeapYear(year);
        }
        /// <summary>
        /// 计算某年共有多少天
        /// </summary>
        /// <param name="year">需要计算的年份</param>
        /// <returns>共有多少天</returns>
        public static int YearOfTotalDay(int year)
        {
            if (year <= 0001 || year >= 9999) return -1;
            return YearOfLastDay(year).DayOfYear;
        }
        /// <summary>
        /// 获得某年第一天的日期
        /// </summary>
        /// <param name="year">需要计算的年份</param>
        /// <returns>第一天日期</returns>
        public static DateTime YearOfFirstDay(int year)
        {
            if (year <= 0001 || year >= 9999) return DateTime.MaxValue;
            DateTime result;
            DateTime.TryParse(string.Format("{0}-01-01", year), out result);
            return result;
        }
        /// <summary>
        /// 获得某年最后一天的日期
        /// </summary>
        /// <param name="year">需要计算的年份</param>
        /// <returns>最后一天日期</returns>
        public static DateTime YearOfLastDay(int year)
        {
            if (year <= 0001 || year >= 9999) return DateTime.MaxValue;
            DateTime result;
            DateTime.TryParse(string.Format("{0}-12-31", year), out result);
            return result;
        }

        #endregion

        #region 月

        /// <summary>
        /// 获得当前年当前月的总天数
        /// </summary>
        /// <returns>共有多少天</returns>
        public static int MonthOfTotalDay()
        {
            DateTime now = DateTime.Now;
            return DateTime.DaysInMonth(now.Year, now.Month);
        }
        /// <summary>
        /// 获得某年某月的总天数
        /// </summary>
        /// <param name="year">需要计算的年份</param>
        /// <param name="month">需要计算的月份</param>
        /// <returns>共有多少天</returns>
        public static int MonthOfTotalDay(int year, int month)
        {
            if (year <= 0001 || year >= 9999) return -1;
            if (month < 1 || month > 12) return -1;
            return DateTime.DaysInMonth(year, month);
        }
        /// <summary>
        /// 获得当前年当前月第一天的日期
        /// </summary>
        /// <returns>第一天日期</returns>
        public static DateTime MonthOfFirstDay()
        {
            DateTime now = DateTime.Now;
            return MonthOfFirstDay(now.Year, now.Month);
        }
        /// <summary>
        /// 获得某年某月第一天的日期
        /// </summary>
        /// <param name="year">需要计算的年份</param>
        /// <param name="month">需要计算的月份</param>
        /// <returns>第一天日期</returns>
        public static DateTime MonthOfFirstDay(int year, int month)
        {
            if (year <= 0001 || year >= 9999) return DateTime.MaxValue;
            if (month < 1 || month > 12) return DateTime.MaxValue;
            DateTime result;
            DateTime.TryParse(string.Format("{0}-{1}-01", year, month), out result);
            return result;
        }
        /// <summary>
        /// 获得当前年当前月最后一天的日期
        /// </summary>
        /// <returns>最后一天日期</returns>
        public static DateTime MonthOfLastDay()
        {
            DateTime now = DateTime.Now;
            return MonthOfLastDay(now.Year, now.Month);
        }
        /// <summary>
        /// 获得某年某月最后一天的日期
        /// </summary>
        /// <param name="year">需要计算的年份</param>
        /// <param name="month">需要计算的月份</param>
        /// <returns>最后一天日期</returns>
        public static DateTime MonthOfLastDay(int year, int month)
        {
            if (year <= 0001 || year >= 9999) return DateTime.MaxValue;
            if (month < 1 || month > 12) return DateTime.MaxValue;
            DateTime result;
            string tmpString = string.Format("{0}-{1}-{2}", year, month, DateTime.DaysInMonth(year, month));
            DateTime.TryParse(tmpString, out result);
            return result;
        }

        #endregion

        #region 周

        /// <summary>
        /// 周（星期）信息实体类
        /// </summary>
        [Serializable]
        public class WeekInfo
        {
            /// <summary>
            /// 周（星期）信息实体类
            /// </summary>
            public WeekInfo()
            {
                Number = 0;
                BeginDate = DateTime.MinValue;
                EndDate = DateTime.MaxValue;
            }
            /// <summary>
            /// 周数
            /// </summary>
            public int Number { get; set; }
            /// <summary>
            /// 开始时间
            /// </summary>
            public DateTime BeginDate { get; set; }
            /// <summary>
            /// 结束时间
            /// </summary>
            public DateTime EndDate { get; set; }
            /// <summary>
            /// 输出第几周和日期间隔
            /// <para>默认格式：</para>
            /// <para>第几周，从2012年12月21日至2112年12月21日。</para>
            /// </summary>
            /// <returns>第几周和日期间隔</returns>
            public override string ToString()
            {
                return string.Format("第{0}周，从{1:yyyy年MM月dd日}至{2:yyyy年MM月dd日}。", Number, BeginDate, EndDate);
            }
            /// <summary>
            /// 输出第几周
            /// </summary>
            /// <param name="isFill">少于2位时是否补零</param>
            /// <returns>第几周</returns>
            public string GetWeekString(bool isFill)
            {
                string format = "第{0}周";
                if (isFill && Number < 10) format = "第0{0}周";
                return string.Format(format, Number);
            }
            /// <summary>
            /// 输出日期间隔
            /// </summary>
            /// <param name="inputString">输出格式化字符串</param>
            /// <param name="dateString">日期格式化字符串</param>
            /// <returns>日期间隔</returns>
            public string GetDateString(string inputString, string dateString)
            {
                if (string.IsNullOrWhiteSpace(inputString) || string.IsNullOrWhiteSpace(dateString)) return null;
                try
                {
                    return string.Format(inputString, BeginDate.ToString(dateString), EndDate.ToString(dateString));
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
        }
        /// <summary>
        /// 获得年度第一个周一的日期
        /// </summary>
        /// <param name="year">需要计算的年份</param>
        /// <param name="jumpYear">年度第一周是否跳过跨年的周数</param>
        /// <param name="offset">年度第一个周一偏移量</param>
        /// <returns>年度第一个周一的日期</returns>
        public static DateTime WeekOfFirstDay(int year, bool jumpYear, out int offset)
        {
            DateTime firstDate = YearOfFirstDay(year); //该年的第一天
            int firstWeek = (int)firstDate.DayOfWeek; //该年的第一天是周几
            offset = 0; //周一偏移量
            switch (firstWeek)
            {
                case 1: //星期一
                    offset = 0;
                    break;
                case 2: //星期二
                    offset = jumpYear ? 6 : -1;
                    break;
                case 3: //星期三
                    offset = jumpYear ? 5 : -2;
                    break;
                case 4: //星期四
                    offset = jumpYear ? 4 : -3;
                    break;
                case 5: //星期五
                    offset = jumpYear ? 3 : -4;
                    break;
                case 6: //星期六
                    offset = jumpYear ? 2 : -5;
                    break;
                case 0: //星期日
                    offset = jumpYear ? 1 : -6;
                    break;
            }
            firstDate = firstDate.AddDays(offset);
            return firstDate;
        }
        /// <summary>
        /// 获得年度周（星期）信息实体集合列表
        /// </summary>
        /// <param name="year">需要计算的年份</param>
        /// <param name="jumpYear">年度第一周是否跳过跨年的周数</param>
        /// <returns>周（星期）信息实体集合列表对象</returns>
        public static IList<WeekInfo> WeekOfList(int year, bool jumpYear)
        {
            IList<WeekInfo> weekList = new List<WeekInfo>();
            if (year <= 0001 || year >= 9999) return weekList;
            int offset;
            DateTime firstDate = WeekOfFirstDay(year, jumpYear, out offset); //年度周一的日期
            int index = 1;
            while (true)
            {
                if (index > 54) break;
                var weekInfo = new WeekInfo
                {
                    Number = index,
                    BeginDate = firstDate,
                    EndDate = firstDate.AddDays(6)
                };
                //周一
                //周日
                weekList.Add(weekInfo);
                firstDate = firstDate.AddDays(7); //下周
                if (jumpYear)
                {
                    if (firstDate.Year != year) break;
                }
                else
                {
                    if (firstDate.AddDays(6).Year != year) break;
                }
                index++;
            }
            return weekList;
        }
        /// <summary>
        /// 获得某年第某周的开始日期和结束日期
        /// </summary>
        /// <param name="year">需要计算的年份</param>
        /// <param name="weekNumber">需要计算的周数</param>
        /// <param name="jumpYear">年度第一周是否跳过跨年的周数</param>
        /// <param name="weekBeginDate">开始日期</param>
        /// <param name="weekEndDate">结束日期</param>
        public static void WeekOfDate(int year, int weekNumber, bool jumpYear, out DateTime weekBeginDate, out DateTime weekEndDate)
        {
            weekBeginDate = DateTime.MinValue;
            weekEndDate = DateTime.MaxValue;
            if (year <= 0001 || year >= 9999 || weekNumber < 1 || weekNumber > 54) return;
            int offset;
            DateTime firstDate = WeekOfFirstDay(year, jumpYear, out offset); //年度周一的日期
            firstDate = firstDate.AddDays((weekNumber - 1) * 7);
            weekBeginDate = firstDate;
            weekEndDate = firstDate.AddDays(6);
        }
        /// <summary>
        /// 获得某个日期属于某年的第几周
        /// </summary>
        /// <param name="currentDate">需要计算的日期</param>
        /// <param name="jumpYear">年度第一周是否跳过跨年的周数</param>
        /// <returns>某年的第几周</returns>
        public static int WeekOfCurrent(DateTime currentDate, bool jumpYear)
        {
            if (DateTime.MinValue == currentDate || DateTime.MaxValue == currentDate) return 0;
            int result, offset;
            DateTime firstDate = WeekOfFirstDay(currentDate.Year, jumpYear, out offset); //年度周一的日期
            int firstWeek = (int)firstDate.DayOfWeek; //该年的第一天是周几
            int currentDay = currentDate.DayOfYear;
            if (offset > 0)
            {
                currentDay += offset;
            }
            else
            {
                currentDay -= offset;
            }
            int remainderDay = currentDay - (7 - firstWeek);
            if (remainderDay < 1)
            {
                result = 1;
            }
            else
            {
                result = remainderDay / 7;
                if (remainderDay % 7 != 0) result++;
                result++;
            }
            return result;
        }
        /// <summary>
        /// 统计一段时间内有多少个星期几
        /// </summary>
        /// <param name="beginDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="weekNumber">星期几</param>
        /// <returns>多少个星期几</returns>
        public static int WeekOfTotalWeeks(DateTime beginDate, DateTime endDate, DayOfWeek weekNumber)
        {
            TimeSpan dayTotal = new TimeSpan(endDate.Ticks - beginDate.Ticks);
            int result = (int)dayTotal.TotalDays / 7;
            double iLen = dayTotal.TotalDays % 7;
            for (int i = 0; i <= iLen; i++)
            {
                if (beginDate.AddDays(i).DayOfWeek == weekNumber)
                {
                    result++;
                    break;
                }
            }
            return result;
        }

        #endregion

        #region 季度

        /// <summary>
        /// 计算当前月属于第几季度
        /// </summary>
        /// <returns>当前年第几季度</returns>
        public static int QuarterOfCurrent()
        {
            return QuarterOfCurrent(DateTime.Now.Month);
        }
        /// <summary>
        /// 计算某个月属于第几季度
        /// </summary>
        /// <param name="month">需要计算的月份</param>
        /// <returns>某年第几季度</returns>
        public static int QuarterOfCurrent(int month)
        {
            if (month < 1 || month > 12) return -1;
            return (month - 1) / 3 + 1;
        }
        /// <summary>
        /// 获得当前年当前季度的开始日期和结束日期
        /// </summary>
        /// <param name="quarterBeginDate">开始日期</param>
        /// <param name="quarterEndDate">结束日期</param>
        public static void QuarterOfDate(out DateTime quarterBeginDate, out DateTime quarterEndDate)
        {
            int quarter = QuarterOfCurrent(DateTime.Now.Month);
            QuarterOfDate(DateTime.Now.Year, quarter, out quarterBeginDate, out quarterEndDate);
        }
        /// <summary>
        /// 获得指定日期所在季度的开始日期和结束日期
        /// </summary>
        /// <param name="fromDate">需要计算的日期</param>
        /// <param name="quarterBeginDate">开始日期</param>
        /// <param name="quarterEndDate">结束日期</param>
        public static void QuarterOfDate(DateTime fromDate, out DateTime quarterBeginDate, out DateTime quarterEndDate)
        {
            int quarter = QuarterOfCurrent(fromDate.Month);
            QuarterOfDate(fromDate.Year, quarter, out quarterBeginDate, out quarterEndDate);
        }
        /// <summary>
        /// 获得某年第某季度的开始日期和结束日期
        /// </summary>
        /// <param name="year">需要计算的年份</param>
        /// <param name="quarter">需要计算的季度</param>
        /// <param name="quarterBeginDate">开始日期</param>
        /// <param name="quarterEndDate">结束日期</param>
        public static void QuarterOfDate(int year, int quarter, out DateTime quarterBeginDate, out DateTime quarterEndDate)
        {
            quarterBeginDate = DateTime.MinValue;
            quarterEndDate = DateTime.MaxValue;
            if (year <= 0001 || year >= 9999 || quarter < 1 || quarter > 4) return;
            int month = (quarter - 1) * 3 + 1;
            quarterBeginDate = new DateTime(year, month, 1);
            quarterEndDate = quarterBeginDate.AddMonths(3).AddMilliseconds(-1);
        }

        #endregion

        #region 日

        /// <summary>
        /// 返回某年某月最后一天
        /// </summary>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <returns>日</returns>
        public static int GetMonthLastDate(int year, int month)
        {
            DateTime lastDay = new DateTime(year, month, new System.Globalization.GregorianCalendar().GetDaysInMonth(year, month));
            int day = lastDay.Day;
            return day;
        }

        
        /// <summary>
        //返回每月的第一天和最后一天
        /// </summary>
        /// <param name="month">月份</param>
        /// <param name="firstDay">返回该月第一天日期</param>
        /// <param name="lastDay">返回该月最后一天日期</param>
        public static void ReturnDateFormat(int month, out string firstDay, out string lastDay)
        {
            int year = DateTime.Now.Year + month / 12;
            if (month != 12)
            {
                month = month % 12;
            }
            switch (month)
            {
                case 1:
                    firstDay = DateTime.Now.ToString(year + "-0" + month + "-01");
                    lastDay = DateTime.Now.ToString(year + "-0" + month + "-31");
                    break;
                case 2:
                    firstDay = DateTime.Now.ToString(year + "-0" + month + "-01");
                    lastDay = DateTime.IsLeapYear(DateTime.Now.Year) ? DateTime.Now.ToString(year + "-0" + month + "-29") : DateTime.Now.ToString(year + "-0" + month + "-28");
                    break;
                case 3:
                    firstDay = DateTime.Now.ToString(year + "-0" + month + "-01");
                    lastDay = DateTime.Now.ToString("yyyy-0" + month + "-31");
                    break;
                case 4:
                    firstDay = DateTime.Now.ToString(year + "-0" + month + "-01");
                    lastDay = DateTime.Now.ToString(year + "-0" + month + "-30");
                    break;
                case 5:
                    firstDay = DateTime.Now.ToString(year + "-0" + month + "-01");
                    lastDay = DateTime.Now.ToString(year + "-0" + month + "-31");
                    break;
                case 6:
                    firstDay = DateTime.Now.ToString(year + "-0" + month + "-01");
                    lastDay = DateTime.Now.ToString(year + "-0" + month + "-30");
                    break;
                case 7:
                    firstDay = DateTime.Now.ToString(year + "-0" + month + "-01");
                    lastDay = DateTime.Now.ToString(year + "-0" + month + "-31");
                    break;
                case 8:
                    firstDay = DateTime.Now.ToString(year + "-0" + month + "-01");
                    lastDay = DateTime.Now.ToString(year + "-0" + month + "-31");
                    break;
                case 9:
                    firstDay = DateTime.Now.ToString(year + "-0" + month + "-01");
                    lastDay = DateTime.Now.ToString(year + "-0" + month + "-30");
                    break;
                case 10:
                    firstDay = DateTime.Now.ToString(year + "-" + month + "-01");
                    lastDay = DateTime.Now.ToString(year + "-" + month + "-31");
                    break;
                case 11:
                    firstDay = DateTime.Now.ToString(year + "-" + month + "-01");
                    lastDay = DateTime.Now.ToString(year + "-" + month + "-30");
                    break;
                default:
                    firstDay = DateTime.Now.ToString(year + "-" + month + "-01");
                    lastDay = DateTime.Now.ToString(year + "-" + month + "-31");
                    break;
            }
        }

        #endregion
    }
}
