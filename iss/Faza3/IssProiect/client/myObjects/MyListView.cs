using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client.myObjects
{
    class ListViewEx : ListView
    {
        const int WM_LBUTTONDOWN = 0x0201;
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_LBUTTONDOWN)
            {
                if (suppressClick())
                {
                    m.Result = (IntPtr)1;
                    return;
                }
            }
            base.WndProc(ref m);
        }
        private bool suppressClick()
        {
            var hitTest = HitTest(PointToClient(MousePosition));
            if (hitTest.Item == null || string.IsNullOrEmpty(hitTest.Item.Text))
            {
                return true;
            }

            return false;
        }

    }
}
