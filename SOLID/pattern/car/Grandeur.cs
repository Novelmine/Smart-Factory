using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace soul
{
    class Grandeur : Builder
    {
        public Grandeur()
        {
            car = new Car("Grandeur");
        }
        public override void Engine()
        {
            car["engine"] = "AType";
        }

        public override void MissionCooler()
        {
            car["missionCooler"] = "무";
        }
        public override void Tire()
        {
            car["tire"] = "한국타이어";
        }
        public override void Tubor()
        {
            car["tubor"] = "무";
        }
        public override void price()
        {
            car["price"] = "단돈 백원!";
        }   
    }
}
