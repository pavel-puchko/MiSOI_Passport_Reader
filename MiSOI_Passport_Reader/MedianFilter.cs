using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MedianFilter
{
    class MedianFilter
    {
        public Bitmap UseFilter(Bitmap image_in, byte n)
        {
            // filter works only for odd matrix
            if(n % 2 == 0)
            {
                n++;
            }

            Bitmap image_out = new Bitmap(image_in.Width, image_in.Height);
            Color[,] pixels_in = new Color[image_in.Width, image_in.Height];

            for (int x = 0; x < image_in.Width; ++x)
            {
                for (int y = 0; y < image_in.Height; ++y)
                {
                    pixels_in[x, y] = image_in.GetPixel(x, y);
                }
            }

            byte[] sort_R = new byte[n * n];
            byte[] sort_G = new byte[n * n];
            byte[] sort_B = new byte[n * n];

            int offset = (n - 1) / 2;

            // loop through all pixels
            for (int x = offset; x < image_in.Width - offset; ++x)
            {
                for (int y = offset; y < image_in.Height - offset; ++y)
                {
                    // loop through matrix for current pixel
                    for (int k = 0; k < n; k++ )
                    {
                        for (int l = 0; l < n; l++)
                        {
                            sort_R[k * n + l] = pixels_in[x - offset + k, y - offset + l].R;
                            sort_G[k * n + l] = pixels_in[x - offset + k, y - offset + l].G;
                            sort_B[k * n + l] = pixels_in[x - offset + k, y - offset + l].B;
                        }
                    }

                    Array.Sort(sort_R);
                    Array.Sort(sort_G);
                    Array.Sort(sort_B);

                    Color color = Color.FromArgb(sort_R[(n * n - 1) / 2], sort_G[(n * n - 1) / 2], sort_B[(n * n - 1) / 2]);
                    image_out.SetPixel(x, y, color);
                }
            }

            return image_out;
        }
    }
}
