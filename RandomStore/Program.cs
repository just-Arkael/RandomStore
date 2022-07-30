using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomStore
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Product[] products = new Product[5];
            products[0] = new ToiletPaper();
            products[1] = new PaperTowels();
            products[2] = new ToothPaste();
            products[3] = new ToothBrush(50);
            products[4] = new Soap("Lavender");
            Console.WriteLine("How much did we save up?");
            decimal wallet = 0;
            decimal.TryParse(Console.ReadLine(), out wallet);

            foreach (Product p in products)
            {
                p.ShowPrice();
                Console.WriteLine("Buy? y/n");
                if (Console.ReadLine() == "y")
                {
                    p.BuyItem(ref wallet);
                }
                Console.WriteLine();
            }

            Console.ReadKey();

        }
    }


    class Product
    {
        public string name;
        public decimal price;


        public virtual void ShowPrice()
        {
            Console.Write(name + ": ");
            if (this is ToothBrush || this is PaperTowels)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("was " + price + "/");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("SALE " + (price * (decimal)0.7));
                Console.ForegroundColor= ConsoleColor.White; 
            }
            else
            {
                Console.WriteLine(price);
            }

        }

        public virtual void BuyItem(ref decimal wallet)
        {
            if (wallet >= price)
            {
                Console.WriteLine("Thank you for buying one {0}!", name);
                Console.WriteLine("Total: " + price);
                wallet -= price;
            }
            else
            {
                Console.WriteLine("Sorry, but you're {0} short", price - wallet);
            }
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Money left: " + wallet);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }


    class ToiletPaper : Product
    {
        public ToiletPaper()
        {
            name = "Toilet Paper";
            price = 25;
        }
    }

    class ToothPaste : Product
    {
        public ToothPaste()
        {
            name = "Tooth Paste";
            price = 15;
        }
    }

    class ToothBrush : Product
    {
        int softness;
        public ToothBrush(int softness)
        {
            name = "Tooth Brush";
            this.softness = softness;
            price = 10;
        }

        public override void BuyItem(ref decimal wallet)
        {
            decimal trueprice = price;

            price *= (decimal)0.7;

            base.BuyItem(ref wallet);

            price = trueprice;
        }

        public override void ShowPrice()
        {
            base.ShowPrice();
            Softness();
        }
        void Softness()
        {
            Console.WriteLine(softness + "% softness");
        }
    }

    class PaperTowels : Product
    {
        public PaperTowels()
        {
            name = "Paper Towels";
            price = 30;
        }

        public override void BuyItem(ref decimal wallet)
        {
            decimal trueprice = price;

            price *= (decimal)0.7;

            base.BuyItem(ref wallet);

            price = trueprice;
        }
    }

    class Soap : Product
    {
        string smell;
        public Soap(string scent)
        {
            name = "Soap";
            smell = scent;
            price = 12;
        }
        public override void ShowPrice()
        {
            base.ShowPrice();
            Smell();
        }
        void Smell()
        {
            Console.WriteLine(smell + " scented");
        }
    }
}
