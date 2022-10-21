using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp9
{
    abstract class Bulider
    {
        public abstract void GargeParts();
        public abstract void SwimmingPoolParts();

        public abstract void FancyStatuesParts();
        public abstract void GardenParts();
        public abstract House GetResult();
    }
}
