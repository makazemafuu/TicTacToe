using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace TicTacToe
{
    class Program
    {
        static void Main(string[] args)
        {

            Dictionary<string, string> game = new Dictionary<string, string>()
            {
                {"A1", " " },
                {"A2", " " },
                {"A3", " " },
                {"B1", " " },
                {"B2", " " },
                {"B3", " " },
                {"C1", " " },
                {"C2", " " },
                {"C3", " " }
            };

            List<string> locations = game.Keys.ToList();
            Console.WriteLine("Tic Tac Toe Game");
            string playerChoice;
            Random rnd = new Random();

            while (locations.Any())
            {
                //player's choice
                while (true)
                {
                    Console.WriteLine("[Joueur] Choose wisely :");
                    playerChoice = Console.ReadLine();

                    if (locations.Contains(playerChoice))
                    {
                        break;
                    }
                }

                game[playerChoice] = "X";
                locations.Remove(playerChoice);

                //player wins
                if (EndGame(game, playerChoice))
                {
                    Display(game);
                    Console.WriteLine("GGWP !");
                    break;
                }

                Console.WriteLine("[IA] Lemme think...");
                Thread.Sleep(1000);

                //IA's choice
                int newValue = rnd.Next(locations.Count);
                string iaChoice = locations[newValue];

                Console.WriteLine("[IA] I pick {0}", locations[newValue]);
                game[iaChoice] = "O";
                locations.RemoveAt(newValue);

                //IA wins
                if (EndGame(game, iaChoice))
                {
                    Display(game);
                    Console.WriteLine("Oh oh... GAME OVER ! :(");
                    break;
                }

            }
        }
        static void Display(Dictionary<string, string> game)
        {
            Console.Write("\n");

            List<string> lines = new List<string>() { "A", "B", "C" };
            List<string> columns = new List<string>() { "1", "2", "3" };

            string sep = "  +---+---+---+";

            string coord;

            Console.Write(" ");

            for (int i = 0; i < columns.Count; i++)
            {
                Console.Write("   " + columns[i]);
            }

            Console.Write("\n");

            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine(sep);
                Console.Write(lines[i] + " | ");

                for (int j = 0; j < 3; j++)
                {
                    coord = string.Concat(lines[i], columns[j]);
                    Console.Write(game[coord]);
                    Console.Write(" | ");
                }

                Console.Write("\n");
            }

            Console.WriteLine(sep);
            Console.Write("\n");
        }

        static bool CountMarks(Dictionary<string, string> game, string symbol, List<string> l)
        {
            int marksCount = 0;

            foreach (string coord in l)
            {
                if (game[coord] == symbol)
                {
                    marksCount++;
                }
            }

            if (marksCount == 3) return true;

            return false;
        }

        static bool EndGame(Dictionary<string, string> game, string choice)
        {
            string symbol = game[choice];

            List<string> lines = new List<string>() { "A", "B", "C" };
            List<string> columns = new List<string>() { "1", "2", "3" };

            char l = choice[0];
            char c = choice[1];

            //ligne
            List<string> line = new List<string>();
            for (int i = 1; i < 4; i++)
            {
                line.Add(string.Concat(l, i));
            }

            //Colonne
            List<string> col = new List<string>();
            for (int i = 0; i < 3; i++)
            {
                col.Add(string.Concat(lines[i], c));
            }

            //Diagonales
            List<string> diag1 = new List<string>() { "A1", "B2", "C3" };
            List<string> diag2 = new List<string>() { "A3", "B2", "C1" };

            if (CountMarks(game, symbol, line) || CountMarks(game, symbol, col)
               || CountMarks(game, symbol, diag1) || CountMarks(game, symbol, diag2))
            {
                return true;
            }

            return false;
        }
    }
}