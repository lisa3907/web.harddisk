using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using LIB.Logging;
using UIC.Win.Control;
using WebHard.WinCtrl.Forms;
using WebHard.WinCtrl.Library;
using WebHard.Proxy;

namespace WebHard.WinCtrl.Dialogs
{
    //=============================================================================================
    //
    //=============================================================================================
    /// <summary></summary>
    public partial class RightsDialog : DevExpress.XtraEditors.XtraForm
    {
        //=========================================================================================
        //
        //=========================================================================================
        /// <summary></summary>
        /// <param name="p_title"></param>
        public RightsDialog(string p_title)
        {
            this.InitializeComponent();

            AppMediator.SINGLETON.ResourceHelper.TranslateText(this, this.components);

            this.InitializeAuth();
        }

        /// <summary></summary>
        /// <param name="p_dialog"></param>
        /// <param name="p_title"></param>
        public RightsDialog(AuthListDialog p_dialog, string p_title)
        {
            this.m_authlistDialog = p_dialog;
            this.InitializeComponent();

            AppMediator.SINGLETON.ResourceHelper.TranslateText(this, this.components);

            this.InitializeAuthList();
            this.InitializeAuth();

            this.Text = p_title;
        }

        //=========================================================================================
        //
        //=========================================================================================
        private int m_authGridSelectedIndex = -1;
        private int m_authlistGridSelectedIndex = -1;

        private DataTable m_author;
        private DataTable m_authorlist;

        private AuthListDialog m_authlistDialog;

        private ContextMenuItem[] m_cmItems = new ContextMenuItem[1];

        //=========================================================================================
        //
        //=========================================================================================
        /// <summary></summary>
        private void AddAuth()
        {
            if (this.authlistGridView.RowCount > -1)
            {
                DataRow row = this.authlistGridView.GetDataRow(this.m_authlistGridSelectedIndex);
                DataRow newrow = this.m_author.NewRow();

                newrow["member"] = row["member"].ToString();
                newrow["mtype"] = row["mtype"].ToString();
                newrow["name"] = row["name"].ToString();
                newrow["control"] = row["control"].ToString();
                newrow["cmodify"] = row["cmodify"].ToString();
                newrow["cread"] = row["cread"].ToString();
                newrow["cdelete"] = row["cdelete"].ToString();
                newrow["cview"] = row["cview"].ToString();
                newrow["cfolder"] = row["cfolder"].ToString();
                newrow["cfile"] = row["cfile"].ToString();

                this.m_author.Rows.Add(newrow);

                this.m_author.DefaultView.Sort = "mtype DESC, name DESC";

                this.authlistGridView.DeleteRow(this.m_authlistGridSelectedIndex);

                if (this.authlistGridView.RowCount > 0)
                {
                    try
                    {
                        this.authlistGridView.FocusedRowHandle = this.m_authlistGridSelectedIndex;
                    }
                    catch
                    {
                        this.authlistGridView.FocusedRowHandle = this.m_authlistGridSelectedIndex - 1;
                    }
                }
            }
        }

        /// <summary></summary>
        private void AddAuthList()
        {
            try
            {
                if (this.authGridView.RowCount > -1)
                {
                    DataRow row = this.authGridView.GetDataRow(this.m_authGridSelectedIndex);
                    DataRow newrow = this.m_authorlist.NewRow();

                    newrow["guid"] = String.Empty;
                    newrow["member"] = row["member"].ToString();
                    newrow["mtype"] = row["mtype"].ToString();
                    newrow["name"] = row["name"].ToString();
                    newrow["control"] = row["control"].ToString();
                    newrow["cmodify"] = row["cmodify"].ToString();
                    newrow["cread"] = row["cread"].ToString();
                    newrow["cdelete"] = row["cdelete"].ToString();
                    newrow["cview"] = row["cview"].ToString();
                    newrow["cfolder"] = row["cfolder"].ToString();
                    newrow["cfile"] = row["cfile"].ToString();

                    this.m_authorlist.Rows.Add(newrow);
                }

                this.m_authorlist.DefaultView.Sort = "mtype DESC, name DESC";

                this.authGridView.DeleteRow(this.m_authGridSelectedIndex);

                if (this.authGridView.RowCount > 0)
                {
                    try
                    {
                        this.authGridView.FocusedRowHandle = this.m_authGridSelectedIndex;
                    }
                    catch
                    {
                        this.authGridView.FocusedRowHandle = this.m_authGridSelectedIndex - 1;
                    }
                }
            }
            catch (Exception ex)
            {
                OFileLog.SNG.WriteLog(ex.ToString());
            }
        }

