using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp7
{
    public class StrategyPatternWiki
    {
        public static void Main(String[] args)
        {
        
            var normalStrategy = new NormalStrategy();
            var happyHourStrategy = new HappyHourStrategy();

            var firstCustomer = new CustomerBill(normalStrategy);

            firstCustomer.Add(1.0, 1);

            firstCustomer.Strategy = happyHourStrategy;
            firstCustomer.Add(1.0, 2);

            var secondCustomer = new CustomerBill(happyHourStrategy);
            secondCustomer.Add(0.8, 1);

            firstCustomer.Print();


            secondCustomer.Strategy = normalStrategy;
            secondCustomer.Add(1.3, 2);
            secondCustomer.Add(2.5, 1);
            secondCustomer.Print();
        }
    }

    class CustomerBill
    {
        private IList<double> drinks;

        public IBillingStrategy Strategy { get; set; }

        public CustomerBill(IBillingStrategy strategy)
        {
            this.drinks = new List<double>();
            this.Strategy = strategy;
        }

        public void Add(double price, int quantity)
        {
            this.drinks.Add(this.Strategy.GetActPrice(price * quantity));
        }
        public void Print()
        {
            double sum = 0;
            foreach (var drinkCost in this.drinks)
            {
                sum += drinkCost;
            }
            Console.WriteLine($"Total due: {sum}.");
            this.drinks.Clear();
        }
    }

    interface IBillingStrategy
    {
        double GetActPrice(double rawPrice);
    }

    class NormalStrategy : IBillingStrategy
    {
        public double GetActPrice(double rawPrice) => rawPrice;
    }

    class HappyHourStrategy : IBillingStrategy
    {
        public double GetActPrice(double rawPrice) => rawPrice * 0.5;
    }
}
