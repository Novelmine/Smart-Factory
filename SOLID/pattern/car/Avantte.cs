using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace soul
{
     class Avantte : Builder
     {
        public Avantte()
        {
            car = new Car("Avantte");
        }
        public override void Engine()
        {
            car["engine"] = "BType";
        }

        public override void MissionCooler()
        {
            car["missionCooler"] = "유";
        }
        public override void Tire()
        {
            car["tire"] = "금호타이어";
        }
        public override void Tubor()
        {
            car["tubor"] = "유";
        }
        public override void price()
        {
            car["price"] = "단돈 천원!";
        }
    }
}
