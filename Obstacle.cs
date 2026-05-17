using System;

public class Obstacle
{
	private string obstacleString;
	private int Y;

	public Obstacle(string obstacleString, int Y)
	{
		this.obstacleString = obstacleString;
		this.Y = Y;

	}

	public int getY()
	{
		return this.Y;
	}

	public string getObstacleString()
	{
		return this.obstacleString;
	}

	public void fall(int distance)
	{
		this.Y += distance;
	}
}
