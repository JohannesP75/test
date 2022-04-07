// See https://aka.ms/new-console-template for more information
using System;

namespace MiniJeux
{
    class Program
    {
        static int Main()
        {
            bool quit=false;
            uint numProg=0;

            do
            {
                Console.WriteLine("Entrez le numéro du programme que vous voulez :");

                try
                {
                    numProg = (uint)Int32.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {
                    quit = true;
                }

                if (!quit)
                {
                    switch (numProg)
                    {
                        case 1:
                            PlusOuMoins();
                            break;
                        case 2:
                            Pendu();
                            break;
                        default:
                            quit = true;
                            break;
                    }
                }
                Console.WriteLine("\tFIN DU JEU");
            } while (!quit);

            return 0;
        }

        static void PlusOuMoins()
        {
            Random rand = new Random();
            const int MAX_FACILE = 100, MAX_MEDIUM = 250, MAX_DIFFICILE=1000;
            int cible = -1;
            int nbreJoueur=-1, difficult=0;

            Console.WriteLine("\tJEU DU PLUS OU MOINS");
            Console.WriteLine("Vous allez jouer au jeu du 'Plus ou Moins' !");
            Console.WriteLine("Choississez le niveau de difficulté, de 1 à 3 :");
            Console.WriteLine($"\t1 : le nombre sera entre 0 et {MAX_FACILE}");
            Console.WriteLine($"\t2 : le nombre sera entre 0 et {MAX_MEDIUM}");
            Console.WriteLine($"\t3 : le nombre sera entre 0 et {MAX_DIFFICILE}");

            try
            {
                difficult = Int32.Parse(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Error.WriteLine("Vous deviez entrer un nombre !\nPour vous punir, vous prendrez la difficulté maximale!");
                Console.ResetColor();
                difficult = 3;
            }

            switch (difficult)
            {
                case 1:
                    cible = rand.Next(0, MAX_FACILE);
                    break;
                case 2:
                    cible = rand.Next(0, MAX_MEDIUM);
                    break;
                case 3:
                    cible = rand.Next(0, MAX_DIFFICILE);
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Error.WriteLine("Vous deviez entrer un nombre entre 1 et 3 !\nPour vous punir, vous prendrez la difficulté maximale!");
                    Console.ResetColor();
                    cible = rand.Next(0, MAX_DIFFICILE);
                    break;
            }

            do
            {
                Console.WriteLine("Entrez un nombre :");
                try
                {
                    nbreJoueur = Int32.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Error.WriteLine("Vous deviez entrer un nombre !");
                    Console.ResetColor();
                    nbreJoueur = -1;
                }

                if(nbreJoueur == cible)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Félicitation, vous avez gagné !");
                    Console.ResetColor();
                }
                else if(nbreJoueur > cible)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("Trop haut !");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("Trop bas !");
                    Console.ResetColor();
                }

                if (Math.Abs(nbreJoueur-cible)<10 && nbreJoueur!=cible)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Vous chauffez !");
                    Console.ResetColor();
                }
            } while (nbreJoueur != cible);
        }

        static void Pendu()
        {
            List<string> listeMots = new List<string>() { "chien","verre", "loire", "gland", "vigne"};
            int nbreTours=10;
            Console.WriteLine("\tJEU DU PENDU");
            bool won = false;
            bool[] lettresTrouve = new bool[5];
            Array.Clear(lettresTrouve);
            char input;
            Random rand = new Random();
            String motJeu = listeMots[rand.Next(0, listeMots.Count)];

            do
            {
                Console.WriteLine($"Vous avez {nbreTours} essais.\nEntrez une lettre :");
                //Console.WriteLine($"{motAffichage}");
                for (int i = 0; i < motJeu.Length; i++) Console.Write($"{(lettresTrouve[i]?motJeu[i]:'X')}");
                Console.WriteLine("");
                input = Console.ReadKey().KeyChar;
                Console.WriteLine("");

                if (motJeu.Contains(input))
                {
                    Console.WriteLine("Bonne réponse !");

                    for (int i = 0; i < motJeu.Length; i++) if (motJeu[i] == input) {
                            //motAffichage[i] = motJeu[i];
                            lettresTrouve[i] = true;
                        }

                    if (!lettresTrouve.Contains(false))won = true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Mauvaise réponse !");
                    Console.ResetColor();
                    nbreTours--;
                }
            } while (!won && nbreTours > 0);

            if (won)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("FELICITATIONS !");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("PERDU !");
                Console.ResetColor();
                Console.WriteLine($"Le mot était : {motJeu}");
            }
        }
    }
}