using System;
using System.Runtime.InteropServices;
using System.Text;

namespace WebHard.WinCtrl.Library
{
    //=============================================================================================
    //
    //=============================================================================================
    /// <summary></summary>
    public class StringHelper
    {
        //=========================================================================================
        //
        //=========================================================================================
        [DllImport("shlwapi.dll", CharSet = CharSet.Auto)]
        private static extern bool PathCompactPathEx(StringBuilder pszOut, string pszPath, int cchMax, int reserved);

        //=========================================================================================
        //
        //=========================================================================================
        /// <summary></summary>
        /// <param name="p_length"></param>
        /// <returns></returns>
        public static string ConvertSizeToString(long p_length)
        {
            string result = String.Empty;

            if (p_length > 1024 * 1024)
            {
                double size = (double)p_length / (1024 * 1024);
                result = size.ToString("###,##0.0") + "MB";
            }
            else if (p_length > 1024)
            {
                double size = (double)p_length / (1024);
                result = size.ToString("###,##0.0") + "KB";
            }
            else
            {
                result = p_length.ToString() + "Bytes";
            }

            return result;
        }

        /// <summary></summary>
        /// <param name="p_size"></param>
        /// <param name="p_selected"></param>
        /// <returns></returns>
        public static string ConvertSizeToString(double p_size, int p_selected)
        {
            string result = p_selected + " 개의 항목 선택됨";

            if (p_size / 1024 < 1)
            {
                result += "(1KB)";
            }
            else if (p_size / 1024 / 1024 < 1)
            {
                result += "(" + (p_size / 1024).ToString("N0") + "KB)";
            }
            else if (p_size / 1024 / 1024 / 1024 < 1)
            {
                result += "(" + (p_size / 1024 / 1024).ToString("N1") + "MB)";
            }
            else
            {
                result += "(" + (p_size / 1024 / 1024 / 1024).ToString("N2") + "GB)";
            }
            return result;
        }

        /// <summary></summary>
        /// <param name="p_second"></param>
        /// <returns></returns>
        public static string ConvertTimeToString(double p_second)
        {
            return ConvertTimeToString(TimeSpan.FromSeconds(p_second));
        }

        /// <summary></summary>
        /// <param name="span"></param>
        /// <returns></returns>
        public static string ConvertTimeToString(TimeSpan span)
        {
            if (Convert.ToInt32(span.TotalSeconds) > 60 * 60)
                return span.Hours.ToString() + "hour " + span.Minutes.ToString() + "min " + span.Seconds.ToString() + "sec";

            if (Convert.ToInt32(span.TotalSeconds) > 60)
                return span.Minutes.ToString() + "min " + span.Seconds.ToString() + "sec";

            return span.Seconds.ToString() + "sec";
        }

        /// <summary></summary>
        /// <param name="p_longname"></param>
        /// <param name="p_maxlength"></param>
        /// <returns></returns>
        public static string GetShortDisplayName(string p_longname, int p_maxlength)
        {
            StringBuilder output = new StringBuilder(p_maxlength + p_maxlength + 2);

            if (PathCompactPathEx(output, p_longname, p_maxlength, 0))
                return output.ToString();

            return p_longname;
        }
    }
}