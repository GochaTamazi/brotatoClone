using System;
using Godot;

namespace Objects.EnemySpawner;

public partial class EnemySpawner : Node2D
{
    [Export] public PackedScene[] EnemyScene;
    private bool _waitForSpace = false;
    private float _emptyRadius = 20.0f;
    private bool _paused = true;
    private bool _busy = false;
    private PhysicsShapeQueryParameters2D _emptySpaceQuery;
    private Vector2 _mapLeftTop;
    private Vector2 _mapRightBot;

    public override void _Ready()
    {
        var query = new PhysicsShapeQueryParameters2D();
        query.CollideWithAreas = true;
        query.CollideWithBodies = true;
        query.CollisionMask = Global.EnemyCollisionLayer + Global.RaycastEnemyCollisionLayer;
        query.Transform = Transform;

        var shape = new CircleShape2D();
        shape.Radius = _emptyRadius;
        query.Shape = shape;
        _emptySpaceQuery = query;

        _mapLeftTop = Global.Ground.LeftTop;
        _mapRightBot = Global.Ground.RightBot;

        var timer = new Timer();
        timer.WaitTime = 0.01f;
        timer.Timeout += SpawnEnemy;
        AddChild(timer);
        timer.Start();
    }

    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("PauseSpawner"))
            _paused = !_paused;
    }

    private async void SpawnEnemy()
    {
        if (_busy || _paused)
            return;
        if (_waitForSpace)
        {
            _busy = true;
            await ToSignal(GetTree(), SceneTree.SignalName.PhysicsFrame);
            _busy = false;
            var spaceState = GetWorld2D().DirectSpaceState;
            if (spaceState == null)
                return;
            var result = spaceState.IntersectShape(_emptySpaceQuery);
            if (result.Count > 0)
                return;
        }

        var enemy = GetRandomEnemy();
        enemy.Position = GetMapRandCoords();
        Global.GroundObjects.AddChild(enemy);
    }

    private dynamic GetRandomEnemy()
    {
        var randomIndex = GD.Randi() % EnemyScene.Length;
        var selectedMobScene = EnemyScene[randomIndex];
        return selectedMobScene.Instantiate();
    }

    private Vector2 GetMapRandCoords()
    {
        return new Vector2(
            (float)GD.RandRange(_mapLeftTop.X, _mapRightBot.X),
            (float)GD.RandRange(_mapLeftTop.Y, _mapRightBot.Y)
        );
    }
}