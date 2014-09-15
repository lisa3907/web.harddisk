using System;
using System.Data;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

using uBizSoft.UIC.Win.Control;
using uBizSoft.UIC.Win.Control.Library;

namespace WebHard.WinCtrl.Library
{
    //=============================================================================================
    //
    //=============================================================================================
    /// <summary></summary>
    public class ResourceHelper : System.IDisposable
    {
        //=========================================================================================
        //
        //=========================================================================================
        /// <summary></summary>
        public ResourceHelper()
        {
        }

        //=========================================================================================
        //
        //=========================================================================================
        private const string UNIQUE_ID = "54ED9087-E714-4287-8BF4-11AAD4B1C1EC";

        //=========================================================================================
        //
        //=========================================================================================
        private string m_uniqueId = String.Empty;

        //=========================================================================================
        //
        //=========================================================================================
        /// <summary></summary>
        public string UniqueID
        {
            get
            {
                this.m_uniqueId = AppMediator.SINGLETON.GetConstantFromClient("UniqueID", String.Empty);

                if (String.IsNullOrEmpty(this.m_uniqueId) == true)
                    this.m_uniqueId = UNIQUE_ID;

                return this.m_uniqueId;
            }
        }

        //=========================================================================================
        //
        //=========================================================================================
        private CultureInfo m_cultureinfo = null;
        private CultureInfo g_cultureinfo
        {
            get
            {
                if (this.m_cultureinfo == null)
                {
                    if (String.IsNullOrEmpty(AppMediator.SINGLETON.Culture) == false)
                        this.m_cultureinfo = new CultureInfo(AppMediator.SINGLETON.Culture);

                    if (this.m_cultureinfo == null)
                        this.m_cultureinfo = Thread.CurrentThread.CurrentCulture;

                    //this.m_cultureinfo = new CultureInfo("zh-CN");
                }
                return this.m_cultureinfo;
            }
        }

        private TranslatorHelper m_trnsHelper = null;
        private TranslatorHelper g_trnsHelper
        {
            get
            {
                if (this.m_trnsHelper == null)
                    this.m_trnsHelper = TranslatorHelper.SNG;

                this.m_trnsHelper.UniqueID = this.UniqueID;
                this.m_trnsHelper.DoTranslationTextSet = this.DoTranslationTextSet;
                this.m_trnsHelper.DoTranslationWord = this.DoTranslationWord;

                return this.m_trnsHelper;
            }
        }

        //=========================================================================================
        // System.IDisposable ¸â¹ö ±¸Çö
        //=========================================================================================
        /// <summary></summary>
        public void Dispose()
        {
        }

        //=========================================================================================
        //
        //=========================================================================================
        /// <summary></summary>
        /// <param name="p_orgset"></param>
        /// <param name="p_tablset"></param>
        /// <param name="p_field"></param>
        /// <param name="p_culture"></param>
        /// <returns></returns>
        private DataSet DoTranslationTextSet(DataSet p_orgset, int p_tablset, string p_field, string p_culture)
        {
            return AppMediator.SINGLETON.TranslateText(p_orgset, p_tablset, p_culture, p_field);
        }

        /// <summary></summary>
        /// <param name="p_korean"></param>
        /// <param name="p_culture"></param>
        /// <returns></returns>
        private string DoTranslationWord(string p_korean, string p_culture)
        {
            return AppMediator.SINGLETON.TranslateText(p_culture, p_korean);
        }

        //=========================================================================================
        //
        //=========================================================================================
        /// <summary></summary>
        /// <param name="p_form"></param>
        /// <param name="p_comp"></param>
        public void TranslateText(System.Windows.Forms.Form p_form, System.ComponentModel.IContainer p_comp)
        {
            if (this.g_cultureinfo.Name != "ko-KR")
                this.g_trnsHelper.TranslateText(p_form, p_comp, g_cultureinfo.Name);
        }

        /// <summary></summary>
        /// <param name="p_usrctrl"></param>
        /// <param name="p_comp"></param>
        public void TranslateText(System.Windows.Forms.UserControl p_usrctrl, System.ComponentModel.IContainer p_comp)
        {
            if (this.g_cultureinfo.Name != "ko-KR")
                this.g_trnsHelper.TranslateText(p_usrctrl, p_comp, g_cultureinfo.Name);
        }

        /// <summary></summary>
        /// <param name="p_koean"></param>
        /// <returns></returns>
        public string TranslateWord(string p_koean)
        {
            string result = p_koean;

            if (this.g_cultureinfo.Name != "ko-KR")
                result = this.g_trnsHelper.TranslateWord(this.UniqueID, p_koean, g_cultureinfo.Name);

            return result;
        }
    }
}