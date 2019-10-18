﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace UniOrm
{
    public static class TypeConverExtension
    {


        static readonly string[] NumLetter =
        {
        "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F", "G",
        "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "#"
        //, "a", "b", "c","d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y","z"
    };

        /// <summary>
        /// 获取下一个识别码
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static string GetNextLetter(string c)
        {
            var x = Array.IndexOf(NumLetter, c) + 1;
            if (x < NumLetter.Length - 1)
                return NumLetter[x];
            return "#";
        }

        #region TrimEx
        /// <summary>
        /// 替换字符串开头的空格、tab字符、换行符和新行(newline)
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string TrimStartEx(this string val)
        {
            return Regex.Replace(val, @"^\s+", "");
        }
        /// <summary>
        /// 替换字符串结尾的空格、tab字符、换行符和新行(newline)
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string TrimEndEx(this string val)
        {
            return Regex.Replace(val, @"\s+$", "");
        }
        /// <summary>
        /// 替换字符串开头和结尾的空格、tab字符、换行符和新行(newline)
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string TrimStartEndEx(this string val)
        {
            return val.TrimStartEx().TrimEndEx();
        }

        /// <summary>
        /// 替换字符串中的空格、tab字符、换行符和新行(newline)
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string TrimEx(this string val)
        {
            return Regex.Replace(val, @"\s", "");
        }
        /// <summary>
        /// 替换字符串中的换行符和新行(newline)
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string TrimExRN(this string val)
        {
            if (string.IsNullOrWhiteSpace(val))
                return val;
            return Regex.Replace(val, @"\r|\n", "");
        }

        #endregion

        /// <summary>
        /// 判断字符串是否符合a-zA-Z0-9开头，只包含a-zA-Z0-9-_
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool TestCodeReg(this string val)
        {
            return Regex.Match(val, "^[a-zA-Z0-9]+[a-zA-Z0-9-_]*$").Success;
        }

        #region 数值格式化、金额格式化
        /// <summary>
        /// 字符串转HTML（换行）
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string ToHtml(this string val)
        {
            return val.Replace("\n", "<br />").Replace("\r", "<br />");
        }

        /// <summary>
        /// 去除数值尾部多余的0
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static decimal ToDecTrimEnd0(this decimal val)
        {
            string v = string.Format("{0:0.#########;;#}", val);
            return v == "" ? 0M : v.ToDecimal();
        }

        /// <summary>
        /// 日期格式化 yyyy-MM-dd
        /// </summary>
        /// <param name="dt">Datetime?</param>
        /// <returns>string</returns>
        static public string ToDateString(this DateTime? dt)
        {
            return dt.HasValue ? ((DateTime)dt).ToString("yyyy-MM-dd") : "";
        }

        /// <summary>
        /// 日期格式化 yyyy-MM-dd
        /// </summary>
        /// <param name="dt">Datetime?</param>
        /// <returns>string</returns>
        static public string ToDateString(this DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// 日期格式化 yyyy-MM-dd
        /// </summary>
        /// <param name="dt">Datetime?</param>
        /// <returns>string</returns>
        static public DateTime ToDate(this object val)
        {

            if (val == null)
                return DateTime.MinValue;
            return Convert.ToDateTime(val.ToString());
        }

        /// <summary>
        /// 日期格式化 yyyy-MM-dd
        /// </summary>
        /// <param name="dt">string</param>
        /// <returns>string</returns>
        static public string ToDateString(this string val)
        {
            try
            {
                DateTime dt = DateTime.Parse(val);
                return dt.ToString("yyyy-MM-dd");
            }
            catch (Exception ex)
            {
                return val;
            }
        }
        /// <summary>
        /// 日期格式化 yyyy-MM-dd
        /// </summary>
        /// <param name="dt">string</param>
        /// <returns>string</returns>
        static public string ToDateString(this object val)
        {
            try
            {
                var s = val == null ? "" : val.ToString();
                DateTime dt = DateTime.Parse(s);
                return dt.ToString("yyyy-MM-dd");
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        /// <summary>
        /// 金额取整
        /// </summary>
        /// <param name="val">decimal?</param>
        /// <returns>string</returns>
        static public string ToAmtString(this decimal? val)
        {
            if (val.HasValue)
                return Math.Round((decimal)val).ToString();
            else
                return "";
        }

        /// <summary>
        /// 金额格式化
        /// </summary>
        /// <param name="val">string</param>
        /// <param name="i">精度</param>
        /// <returns>string</returns>
        static public string ToAmtString(this decimal? val, int i)
        {
            if (val.HasValue)
                return Math.Round((decimal)val, i).ToString();
            else
                return "";
        }
        /// <summary>
        /// 金额格式化
        /// </summary>
        /// <param name="val">string</param>
        /// <param name="i">精度</param>
        /// <returns>string</returns>
        static public string ToAmtString(this decimal val, int i)
        {
            return Math.Round((decimal)val, i).ToString();
        }

        /// <summary>
        /// 字符串金额取整
        /// </summary>
        /// <param name="val">string</param>
        /// <returns>string</returns>
        static public string ToAmtString(this string val)
        {
            return StrToAmtString(val, 0);
        }

        /// <summary>
        /// 字符串金额取2位精度
        /// </summary>
        /// <param name="val">string</param>
        /// <returns>string</returns>
        static public string ToAmtString2(this string val)
        {
            return StrToAmtString(val, 2);
        }


        /// <summary>
        /// 字符串金额取3位精度
        /// </summary>
        /// <param name="val">string</param>
        /// <returns>string</returns>
        static public string ToAmtString3(this string val)
        {
            return StrToAmtString(val, 3);
        }


        /// <summary>
        /// 字符串金额取4位精度
        /// </summary>
        /// <param name="val">string</param>
        /// <returns>string</returns>
        static public string ToAmtString4(this string val)
        {
            return StrToAmtString(val, 4);
        }

        /// <summary>
        /// 金额精度格式化
        /// </summary>
        /// <param name="val">string</param>
        /// <param name="i">int32</param>
        /// <returns>string</returns>
        static public string StrToAmtString(string val, Int32 i)
        {
            try
            {
                if (val == "")
                    return "";
                decimal dval = decimal.Parse(val);
                if (dval == 0m)
                    return "";
                dval = Math.Round(dval, i);
                return dval.ToString();
            }
            catch (Exception ex)
            {
                return val;
            }
        }

        /// <summary>
        /// object转金额
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        static public decimal ToDecimal(this object val)
        {
            decimal d;
            if (val == null) return 0M;
            return decimal.TryParse(val.ToString().Trim(), out d) ? d : 0M;
        }

        /// <summary>
        /// object转金额
        /// </summary>
        /// <param name="val">object</param>
        /// <param name="x">精度</param>
        /// <returns></returns>
        static public decimal ToDecimal(this object val, int x)
        {
            decimal d = val.ToDecimal();
            //d= decimal.TryParse(val.ToString().Trim(), out d) ? d : 0M;
            return Math.Round(d, x);
        }


        /// <summary>
        /// 字符串转金额
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        static public decimal ToDecimal(this string val)
        {
            decimal d;
            if (decimal.TryParse(val, out d))
                return d;
            else
                return 0M;
        }

        /// <summary>
        /// 字符串转金额
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        static public decimal? ToDecimalOrNull(this string val)
        {
            decimal d;
            if (decimal.TryParse(val, out d))
                return d;
            else
                return null;
        }

        /// <summary>
        /// 字符串转金额
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        static public decimal ToDecimal(this string val, int i)
        {
            decimal d;
            if (decimal.TryParse(val, out d))
            {
                d = Math.Round(d, i);
                return d;
            }
            else
                return 0M;
        }

        /// <summary>
        /// 字符转整数
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        static public int ToInt(this string val)
        {
            int i = 0;
            if (!string.IsNullOrWhiteSpace(val))
                int.TryParse(val, out i);
            return i;
        }

        /// <summary>
        /// object转Int
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static int ToInt(this object val)
        {
            if (val == null)
                return 0;
            string v = val.ToString().Trim();
            int i;
            int.TryParse(v, out i);
            return i;
        }

        /// <summary>
        /// object转Int
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool ToBool(this object val)
        {
            if (val == null)
                return false;
            try
            {
                return Convert.ToBoolean(val.ToString().Trim());
            }
            catch
            {
                return false;
            }

        }

        #endregion

        #region 全角半角转换
        /// <summary>
        /// 转全角的函数(SBC case)
        /// </summary>
        /// <param name="input">任意字符串</param>
        /// <returns>全角字符串</returns>
        ///<remarks>
        ///全角空格为12288，半角空格为32
        ///其他字符半角(33-126)与全角(65281-65374)的对应关系是：均相差65248
        ///</remarks>
        public static string ToSbc(this string input)
        {
            //半角转全角：
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 32)
                {
                    c[i] = (char)12288;
                    continue;
                }
                if (c[i] < 127)
                    c[i] = (char)(c[i] + 65248);
            }
            return new string(c);
        }
        /// <summary> 转半角的函数(DBC case) </summary>
        /// <param name="input">任意字符串</param>
        /// <returns>半角字符串</returns>
        ///<remarks>
        ///全角空格为12288，半角空格为32
        ///其他字符半角(33-126)与全角(65281-65374)的对应关系是：均相差65248
        ///</remarks>
        public static string ToDbc(this string input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 12288)
                {
                    c[i] = (char)32;
                    continue;
                }
                if (c[i] > 65280 && c[i] < 65375)
                    c[i] = (char)(c[i] - 65248);
            }
            return new string(c);
        }
        #endregion
        private static readonly string key = "lskdflsdfk23n2i3usndbfh2b3erh231n2shdkfjk2j3h412esdlwej";
        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="encryptString"></param>
        /// <returns></returns>
        public static string DesEncrypt(this string encryptString)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(key.Substring(0, 8));
            byte[] keyIV = keyBytes;
            byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            MemoryStream mStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(mStream, provider.CreateEncryptor(keyBytes, keyIV), CryptoStreamMode.Write);
            cStream.Write(inputByteArray, 0, inputByteArray.Length);
            cStream.FlushFinalBlock();
            return Convert.ToBase64String(mStream.ToArray());
        }

        public static string UrlDecode(this object text)
        {
            if(text == null)
            {
                return "";
            }
           
            return System.Net.WebUtility.UrlDecode(text.ToString());
        }
        public static string UrlEncode(this object  text)
        {
            if (text == null)
            {
                return "";
            }
             
            return System.Net.WebUtility.UrlEncode(text.ToString());
        }
        /// <summary>
        /// DES解密
          /// </summary>
        /// <param name="decryptString"></param>
        /// <returns></returns>
        public static string DesDecrypt(this string decryptString)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(key.Substring(0, 8));
            byte[] keyIV = keyBytes;
            byte[] inputByteArray = Convert.FromBase64String(decryptString);
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            MemoryStream mStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(mStream, provider.CreateDecryptor(keyBytes, keyIV), CryptoStreamMode.Write);
            cStream.Write(inputByteArray, 0, inputByteArray.Length);
            cStream.FlushFinalBlock();
            return Encoding.UTF8.GetString(mStream.ToArray());
        }

        public static string SafeSubString(this string decryptString, int length)
        {
            if (string.IsNullOrEmpty(decryptString))
            {
                return "";
            }
            else
            {
                if(decryptString.Length<=length)
                {
                    return decryptString;
                }
                else
                {
                    return decryptString.Substring(length);
                }
            }
        }

        public static string SafeSubString(this string decryptString, int start, int length)
        {
            if (string.IsNullOrEmpty(decryptString))
            {
                return "";
            }
            else
            {
                if (decryptString.Length < start)
                {
                    return decryptString;
                }
                else
                {


                    if (decryptString.Length <= length)
                    {
                        return decryptString;
                    }
                    else
                    {
                        return decryptString.Substring(start, length);
                    }
                }
            }
        }
    }
}