using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace soul
{
    class Shop
    {
        public void CST(Builder car)
        {
            car.Engine();
            car.Tubor();
            car.Tire();
            car.price();
            car.MissionCooler();
        }
    }
}
