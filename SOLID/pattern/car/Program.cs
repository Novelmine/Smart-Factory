using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace soul
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Builder builder;

            Shop shop = new Shop();

            builder = new Avantte();
            shop.CST(builder);
            builder.Car.Show();

            builder = new Grandeur();
            shop.CST(builder);
            builder.Car.Show();

            builder = new Sonata();
            shop.CST(builder);
            builder.Car.Show();
        }
    }
}
