using System;
using System.Runtime.InteropServices; // Windows API'ye erişmek için gerekli

namespace SpaceInvadors;

class Program
{
    // --- BURAYI EKLİYORSUNUZ ---
    private const int GWL_STYLE = -16;
    private const int WS_MAXIMIZEBOX = 0x00010000;
    private const int WS_SIZEBOX = 0x00040000;

    [DllImport("kernel32.dll", ExactSpelling = true)]
    private static extern IntPtr GetConsoleWindow();

    [DllImport("user32.dll")]
    private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

    [DllImport("user32.dll")]
    private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
    // ---------------------------


    static void Main(string[] args)
    {
        Console.SetWindowSize(120, 30);
        Console.SetBufferSize(120, 30);

        IntPtr handle = GetConsoleWindow();
        int style = GetWindowLong(handle, GWL_STYLE);
        style = style & ~WS_MAXIMIZEBOX & ~WS_SIZEBOX;
        SetWindowLong(handle, GWL_STYLE, style);

        //set window size

        Console.Clear();
        Console.WriteLine(Console.WindowWidth); //120
        Console.WriteLine(Console.WindowHeight); //30
        Console.WriteLine("===========================");
        Console.WriteLine("      SPACE INVADORS       ");
        Console.WriteLine("===========================");
        Console.WriteLine();
        Console.WriteLine("[P] - Oyuna Başla (Play)");
        Console.WriteLine("[E] - Çıkış Yap (Exit)");
        Console.WriteLine();
        Console.Write("Seçiminiz: ");

        // Programın açık kalmasını sağlayan değişken

        ConsoleKeyInfo keyInfo = Console.ReadKey(true);
        if (keyInfo.Key == ConsoleKey.E)
        {
            FinishGame();
        }
        else if(keyInfo.Key == ConsoleKey.P)
        {
            // Ana döngü
            while (true)
            {
                StartGame();
            }
        }
        
    }

    static void StartGame()
    {
        GamePanel mygame = new GamePanel();
        mygame.Run();
    }

    static void FinishGame()
    {
        Environment.Exit(0);
    }

}