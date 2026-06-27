
using SkiaSharp.Views.Desktop;

namespace hahahalib
{
    namespace ui
    {
        /// <summary>
        /// 專案內給 SkiaSharp 桌面控制項使用的擴充型別。
        /// 目前行為等同 <see cref="SKControl"/>，但保留穩定型別供未來擴充與設計工具使用。
        /// </summary>
        public class SK_Control : SKControl
        {
            public SK_Control()
            {
            }
        }
    }
}
