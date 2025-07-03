using Godot;

namespace Objects.Spawners;

public partial class Spawner : Node2D
{
    protected PackedScene EnemyScene;
    protected dynamic Sprite;
    protected double WaitTime;
    public override void _Ready()
    {
        var timer = new Timer();
        timer.WaitTime = WaitTime;
        timer.OneShot = true;
        timer.Timeout += Timeout;
        AddChild(timer);
        timer.Start();
    }

    private void Timeout()
    {
        if (EnemyScene != null)
        {
            dynamic enemy = EnemyScene.Instantiate();
            enemy.Position = new Vector2(Position.X, Position.Y);
            Global.Enemies.AddChild(enemy);
        }

        QueueFree();
    }
}