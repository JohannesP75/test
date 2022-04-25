// See https://aka.ms/new-console-template for more information
using System;
using System.Drawing;

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
                Console.WriteLine("Entrez le numéro du programme que vous voulez :\n\t(1) Plus ou moins\n\t(2) Pendu");

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
                Console.Clear();
            } while (!quit);
            
            Console.WriteLine("Au revoir !");

            return 0;
        }

        static void PlusOuMoins()
        {
            Console.Clear();
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
            const int MAX_FACILE = 5, MAX_MEDIUM = 8, MAX_DIFFICILE = 12;
            int nbreTours = 10;
            Console.Clear();
            Console.WriteLine("\tJEU DU PENDU");
            bool won = false;
            bool[] lettresTrouve = null;
            char input;
            Random rand = new Random();
            Console.WriteLine("Choississez le niveau de difficulté, de 1 à 3 :");
            Console.WriteLine($"\t1 : les mots auront {MAX_FACILE} lettres");
            Console.WriteLine($"\t2 : les mots auront {MAX_MEDIUM} lettres");
            Console.WriteLine($"\t3 : les mots auront {MAX_DIFFICILE} lettres");
            int choix = 3, diff = MAX_DIFFICILE;
            if (!Int32.TryParse(Console.ReadLine(), out choix)) choix = -1;

            switch (choix)
            {
                case 1:diff = MAX_FACILE;break;
                case 2:diff = MAX_MEDIUM;break;
                case 3:diff = MAX_DIFFICILE; break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Error.WriteLine("Vous deviez entrer un nombre entre 1 et 3 !\nPour vous punir, vous prendrez la difficulté maximale!");
                    Console.ResetColor();
                    break;
            }

            switch (diff)
            {
                case MAX_FACILE: lettresTrouve = new bool[MAX_FACILE]; break;
                case MAX_MEDIUM: lettresTrouve = new bool[MAX_MEDIUM]; break;
                case MAX_DIFFICILE: lettresTrouve = new bool[MAX_DIFFICILE]; break;
            }

            Array.Clear(lettresTrouve);

            // Chargement des mots
            switch (diff)
            {
                case MAX_FACILE: listeMots = File.ReadAllLines("liste_mots.txt").ToList<string>(); break;
                case MAX_MEDIUM: listeMots = File.ReadAllLines("liste_mots_medium.txt").ToList<string>(); break;
                case MAX_DIFFICILE: listeMots = File.ReadAllLines("liste_mots_difficile.txt").ToList<string>(); break;
            }
            String motJeu = listeMots[rand.Next(0, listeMots.Count)];
            // Jeu proprement dit
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

        static void TicTacToe()
        {
            char[] ValeurCase = { ' ', 'X', 'O' }; // ' ' pour case vide, 'X' pour 1er joueur et 'O' pour 2d
            const int DimGrille = 3;
            bool isX = false; // X commence en premier; variable servant à déterminer si le joueur humain est 'X'
            Console.Clear();
            Console.WriteLine("\tJEU DU TIC-TAC-TOE\nVoulez-vous jouer en premier ?");
            if(!Boolean.TryParse(Console.ReadLine(), out isX))isX=false;

            char[,] grille = new char[DimGrille, DimGrille];
            for (int i = 0; i < DimGrille; i++) for (int j = 0; j < DimGrille; j++) grille[i, j] = ValeurCase[0];

            bool EstFinie = false; // Si la partie est finie
            bool Joueur1 = true; // Si c'est au joueur 1 de jouer
            string? input;
            while (!EstFinie)
            {
                // Montrer la grille
                // Affichage de l'en-tete
                Console.Write(" ");
                for (int i = 0; i < DimGrille; i++) Console.Write($"{Char.ConvertFromUtf32('A' + i)}");

                // Corps proprement dit
                for (int i = 0; i < DimGrille; i++) {
                    Console.Write($"{i+1}");

                    for (int j = 0; j < DimGrille; j++) Console.Write($"{grille[i, j]}");

                    Console.Write($"");
                }

                int X, Y;
                if (Joueur1)
                {
                    if (isX)
                    {
                        // C'est au jouur de choisir le coup
                        bool inputCorrect = false;
                        do
                        {
                            Console.WriteLine("Choississez votre coup (Coordonnées sous la forme \"E7\") :");
                            input = Console.ReadLine().ToUpper();

                            if (input.Length >= 2)
                            {
                                X = input[0] - 'A';
                                Int32.TryParse(input.Substring(1), out Y);
                                if (Math.Abs(X)<DimGrille&&Y<DimGrille)
                                    if (grille[X,Y] == ValeurCase[0])
                                    {
                                        grille[X, Y] = ValeurCase[1];// la case est libre
                                        inputCorrect = true;
                                    }
                            }
                        } while (!inputCorrect);
                    }
                    else
                    {
                        // C'est le CPU qui joue; pour l'instant, il mettra des coups au hasard
                        Console.WriteLine("L'ordinateur joue :");
                    }
                }
            }
        }
    }

    class GrilleTicTac
    {
        /// <summary>
        /// Valeurs prises par les cases du tableau (' ': rien, 'X': joueur 1 et 'O': joueur 2)
        /// </summary>
        static char[] ValeurCase = { ' ', 'X', 'O' };
        /// <summary>
        /// Dimensions du tableau
        /// </summary>
        public int DimGrille { get; init; }
        /// <summary>
        /// Grille représentant le plateau
        /// </summary>
        public char[,]? grille { get; set; }
        /// <summary>
        /// Abscisse du dernier coup entré
        /// </summary>
        public int AbscisseDernierCoup { get; set; }
        /// <summary>
        /// Ordonnée du dernier coup entré
        /// </summary>
        public int OrdonneDernierCoup { get; set; }
        /// <summary>
        /// Symbole du gagnant
        /// </summary>
        public char SymboleGagnant { get; set; } = ' ';
        /// <summary>
        /// Constructeur de la classe
        /// </summary>
        /// <param name="dim">Dimensions de la grille</param>
        public GrilleTicTac(int dim)
        {
            DimGrille = dim;
            grille = new char[DimGrille, DimGrille];
            AbscisseDernierCoup = -1;
            OrdonneDernierCoup = -1;

            for (int i = 0; i < DimGrille; i++) for (int j = 0; j < DimGrille; j++) grille[i, j] = ValeurCase[0];
        }
        /// <summary>
        /// Affiche la grille
        /// </summary>
        public void Afficher()
        {
            // Montrer la grille
            // Affichage de l'en-tete
            Console.Write(" ");
            for (int i = 0; i < DimGrille; i++) Console.Write($"{Char.ConvertFromUtf32('A' + i)}");

            // Corps proprement dit
            for (int i = 0; i < DimGrille; i++)
            {
                Console.Write($"{i + 1}");

                for (int j = 0; j < DimGrille; j++) Console.Write($"{grille[i, j]}");

                Console.Write($"");
            }
        }
        /// <summary>
        /// Envoie les coups sur le plateau
        /// </summary>
        /// <param name="x">Abscisse du coup</param>
        /// <param name="y">Ordonnée du coup</param>
        /// <param name="isX">S'il s'agit du joueur 'X' ou non</param>
        /// <returns>Si le coup à bien pu être placé</returns>
        public bool EnvoyerCoup(int x, int y, bool isX)
        {
            bool S = false;

            if (x < DimGrille && y < DimGrille && x > 0 && y > 0)
                if (grille[x, y] == ValeurCase[0]) {
                    AbscisseDernierCoup = x;
                    OrdonneDernierCoup = y;
                    grille[AbscisseDernierCoup, OrdonneDernierCoup] = (isX) ? ValeurCase[1] : ValeurCase[2];
                        S = true;
                }
                else
                {
                    AbscisseDernierCoup = -1;
                    OrdonneDernierCoup = -1;
                }
            else
            {
                AbscisseDernierCoup = -1;
                OrdonneDernierCoup = -1;
            }

            return S;
        }

        /// <summary>
        /// Vérifie si le jeu est fini
        /// </summary>
        /// <returns>Vrai si jeu fini (victoire ou nul), faux sinon</returns>
        public bool VerifieFinJeu()
        {
            bool S = true;
            Point p = new(AbscisseDernierCoup, OrdonneDernierCoup); // Point de la grille ayant été frappé
            char symbole = grille[p.X, p.Y]; // Symbole de ce point

            // Vérification sur (Ox)
            for (int i = 0; i < DimGrille; i++)
                if(grille[i, p.Y] != symbole)
                {
                    S = false;
                    break;
                }

            // Vérification sur (Oy)
            if (!S) //// Si S est vraie après le est sur Ox alors il n'est plus la peine de coninuer à tester
            {
                for (int i = 0; i < DimGrille; i++)
                    if (grille[p.X, i] != symbole)
                    {
                        S = false;
                        break;
                    }
            }
            // Vérification sur la diagonale NO-SE
            // Vérification sur la diagonale NE-SO
            /*
            if (S)
            {
                int X0=(p.X<p.Y)?0: p.Y-p.X, Y0; // Points de départ de la diagonale

                for (int i = 0; ; i++)
                    if (grille[X0+i, Y0+i] != symbole)
                    {
                        S = false;
                        break;
                    }
            }
            */

            if (S)
            {
                SymboleGagnant = symbole;
            }

            return S;
        }
    }

    class CPUTicTac
    {

    }
}