        /// <summary></summary>
        private void DeleteAuth()
        {
            this.AddAuth();
        }

        /// <summary></summary>
        private void InitializeAuth()
        {
            this.m_author = new DataTable();

            this.m_author.Columns.Add("member", typeof(string));
            this.m_author.Columns.Add("mtype", typeof(string));
            this.m_author.Columns.Add("name", typeof(string));
            this.m_author.Columns.Add("control", typeof(string));
            this.m_author.Columns.Add("cmodify", typeof(string));
            this.m_author.Columns.Add("cread", typeof(string));
            this.m_author.Columns.Add("cdelete", typeof(string));
            this.m_author.Columns.Add("cview", typeof(string));
            this.m_author.Columns.Add("cfolder", typeof(string));
            this.m_author.Columns.Add("cfile", typeof(string));

            this.authGrid.DataSource = this.m_author;

            this.m_author.Rows.Clear();

            DataSet orgSet = AppMediator.SINGLETON.GetOrgCenterList();

            for (int i = 0; i < orgSet.Tables[0].Rows.Count; i++)
            {
                bool exits = true;

                for (int detail = 0; detail < this.authlistGridView.RowCount; detail++)
                {
                    if (this.authlistGridView.GetDataRow(detail)["mtype"].ToString() == orgSet.Tables[0].Rows[i][0].ToString() &&
                        this.authlistGridView.GetDataRow(detail)["member"].ToString() == orgSet.Tables[0].Rows[i][1].ToString() &&
                        this.authlistGridView.GetDataRow(detail)["name"].ToString() == orgSet.Tables[0].Rows[i][2].ToString())
                    {
                        exits = false;
                    }
                }

                if (exits == true)
                {
                    DataRow newrow = this.m_author.NewRow();

                    newrow["mtype"] = orgSet.Tables[0].Rows[i][0].ToString();
                    newrow["member"] = orgSet.Tables[0].Rows[i][1].ToString();
                    newrow["name"] = orgSet.Tables[0].Rows[i][2].ToString();
                    newrow["control"] = "T";
                    newrow["cmodify"] = "T";
                    newrow["cread"] = "T";
                    newrow["cdelete"] = "T";
                    newrow["cview"] = "T";
                    newrow["cfolder"] = "T";
                    newrow["cfile"] = "T";

                    this.m_author.Rows.Add(newrow);
                }

                this.m_author.DefaultView.Sort = "mtype DESC, name DESC";
            }
        }

        /// <summary></summary>
        private void InitializeAuthList()
        {
            // guid, member, mtype, name, control, cmodify, cread, cdelete, cview, cfolder, cfile
            this.m_authorlist = new DataTable();

            this.m_authorlist.Columns.Add("guid", typeof(string));
            this.m_authorlist.Columns.Add("member", typeof(string));
            this.m_authorlist.Columns.Add("mtype", typeof(string));
            this.m_authorlist.Columns.Add("name", typeof(string));
            this.m_authorlist.Columns.Add("control", typeof(string));
            this.m_authorlist.Columns.Add("cmodify", typeof(string));
            this.m_authorlist.Columns.Add("cread", typeof(string));
            this.m_authorlist.Columns.Add("cdelete", typeof(string));
            this.m_authorlist.Columns.Add("cview", typeof(string));
            this.m_authorlist.Columns.Add("cfolder", typeof(string));
            this.m_authorlist.Columns.Add("cfile", typeof(string));

            this.authlistGrid.DataSource = this.m_authorlist;

            this.m_authorlist.Rows.Clear();

            if (this.m_authlistDialog.gridAuthView.RowCount > 0)
            {
                for (int i = 0; i < this.m_authlistDialog.gridAuthView.RowCount; i++)
                {
                    DataRow row = this.m_authlistDialog.gridAuthView.GetDataRow(i);
                    DataRow newrow = this.m_authorlist.NewRow();

                    newrow["guid"] = row["guid"].ToString();
                    newrow["member"] = row["member"].ToString();
                    newrow["mtype"] = row["mtype"].ToString();
                    newrow["name"] = row["name"].ToString();
                    newrow["control"] = row["control"].ToString();
                    newrow["cmodify"] = row["cmodify"].ToString();
                    newrow["cread"] = row["cread"].ToString();
                    newrow["cdelete"] = row["cdelete"].ToString();
                    newrow["cview"] = row["cview"].ToString();
                    newrow["cfolder"] = row["cfolder"].ToString();
                    newrow["cfile"] = row["cfile"].ToString();

                    this.m_authorlist.Rows.Add(newrow);
                }

                this.m_authorlist.DefaultView.Sort = "mtype DESC, name ASC";
            }
        }

