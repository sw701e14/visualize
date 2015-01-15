using System.Drawing;
using System.Drawing.Drawing2D;

namespace BikeVisualizer
{
    public static class MatrixEntension
    {
        public static PointF TransformPoint(this Matrix matrix, PointF point)
        {
            PointF[] array = new PointF[] { point };
            matrix.TransformPoints(array);
            return array[0];
        }
    }
}
