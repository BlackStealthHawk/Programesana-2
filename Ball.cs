using Godot;
using System;

public partial class Ball : CharacterBody2D
{
	private Vector2 velocity;
	private game gameManager;
	[Export] public float BaseSpeed = 250.0f;

	public override void _Ready()
	{
		gameManager = GetParent().GetParent() as game;
		ResetBall();
	}

	public void ResetBall()
	{
		Position = new Vector2(640, 360);
		velocity = new Vector2(BaseSpeed, (float)GD.RandRange(-BaseSpeed, BaseSpeed));
		SetPhysicsProcess(true);
	}

	public void StopBall()
	{
		velocity = Vector2.Zero;
		SetPhysicsProcess(false);
	}

	public override void _PhysicsProcess(double delta)
	{
		KinematicCollision2D collision = MoveAndCollide(velocity * (float)delta);

		if (collision != null)
		{
			Vector2 normal = collision.GetNormal();
			velocity = velocity.Bounce(normal).Normalized() * BaseSpeed;
			Position += normal * 0.5f;

			if (collision.GetCollider() is Paddle paddle)
			{
				float direction = Position.X < paddle.Position.X ? -1.0f : 1.0f;
				velocity.X = direction * Mathf.Max(Mathf.Abs(velocity.X), BaseSpeed * 0.5f);
				velocity = velocity.Normalized() * BaseSpeed;
			}
		}

		if (Position.X < 0)
		{
			gameManager.OnScore(false);
		}
		else if (Position.X > 1280)
		{
			gameManager.OnScore(true);
		}
	}
}
