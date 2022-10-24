using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp11
{
    public class PizzaStore
    {
        private readonly SimplePizzaFactory _factory;

        public PizzaStore(SimplePizzaFactory factory)
        {
            _factory = factory;
        }

        public Pizza OrderPizza(string type , string type1 , string type2)
        {
            Pizza pizza = _factory.CreatePizza(type , type1 ,type2);

            pizza.prepare();
            pizza.bake();
            pizza.cut();
            pizza.box();
            Console.WriteLine(pizza.ToString());

            return pizza;
        }
    }
}
