using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Console_Pacman
{
    class Program
    {
        static CPacman pPac;
        static void Main(string[] args)
        {
            pPac = new CPacman();
            pPac.Start();
        }
    }

    class CPacman
    {
        static string[,] Board;
        int m_iPosX;
        int m_iPosY;
        int m_iScore;
        int m_iPowerUP; //frames of power up
        bool m_boGameOver;
        bool m_boDebug;
        char m_chrCurDir;    //current direction
        char m_chrDir;       //new direction
        List<CBakDot> LstBakDot = new List<CBakDot>();          //Liste de Dot
        List<CBakPower> LstBakPower = new List<CBakPower>();    //Liste de Power Dot
        CJP GhostJP;
        CChloe GhostCL;
        //debug 
        int fps = 0;


        public CPacman()
        {
            //Board = new string[,] { { "╔", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "╗" }, { /*"║"*/"1", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", /*"║"*/"2" }, { "║", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "║" }, { "║", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "║" }, { "║", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "║" }, { "║", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "║" }, { "║", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "║" }, { "║", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "║" }, { "║", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "║" }, { "║", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "║" }, { "║", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "║" }, { "║", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "║" }, { "║", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "║" }, { "║", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "║" }, { "║", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "║" }, { "║", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "║" }, { "║", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "║" }, { "║", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "║" }, { "║", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "P", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "║" }, { "╚", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "╝" } };
            //Board = new string[,] { { "╔", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "3", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "╗" }, { "║", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "║" }, { "║", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "║" }, { "║", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "║" }, { "║", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "║" }, { "║", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "║" }, { "║", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "║" }, { "║", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "╔", "═", "═", "═", "=", "=", "=", "═", "═", "═", "╗", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "║" }, { "║", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "║", " ", " ", " ", " ", " ", " ", " ", " ", " ", "║", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "║" }, { "1", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "║", " ", " ", " ", " ", " ", " ", " ", " ", " ", "║", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "2" }, { "║", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "║", " ", " ", " ", " ", " ", " ", " ", " ", " ", "║", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "║" }, { "║", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "╚", "═", "═", "═", "═", "═", "═", "═", "═", "═", "╝", "●", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "║" }, { "║", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "║" }, { "║", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "║" }, { "║", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "║" }, { "║", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "║" }, { "║", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "║" }, { "║", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "P", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "║" }, { "║", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "║" }, { "╚", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "4", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "╝" } };
            Board = new string[,] { { "╔", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "╗", "╔", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "╗" }, { "║", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "║", "║", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "║" }, { "║", ".", "╔", "═", "═", "╗", ".", "╔", "═", "═", "═", "╗", ".", "║", "║", ".", "╔", "═", "═", "╗", ".", "╔", "═", "═", "═", "╗", ".", "║" }, { "║", "●", "║", " ", " ", "║", ".", "║", " ", " ", " ", "║", ".", "║", "║", ".", "║", " ", " ", "║", ".", "║", " ", " ", " ", "║", "●", "║" }, { "║", ".", "╚", "═", "═", "╝", ".", "╚", "═", "═", "═", "╝", ".", "╚", "╝", ".", "╚", "═", "═", "╝", ".", "╚", "═", "═", "═", "╝", ".", "║" }, { "║", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "║" }, { "║", ".", "╔", "═", "═", "╗", ".", "╔", "╗", ".", "╔", "═", "═", "═", "═", "═", "═", "╗", ".", "╔", "╗", ".", "╔", "═", "═", "╗", ".", "║" }, { "║", ".", "╚", "═", "═", "╝", ".", "║", "║", ".", "╚", "═", "═", "╗", "╔", "═", "═", "╝", ".", "║", "║", ".", "╚", "═", "═", "╝", ".", "║" }, { "║", ".", ".", ".", ".", ".", ".", "║", "║", ".", ".", ".", ".", "║", "║", ".", ".", ".", ".", "║", "║", ".", ".", ".", ".", ".", ".", "║" }, { "╚", "═", "═", "═", "═", "╗", ".", "║", "╚", "═", "═", "╗", " ", "║", "║", " ", "╔", "═", "═", "╝", "║", ".", "╔", "═", "═", "═", "═", "╝" }, { " ", " ", " ", " ", " ", "║", ".", "║", "╔", "═", "═", "╝", " ", "╚", "╝", " ", "╚", "═", "═", "╗", "║", ".", "║", " ", " ", " ", " ", " " }, { " ", " ", " ", " ", " ", "║", ".", "║", "║", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", "║", "║", ".", "║", " ", " ", " ", " ", " " }, { " ", " ", " ", " ", " ", "║", ".", "║", "║", " ", "╔", "═", "=", "=", "=", "=", "═", "╗", " ", "║", "║", ".", "║", " ", " ", " ", " ", " " }, { "╔", "═", "═", "═", "═", "╝", ".", "╚", "╝", " ", "║", " ", " ", " ", " ", " ", " ", "║", " ", "╚", "╝", ".", "╚", "═", "═", "═", "═", "╗" }, { "1", " ", " ", " ", " ", " ", ".", " ", " ", " ", "║", " ", " ", " ", " ", " ", " ", "║", " ", " ", " ", ".", " ", " ", " ", " ", " ", "2" }, { "╚", "═", "═", "═", "═", "╗", ".", "╔", "╗", " ", "║", " ", " ", " ", " ", " ", " ", "║", " ", "╔", "╗", ".", "╔", "═", "═", "═", "═", "╝" }, { " ", " ", " ", " ", " ", "║", ".", "║", "║", ".", "╚", "═", "═", "═", "═", "═", "═", "╝", ".", "║", "║", ".", "║", " ", " ", " ", " ", " " }, { " ", " ", " ", " ", " ", "║", ".", "║", "║", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "║", "║", ".", "║", " ", " ", " ", " ", " " }, { " ", " ", " ", " ", " ", "║", ".", "║", "║", ".", "╔", "═", "═", "═", "═", "═", "═", "╗", ".", "║", "║", ".", "║", " ", " ", " ", " ", " " }, { "╔", "═", "═", "═", "═", "╝", ".", "╚", "╝", ".", "╚", "═", "═", "╗", "╔", "═", "═", "╝", ".", "╚", "╝", ".", "╚", "═", "═", "═", "═", "╗" }, { "║", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "║", "║", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "║" }, { "║", ".", "╔", "═", "═", "╗", ".", "╔", "═", "═", "═", "╗", ".", "║", "║", ".", "╔", "═", "═", "═", "╗", ".", "╔", "═", "═", "╗", ".", "║" }, { "║", ".", "╚", "═", "╗", "║", ".", "╚", "═", "═", "═", "╝", ".", "╚", "╝", ".", "╚", "═", "═", "═", "╝", ".", "║", "╔", "═", "╝", ".", "║" }, { "║", "●", ".", ".", "║", "║", ".", ".", ".", ".", ".", ".", ".", ".", "P", ".", ".", ".", ".", ".", ".", ".", "║", "║", ".", ".", "●", "║" }, { "╚", "═", "╗", ".", "║", "║", ".", "╔", "╗", ".", "╔", "═", "═", "═", "═", "═", "═", "╗", ".", "╔", "╗", ".", "║", "║", ".", "╔", "═", "╝" }, { "╔", "═", "╝", ".", "╚", "╝", ".", "║", "║", ".", "╚", "═", "═", "╗", "╔", "═", "═", "╝", ".", "║", "║", ".", "╚", "╝", ".", "╚", "═", "╗" }, { "║", ".", ".", ".", ".", ".", ".", "║", "║", ".", ".", ".", ".", "║", "║", ".", ".", ".", ".", "║", "║", ".", ".", ".", ".", ".", ".", "║" }, { "║", ".", "╔", "═", "═", "═", "═", "╝", "╚", "═", "═", "╗", ".", "║", "║", ".", "╔", "═", "═", "╝", "╚", "═", "═", "═", "═", "╗", ".", "║" }, { "║", ".", "╚", "═", "═", "═", "═", "═", "═", "═", "═", "╝", ".", "╚", "╝", ".", "╚", "═", "═", "═", "═", "═", "═", "═", "═", "╝", ".", "║" }, { "║", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "║" }, { "╚", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "═", "╝" } };
            m_boGameOver = false;
            m_iScore = 0;
            m_iPowerUP = 0;
        }

        private void init()
        {
            m_boDebug = false;
            for (int i = 0; i < Board.GetLength(0); i++)
                for (int j = 0; j < Board.GetLength(1); j++)
                {
                    if (Board[i, j] == "P")
                    {
                        m_iPosX = i;
                        m_iPosY = j;
                    }
                    else if (Board[i, j] == ".")
                        LstBakDot.Add(new CBakDot(i, j));
                    else if (Board[i, j] == "●")
                        LstBakPower.Add(new CBakPower(i, j));
                }
            if (!(m_iPosX > 0 && m_iPosY > 0 && m_iPosX < Board.GetLength(0) - 1 && m_iPosY < Board.GetLength(1) - 1))
            {
                m_boGameOver = true;
                Console.WriteLine("Board incorrect");
                Console.ReadKey();
            }

            GhostJP = new CJP(14, 14);
            GhostCL = new CChloe(15, 15);
            //GhostJP.Start();
        }

        public void Start()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            
            init();
            while (!m_boGameOver)
            {
                Afficher();
                KeyInput();
                Move(ref m_iPosX, ref m_iPosY, ref m_chrCurDir, m_chrDir);
                Colision(ref m_iPosX, ref m_iPosY);
                GhostJP.Start();
                Colision(ref m_iPosX, ref m_iPosY);

                if (m_iPowerUP > 0)
                    m_iPowerUP--;

                if (LstBakDot.Count() == 0 && LstBakPower.Count() == 0)
                {
                    m_boGameOver = true;
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("WON");
                }
                System.Threading.Thread.Sleep(30);  //tellement la pire affaire
            }

            Console.ReadKey();
        }


        private void Afficher()
        {
            Console.SetCursorPosition(0, 0);
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("BakerMan            Score: " + m_iScore);

            for (int i = 0; i < Board.GetLength(0); i++)
            {
                for (int j = 0; j < Board.GetLength(1); j++)
                {
                    if (Board[i, j] == "╔" || Board[i, j] == "╗" || Board[i, j] == "╝" ||
                        Board[i, j] == "╚" || Board[i, j] == "═" || Board[i, j] == "║")
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write(" " + Board[i, j]);
                    }
                    else if(Board[i,j] == "=")
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write(" " + Board[i, j]);
                    }
                    //Display GhostJP
                    else if(i == GhostJP.m_iPosX && j == GhostJP.m_iPosY)
                    {
                        if (m_iPowerUP > 0)
                            Console.ForegroundColor = ConsoleColor.Blue;
                        else
                            Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("JP");
                    }
                    //Display GhostCL
                    else if (i == GhostCL.m_iPosX && j == GhostCL.m_iPosY)
                    {
                        if (m_iPowerUP > 0)
                            Console.ForegroundColor = ConsoleColor.Blue;
                        else
                            Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("CL");
                    }
                    else if (i == m_iPosX && j == m_iPosY)
                    {
                        if (m_iPowerUP > 0)
                            Console.ForegroundColor = ConsoleColor.Magenta;
                        else
                            Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("BM");
                    }
                    else if (LstBakDot.FindIndex(x => (x.m_iPosX == i) && (x.m_iPosY == j)) > -1)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(" .");
                    }
                    else if (LstBakPower.FindIndex(x => (x.m_iPosX == i) && (x.m_iPosY == j)) > -1)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(" ●");
                    }
                    else
                        Console.Write("  ");
                }

                Console.WriteLine();
            }


            //Debug
            fps++;
            if (m_boDebug)
            {
                Console.SetWindowSize(Console.WindowWidth, 40);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Coord: " + m_iPosX + ", " + m_iPosY);
                Console.WriteLine("Dir: " + m_chrCurDir.ToString());
                Console.WriteLine("Dot left: " + LstBakDot.Count());
                Console.WriteLine("PowerUP Time: " + m_iPowerUP);
                Console.WriteLine("FPS count: " + fps);
                //U+2500
                Console.WriteLine("☼ ● ○ ♠ ♣ ♥ ♦");
                //Ghost directions
            }
            else
                Console.SetWindowSize(85, 35);

        }

        /// <summary>
        /// KeyListener equivalent
        /// </summary>
        private void KeyInput()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        m_chrDir = 'N';
                        break;
                    case ConsoleKey.DownArrow:
                        m_chrDir = 'S';
                        break;
                    case ConsoleKey.LeftArrow:
                        m_chrDir = 'W';
                        break;
                    case ConsoleKey.RightArrow:
                        m_chrDir = 'E';
                        break;
                    case ConsoleKey.W:
                        m_chrDir = 'N';
                        break;
                    case ConsoleKey.S:
                        m_chrDir = 'S';
                        break;
                    case ConsoleKey.A:
                        m_chrDir = 'W';
                        break;
                    case ConsoleKey.D:
                        m_chrDir = 'E';
                        break;
                    case ConsoleKey.F1:
                        m_boDebug = !m_boDebug;
                        Console.Clear();
                        break;
                    case ConsoleKey.Escape:
                        m_boGameOver = true;
                        break;
                    default:
                        break;
                }
            }
        }


        private void Move(ref int _iPosX, ref int _iPosY, ref char _chrCurDir, char _chrDir)
        {
            if (_chrDir == 'N' && (Board[_iPosX - 1, _iPosY] != "═") && _iPosX != 0 &&
                   (Board[_iPosX - 1, _iPosY] != "╝") && (Board[_iPosX - 1, _iPosY] != "╚"))
                _chrCurDir = _chrDir;
            else if (_chrDir == 'S' && (Board[_iPosX + 1, _iPosY] != "═") && _iPosX != Board.GetLength(0) &&
                (Board[_iPosX + 1, _iPosY] != "╗") && (Board[_iPosX + 1, _iPosY] != "╔") && (Board[_iPosX + 1, _iPosY] != "="))
                _chrCurDir = _chrDir;
            else if (_chrDir == 'E' && (Board[_iPosX, _iPosY + 1] != "║") && _iPosY != Board.GetLength(1) &&
                (Board[_iPosX, _iPosY + 1] != "╔") && (Board[_iPosX, _iPosY + 1] != "╚"))
                _chrCurDir = _chrDir;
            else if (_chrDir == 'W' && (Board[_iPosX, _iPosY - 1] != "║") && _iPosY != 0 &&
                (Board[_iPosX, _iPosY - 1] != "╝") && (Board[_iPosX, _iPosY - 1] != "╗"))
                _chrCurDir = _chrDir;

            if (_chrCurDir == 'N' && (Board[_iPosX - 1, _iPosY] != "═") && _iPosX != 0 &&
                (Board[_iPosX - 1, _iPosY] != "╝") && (Board[_iPosX - 1, _iPosY] != "╚"))
                _iPosX--;
            else if (_chrCurDir == 'S' && (Board[_iPosX + 1, _iPosY] != "═") && _iPosX != Board.GetLength(0) &&
                (Board[_iPosX + 1, _iPosY] != "╗") && (Board[_iPosX + 1, _iPosY] != "╔") && (Board[_iPosX + 1, _iPosY] != "="))
                _iPosX++;
            else if (_chrCurDir == 'E' && (Board[_iPosX, _iPosY + 1] != "║") && _iPosY != Board.GetLength(1) &&
                (Board[_iPosX, _iPosY + 1] != "╔") && (Board[_iPosX, _iPosY + 1] != "╚"))
                _iPosY++;
            else if (_chrCurDir == 'W' && (Board[_iPosX, _iPosY - 1] != "║") && _iPosY != 0 &&
                (Board[_iPosX, _iPosY - 1] != "╝") && (Board[_iPosX, _iPosY - 1] != "╗"))
                _iPosY--;
        }

        private void Colision(ref int _iPosX, ref int _iPosY)
        {
            //eat a pellet
            int remIndexDot = LstBakDot.FindIndex(x => (x.m_iPosX == m_iPosX) && (x.m_iPosY == m_iPosY));
            if (remIndexDot != -1)
            {
                LstBakDot.RemoveAt(remIndexDot);
                m_iScore++;
            }
            //eat a power pellet
            int remIndexPow = LstBakPower.FindIndex(x => (x.m_iPosX == m_iPosX) && (x.m_iPosY == m_iPosY));
            if (remIndexPow != -1)
            {
                m_iPowerUP = 200;
                LstBakPower.RemoveAt(remIndexPow);
                m_iScore += 10;
            }

            //C++ snake code for portal colision (adapted)
            int iPortal;
            if ((_iPosX == 0 || _iPosX == Board.GetLength(0) - 1 || _iPosY == 0 || _iPosY == Board.GetLength(1) - 1))
            {
                int iPortalNb = int.Parse(Board[_iPosX, _iPosY]);
                if (iPortalNb % 2 == 0)
                {
                    for (int ii = 0; ii <= Board.GetLength(0) - 1; ii++)		//double boucle pour parcourir et afficher le labyrinthe
                        for (int ij = 0; ij <= Board.GetLength(1) - 1; ij++)
                            if (Int32.TryParse(Board[ii, ij], out iPortal))
                                if (iPortal == iPortalNb - 1)
                                {
                                    _iPosX = ii; _iPosY = ij;
                                }
                }
                else
                {
                    for (int ii = 0; ii <= Board.GetLength(0)-1; ii++)		//double boucle pour parcourir et afficher le labyrinthe
                        for (int ij = 0; ij <= Board.GetLength(1)-1; ij++)
                            if (Int32.TryParse(Board[ii, ij], out iPortal))
                                if (iPortal == iPortalNb + 1)
                                {
                                    _iPosX = ii; _iPosY = ij;
                                }
                }
            }
            //prevent double port crash
            if (_iPosX == Board.GetLength(0)-1)
                _iPosX--;
            if (_iPosX == 0)
                _iPosX++;
            if (_iPosY == Board.GetLength(1)-1)
                _iPosY--;
            if (_iPosY == 0)
                _iPosY++;

            GhostColision(GhostJP.m_iPosX, GhostJP.m_iPosY);
            GhostColision(GhostCL.m_iPosX, GhostCL.m_iPosY);
        }

        private void GhostColision(int _X, int _Y)
        {
            if (m_iPosX == _X && m_iPosY == _Y)
                if (m_iPowerUP > 0)
                {
                    if (GhostJP.m_iPosX == _X && GhostJP.m_iPosY == _Y)
                        GhostJP.Die();
                    m_iScore += 100;
                }
                else
                    m_boGameOver = true;
                    
        }

        static public string BoardValue(int i, int j)
        {
            return Board[i, j].ToString();
        }
    }



    class CBakDot
    {
        public int m_iPosX { get; set; }
        public int m_iPosY { get; set; }

        public CBakDot(int _iPosX, int _iPosY)
        {
            m_iPosX = _iPosX;
            m_iPosY = _iPosY;
        }
    }

    class CBakPower
    {
        public int m_iPosX { get; set; }
        public int m_iPosY { get; set; }

        public CBakPower(int _iPosX, int _iPosY)
        {
            m_iPosX = _iPosX;
            m_iPosY = _iPosY;
        }
    }

    class CJP
    {
        public int m_iPosX { get; set; }
        public int m_iPosY { get; set; }
        public int m_iDir { get; set; }
        bool m_boChD = true;
        bool m_boExit = false;

        public CJP(int _iPosX, int _iPosY)
        {
            m_iPosX = _iPosX;
            m_iPosY = _iPosY;
            m_iDir = 0;
        }

        public void Start()
        {
            Move();
        }

        public void Die()
        {
            m_boExit = false;
            m_iPosX = 14;
            m_iPosY = 14;
            m_iDir = 0;
        }

        private void ChangeDirection()
        {
            string strCol = CPacman.BoardValue(m_iPosX + (Translation(m_iDir, true)*2),
                                               m_iPosY + (Translation(m_iDir, false)*2));

            if (strCol == "║" || strCol == "╝" || strCol == "╗" || strCol == "╔" || strCol == "╚" || strCol == "═"
                || (m_boExit && strCol == "="))
            {
                m_iDir--;

                if (m_iDir == -1)
                    m_iDir = 3;

                if (m_boChD)
                {
                    ChangeDirection();
                    m_boChD = !m_boChD;
                }
                else
                    ChangeDirection(strCol);

            }
        }

        private void ChangeDirection(string strCol)
        {
            if (strCol == "║" || strCol == "╝" || strCol == "╗" || strCol == "╔" || strCol == "╚" || strCol == "═" 
                || (m_boExit && strCol == "="))
            {
                if (m_iDir >= 2)
                    m_iDir -= 2;
                else
                    m_iDir += 2;

                m_boChD = !m_boChD;
            }
        }

        private void Move()
        {
            m_iPosX = m_iPosX + Translation(m_iDir, true);
            m_iPosY = m_iPosY + Translation(m_iDir, false);
            //Console.WriteLine(Translation(m_iDir, true));
            //Console.WriteLine(Translation(m_iDir, false));
            ChangeDirection();

            if (m_iPosX <= 11)
                m_boExit = true;
        }

        private int Translation(int _Dir, bool _boX)
        {
            if(_boX)
            {
                if (_Dir == 0)
                    return -1;
                if (_Dir == 2)
                    return 1;
                else 
                    return 0;
            }
            else
            {
                if (_Dir == 1)
                    return -1;
                if (_Dir == 3)
                    return 1;
                else
                    return 0;
            }

        }
    }
    //sa prend un ghost qui fait linverse par Bakman
    class CChloe
    {
        public int m_iPosX { get; set; }
        public int m_iPosY { get; set; }
        public char m_chrDir { get; set; }

        public CChloe(int _iPosX, int _iPosY)
        {
            m_iPosX = _iPosX;
            m_iPosY = _iPosY;
            m_chrDir = 'N';
        }
    }

    class CBleb
    {
        public int m_iPosX { get; set; }
        public int m_iPosY { get; set; }
        public char m_chrDir { get; set; }

        public CBleb(int _iPosX, int _iPosY)
        {
            m_iPosX = _iPosX;
            m_iPosY = _iPosY;
            m_chrDir = 'N';
        }
    }
}
