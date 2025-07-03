using Godot;

namespace Objects.Characters.Enemies.Mob2;

public partial class Mob2 : Enemy
{
    public override void _Ready()
    {
        base._Ready();
        Sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        MaximumSpeed = Global.Speed * 7.5f;
        Initialize();

        var timer = new Timer();
        timer.WaitTime = 1 + GD.Randi() % 10;
        timer.OneShot = true;
        timer.Timeout += () =>
        {
            SpawnPills(3);
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