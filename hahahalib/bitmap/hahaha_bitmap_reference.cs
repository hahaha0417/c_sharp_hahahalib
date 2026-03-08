using Emgu.CV;
using System;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace hahahalib
{
    public class hahaha_bitmap_reference : IDisposable
    {
        public Mat? Mat_;
        public Bitmap? Bitmap_;
        public int Count_Reference_ = 1;
        public bool Disposed_;
        public readonly object lock_ = new object();

        // ---------------------------------------------------------
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
        // ---------------------------------------------------------
        public Bitmap Get_Bitmap()
        {
            return Bitmap_!;
        }
        // ---------------------------------------------------------
        public void Add_Ref()
        {
            lock (lock_)
            {
                Count_Reference_++;
            }
        }
        // ---------------------------------------------------------
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
        // ---------------------------------------------------------
    }
}
