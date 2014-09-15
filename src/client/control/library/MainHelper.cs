using System;
using System.Data;
using System.Windows.Forms;

namespace WebHard.WinCtrl.Library
{
    //=============================================================================================
    //
    //=============================================================================================
    /// <summary></summary>
    public class MainHelper : System.IDisposable
    {
        //=========================================================================================
        //
        //=========================================================================================
        /// <summary></summary>
        public MainHelper()
        {
            dragging = false;
        }

        //=========================================================================================
        //
        //=========================================================================================
        public TreeNode dragNode, dropNode, overNode, currNode;
        public bool dragging;

        public TreeNode dropTreeNode;
        public int rootIndex;

        //=========================================================================================
        // System.IDisposable ��� ����
        //=========================================================================================
        /// <summary></summary>
        public void Dispose()
        {
        }
    }
}
