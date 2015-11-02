using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Services
{
	class Binarization
    {
        private const int COLOR_COUNT = 256;

        public Bitmap process(Bitmap bitmap)
        {
            Bitmap newBitmap = toGray(bitmap);
            newBitmap = binarize(bitmap);
            return newBitmap;
        }

        private Bitmap toGray(Bitmap image)
        {
            Bitmap grayscaleImage = new Bitmap(image.Width, image.Height);
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    Color pixelColor = image.GetPixel(x, y);
                    int alpha = pixelColor.A;
                    int red = pixelColor.R;
                    int blue = pixelColor.B;
                    int green = pixelColor.G;
                    red = (int) (0.21 * red + 0.71 * green + 0.07 * blue);
                    Color newPixelColor = colorToRGB(alpha, red, green, blue);
                    grayscaleImage.SetPixel(x, y, newPixelColor);
                }
            }
            return grayscaleImage;
        }

        private Bitmap binarize(Bitmap originalImage)
        {
            int threshold = otsuTreshold(originalImage);
            Bitmap resultImage = new Bitmap(originalImage.Width, originalImage.Height);
 
            for (int i = 0; i < originalImage.Width; i++)
            {
                for (int j = 0; j < originalImage.Height; j++)
                {
                    int red = originalImage.GetPixel(i, j).R;
                    int alpha = originalImage.GetPixel(i, j).A;
                    int newPixel;
                    if (red > threshold)
                    {
                        newPixel = 255;
                    }
                    else
                    {
                        newPixel = 0;
                    }
                    Color newPixelColor = colorToRGB(alpha, newPixel, newPixel, newPixel);
                    resultImage.SetPixel(i, j, newPixelColor);

                }
            }
            return resultImage;
        }


        private int otsuTreshold(Bitmap originalImage)
        {

            int[] histogram = createHistogram(originalImage);
            int total = originalImage.Height * originalImage.Width;

            float sum = 0;
            for (int i = 0; i < 256; i++) sum += i * histogram[i];

            float sumB = 0;
            int wB = 0;
            int wF = 0;

            float varMax = 0;
            int threshold = 0;

            for (int i = 0; i < 256; i++)
            {
                wB += histogram[i];
                if (wB == 0) continue;
                wF = total - wB;

                if (wF == 0) break;

                sumB += (float)(i * histogram[i]);
                float mB = sumB / wB;
                float mF = (sum - sumB) / wF;

                float varBetween = (float)wB * (float)wF * (mB - mF) * (mB - mF);

                if (varBetween > varMax)
                {
                    varMax = varBetween;
                    threshold = i;
                }
            }

            return threshold;

        }



        private Color colorToRGB(int alpha, int red, int green, int blue)
        {

            int newPixel = 0;
            newPixel += alpha;
            newPixel = newPixel << 8;
            newPixel += red;
            newPixel = newPixel << 8;
            newPixel += green;
            newPixel = newPixel << 8;
            newPixel += blue;
            Color newpixelColor = Color.FromArgb(newPixel);
            return newpixelColor;
        }


        private int[] createHistogram(Bitmap image)
        {
            int[] histogramm = new int[256];
            for (int i = 0; i < COLOR_COUNT; i++)
            {
                histogramm[i] = 0;
            }
            

            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    Color pixelColor = image.GetPixel(x, y);
                    byte red = pixelColor.R;
                    histogramm[red]++;
                }
            }
            return histogramm;
        }
        


    }
}
