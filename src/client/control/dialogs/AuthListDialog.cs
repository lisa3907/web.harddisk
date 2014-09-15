using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
    public partial class AuthListDialog : DevExpress.XtraEditors.XtraForm
    {
        //=========================================================================================
        //
        //=========================================================================================
        /// <summary></summary>
        /// <param name="p_mainBox"></param>
        /// <param name="p_authSet"></param>
        /// <param name="p_isFile"></param>
        /// <param name="p_title"></param>
        public AuthListDialog(MainBox p_mainBox, DataSet p_authSet, bool p_isFile, string p_title)
        {
            this.m_isFile = p_isFile;
            this.m_mainBox = p_mainBox;
            this.m_authSet = p_authSet.Copy();

            this.InitializeComponent();

            AppMediator.SINGLETON.ResourceHelper.TranslateText(this, this.components);

            this.InitializeAuthList();

            this.Text = p_title;

            if (p_isFile == true)
            {
                this.cbSub.Visible = false;
                this.cbAllSub.Visible = false;
            }
            else
            {
                this.cbSub.Visible = true;
                this.cbAllSub.Visible = true;
            }
        }

        //=========================================================================================
        //
        //=========================================================================================
        private int m_selectedIndex = -1;

        private DataSet m_authSet = null;

        private DataTable m_authTable = null;
        private DataTable m_authListTable = null;

        private MainBox m_mainBox = null;
        private bool m_isFile;

        //=========================================================================================
        //
        //=========================================================================================
        /// <summary></summary>
        public bool AllSubFoldersChecked
        {
            get
            {
                return this.cbAllSub.Checked;
            }
        }

        /// <summary></summary>
        public DataTable AuthListTable
        {
            get
            {
                if (this.m_authListTable == null)
                    this.m_authListTable = new DataTable();
                return this.m_authListTable;
            }
        }

        /// <summary></summary>
        public DataTable AuthTable
        {
            get
            {
                if (this.m_authTable == null)
                    this.m_authTable = new DataTable();
                return this.m_authTable;
            }
        }

        /// <summary></summary>
        public bool SubFolderChecked
        {
            get
            {
                return this.cbSub.Checked;
            }
        }

        //=========================================================================================
        //
        //=========================================================================================
        /// <summary></summary>
        private void ChangeChoiceSelectedRows()
        {
            if (this.gridListView.DataSource == null)
                return;

            DataRow masterrow = this.gridAuthView.GetDataRow(this.gridAuthView.FocusedRowHandle);
            DataRow childrow = this.gridListView.GetDataRow(this.gridListView.FocusedRowHandle);

            bool ischecked = Convert.ToBoolean(childrow["choice"]);
            childrow["choice"] = !ischecked;

            string field = childrow["field"].ToString();
            masterrow[field] = ischecked == true ? "F" : "T";
        }

        /// <summary></summary>
        private void InitializeAuthList()
        {
            this.gridList.DataSource = this.AuthListTable;

            this.AuthListTable.Columns.Add("field", typeof(string));
            this.AuthListTable.Columns.Add("right", typeof(string));
            this.AuthListTable.Columns.Add("choice", typeof(bool));

            this.gridAuth.DataSource = this.AuthTable;

            this.AuthTable.Clear();
            this.AuthTable.Merge(this.m_authSet.Tables[0]);
            this.AuthTable.AcceptChanges();

            this.AuthTable.DefaultView.Sort = "mtype DESC, name DESC";

            AppMediator.SINGLETON.SelectRow(this.gridAuthView);
            this.SelectAuthGrid();
        }

        /// <summary></summary>
        /// <param name="field"></param>
        /// <param name="name"></param>
        /// <param name="istrue"></param>
        private void InsertListRow(string field, string name, bool istrue)
        {
            DataRow newrow = this.AuthListTable.NewRow();

            newrow["field"] = field;
            newrow["right"] = AppMediator.SINGLETON.ResourceHelper.TranslateWord(name);
            newrow["choice"] = istrue;

            this.AuthListTable.Rows.Add(newrow);
        }

        /// <summary></summary>
        private void SelectAuthGrid()
        {
            if (this.gridAuthView.FocusedRowHandle < 0)
                return;

            this.m_selectedIndex = this.gridAuthView.FocusedRowHandle;

            DataRow row = this.gridAuthView.GetDataRow(this.gridAuthView.FocusedRowHandle);

            this.AuthListTable.Rows.Clear();

            this.InsertListRow("control", "관리", row["control"].ToString() == "T");
            this.InsertListRow("cmodify", "변경", row["cmodify"].ToString() == "T");
            this.InsertListRow("cread", "읽기", row["cread"].ToString() == "T");
            this.InsertListRow("cdelete", "삭제", row["cdelete"].ToString() == "T");
            this.InsertListRow("cview", "보기", row["cview"].ToString() == "T");

            if (m_isFile == false)
            {
                this.InsertListRow("cfolder", "폴더작성", row["cfolder"].ToString() == "T");
                this.InsertListRow("cfile", "파일작성", row["cfile"].ToString() == "T");
            }
        }

        //=========================================================================================
        //
        //=========================================================================================
        private void OnAuthGridCustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column.FieldName == "name")
            {
                e.DisplayText = "     " + e.CellValue.ToString();

                if (this.gridAuthView.GetDataRow(e.RowHandle)["mtype"].ToString() == "T")
                {
                    e.Graphics.DrawImage(this.lvHeadIcon.Images[1], e.Bounds.Left, e.Bounds.Top + 1);
                }
                else
                {
                    e.Graphics.DrawImage(this.lvHeadIcon.Images[0], e.Bounds.Left, e.Bounds.Top + 1);
                }

                e.Handled = false;
            }
        }

        private void OnAuthGridSelectedIndexChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            this.SelectAuthGrid();
        }

        private void OnAuthListGridClick(object sender, EventArgs e)
        {
            if (this.gridListView.FocusedColumn.Name == "clmnChoice")
                this.ChangeChoiceSelectedRows();
        }

        private void OnChoiceCheckBoxEditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            this.ChangeChoiceSelectedRows();
        }

        private void OnAddButtonClick(object sender, System.EventArgs e)
        {
            using (RightsDialog dialog = new RightsDialog(this, this.Text))
            {
                dialog.ShowInTaskbar = false;
                dialog.ShowDialog();
            }
        }

        private void OnDeleteButtonClick(object sender, System.EventArgs e)
        {
            if (this.gridAuthView.GetSelectedRows().Length > 0)
            {
                this.gridAuthView.DeleteRow(this.m_selectedIndex);

                if (this.gridAuthView.RowCount > 0)
                {
                    try
                    {
                        this.gridAuthView.FocusedRowHandle = this.m_selectedIndex;
                        this.gridAuthView.SelectRow(this.m_selectedIndex);
                    }
                    catch
                    {
                        this.gridAuthView.FocusedRowHandle = this.m_selectedIndex - 1;
                        this.gridAuthView.SelectRow(this.m_selectedIndex - 1);
                    }
                }
                else
                {                    
                    this.AuthListTable.Rows.Clear();
                }

                this.gridAuthView.GridControl.Refresh();
            }
        }

        private void OnOkButtonClick(object sender, System.EventArgs e)
        {
            this.m_mainBox.AuthListSet.Clear();
            this.m_mainBox.AuthListSet.Tables.Clear();

            DataTable authTable = new DataTable("WebHardAuthList");

            authTable.Columns.Add("guid", typeof(String));
            authTable.Columns.Add("member", typeof(String));
            authTable.Columns.Add("mtype", typeof(String));
            authTable.Columns.Add("name", typeof(String));
            authTable.Columns.Add("control", typeof(String));
            authTable.Columns.Add("cmodify", typeof(String));
            authTable.Columns.Add("cread", typeof(String));
            authTable.Columns.Add("cdelete", typeof(String));
            authTable.Columns.Add("cview", typeof(String));
            authTable.Columns.Add("cfolder", typeof(String));
            authTable.Columns.Add("cfile", typeof(String));

            for (int i = 0; i < this.gridAuthView.RowCount; i++)
            {
                DataRow row = this.gridAuthView.GetDataRow(i);
                DataRow newrow = authTable.NewRow();

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

                authTable.Rows.Add(newrow);
            }

            this.m_mainBox.AuthListSet.Tables.Add(authTable);
        }

        //=========================================================================================
        //
        //=========================================================================================
    }
}