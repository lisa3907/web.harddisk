using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace WebHard.WinCtrl.Library
{
    public class ContextMenuItem
    {
        private int index;
        private MenuItem menuItem;

        public ContextMenuItem(int index)
        {
            this.index = index;
        }

        public ContextMenuItem(int index, MenuItem contextItem)
        {
            this.index = index;
            menuItem = contextItem;
        }

        public int Index
        {
            get
            {
                return index;
            }
        }

        public MenuItem MenuItem
        {
            get
            {
                return menuItem;
            }
        }
    }
}
