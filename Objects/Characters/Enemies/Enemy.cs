using Godot;

namespace Objects.Characters.Enemies;

public partial class Enemy : Character
{
    private PackedScene _pillScene;

    public override void _Ready()
    {
        _pillScene = ResourceLoader.Load<PackedScene>("res://Objects/Pill/Pill.tscn");
    }

    public override void _Process(double delta)
    {
        InputDirection = Position.DirectionTo(Global.Player.Position);
        FlipSprite();
        Move(delta);
    }

    protected void SpawnPills(short pillsCount)
    {
        for (short i = 0; i < pillsCount; i++)
        {
            Node2D pill = _pillScene.Instantiate<Node2D>();
            pill.Position = Position + new Vector2(GD.RandRange(-10, 10), GD.RandRange(-10, 10));
            Global.Pills.AddChild(pill);
        }
    }
}