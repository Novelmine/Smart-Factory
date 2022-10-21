using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace soul
{
    abstract class Builder
    {
        protected Car car;

        public Car Car
        {
            get { return car; }
        }

        public abstract void Engine();
        public abstract void MissionCooler();
        public abstract void Tire();
        public abstract void Tubor();
        public abstract void price();

    }
}
