using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace SoloLearn
{
    class Account { public int balance; public int wins; public int los; public double first12; public double mid12; public double last12; public double zero; }
    class Program
    {

        static void Main(string[] args)
        {
            Account track = new Account();
            track.wins = 0;
            track.los = 0;
            track.first12 = 0;
            track.mid12 = 0;
            track.last12 = 0;
            track.zero = 0;
            Console.WriteLine("Welcome to Martingale Strategy Program. This program helps you to determine if your martingale strategy is profitable or not. Made by Yusuf Tamer.");
            Console.WriteLine("Put your inital money: ");
            int iInitial = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Choose playing method: \n1- First 12 \n2- Mid 12\n3- Last 12 \n4- One by one \n \nEnter your answer here:  ");
            int iMethod = Convert.ToInt32(Console.ReadLine());
            int iMax = 12;
            int iMin = 1;
            bool method4 = false;
            Console.WriteLine("\nHow many tries would you want: ");
            int Tries = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("\nHow muc margin would you want: ");
            double Margin = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("\nDo you want details? y or n ");
            string details = Console.ReadLine();

            bool fdetails = false;

            if (details == "y") { fdetails = true; }
            else if (details == "n") { fdetails = false; }

            if (iMethod == 1) { iMax = 12; iMin = 1; }
            else if (iMethod == 2) { iMax = 24; iMin = 13; }
            else if (iMethod == 3) { iMax = 36; iMin = 25; }
            else if (iMethod == 4) { method4 = true; iMethod = 1; Console.WriteLine("\nYour initial start is first 12"); }

            for (int i = 1; i <= Tries; i++)
            {
                Console.WriteLine("\n \nHere is your " + "try " + i + ": ");

                Random rnd = new Random();
                int a = 1;
                int initial = iInitial;


                Account hesap = new Account();
                hesap.balance = initial;
                bool go = true;
                while (go)
                {

                    hesap.balance -= a;
                    if (fdetails == true) { Console.Write("You betted: " + a + ", New balance is : " + hesap.balance + ", "); }
                    /*while breaker*/
                    if (hesap.balance <= 0) { Console.WriteLine("You got bankrupted because your final balance is: " + hesap.balance); go = false; track.los++; break; }
                    int roulette = rnd.Next(0, 37);
                    if (roulette <= 12 && roulette >= 1) { track.first12++; }
                    else if (roulette <= 24 && roulette >= 13) { track.mid12++; }
                    else if (roulette <= 36 && roulette >= 25) { track.last12++; }
                    else if (roulette == 0) { track.zero++; }

                    if (fdetails == true) { Console.Write("Lucky Number: " + roulette + ", "); }
                    if (roulette <= iMax && roulette >= iMin)
                    {
                        hesap.balance += 3 * a;
                        a = 1;
                        Console.Write(" You won! Final balance is:" + hesap.balance + "\n");
                        if (method4 == true)
                        {
                            iMethod = (iMethod + 1) % 3;
                            if (iMethod == 1) { iMax = 12; iMin = 1; if (fdetails == true) { Console.WriteLine(" \n\nYour new bet is first 12"); } }
                            else if (iMethod == 2) { iMax = 24; iMin = 13; if (fdetails == true) { Console.WriteLine(" \n\nYour new bet is mid 12"); } }
                            else if (iMethod == 0) { iMax = 36; iMin = 25; if (fdetails == true) { Console.WriteLine(" \n\nYour new bet is last 12"); } }
                        }
                    }
                    else
                    {
                        a = 2 * a;
                        if (fdetails == true) { Console.Write("No gain this round" + ", Final Balance is: " + hesap.balance + "\n"); }
                    }

                    /*while breaker*/
                    if (hesap.balance >= Margin * initial) { Console.WriteLine("You have reached your goal by: " + hesap.balance); go = false; track.wins++; break; }


                }
            }
            double winratio = track.wins * 100 / (track.wins + track.los);
            Console.WriteLine("\nTotal wins: " + track.wins + ", Total Loss: " + track.los + "\n \nWin Ratio: " + winratio);
            Double expected_ratio = 100.0 / Margin;
            Console.WriteLine("Your expected raio is: " + expected_ratio);
            if (expected_ratio <= winratio) { Console.WriteLine("Congrats you have the winning strategy by " + (winratio - expected_ratio) + "percent."); }
            else { Console.WriteLine("Sorry you have the losing strategy by " + (winratio - expected_ratio) + "percent. \n"); }
            double total = track.first12 + track.mid12 + track.last12 + track.zero;
            double ratiof = track.first12 * 100 / total;
            double ratiom = track.mid12 * 100 / total;
            double ratiol = track.last12 * 100 / total;
            double ratioz = track.zero * 100 / total;

            Console.WriteLine("Do blame the odds: \n" + "First 12 probibality is: " + ratiof + "% (expected is 32,43)");
            Console.WriteLine("Mid 12 probibality is: " + ratiom + "% (expected is 32,43)");
            Console.WriteLine("Last 12 probibality is: " + ratiol + "% (expected is 32,43)");
            Console.WriteLine("0's probibality is: " + ratioz + "% (expected is 2,7)");

        }


    }

}

