using Godot;

namespace Objects.Spawners.Spawner1;

public partial class Spawner1 : Spawner
{
    public override void _Ready()
    {
        EnemyScene = ResourceLoader.Load<PackedScene>("res://Objects/Characters/Enemies/Mob1/Mob1.tscn");

        Sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        Sprite.Frame = GD.RandRange(0, Sprite.SpriteFrames.GetFrameCount("Default"));
        Sprite.Play("Default");

        //Sprite.Rotation = GD.RandRange(-360, 360);
        WaitTime = GD.RandRange(2.0f, 3.0f);

        base._Ready();
    }
}