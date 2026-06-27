using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace hahahalib
{
    namespace ui
    {
        /// <summary>
        /// 可隱藏原生頁籤標頭，但保留分頁切換能力的 TabControl。
        /// </summary>
        public class Tab_Controll_Base : TabControl
        {
            private bool _hideTabHeader = false;

            /// <summary>
            /// 設為 <c>true</c> 時隱藏原生 tab 頁籤列。
            /// </summary>
            [Category("Appearance")]
            [Description("隱藏 TabControl 的頁籤列，但保留頁面內容顯示。")]
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
                    // 回傳非 0，避免控制項為頁籤標頭保留空間。
                    m.Result = (IntPtr)1;
                    return;
                }
                base.WndProc(ref m);
            }
        }
    }
}
