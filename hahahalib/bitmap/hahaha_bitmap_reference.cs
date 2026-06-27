using Emgu.CV;
using System;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace hahahalib
{
    /// <summary>
    /// 以手動參考計數共享 EmguCV 的 <see cref="Mat"/> 與對應的 <see cref="Bitmap"/> 視圖。
    /// Bitmap 直接指向 Mat 的記憶體，因此兩者生命週期必須一致。
    /// </summary>
    public class hahaha_bitmap_reference : IDisposable
    {
        public Mat? Mat_;
        public Bitmap? Bitmap_;
        public int Count_Reference_ = 1;
        public bool Disposed_;
        public readonly object lock_ = new object();

        public hahaha_bitmap_reference(Mat mat)
        {
            Mat_ = mat;

            int width_ = Mat_.Cols;
            int height_ = Mat_.Rows;
            int stride_ = (int)Mat_.Step;
            IntPtr ptr_ = Mat_.DataPointer;

            PixelFormat format_ = PixelFormat.Format24bppRgb;

            if (Mat_.NumberOfChannels == 4)
            {
                format_ = PixelFormat.Format32bppArgb;
            }

            Bitmap_ = new Bitmap(width_, height_, stride_, format_, ptr_);
        }

        /// <summary>
        /// 取得由 Mat 記憶體緩衝區支撐的 Bitmap 視圖。
        /// </summary>
        public Bitmap Get_Bitmap()
        {
            return Bitmap_!;
        }

        /// <summary>
        /// 當有其他持有者要共用影像時，增加手動參考計數。
        /// </summary>
        public void Add_Ref()
        {
            lock (lock_)
            {
                Count_Reference_++;
            }
        }

        /// <summary>
        /// 釋放一個參考，當計數歸零時一併釋放 Mat 與 Bitmap。
        /// </summary>
        public void Dispose()
        {
            if (Disposed_)
            {
                return;
            }

           lock (lock_)
            {
                Count_Reference_--;
            }
            if (Count_Reference_ <= 0)
            {
                if (!Disposed_)
                {
                    Bitmap_?.Dispose();
                    Mat_?.Dispose();
                    Disposed_ = true;
                }
            }
                
            
        }
    }
}
