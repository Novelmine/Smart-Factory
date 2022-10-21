using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp9
{
    class House
    {
        private List<string> _place = new List<string>();

        public void add(string place)
        {
            _place.Add(place);
        }

        public void Show()
        {
            Console.WriteLine("\n ----하우스 구조---");
            Console.WriteLine("차 고지 : " + _place[0]);
            Console.WriteLine("수영장 : " + _place[1]);
            Console.WriteLine("조형물 : " + _place[2]);
            Console.WriteLine("정원 : " + _place[3]);
        }
    }
}
