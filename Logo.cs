using System.IO;
using System;
using System.Drawing;

namespace chatbot_part02
{
    public class Logo
    {
        public Logo()
        {
            // Get the full path to the project directory
            string path_project = AppDomain.CurrentDomain.BaseDirectory;

            // Replacing "bin\\Debug\\"
            string new_path_project = path_project.Replace("bin\\Debug\\", "");

            // Combine the project full path with the image name
            string full_path = Path.Combine(new_path_project, "logo02.jpg");

            // Add the ASCII to the image
            try
            {
                Bitmap image = new Bitmap(full_path);
                image = new Bitmap(image, new Size(210, 200)); // Resizing the image

                // Loop through each pixel of the image
                for (int height = 0; height < image.Height; height++)
                {
                    // The width
                    for (int width = 0; width < image.Width; width++)
                    {
                        // Get the color of the pixel
                        Color pixelColor = image.GetPixel(width, height);

                        // Calculate the brightness of the pixel (average of R, G, B)
                        int color = (pixelColor.R + pixelColor.G + pixelColor.B) / 3;

                        // Choose an ASCII character based on the brightness
                        char ascii_design;
                        if (color > 200) ascii_design = '.';
                        else if (color > 150) ascii_design = '*';
                        else if (color > 100) ascii_design = '0';
                        else if (color > 50) ascii_design = '#';
                        else ascii_design = '@'; // Using '@' for dark pixels for better contrast

                        // Output the ASCII character
                        Console.Write(ascii_design);
                    }

                    // After each row, print a newline to move to the next line in the console
                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        
    }
    }
}