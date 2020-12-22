    using System.IO;

    public abstract class PixelStream : Stream
    {
        private Point Location;

        public int Width, Height;

        public PixelStream(int Width, int Height)
        {
            this.Width = Width;
            this.Height = Height;
        }

        public PixelStream(long _Length, int MaxWidth)
        {
            int A = (int)_Length / 3;
            if (_Length < MaxWidth)
            {
                Height = 1;
                Width = A + 1;
            }
            else
            {
                Height = (int)A / MaxWidth;
                if (Height == 0)
                {
                    Height = 1;
                    Width = A + 1;
                }
                else
                    Width = MaxWidth;
            }
            Height += +1;
        }

        private struct Point
        {
            public Point(int X, int Y)
            {
                this.X = X;
                this.Y = Y;
            }
            public int X, Y;
        }


        public override bool CanRead
        {
            get
            {
                return true;
            }
        }

        public override bool CanSeek
        {
            get
            {
                return false;
            }
        }

        public override bool CanWrite
        {
            get
            {
                return true;
            }
        }

        public override long Length
        {
            get
            {
                return ImageWidth() * Height;
            }
        }

        public override long Position
        {
            get
            {
                return (long)PointToInteger(Location, ImageWidth());
            }
            set
            {
                Location = IntegerToPoint((int)value, ImageWidth());
            }
        }

        private Point IntegerToPoint(int Value, int Width)
        {
            int X = 0, Y = 0;
            int vlr = Value;
            while (true)
            {
                if (vlr < Width)
                    break;
                vlr += -Width;
                Y += +1;
            }
            X = vlr;
            return new Point(X, Y);
        }

        private int PointToInteger(Point Value, int Width)
        {
            return (Width * Value.Y) + Value.X;
        }

        private int ImageWidth()
        {
            return Width * 3;
        }

        public override void Flush()
        {
        }

        public override void SetLength(long value)
        {
        }

        private Pixel GetPixel()
        {
            Point P = IntegerToPoint(Location.X, 3);
            return new Pixel(P.Y, Location.Y, P.X + 1);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            var Total = count;
            for (var i = offset; i <= Total - 1; i++)
                WriteByte(buffer[i]);
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return 0;
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            int SD = 0;
            var Total = count - 1;
            for (var i = offset; i <= Total; i++)
            {
                var A = Total - i;
                buffer[i] = (byte)ReadByte();
                SD += +1;
            }
            return SD;
        }

        public override int ReadByte()
        {
            Pixel Args = GetPixel();
            GetPixel(Args);
            Position += +1;
            return Args.Value;
        }


        public override void WriteByte(byte value)
        {
            Pixel Args = GetPixel();
            Args.Value = value;
            SetPixel(Args);
            Position += +1;
        }

        public abstract void SetPixel(Pixel e);
        public abstract void GetPixel(Pixel e);
    }

    public class Pixel
    {
        public Pixel(int X, int Y, int Index)
        {
            _X = X;
            _Y = Y;
            _Index = Index;
        }
        private int _X;
        public int X
        {
            get
            {
                return _X;
            }
        }
        private int _Y;
        public int Y
        {
            get
            {
                return _Y;
            }
        }
        public byte Value { get; set; }
        private int _Index;
        public int Index
        {
            get
            {
                return _Index;
            }
        }
    }
 
