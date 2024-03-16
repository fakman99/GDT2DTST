using Godot;
using System;

public partial class PlayerInputs : Node2D
{
	private CharacterBody2D _body2D;
	private float speed = 15;
	private float maxSpeed = 60;
	private float jumpForce = 25;
	private float gravity = 5;
	private float terminalVelocity = 100;
	private bool isOnGround;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_body2D = GetNode<CharacterBody2D>("Body");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Vector2 curVelocity = _body2D.Velocity;
		GD.Print(curVelocity.ToString());
		var direction = Input.GetVector("Left", "Right", "Up", "Down");
		_body2D.Velocity = new Vector2(Math.Clamp(direction.X + curVelocity.X, -maxSpeed, maxSpeed), Math.Min(curVelocity.Y, terminalVelocity));
		if (Input.IsActionPressed("Jump") && _body2D.IsOnFloor())
		{
			_body2D.Velocity = new Vector2(curVelocity.X,jumpForce);
		}

		if (!_body2D.IsOnFloor())
		{
			_body2D.Velocity = new Vector2(curVelocity.X,curVelocity.Y + (gravity + (float)delta));
		}

		_body2D.MoveAndSlide();
	}
	
}


