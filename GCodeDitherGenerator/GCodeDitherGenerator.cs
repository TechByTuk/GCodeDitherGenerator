using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Imaging.Filters;
using AForge.Imaging.ColorReduction;
using System.Runtime.InteropServices;
using System.IO;

namespace GCodeDitherGenerator
{
    public partial class GCodeDitherGenerator : Form
    {
        public Bitmap OriginalImage { get; set; }
        public Bitmap WorkingImage { get; set; }
        private List<Dot> dotsList { get; set; }
        public int MinX { get; set; }
        public int MaxX { get; set; }
        public int MinY { get; set; }
        public int MaxY { get; set; }
        public const int LimitXMm = 254;
        public const int LimitYMm = 185;
        public double dotsPerMm { get; set; }
        
        public GCodeDitherGenerator()
        {
            InitializeComponent();
            OriginalPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            OutputPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            OutputProgressBar.Maximum = 100;
            ProcessImageButton.Enabled = false;
            GenerateGCodeButton.Enabled = false;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.SelectedItem = comboBox1.Items[0];
        }

        private void importImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Title = "Open Image";
                dlg.Filter = "Image Files (*.bmp;*.jpg;*.jpeg,*.png)|*.BMP;*.JPG;*.JPEG;*.PNG";

                if (dlg.ShowDialog() == DialogResult.OK)
                {

                    OriginalImage = (new Bitmap(dlg.FileName));
                    WorkingImage = new Bitmap(OriginalImage);

                    PictureBox PictureBox1 = new PictureBox();

                    // Create a new Bitmap object from the picture file on disk,
                    // and assign that to the OriginalPictureBox.Image property
                    OriginalPictureBox.Image = new Bitmap(OriginalImage);
                    ProcessImageButton.Enabled = true;
                }
            }
        }

        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        private void ProcessImageButton_Click(object sender, EventArgs e)
        {
            if (OutputPictureBox.Image != null)
                OutputPictureBox.Image.Dispose();

            if (WorkingImage != null)
            {
                WorkingImage.Dispose();
                WorkingImage = new Bitmap(OriginalImage);
                WorkingImage = ResizeImage(WorkingImage, OriginalImage.Width * (int)DotSizeNumericUpDown.Value / 50, OriginalImage.Height * (int)DotSizeNumericUpDown.Value / 50); 
            }
            
            OutputProgressBar.Value = 0;
            var WorkTask = new Task(new Action(() => ProcessImage()));
            //WorkTask.Start();
            ProcessImage();
            GenerateGCodeButton.Enabled = true;
        }

        private void ProcessImage()
        {
            ApplyImageSettings();
            //Apply grayscale
            // create grayscale filter (BT709)
            // OLD: Grayscale filter = new Grayscale(0.2125, 0.7154, 0.0721);
            Grayscale filter = new Grayscale(0.299, 0.587, 0.114);
            // apply the filter
            WorkingImage = filter.Apply(WorkingImage);
            WorkingImage = DitherImage(WorkingImage);
            ///OutputPictureBox.Invoke(new Action(() => OutputPictureBox.Image = new Bitmap(WorkingImage)));

            ListAllDots(WorkingImage);

            OutputProgressBar.Invoke(new Action(() => OutputProgressBar.Value = 100));

            OutputPictureBox.Invoke(new Action(() => OutputPictureBox.Image = new Bitmap(WorkingImage)));
        }

        private void ListAllDots(Bitmap bmp)
        {
            // Lock the bitmap's bits.  
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            // Get the address of the first line.
            IntPtr ptr = bmpData.Scan0;

            // Declare an array to hold the bytes of the bitmap.
            int bytes = bmpData.Stride * bmp.Height;
            byte[] rgbValues = new byte[bytes];

            // Copy the RGB values into the array.
            Marshal.Copy(ptr, rgbValues, 0, bytes);

            MinX = int.MaxValue;
            MaxX = 0;
            MinY = int.MaxValue;
            MaxY = 0;

            int stride = bmpData.Stride;
            dotsList = new List<Dot>();

            for (int Y = 0; Y < bmpData.Height; Y++)
            {
                for (int X = 0; X < bmpData.Width; X++)
                {
                    var value = (byte)(rgbValues[(Y * stride) + (X * 3)]);
                    if (value < 40)
                    {
                        dotsList.Add(new Dot(value, X, Y));
                        if (X < MinX)
                        {
                            MinX = X;
                        }
                        if (X > MaxX)
                        {
                            MaxX = X;
                        }
                        if (Y < MinY) 
                        {
                            MinY = Y;
                        }
                        if (Y > MaxY)
                        {
                            MaxY = Y;
                        }
                    }

                }
            }
            
            if ((MaxX - MinX) / (double)LimitXMm > (MaxY - MinY) / (double)LimitYMm)
            {
                dotsPerMm = (MaxX - MinX) / (double)LimitXMm;
            }
            else
            {
                dotsPerMm = (MaxY - MinY) / (double)LimitYMm;
            }

            DotsPerMmLabel.Invoke(new Action(() => DotsPerMmLabel.Text = Math.Round(dotsPerMm,2).ToString()));

            NumOfDotsLabel.Invoke(new Action(() => NumOfDotsLabel.Text = dotsList.Count.ToString()));

            WorkingImage = new Bitmap(bmpData.Width, bmpData.Height);

            List<Dot> sortedDotsList = new List<Dot>();

            FindNextDot(dotsList[0], ref sortedDotsList);

            while (closestDot != null)
            {
                FindNextDot(closestDot, ref sortedDotsList);
            }

            using (Graphics g = Graphics.FromImage(WorkingImage))
            {
                using (Brush b = new SolidBrush(Color.Black))
                {
                    foreach (var dot in sortedDotsList)
                    {
                        g.FillEllipse(b, dot.x, dot.y, 3, 3);
                    }
                }
            }

            //foreach (var dot in sortedDotsList)
            //{
            //    WorkingImage.SetPixel(dot.x, dot.y, Color.Black);
            //}
            
            dotsList = sortedDotsList;
            
        }

        private Dot closestDot; 

        private void FindNextDot(Dot dot, ref List<Dot> sortedDotsList)
        {
            var x1 = dot.x;
            var y1 = dot.y;
            dot.used = true;
            sortedDotsList.Add(dot);
                        
            var minDistance = int.MaxValue;
            closestDot = null;

            foreach (var oDot in dotsList)
            {
                if (oDot.used == false)
                {
                    var x2 = oDot.x;
                    var y2 = oDot.y;

                    var dist = ((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));

                    if (dist < minDistance)
                    {
                        minDistance = dist;
                        closestDot = oDot;
                    }
                }

            }
        }

        private Bitmap TukImage(Bitmap bmp)
        {
            
            int[] xCircleCoordArray = new int[663] {0, 1, -1, 0, 0, 1, -1, 1, -1, 2, -2, 0, 0, 2, -2, 2, -2, 1, -1, 1, -1, 2, -2, 2, -2, 3, -3, 0, 0, 3, -3, 3, -3, 1, -1, 1, -1, 3, -3, 3, -3, 2, -2, 2, -2, 4, -4, 0, 0, 4, -4, 4, -4, 1, -1, 1, -1, 3, -3, 3, -3, 4, -4, 4, -4, 2, -2, 2, -2, 5, -5, 4, -4, 4, -4, 3, -3, 3, -3, 0, 0, 5, -5, 5, -5, 1, -1, 1, -1, 5, -5, 5, -5, 2, -2, 2, -2, 4, -4, 4, -4, 5, -5, 5, -5, 3, -3, 3, -3, 6, -6, 0, 0, 6, -6, 6, -6, 1, -1, 1, -1, 6, -6, 6, -6, 2, -2, 2, -2, 5, -5, 5, -5, 4, -4, 4, -4, 6, -6, 6, -6, 3, -3, 3, -3, 7, -7, 0, 0, 7, -7, 7, -7, 5, -5, 5, -5, 1, -1, 1, -1, 6, -6, 6, -6, 4, -4, 4, -4, 7, -7, 7, -7, 2, -2, 2, -2, 7, -7, 7, -7, 3, -3, 3, -3, 6, -6, 6, -6, 5, -5, 5, -5, 8, -8, 0, 0, 8, -8, 8, -8, 7, -7, 7, -7, 4, -4, 4, -4, 1, -1, 1, -1, 8, -8, 8, -8, 2, -2, 2, -2, 6, -6, 6, -6, 8, -8, 8, -8, 3, -3, 3, -3, 7, -7, 7, -7, 5, -5, 5, -5, 8, -8, 8, -8, 4, -4, 4, -4, 9, -9, 0, 0, 9, -9, 9, -9, 1, -1, 1, -1, 9, -9, 9, -9, 7, -7, 7, -7, 6, -6, 6, -6, 2, -2, 2, -2, 8, -8, 8, -8, 5, -5, 5, -5, 9, -9, 9, -9, 3, -3, 3, -3, 9, -9, 9, -9, 4, -4, 4, -4, 7, -7, 7, -7, 10, -10, 8, -8, 8, -8, 6, -6, 6, -6, 0, 0, 10, -10, 10, -10, 1, -1, 1, -1, 10, -10, 10, -10, 2, -2, 2, -2, 9, -9, 9, -9, 5, -5, 5, -5, 10, -10, 10, -10, 3, -3, 3, -3, 8, -8, 8, -8, 7, -7, 7, -7, 10, -10, 10, -10, 4, -4, 4, -4, 9, -9, 9, -9, 6, -6, 6, -6, 11, -11, 0, 0, 11, -11, -11, 1, -1, 1, 11, -11, 11, -11, 10, -10, 10, -10, 5, -5, 5, -5, 2, -2, 2, -2, 8, -8, 8, -8, 11, -11, 11, -11, 9, -9, 9, -9, 7, -7, 7, -7, 3, -3, 3, -3, 10, -10, 10, -10, 6, -6, 6, -6, 11, -11, 11, -11, 4, -4, 4, -4, 12, -12, 0, 0, 12, -12, 12, -12, 9, -9, 9, -9, 8, -8, 8, -8, 1, -1, 1, -1, 11, -11, 11, -11, 5, -5, 5, -5, 12, -12, 12, -12, 2, -2, 2, -2, 10, -10, 10, -10, 7, -7, 7, -7, 12, -12, 12, -12, 3, -3, 3, -3, 11, -11, 11, -11, 6, -6, 6, -6, 12, -12, 12, -12, 4, -4, 4, -4, 9, -9, 9, -9, 10, -10, 10, -10, 8, -8, 8, -8, 13, -13, 12, -12, 12, -12, 5, -5, 5, -5, 0, 0, 13, -13, 13, -13, 11, -11, 11, -11, 7, -7, 7, -7, 1, -1, 1, -1, 13, -13, 13, -13, 2, -2, 2, -2, 13, -13, 13, -13, 3, -3, 3, -3, 12, -12, 12, -12, 6, -6, 6, -6, 10, -10, 10, -10, 9, -9, 9, -9, 13, -13, 13, -13, 11, -11, 11, -11, 8, -8, 8, -8, 4, -4, 4, -4, 12, -12, 12, -12, 7, -7, 7, -7, 13, -13, 13, -13, 5, -5, 5, -5, 14, -14, 0, 0, 14, -14, 14, -14, 1, -1, 1, -1, 14, -14, 14, -14, 10, -10, 10, -10, 2, -2, 2, -2, 11, -11, 11, -11, 9, -9, 9, -9, 14, -14, 14, -14, 13, -13, 13, -13, 6, -6, 6, -6, 3, -3, 3, -3, 12, -12, 12, -12, 8, -8, 8, -8};
            int[] yCircleCoordArray = new int[663] {0, 0, 0, -1, 1, -1, -1, 1, 1, 0, 0, -2, 2, -1, -1, 1, 1, -2, -2, 2, 2, -2, -2, 2, 2, 0, 0, -3, 3, -1, -1, 1, 1, -3, -3, 3, 3, -2, -2, 2, 2, -3, -3, 3, 3, 0, 0, -4, 4, -1, -1, 1, 1, -4, -4, 4, 4, -3, -3, 3, 3, -2, -2, 2, 2, -4, -4, 4, 4, 0, 0, -3, -3, 3, 3, -4, -4, 4, 4, -5, 5, -1, -1, 1, 1, -5, -5, 5, 5, -2, -2, 2, 2, -5, -5, 5, 5, -4, -4, 4, 4, -3, -3, 3, 3, -5, -5, 5, 5, 0, 0, -6, 6, -1, -1, 1, 1, -6, -6, 6, 6, -2, -2, 2, 2, -6, -6, 6, 6, -4, -4, 4, 4, -5, -5, 5, 5, -3, -3, 3, 3, -6, -6, 6, 6, 0, 0, -7, 7, -1, -1, 1, 1, -5, -5, 5, 5, -7, -7, 7, 7, -4, -4, 4, 4, -6, -6, 6, 6, -2, -2, 2, 2, -7, -7, 7, 7, -3, -3, 3, 3, -7, -7, 7, 7, -5, -5, 5, 5, -6, -6, 6, 6, 0, 0, -8, 8, -1, -1, 1, 1, -4, -4, 4, 4, -7, -7, 7, 7, -8, -8, 8, 8, -2, -2, 2, 2, -8, -8, 8, 8, -6, -6, 6, 6, -3, -3, 3, 3, -8, -8, 8, 8, -5, -5, 5, 5, -7, -7, 7, 7, -4, -4, 4, 4, -8, -8, 8, 8, 0, 0, -9, 9, -1, -1, 1, 1, -9, -9, 9, 9, -2, -2, 2, 2, -6, -6, 6, 6, -7, -7, 7, 7, -9, -9, 9, 9, -5, -5, 5, 5, -8, -8, 8, 8, -3, -3, 3, 3, -9, -9, 9, 9, -4, -4, 4, 4, -9, -9, 9, 9, -7, -7, 7, 7, 0, 0, -6, -6, 6, 6, -8, -8, 8, 8, -10, 10, -1, -1, 1, 1, -10, -10, 10, 10, -2, -2, 2, 2, -10, -10, 10, 10, -5, -5, 5, 5, -9, -9, 9, 9, -3, -3, 3, 3, -10, -10, 10, 10, -7, -7, 7, 7, -8, -8, 8, 8, -4, -4, 4, 4, -10, -10, 10, 10, -6, -6, 6, 6, -9, -9, 9, 9, 0, 0, -11, 11, -1, -1, 1, -11, -11, 11, -2, -2, 2, 2, -5, -5, 5, 5, -10, -10, 10, 10, -11, -11, 11, 11, -8, -8, 8, 8, -3, -3, 3, 3, -7, -7, 7, 7, -9, -9, 9, 9, -11, -11, 11, 11, -6, -6, 6, 6, -10, -10, 10, 10, -4, -4, 4, 4, -11, -11, 11, 11, 0, 0, -12, 12, -1, -1, 1, 1, -8, -8, 8, 8, -9, -9, 9, 9, -12, -12, 12, 12, -5, -5, 5, 5, -11, -11, 11, 11, -2, -2, 2, 2, -12, -12, 12, 12, -7, -7, 7, 7, -10, -10, 10, 10, -3, -3, 3, 3, -12, -12, 12, 12, -6, -6, 6, 6, -11, -11, 11, 11, -4, -4, 4, 4, -12, -12, 12, 12, -9, -9, 9, 9, -8, -8, 8, 8, -10, -10, 10, 10, 0, 0, -5, -5, 5, 5, -12, -12, 12, 12, -13, 13, -1, -1, 1, 1, -7, -7, 7, 7, -11, -11, 11, 11, -13, -13, 13, 13, -2, -2, 2, 2, -13, -13, 13, 13, -3, -3, 3, 3, -13, -13, 13, 13, -6, -6, 6, 6, -12, -12, 12, 12, -9, -9, 9, 9, -10, -10, 10, 10, -4, -4, 4, 4, -8, -8, 8, 8, -11, -11, 11, 11, -13, -13, 13, 13, -7, -7, 7, 7, -12, -12, 12, 12, -5, -5, 5, 5, -13, -13, 13, 13, 0, 0, -14, 14, -1, -1, 1, 1, -14, -14, 14, 14, -2, -2, 2, 2, -10, -10, 10, 10, -14, -14, 14, 14, -9, -9, 9, 9, -11, -11, 11, 11, -3, -3, 3, 3, -6, -6, 6, 6, -13, -13, 13, 13, -14, -14, 14, 14, -8, -8, 8, 8, -12, -12, 12, 12};
                        
            // Lock the bitmap's bits.  
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            // Get the address of the first line.
            IntPtr ptr = bmpData.Scan0;

            // Declare an array to hold the bytes of the bitmap.
            int bytes = bmpData.Stride * bmp.Height;
            byte[] rgbValues = new byte[bytes];

            int curGrey = 0;
            int curX = 0;
            int curY = 0;
            int coordGrey = 0;

            // Copy the RGB values into the array.
            Marshal.Copy(ptr, rgbValues, 0, bytes);
            
            Bitmap NewBitmap = new Bitmap(bmpData.Width, bmpData.Height);

            int stride = bmpData.Stride;
            
            int countSums;            

            for (int bmpY = 0; bmpY < bmpData.Height; bmpY++)
            {
                for (int bmpX = 0; bmpX < bmpData.Width; bmpX++)
                {
                    NewBitmap.SetPixel(bmpX, bmpY, Color.White);
                    countSums = 0;                    
                    for (int iCircle = 0; iCircle < xCircleCoordArray.Length; iCircle++)
                    {
                        curX = bmpX + xCircleCoordArray[iCircle];
                        curY = bmpY + yCircleCoordArray[iCircle];

                        if (curX >= 0 & curX < bmpData.Width & curY >= 0 & curY < bmpData.Height)
                        {
                            curGrey = (byte)(rgbValues[(curY * stride) + (curX * 3)]);
                            coordGrey = (byte)(rgbValues[(bmpY * stride) + (bmpX * 3)]);
                            if (curGrey < coordGrey / 0.65 & curGrey > coordGrey * 0.9)
                            {
                                countSums += 1;
                            }
                            if (countSums / 663 > (0.1 * Math.Pow(coordGrey / 255,1.5) + 0.2 * Math.Pow(coordGrey / 255,5) + 0.7 * Math.Pow(coordGrey / 255, 9)) * 1 & countSums > 0)
                            {
                                NewBitmap.SetPixel(bmpX, bmpY, Color.Black);
                                break;
                            }
                        }
                    }
                }
            }

            return NewBitmap;
        }

        private Bitmap DitherImage(Bitmap ImageToFilter)
        {


            string algorithm = "";

            comboBox1.Invoke(new Action(() => algorithm = comboBox1.SelectedItem.ToString()));
            

            switch (algorithm)
            {
                case "Bayer Dithering":
                    return new BayerDithering().Apply(ImageToFilter);
                case "Burkes Dithering":
                    return new BurkesDithering().Apply(ImageToFilter);
                case "Floyd Steinberg Dithering":
                    return new FloydSteinbergDithering().Apply(ImageToFilter);
                case "Jarvis Judice Ninke Dithering":
                    return new JarvisJudiceNinkeDithering().Apply(ImageToFilter);
                case "Ordered Dithering":
                    return new OrderedDithering().Apply(ImageToFilter);
                case "Sierra Dithering":
                    return new SierraDithering().Apply(ImageToFilter);
                case "Stucki Dithering":
                    return new StuckiDithering().Apply(ImageToFilter);
                case "Tuk's Dither":
                    return TukImage(ImageToFilter);
                default:
                    return new FloydSteinbergDithering().Apply(ImageToFilter);
            }
        }

        private void ApplyImageSettings()
        {

            float brightness = (float)BrightnessNumericUpDown.Value / 5000;
            float contrast = (float)ContrastNumericUpDown.Value / 100;
            float gamma = (float)GammaNumericUpDown.Value / 100;

            //Create matrix that will brighten and contrast the image
            float[][] ptsArray ={
            new float[] {contrast, 0, 0, 0, 0}, // scale red
            new float[] {0, contrast, 0, 0, 0}, // scale green
            new float[] {0, 0, contrast, 0, 0}, // scale blue
            new float[] {0, 0, 0, 1.0f, 0}, // don't scale alpha
            new float[] { brightness, brightness, brightness, 0, 1}};

            var imageAttributes = new ImageAttributes();
            imageAttributes.ClearColorMatrix();
            imageAttributes.SetColorMatrix(new ColorMatrix(ptsArray), ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            imageAttributes.SetGamma(gamma, ColorAdjustType.Bitmap);
            Graphics g = Graphics.FromImage(WorkingImage);
            g.DrawImage(OriginalImage, new Rectangle(0, 0, WorkingImage.Width, WorkingImage.Height)
                , 0, 0, OriginalImage.Width, OriginalImage.Height,
                GraphicsUnit.Pixel, imageAttributes);

            //WorkingImage = BitmapInvertColors(WorkingImage);

        }

        private void GenerateGCodeButton_Click(object sender, EventArgs e)
        {
            double offsetXMm = 0;
            double offsetYMm = 0;
            double coordXMm = 0;
            double coordYMm = 0;
            double sharedDeg = 0;
            double rotDeg = 0;

            offsetXMm = (LimitXMm * dotsPerMm - (MaxX - MinX)) / 2 / dotsPerMm - LimitXMm / 2;
            offsetYMm = (LimitYMm * dotsPerMm - (MaxY - MinY)) / 2 / dotsPerMm + 55; // was 39.2 but hit limit

            foreach (var dot in dotsList)
            {
                coordXMm = (dot.x - MinX) / dotsPerMm + offsetXMm;
                coordYMm = (MaxY - dot.y) / dotsPerMm + offsetYMm;

                sharedDeg = Math.Asin(Math.Sqrt(Math.Pow(coordXMm, 2) + Math.Pow(coordYMm, 2)) / 2 / 140) * 180 / Math.PI;
                rotDeg = Math.Atan2(coordYMm, coordXMm) * 180 / Math.PI - 45;

                dot.xDeg = Math.Round(Math.Round((rotDeg + sharedDeg - 2) / 0.05625, 0) * 0.05625, 3);
                dot.yDeg = Math.Round(Math.Round((sharedDeg * 2 - Math.Asin(39.2 / 140) * 180 / Math.PI - 5) / 0.05625, 0) * 0.05625, 3);
            }

            //Create file and close it (so we are not occupying it)
            File.Create("GCodeForPicture.txt").Close();

            StringBuilder builder = new StringBuilder();

            builder.AppendLine("$1=255");
            builder.AppendLine("G04 P0.500");
            builder.AppendLine("M03 S22.000");
            builder.AppendLine("$H");
            builder.AppendLine("G90");

            //Loops through all dots which are already sorted
            foreach (var dot in dotsList)
            {
                //if (!double.IsNaN(dot.xDeg) && !double.IsNaN(dot.yDeg) && dot.xDeg < 160 && dot.yDeg<175)
                //{
                builder.AppendLine("G0 X" + Math.Round(dot.xDeg, 3) + " Y" + Math.Round(dot.yDeg, 3));
                if (PauseNumericUpDown.Value >= 0) {builder.AppendLine("G04 P" + PauseNumericUpDown.Value);}                
                builder.AppendLine("M03 S26.000");
                builder.AppendLine("G04 P0.100");
                builder.AppendLine("M03 S23.000");
                builder.AppendLine("G04 P0.030");
                //}
            }

            //write DotVinci
            builder.AppendLine("M03 S23.000");
            builder.AppendLine("G04 P0.100");
            builder.AppendLine("G0 X5.556 Y14.629");
            builder.AppendLine("M03 S26.000");
            builder.AppendLine("G04 P0.250");
            builder.AppendLine("G0 X1.445 Y13.229");
            builder.AppendLine("M03 S23.000");
            builder.AppendLine("G04 P0.100");
            builder.AppendLine("G0 X5.76 Y14.276");
            builder.AppendLine("M03 S26.000");
            builder.AppendLine("G04 P0.250");
            builder.AppendLine("G0 X5.171 Y15.341");
            builder.AppendLine("G0 X4.331 Y15.463");
            builder.AppendLine("G0 X1.643 Y14.554");
            builder.AppendLine("G0 X1.113 Y13.965");
            builder.AppendLine("G0 X1.623 Y12.863");
            builder.AppendLine("M03 S23.000");
            builder.AppendLine("G04 P0.100");
            builder.AppendLine("G0 X2.983 Y16.885");
            builder.AppendLine("M03 S26.000");
            builder.AppendLine("G04 P0.250");
            builder.AppendLine("G0 X3.122 Y16.514");
            builder.AppendLine("G0 X2.609 Y15.921");
            builder.AppendLine("G0 X1.278 Y15.483");
            builder.AppendLine("G0 X0.477 Y15.647");
            builder.AppendLine("G0 X0.24 Y16.404");
            builder.AppendLine("G0 X0.786 Y16.991");
            builder.AppendLine("G0 X2.082 Y17.414");
            builder.AppendLine("G0 X2.85 Y17.257");
            builder.AppendLine("G0 X2.983 Y16.885");
            builder.AppendLine("M03 S23.000");
            builder.AppendLine("G04 P0.100");
            builder.AppendLine("G0 X3.474 Y19.763");
            builder.AppendLine("M03 S26.000");
            builder.AppendLine("G04 P0.250");
            builder.AppendLine("G0 X0.357 Y18.717");
            builder.AppendLine("G0 X-0.351 Y18.908");
            builder.AppendLine("G0 X0.205 Y19.493");
            builder.AppendLine("M03 S23.000");
            builder.AppendLine("G04 P0.100");
            builder.AppendLine("G0 X2.438 Y18.573");
            builder.AppendLine("M03 S26.000");
            builder.AppendLine("G04 P0.250");
            builder.AppendLine("G0 X2.06 Y20.101");
            builder.AppendLine("M03 S23.000");
            builder.AppendLine("G04 P0.100");
            builder.AppendLine("G0 X3.072 Y21.483");
            builder.AppendLine("M03 S26.000");
            builder.AppendLine("G04 P0.250");
            builder.AppendLine("G0 X-0.663 Y21.072");
            builder.AppendLine("G0 X2.806 Y23.036");
            builder.AppendLine("M03 S23.000");
            builder.AppendLine("G04 P0.100");
            builder.AppendLine("G0 X1.527 Y23.623");
            builder.AppendLine("M03 S26.000");
            builder.AppendLine("G04 P0.250");
            builder.AppendLine("G0 X-0.805 Y22.872");
            builder.AppendLine("M03 S23.000");
            builder.AppendLine("G04 P0.100");
            builder.AppendLine("G0 X2.68 Y24.019");
            builder.AppendLine("M03 S26.000");
            builder.AppendLine("G04 P0.250");
            builder.AppendLine("G0 X2.68 Y24.019");
            builder.AppendLine("M03 S23.000");
            builder.AppendLine("G04 P0.100");
            builder.AppendLine("G0 X1.449 Y24.622");
            builder.AppendLine("M03 S26.000");
            builder.AppendLine("G04 P0.250");
            builder.AppendLine("G0 X-0.844 Y23.884");
            builder.AppendLine("G0 X0.306 Y24.246");
            builder.AppendLine("G0 X1.407 Y25.426");
            builder.AppendLine("G0 X1.392 Y25.831");
            builder.AppendLine("G0 X0.826 Y26.052");
            builder.AppendLine("G0 X-0.852 Y25.519");
            builder.AppendLine("M03 S23.000");
            builder.AppendLine("G04 P0.100");
            builder.AppendLine("G0 X1.409 Y28.906");
            builder.AppendLine("M03 S26.000");
            builder.AppendLine("G04 P0.250");
            builder.AppendLine("G0 X1.376 Y27.667");
            builder.AppendLine("G0 X0.826 Y27.075");
            builder.AppendLine("G0 X-0.273 Y26.722");
            builder.AppendLine("G0 X-0.807 Y26.965");
            builder.AppendLine("G0 X-0.732 Y28.217");
            builder.AppendLine("M03 S23.000");
            builder.AppendLine("G04 P0.100");
            builder.AppendLine("G0 X1.46 Y29.948");
            builder.AppendLine("M03 S26.000");
            builder.AppendLine("G04 P0.250");
            builder.AppendLine("G0 X-0.646 Y29.269");
            builder.AppendLine("M03 S23.000");
            builder.AppendLine("G04 P0.100");
            builder.AppendLine("G0 X2.507 Y30.307");
            builder.AppendLine("M03 S26.000");
            builder.AppendLine("G04 P0.250");
            builder.AppendLine("G0 X2.507 Y30.307");
            builder.AppendLine("M03 S23.000");
            builder.AppendLine("G04 P0.250");
            builder.AppendLine("G0 X5 Y5");
                
            builder.AppendLine("M03 S21.000");
            builder.AppendLine("G04 P0.250");
            builder.AppendLine("G0 X5 Y5");
            builder.AppendLine("$1 = 250");

            //Write finished text to file
            File.AppendAllText("GCodeForPicture.txt", builder.ToString());
        }
    }
}
