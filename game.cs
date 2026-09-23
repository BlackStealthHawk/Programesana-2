using Godot;
using System;

public partial class game : Node2D
{
	private int scoreLeft = 0;
	private int scoreRight = 0;
	private Ball ball;
	private Label scoreLabel;
	private bool gameOver;

	public override void _Ready()
	{
		ball = GetNode<Ball>("spele/ball");
		scoreLabel = GetNode<Label>("ui/punkti");
		UpdateScore();
	}

	private void UpdateScore()
	{
		scoreLabel.Text = $"{scoreLeft} : {scoreRight}";
	}

	public void OnScore(bool leftPlayer)
	{
		if (gameOver)
		{
			return;
		}

		if (leftPlayer)
		{
			scoreLeft++;
		}
		else
		{
			scoreRight++;
		}

		UpdateScore();

		if (scoreLeft >= 5 || scoreRight >= 5)
		{
			gameOver = true;
			scoreLabel.Text = scoreLeft >= 5
				? $"Kreisais spēlētājs uzvar! {scoreLeft} : {scoreRight}"
				: $"Labais spēlētājs uzvar! {scoreLeft} : {scoreRight}";
			ball.StopBall();
			return;
		}

		ball.ResetBall();
	}
}
