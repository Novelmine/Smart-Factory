using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp9
{
    class Shop
    {
        public void Construct(Bulider builder)
        {
            builder.GargeParts();
            builder.SwimmingPoolParts();
            builder.FancyStatuesParts();
            builder.GardenParts();
        }
    }
}
