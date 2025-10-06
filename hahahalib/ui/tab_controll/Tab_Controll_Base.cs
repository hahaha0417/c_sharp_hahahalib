using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace hahahalib
{
    namespace ui
    {
        public class Tab_Controll_Base : TabControl
        {
            private bool _hideTabHeader = false;

            /// <summary>
            /// 設定是否隱藏 Tab 頁籤
            /// </summary>
            [Category("Appearance")]
            [Description("設定是否隱藏 TabControl 的頁籤")]
            [DefaultValue(false)]
            public bool HideTabHeader
            {
                get => _hideTabHeader;
                set
                {
                    if (_hideTabHeader != value)
                    {
                        _hideTabHeader = value;
                        this.Invalidate(); // 重新繪製
                    }
                }
            }

            protected override void WndProc(ref Message m)
            {
                const int TCM_ADJUSTRECT = 0x1328;
                if (m.Msg == TCM_ADJUSTRECT && !DesignMode && _hideTabHeader)
                {
                    m.Result = (IntPtr)1; // 隱藏 tab header
                    return;
                }
                base.WndProc(ref m);
            }
        }
    }
}
