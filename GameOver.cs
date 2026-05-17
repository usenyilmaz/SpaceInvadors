using System;
using System.IO;

namespace SpaceInvadors;


public class GameOver
{
	const string BestScoreFile = "BestScore.txt";
	int points;

	public GameOver(int points)
	{
		this.points = points;
		Console.Clear();
		Console.WriteLine("Game Over!!!");
        Console.WriteLine("Your Spaceship got srushed by a meteor!!!");
		showCredentials(points, readBestScore(BestScoreFile));
        showOptions();
    }
	private void showCredentials(int score, int highestScore)
	{
		Console.WriteLine("Your score is: {0}", score);
		if(score > highestScore) 
		{
            Console.WriteLine("------------------------------");
            Console.WriteLine("NEW RECORD! CONGRATULATIONS!!!");
            Console.WriteLine("------------------------------");

			highestScore = score;
			recordBestScore(highestScore, BestScoreFile);
        }
		else
		{
            Console.WriteLine("Highest score : {0}", highestScore);
        }

        Console.WriteLine("To play again, press P.");
        Console.WriteLine("To exit the game, press E.");

	}

        
	private int readBestScore(string filepath)
	{
        string num = "0";
        if (File.Exists(filepath))
        {
            num = File.ReadAllText(filepath);
        }
        else
        {
            return 0;
        }
        return int.Parse(num);
    }

	private void showOptions()
	{
        while (true)
        {

            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            if (keyInfo.Key == ConsoleKey.P)
            {
                return;
            }
            else if (keyInfo.Key == ConsoleKey.E)
            {
                FinishGame();
            }
        }
    }

	private void recordBestScore(int bestScore, string filepath)
	{
        File.WriteAllText(filepath, bestScore.ToString());
    }

	static void FinishGame()
	{
        Environment.Exit(0);
    }
}
