using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp13
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IBM ibm = new IBM("IBM", 120.00);
            ibm.Attach(new Investor("Sorros"));
            ibm.Attach(new Investor("Berkshire"));

            ibm.Price = 122.10;
            ibm.Price = 121.00;
            ibm.Price = 120.50;
            ibm.Price = 120.75;

            Console.ReadKey();
        }        
    }
    public abstract class Stock
    {
        private string symbol;
        private double price;
        private List<IInvestor> investors = new List<IInvestor>();

        public Stock(string symbol, double price)
        {
            this.symbol = symbol;
            this.price = price;
        }
        public void Attach(IInvestor investor)
        {
            investors.Add(investor);
        }
        public void Detach(IInvestor investor)
        {
            investors.Remove(investor);
        }
        public void Notify()
        {
            foreach (IInvestor investor in investors)
            {
                investor.Update(this);
            }
            Console.WriteLine("-업데이트 종료-");
        }

        public double Price
        {
            get { return price; }
            set
            {
                if (price != value)
                {
                    price = value;
                    Notify();
                }
            }
        }

        public string Symbol
        {
            get { return symbol; }
        }
    }

    public class IBM : Stock
    {

        public IBM(string symbol, double price)
            : base(symbol, price)
        {
        }
    }

    public interface IInvestor
    {
        void Update(Stock stock);
    }

    public class Investor : IInvestor
    {
        private string name;
        private Stock stock;

        public Investor(string name)
        {
            this.name = name;
        }
        public void Update(Stock stock)
        {
            Console.WriteLine(" {1} 회사의 {0} 항목이 " +
                "${2} 으로 변경되었습니다. ", name, stock.Symbol, stock.Price);
        }

        public Stock Stock
        {
            get { return stock; }
            set { stock = value; }
        }
    }
}
