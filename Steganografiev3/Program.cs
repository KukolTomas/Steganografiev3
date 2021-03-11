using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Steganografie
{
    class Program
    {
        static void Main(string[] args)
        {
            //fungují argumenty, ale nefunguje převod na string, vypisuje to náhodné znaky
            string txt = args[2];
            Color barvaObr;
            if (args[1] == "--hide" && args[2] != "" && args[3].Contains(".png"))
            {
                Bitmap obr = new Bitmap("randomObr.png");
                for (int i = 0; i < obr.Width; i++)
                {
                    for (int j = 0; j < obr.Height; j++)
                    {
                        barvaObr = obr.GetPixel(i, j);
                        if (i < 1 && j < txt.Length)
                        {
                            char znak = Convert.ToChar(txt.Substring(j, 1));
                            int hodnota = Convert.ToInt32(znak);
                            obr.SetPixel(i, j, Color.FromArgb(barvaObr.R, barvaObr.G, hodnota));

                        }
                        if (i == obr.Width - 1 && j == obr.Height - 1)
                        {
                            obr.SetPixel(i, j, Color.FromArgb(barvaObr.R, barvaObr.G, txt.Length));
                        }
                    }
                }
                obr.Save(args[3]);
            }
            else if (args[1] == "--show" && args[2].Contains(".png"))
            {
                Bitmap zasifrovanyObr = new Bitmap(args[2]);
                Color posledniPixel = zasifrovanyObr.GetPixel(zasifrovanyObr.Width - 1, zasifrovanyObr.Height - 1);
                Color pixel;
                int delkatxt = posledniPixel.B;
                string desifrovanytxt = "";
                for (int i = 0; i < zasifrovanyObr.Width; i++)
                {
                    for (int j = 0; j < zasifrovanyObr.Height; j++)
                    {
                        pixel = zasifrovanyObr.GetPixel(i, j);
                        if (i < 1 && j < delkatxt)
                        {
                            int hodnota2 = pixel.B;
                            char c = Convert.ToChar(hodnota2);
                            string pismeno = System.Text.Encoding.ASCII.GetString(new byte[] { Convert.ToByte(c) });
                            desifrovanytxt += pismeno;
                        }
                    }
                }
                Console.WriteLine(desifrovanytxt);
                Console.ReadKey();
            }
        }
    }
}
