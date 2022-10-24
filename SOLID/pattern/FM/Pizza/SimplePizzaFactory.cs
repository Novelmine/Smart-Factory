using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp11
{

    public class SimplePizzaFactory
    {
        public Pizza CreatePizza(string item , string item1 , string item2)
        {
            Pizza pizza = null;
            switch(item)
            {
                case "cheese":
                    pizza = new CheesePizza(item1 , item2);
                    break;
                case "veggie":
                    pizza = new VeggiePizza(item1, item2);
                    break;
                case "clam":
                    pizza = new ClamPizza(item1, item2);
                    break;
                case "pepperoni":
                    pizza = new PepperoniPizza(item1, item2);
                    break;
            }

            return pizza;
        }
    }
}
