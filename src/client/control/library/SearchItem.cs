using System;

namespace WebHard.WinCtrl.Library
{
    //=============================================================================================
    //
    //=============================================================================================
    /// <summary></summary>
    public class SearchItem
    {
        //=========================================================================================
        //
        //=========================================================================================
        /// <summary></summary>
        /// <param name="text"></param>
        /// <param name="value"></param>
        /// <param name="imageIndex"></param>
        public SearchItem(string text, string value, int imageIndex)
        {
            this.m_text = text;
            this.m_value = value;
            this.m_imageIndex = imageIndex;
        }

        //=========================================================================================
        //
        //=========================================================================================
        private int m_imageIndex;

        private string m_text;
        private string m_value;

        //=========================================================================================
        //
        //=========================================================================================
        /// <summary></summary>
        public int ImageIndex
        {
            get
            {
                return this.m_imageIndex;
            }
        }

        /// <summary></summary>
        public string Text
        {
            get
            {
                return this.m_text;
            }
        }

        /// <summary></summary>
        public string Value
        {
            get
            {
                return this.m_value;
            }
        }

        //=========================================================================================
        //
        //=========================================================================================
        /// <summary></summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.m_text;
        }
    }
}