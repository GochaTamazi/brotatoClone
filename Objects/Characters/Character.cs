using Godot;

namespace Objects.Characters;

public partial class Character : CharacterBody2D
{
    protected float MaximumSpeed = 100.0f;
    protected Vector2 InputDirection = Vector2.Zero;
    protected dynamic Sprite;

    protected void FlipSprite()
    {
        if (InputDirection.X != 0)
            Sprite.FlipH = InputDirection.X > 0;
    }

    protected void Move(double delta)
    {
        Velocity = InputDirection * MaximumSpeed;
        MoveAndSlide();
        //ZIndex = (int)GlobalPosition.Y;
    }
}