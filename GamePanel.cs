using System;
using System.Collections.Generic;
using System.Threading;
using System.Diagnostics;
using System.Text;

namespace SpaceInvadors
{
    public class GamePanel
    {
        // Sınıfa ait özellikler (Fields)
        private bool isGameOver;
        private int playerX;
        private int playerY;
        private Random random = new Random();

        public int points = 0;

        const int GameTiles = 30;
        const int GameTopLeftX = 45;
        const int GameTopLeftY = 0;

        private const int tileSpeed = 2;
        private Obstacle currentObstacle;
        private StringBuilder currentObstacleString = new StringBuilder(GameTiles);
        private Queue<Obstacle> obstacleQueue = new Queue<Obstacle>();

        Stopwatch sw = new Stopwatch();
        long lastWorkingTime = 0;
        long interval = 1000; // 1 saniye (milisaniye cinsinden)


        // Constructor (Sınıf ilk oluştuğunda çalışacak başlangıç ayarları)
        public GamePanel()
        {
            isGameOver = false;
            // Oyuncuyu ekranın ortasında ve en altında başlatalım
            playerX = Console.WindowWidth / 2;
            playerY = Console.WindowHeight - 2;
        }

        // Oyunun beyni olan, kontrolü tamamen devralan metot
        public void Run()
        {
            // Menüden kalan yazıları tamamen temizliyoruz
            Console.Clear();
            sw.Start();
            Console.CursorVisible = false;



            // Asıl Oyun Döngüsü (Game Loop)
            while (!isGameOver)
            {
                // 1. GİRDİLERİ DİNLE
                HandleInput();

                // 2. MANTIĞI GÜNCELLE (Engelleri düşür vs.)
                long currentTime = sw.ElapsedMilliseconds;
                if(currentTime - lastWorkingTime >= interval)
                {
                    UpdateLogic();
                    lastWorkingTime = currentTime;
                    
                }

                bool gameContiniues = checkCollisions();
                if (!gameContiniues)
                {
                    break;
                }

                // 3. EKRANI ÇİZ
                Draw();

                // 4. BEKLEME (Oyunun hızı)
                Thread.Sleep(50);
            }

            // Döngü bittiğinde (isGameOver = true olduğunda) oyun biter.
            GameOverScreen();
        }

        private void HandleInput()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                if (keyInfo.Key == ConsoleKey.LeftArrow)
                {
                    if (playerX > GameTopLeftX)
                    {
                        playerX--;
                        if (playerX <= GameTopLeftX)
                        {
                            playerX = GameTopLeftX + 1;
                        }
                    }
                    
                }
                    
                else if (keyInfo.Key == ConsoleKey.RightArrow)
                {
                    if (playerX < GameTopLeftX + GameTiles)
                    {
                        playerX++;

                        if (playerX >= GameTopLeftX + GameTiles + 1)
                        {
                            playerX = GameTopLeftX + GameTiles - 1;
                        }
                    }
                    
                }
                else if (keyInfo.Key == ConsoleKey.Escape) // Oyundan erken çıkmak isterse
                    isGameOver = true;


            }
        }

        private void UpdateLogic()
        {
            currentObstacleString.Clear();

            for (int i = 0; i < GameTiles; i++)
            {
                var randNum = random.Next(1, 101);
                if (randNum > 75)
                {
                    currentObstacleString.Append("#");
                }
                else
                {
                    currentObstacleString.Append(" ");
                }
            }

            currentObstacle = new Obstacle(currentObstacleString.ToString(), GameTopLeftY);
            
            obstacleQueue.Enqueue(currentObstacle);

            

            if (obstacleQueue.Count != 0)
            {
                foreach (Obstacle o in obstacleQueue)
                {
                    o.fall(tileSpeed);
                    
                }
                

                while (obstacleQueue.Peek().getY() > Console.WindowHeight - 1)
                {
                    obstacleQueue.Dequeue();
                    points++;
                    Plus1();
                }
            }
        }

        private bool checkCollisions()
        {
            if(obstacleQueue.Count != 0)
            {
                foreach (Obstacle o in obstacleQueue)
                {
                    if (o.getY() == playerY)
                    {
                        int counter = 0;
                        foreach (char c in o.getObstacleString())
                        {
                            counter++;
                            if (c == ' ')
                            {
                                continue;
                            }
                            else if (c == '#')
                            {
                                if (playerX == GameTopLeftX + counter)
                                {
                                    return false;
                                }
                            }
                        }
                    }
                }
            }

            return true;
        }

        private void Draw()
        {
            Console.Clear();
            Console.SetCursorPosition(GameTopLeftX, GameTopLeftY);

            for (int i = 0; i < Console.WindowHeight; i++)
            {
                Console.Write("|");
                for(int j = 0; j < GameTiles; j++)
                {
                    Console.Write(" ");

                }
                Console.Write("|");
                Console.SetCursorPosition(GameTopLeftX, GameTopLeftY + i);

            }

            displayPoints();

            foreach (Obstacle o in obstacleQueue)
            {
                Console.SetCursorPosition(GameTopLeftX + 1, o.getY());
                Console.WriteLine(o.getObstacleString());
            }

            Console.SetCursorPosition(playerX, playerY);
            Console.Write("@");
        }

        private void GameOverScreen()
        {
            GameOver gameover = new GameOver(this.points);
            /*
            Console.Clear();
            Console.WriteLine("===========================");
            Console.WriteLine("        OYUN BİTTİ         ");
            Console.WriteLine("===========================");
            Console.WriteLine("\nMenüye dönmek için herhangi bir tuşa bas...");
            Console.ReadKey(true);
            */
        }
        private static void Clear(StringBuilder value)
        {
            value.Length = 0;
        }
        private void displayPoints()
        {
            Console.SetCursorPosition(110, 0);
            Console.WriteLine("Points: {0}" , points);
        }
        private void Plus1()
        {
            Console.SetCursorPosition(GameTopLeftX + GameTiles + 2, playerY);
            Console.WriteLine("+1");
        }
        public int getPoints()
        {
            return points;
        }
    }
}