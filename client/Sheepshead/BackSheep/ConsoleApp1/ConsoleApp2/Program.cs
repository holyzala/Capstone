using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1;


namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var database = new Sheep()) {
                /*
                var cardtst = new Card
                {
                     Face = "ZZ",
                     Suit = "Clubs",
                     is_Trump = false,
                     Trump_Power = 60,
                     Card_Value = 60
                };
                database.Cards.Add(cardtst);
                database.SaveChanges();
                */

                var yycard = database.Cards.Find(40);
                Console.WriteLine(yycard.Face);
                //var query = "UPDATE Card SET is_Trump = '1' WHERE Face = ";

                //database.Database.ExecuteSqlCommand(query);

            }

        }
    }
}
