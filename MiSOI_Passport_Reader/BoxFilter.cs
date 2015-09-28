using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Filter
{
    public class BoxFilter
    {
        int[] kernel = new int[] { -1, -1, -1, -1, 9, -1, -1, -1, -1 };
        int[] dX = {-1, 0, 1, -1, 0, 1, -1, 0, 1};
        int[] dY = {-1, -1, -1, 0, 0, 0, 1, 1, 1};
        public Bitmap Transform(Bitmap p, int power)
        {
            if (power >= 0 && power < 20)
            {
                kernel[4] =  power;
            }

            int height = p.Height;
            int width = p.Width;
            Color[] temp = new Color[height * width];


            int denominator = 0;
            for (int i = 0; i < kernel.Length; i++)
            {
                denominator += kernel[i];
            }
            if (denominator == 0) denominator = 1;


            int red, green, blue, ired, igreen, iblue;
            Color rgb;

            for (int i = 1; i < height - 1; i++)
            {
                for (int j = 1; j < width - 1; j++)
                {
                    red = green = blue = 0;
                    
                    for (int k = 0; k < kernel.Length; k++)
                    {
                        rgb = p.GetPixel(j + dX[k], i + dY[k]);
                        red += rgb.R * kernel[k];
                        green += rgb.G * kernel[k];
                        blue += rgb.B * kernel[k];
                    }

                    ired = (int)(red / denominator);
                    igreen = (int)(green / denominator);
                    iblue = (int)(blue / denominator);

                    if (ired > 255) 
                        ired = 255;
                    else if (ired < 0) ired = 0;

                    if (igreen > 255) 
                        igreen = 255;
                    else if (igreen < 0) igreen = 0;

                    if (iblue > 255) 
                        iblue = 255;
                    else if (iblue < 0) iblue = 0;

                    temp[(i * width) + j] = Color.FromArgb(255, ired, igreen, iblue);
                }
            }

            return ConvertToBitmap(temp, width, height);
        }

        public Bitmap ConvertToBitmap(Color[] pixels, int width, int height)
        {
            Bitmap result = new Bitmap(width, height);

            for(int i = 0; i < result.Height; i++)
            {
                for (int j = 0; j < result.Width; j++)
                {
                    result.SetPixel(j, i, pixels[(i * result.Width) + j]);
                }
            }

            return result;
        }
    }
}
