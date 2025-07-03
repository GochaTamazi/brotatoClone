using Godot;

namespace Objects.Pill;

public partial class Pill : RigidBody2D
{
    private bool _player = false;
    private float _speed = 200.0f;
    private float _activationDistance = 50.0f;
    private Area2D _area2D;
    private Sprite2D _sprite2D;

    public override void _Ready()
    {
        _area2D = GetNode<Area2D>("Pill");
        _area2D.AreaEntered += Area2DOnAreaEntered;

        _sprite2D = GetNode<Sprite2D>("Sprite2D");
        _sprite2D.RotationDegrees = GD.RandRange(-360, 360);

    }

    private void Area2DOnAreaEntered(Area2D area)
    {
        if (area.Name != "Player") return;
        _player = true;

        var shadow = GetNode<Node2D>("Shadow_0");
        shadow.QueueFree();

        _area2D.AreaEntered -= Area2DOnAreaEntered;
    }

    public override void _Process(double delta)
    {
        if (!_player) return;
        var direction = (Global.Player.GlobalPosition - GlobalPosition).Normalized();
        var distanceToPlayer = GlobalPosition.DistanceTo(Global.Player.GlobalPosition);
        if (distanceToPlayer > _activationDistance)
        {
            GlobalPosition += direction * _speed * (float)delta;
            _speed += 50;
        }
        else
        {
            Global.PillCount++;
            QueueFree();
        }
    }
}