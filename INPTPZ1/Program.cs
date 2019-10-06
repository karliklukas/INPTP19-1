using System;
using System.Collections.Generic;
using System.Drawing;


namespace INPTPZ1
{
    /// <summary>
    /// This program should produce Newton fractals.
    /// See more at: https://en.wikipedia.org/wiki/Newton_fractal
    /// </summary>
    class Program
    {
        private static Bitmap bitmap;

        private const int DEFAULT_BITMAP_SIZE = 300;
        private const int NUMBER_OF_ITERATIONS = 30;
        private const double TOLERANCE_LEVEL = 0.5;
        private const double ROOT_TOLERANCE_LEVEL = 0.01;

        private static double xmin, xmax, ymin, ymax, xstep, ystep;

        private static List<ComplexNumber> roots;

        private static readonly Color[] colors = new Color[]
            {
                Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
            };

        private static Polynome polynome, polynomeDerived;
        

        static void Main(string[] args)
        {            
            InitializeValues(args);
            
            InitializePolynome();

            ProcessImage();

            SaveImage();

        }

        private static void InitializeValues(string[] args)
        {
            if (args.Length == 5)
            {       
                if (Int32.TryParse(args[0],out int bitmapSize) &&                    
                    Double.TryParse(args[2], out xmax) &&
                    Double.TryParse(args[3], out xmin) &&
                    Double.TryParse(args[4], out ymax) &&
                    Double.TryParse(args[5], out ymin))
                {
                    bitmap = new Bitmap(bitmapSize, bitmapSize);
                }
                else
                {
                    throw new FormatException("Arguments are not correct.");
                }
            }
            else
            {
                bitmap = new Bitmap(DEFAULT_BITMAP_SIZE, DEFAULT_BITMAP_SIZE);
                xmin = -1.5;
                xmax = 1.5;
                ymin = -1.5;
                ymax = 1.5;
            }            

            xstep = (xmax - xmin) / bitmap.Width;
            ystep = (ymax - ymin) / bitmap.Height;

            roots = new List<ComplexNumber>();
        }

        private static void InitializePolynome()
        {
            polynome = new Polynome()
            {
                Coefficients =
                {
                    new ComplexNumber() { Real = 1 },
                    ComplexNumber.Zero,
                    ComplexNumber.Zero,
                    new ComplexNumber() { Real = 1 }
                }
            };
            
            polynomeDerived = polynome.Derive();

            Console.WriteLine(polynome);
            Console.WriteLine(polynomeDerived);
        }

        private static void ProcessImage()
        {               
            for (int i = 0; i < bitmap.Height; i++)
            {
                for (int j = 0; j < bitmap.Width; j++)
                {
                    ComplexNumber coordinates = CalculatePixelCoordinates(i, j);

                    int iterations = CalculateNewtonIteration(ref coordinates);

                    int rootNumber = CalculateRootNumber(coordinates);
                    
                    Color pixelColor = CalculateColor(iterations, rootNumber);

                    bitmap.SetPixel(j, i, pixelColor);
                }
            }
        }        

        private static ComplexNumber CalculatePixelCoordinates(int bitmapY, int bitmapX)
        {           
            double x = xmin + bitmapX * xstep;
            double y = ymin + bitmapY * ystep;

            ComplexNumber resultCoordinates = new ComplexNumber()
            {
                Real = x,
                Imaginary = y
            };

            if (resultCoordinates.Real == 0)
                resultCoordinates.Real = 0.0001;

            if (resultCoordinates.Imaginary == 0)
                resultCoordinates.Imaginary = 0.0001;

            return resultCoordinates;
        }

        private static int CalculateNewtonIteration(ref ComplexNumber coordinates)
        {           
            int iterations = 0;
            for (int i = 0; i < NUMBER_OF_ITERATIONS; i++)
            {
                ComplexNumber difference = polynome.Eval(coordinates).Divide(polynomeDerived.Eval(coordinates));
                coordinates = coordinates.Subtract(difference);

                if (Math.Pow(difference.Real, 2) + Math.Pow(difference.Imaginary, 2) >= TOLERANCE_LEVEL)
                {
                    i--;
                }
                iterations++;
            }
            return iterations;
        }

        private static int CalculateRootNumber(ComplexNumber coordinates)
        {
            bool rootIsKnown = false;
            int rootNumber = 0;
            for (int i = 0; i < roots.Count; i++)
            {
                if (Math.Pow(coordinates.Real - roots[i].Real, 2) + Math.Pow(coordinates.Imaginary - roots[i].Imaginary, 2) <= ROOT_TOLERANCE_LEVEL)
                {
                    rootIsKnown = true;
                    rootNumber = i;
                }
            }
            if (!rootIsKnown)
            {
                roots.Add(coordinates);
                rootNumber = roots.Count;
            }

            return rootNumber;
        }

        private static Color CalculateColor(int iterations, int rootNumber)
        {
            Color pixelColor = colors[rootNumber % colors.Length];

            pixelColor = Color.FromArgb(
                Math.Min(Math.Max(0, pixelColor.R - iterations * 2), 255), 
                Math.Min(Math.Max(0, pixelColor.G - iterations * 2), 255), 
                Math.Min(Math.Max(0, pixelColor.B - iterations * 2), 255));

            return pixelColor;
        }

        private static void SaveImage()
        {
            bitmap.Save("../../../out.png");
        }
    }
}