        //=========================================================================================
        //
        //=========================================================================================
        private void OnAuthGridViewCustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column.FieldName == "name")
            {
                e.DisplayText = "     " + e.CellValue.ToString();

                int imgIndex = this.authGridView.GetDataRow(e.RowHandle)["mtype"].ToString() == "T" ? 1 : 0;

                e.Graphics.DrawImage(this.lvHeadIcon.Images[imgIndex], e.Bounds.Left, e.Bounds.Top + 1);
                e.Handled = false;
            }
        }

        private void OnAuthGridViewDoubleClick(object sender, System.EventArgs e)
        {
            this.AddAuthList();
        }

        private void OnAuthGridViewSelectedIndexChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            this.m_authGridSelectedIndex = e.FocusedRowHandle;
        }

        private void OnAuthListGridViewCustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column.FieldName == "name")
            {
                e.DisplayText = "     " + e.CellValue.ToString();

                int imgIndex = this.authlistGridView.GetDataRow(e.RowHandle)["mtype"].ToString() == "T" ? 1 : 0;

                e.Graphics.DrawImage(this.lvHeadIcon.Images[imgIndex], e.Bounds.Left, e.Bounds.Top + 1);
                e.Handled = false;
            }
        }

        private void OnAuthListGridViewKeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                this.AddAuth();
        }

        private void OnAuthListGridViewMouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                popupMenu.ShowPopup(Control.MousePosition);
        }

        private void OnAuthListGridViewSelectedIndexChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            this.m_authlistGridSelectedIndex = e.FocusedRowHandle;
        }

        private void OnDeleteMenuItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.DeleteAuth();
        }

        private void OnAddButtonClick(object sender, System.EventArgs e)
        {
            this.AddAuthList();
        }

        private void OnDeleteButtonClick(object sender, EventArgs e)
        {
            this.DeleteAuth();
        }

        private void OnOkButtonClick(object sender, System.EventArgs e)
        {
            if (this.authlistGridView.RowCount > 0)
            {
                this.m_authlistDialog.AuthTable.Rows.Clear();

                for (int i = 0; i < authlistGridView.RowCount; i++)
                {
                    DataRow row = authlistGridView.GetDataRow(i);
                    DataRow newrow = this.m_authlistDialog.AuthTable.NewRow();

                    newrow["guid"] = row["guid"].ToString();
                    newrow["member"] = row["member"].ToString();
                    newrow["mtype"] = row["mtype"].ToString();
                    newrow["name"] = row["name"].ToString();
                    newrow["control"] = row["control"].ToString();
                    newrow["cmodify"] = row["cmodify"].ToString();
                    newrow["cread"] = row["cread"].ToString();
                    newrow["cdelete"] = row["cdelete"].ToString();
                    newrow["cview"] = row["cview"].ToString();
                    newrow["cfolder"] = row["cfolder"].ToString();
                    newrow["cfile"] = row["cfile"].ToString();

                    this.m_authlistDialog.AuthTable.Rows.Add(newrow);
                }

                this.m_authlistDialog.AuthTable.AcceptChanges();
                this.m_authlistDialog.AuthTable.DefaultView.Sort = "mtype DESC, name DESC";
            }

            this.Close();
        }
    }
}