using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Converters;
using System.Windows.Media.Imaging;

namespace Kercsi
{
    internal class Dice
    {
        Random random = new Random();
        public int[] Forest { get; set; } = { 0, 0 };
        public int Mountain { get; set; }
        public int Hill { get; set; }
        public int Mead { get; set; }
        public int Sand { get; set; }

        public string Roll()
        {
            int rand = random.Next(1, 7);
            string imagePath = $"Images/Dice/{rand}.png";
            ImageBrush ib = new();
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(imagePath, UriKind.Relative);
            bitmap.EndInit();
            ImageSource imageSource = bitmap;
            ib.ImageSource = imageSource;
            MainWindow.DiceOnBoard.Fill = ib;

            if (rand == Mountain)
            {
                return "Mountain";
            }
            else if (rand == Hill)
            {
                return "Hill";
            }
            else if (rand == Mead)
            {
                return "Meadow";
            }
            else if (rand == Sand)
            {
                return "Sand";
            }
            else
            {
                return "Forest";
            }

        }

        public Dice()
        {
            int[] values = { 1, 2, 3, 4, 5, 6 };
            Random rnd = new Random();
            for (int i = 0; i < values.Length; i++)
            {
                int randomIndex = rnd.Next(values.Length);
                int temp = values[i];
                values[i] = values[randomIndex];
                values[randomIndex] = temp;
            }

            Forest[0] = values[0];
            Forest[1] = values[1];
            Mountain = values[2];
            Hill = values[3];
            Mead = values[4];
            Sand = values[5];
        }
    }
}
