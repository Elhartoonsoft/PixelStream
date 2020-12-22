using PixelStream;
using System.Drawing;

public class ImageStream : PixelStream.PixelStream
{
    private Bitmap Bitmap = null;
    public Bitmap BaseBitmap
    {
        get
        {
            return Bitmap;
        }
    }
    public ImageStream(Bitmap _Image) : base(_Image.Width, _Image.Height)
    {
        Bitmap = _Image;
    }
    public ImageStream(long _Length, int MaxWidth) : base(_Length, MaxWidth)
    {
        Bitmap = new Bitmap(Width, Height);
    }
    public override void SetPixel(Pixel e)
    {
        Color P = Bitmap.GetPixel(e.X, e.Y);
        if (e.Index == 1)
            Bitmap.SetPixel(e.X, e.Y, Color.FromArgb(255, e.Value, P.G, P.B));
        else if (e.Index == 2)
            Bitmap.SetPixel(e.X, e.Y, Color.FromArgb(255, P.R, e.Value, P.B));
        else if (e.Index == 3)
            Bitmap.SetPixel(e.X, e.Y, Color.FromArgb(255, P.R, P.G, e.Value));
    }
    public override void GetPixel(Pixel e)
    {
        Color P = Bitmap.GetPixel(e.X, e.Y);
        if (e.Index == 1)
            e.Value = P.R;
        else if (e.Index == 2)
            e.Value = P.G;
        else if (e.Index == 3)
            e.Value = P.B;
    }
}


