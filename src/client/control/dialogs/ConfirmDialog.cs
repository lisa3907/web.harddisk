using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

using WebHard.WinCtrl.Forms;
using WebHard.WinCtrl.Library;
using WebHard.Proxy;

namespace WebHard.WinCtrl.Dialogs
{
    //=============================================================================================
    //
    //=============================================================================================
    /// <summary></summary>
    public partial class ConfirmDialog : DevExpress.XtraEditors.XtraForm
    {
        //=========================================================================================
        //
        //=========================================================================================
        /// <summary></summary>
        /// <param name="p_mainBox"></param>
        /// <param name="p_fileSet"></param>
        /// <param name="p_fileInfo"></param>
        public ConfirmDialog(MainBox p_mainBox, DataSet p_fileSet, FileInfo p_fileInfo)
        {
            this.m_mainBox = p_mainBox;
            this.InitializeComponent();

            AppMediator.SINGLETON.ResourceHelper.TranslateText(this, this.components);

            if (p_fileSet.Tables[0].Rows.Count > 0)
            {
                double fileSize = Convert.ToInt64(p_fileSet.Tables[0].Rows[0]["vsize"].ToString());

                this.lbBeforeFileSize.Text = fileSize.ToString("n0") + AppMediator.SINGLETON.ResourceHelper.TranslateWord("����Ʈ");
                this.lbBeforeFileWriteDate.Text = Convert.ToDateTime(p_fileSet.Tables[0].Rows[0]["wdate"].ToString()).ToString("yyyy�� MM�� dd�� dddd, HH:mm:dd") + " " + AppMediator.SINGLETON.ResourceHelper.TranslateWord("������");
            }
            else
            {
                this.lbBeforeFileSize.Text = "0" + AppMediator.SINGLETON.ResourceHelper.TranslateWord("����Ʈ");
                this.lbBeforeFileWriteDate.Text = String.Empty;
            }

            this.lbAfterFileSize.Text = p_fileInfo.Length.ToString("n0") + AppMediator.SINGLETON.ResourceHelper.TranslateWord("����Ʈ");
            this.lbAfterFileWriteDate.Text = p_fileInfo.LastAccessTime.ToString("yyyy�� MM�� dd�� dddd, HH:mm:dd") + " " + AppMediator.SINGLETON.ResourceHelper.TranslateWord("������");
            this.lbTitle.Text = AppMediator.SINGLETON.ResourceHelper.TranslateWord("�� ������ �̹�") + " '" + p_fileInfo.Name + "' " + AppMediator.SINGLETON.ResourceHelper.TranslateWord("������ �ֽ��ϴ�.");
        }

        /// <summary></summary>
        /// <param name="p_mainBox"></param>
        /// <param name="p_beforeFSize"></param>
        /// <param name="p_beforeWDate"></param>
        /// <param name="p_afterFName"></param>
        /// <param name="p_afterFSize"></param>
        /// <param name="p_afterWDate"></param>
        public ConfirmDialog(MainBox p_mainBox, string p_beforeFSize, string p_beforeWDate, string p_afterFName, string p_afterFSize, string p_afterWDate)
        {
            this.m_mainBox = p_mainBox;
            this.InitializeComponent();

            AppMediator.SINGLETON.ResourceHelper.TranslateText(this, this.components);

            double beforeFSize = Convert.ToInt64(p_beforeFSize);

            this.lbBeforeFileSize.Text = beforeFSize.ToString("n0") + AppMediator.SINGLETON.ResourceHelper.TranslateWord("����Ʈ");
            this.lbBeforeFileWriteDate.Text = Convert.ToDateTime(p_beforeWDate).ToString("yyyy�� MM�� dd�� dddd, HH:mm:dd") + " " + AppMediator.SINGLETON.ResourceHelper.TranslateWord("������");

            double afterFSize = Convert.ToInt64(p_afterFSize);

            this.lbAfterFileSize.Text = afterFSize.ToString("n0") + AppMediator.SINGLETON.ResourceHelper.TranslateWord("����Ʈ");
            this.lbAfterFileWriteDate.Text = Convert.ToDateTime(p_afterWDate).ToString("yyyy�� MM�� dd�� dddd, HH:mm:dd") + " " + AppMediator.SINGLETON.ResourceHelper.TranslateWord("������");

            this.lbTitle.Text = AppMediator.SINGLETON.ResourceHelper.TranslateWord("�� ������ �̹�") + " '" + p_afterFName + "' " + AppMediator.SINGLETON.ResourceHelper.TranslateWord("������ �ֽ��ϴ�.");
        }

        //=========================================================================================
        //
        //=========================================================================================
        private MainBox m_mainBox;

        //=========================================================================================
        //
        //=========================================================================================
    }
}