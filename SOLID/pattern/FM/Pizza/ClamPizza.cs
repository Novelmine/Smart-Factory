using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp11
{
    public class ClamPizza : Pizza
    {
        public ClamPizza(string cut, string box)
        {
            Name = "Clam Pizza";
            Dough = "Thin crust";
            Sauce = "White garlic sauce";
            Cuting = cut;
            Boxc = box;
            Toppings.Add("Clams");
            Toppings.Add("Grated parmesan cheese");
        }
    }
}
