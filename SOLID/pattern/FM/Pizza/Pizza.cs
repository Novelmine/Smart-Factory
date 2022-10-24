using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp11
{
    public abstract class Pizza
    {
        public string Name { get; protected set; }

        public string Cuting { get; protected set; }

        public string Boxc { get; protected set; }
        public string Dough { get; protected set; }
        public string Sauce { get; protected set; }
        public List<string> Toppings { get; protected set; } = new List<string>();  

        public virtual void prepare()
        {
            Console.WriteLine("----준비중----");
            Console.WriteLine(Name);
        }

        public virtual void bake()
        {
            Console.WriteLine("350도에서 25분간 조리");
        }

        public virtual void cut()
        {
            Console.WriteLine(Cuting + " 방식으로 슬라이스");
        }

        public virtual void box()
        {
            Console.WriteLine(Boxc + "사이즈 박스");
        }

        public override string ToString()
        {
            var result = new StringBuilder();

            result.AppendLine("----사용된 재료들----");
            result.AppendLine(Dough);
            result.AppendLine(Sauce);
            foreach (var topping in Toppings)
            {
                result.AppendLine(topping);
            }
            return result.ToString();
        }
    }
}
