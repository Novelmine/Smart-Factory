using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace soul
{
    class Sonata : Builder
    {
        public Sonata()
        {
            car = new Car("Sonata");
        }
        public override void Engine()
        {
            car["engine"] = "CType";
        }

        public override void MissionCooler()
        {
            car["missionCooler"] = "유";
        }
        public override void Tire()
        {
            car["tire"] = "미쉐린타이어";
        }
        public override void Tubor()
        {
            car["tubor"] = "유";
        }
        public override void price()
        {
            car["price"] = "단돈 만원!";
        }
    }
}
