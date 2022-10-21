using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp9
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Shop shop = new Shop();

            Bulider H1 = new HouseBulider();

            shop.Construct(H1);
            House house = H1.GetResult();
            house.Show();
        }
    }
}
