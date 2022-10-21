using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp9
{
    class HouseBulider : Bulider
    {
        private House house = new House();

        public override void GargeParts()
        {
            house.add("무");
        }
        public override void SwimmingPoolParts()
        {
            house.add("무");
        }
        public override void FancyStatuesParts()
        {
            house.add("무");
        }
        public override void GardenParts()
        {
            house.add("무");
        }
        public override House GetResult()
        {
            return house;
        }
    }
}
