using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace soul
{
    class Car
    {
        private string _carty;
        private Dictionary<string, string> _carpt = new Dictionary<string, string>();

        public Car(string carty)
        {
            this._carty = carty;
        }

        public string this[string key]
        {
            get { return _carpt[key]; }
            set { _carpt[key] = value; }
        }

        public void Show()
        {
            Console.WriteLine("\n------------견적서---------------");
            Console.WriteLine(" 자동차 종류 : {0} ", _carty );
            Console.WriteLine(" 엔진 : {0}", _carpt["engine"]);
            Console.WriteLine(" 미션쿨러 : {0}", _carpt["missionCooler"]);
            Console.WriteLine(" 타이어 : {0}", _carpt["tire"]);
            Console.WriteLine(" 터보 : {0}", _carpt["tubor"]);
            Console.WriteLine(" 가격 : {0}", _carpt["price"]);
        }
    }
}
