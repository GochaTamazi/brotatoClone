using Godot;

namespace Objects.Characters.Enemies.Mob1;

public partial class Mob1 : Enemy
{
    public override void _Ready()
    {
        base._Ready();
        Sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        MaximumSpeed = Global.Speed * 3.5f;
        Initialize();

        var timer = new Timer();
        timer.WaitTime = 1 + GD.Randi() % 10;
        timer.OneShot = true;
        timer.Timeout += () =>
        {
            SpawnPills(1);
            QueueFree();
        };
        AddChild(timer);
        timer.Start();
    }

    private void Initialize()
    {
        int totalFrames = Sprite.SpriteFrames.GetFrameCount("Run");
        Sprite.Frame = GD.RandRange(0, totalFrames);
        Sprite.Play("Run");
    }
}