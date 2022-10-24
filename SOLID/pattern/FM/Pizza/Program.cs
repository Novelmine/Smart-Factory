using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp11
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SimplePizzaFactory factory = new SimplePizzaFactory();
            PizzaStore store = new PizzaStore(factory);

            Pizza pizza = store.OrderPizza("cheese" , "6조각" ,"M");
            Console.WriteLine(pizza.Name +" 를 주문하였습니다. " + pizza.Boxc + " 사이즈 " + pizza.Cuting );

            pizza = store.OrderPizza("veggie", "8조각", "L");
            Console.WriteLine(pizza.Name + " 를 주문하였습니다. " + pizza.Boxc + " 사이즈 " + pizza.Cuting);
        }
    }
}
