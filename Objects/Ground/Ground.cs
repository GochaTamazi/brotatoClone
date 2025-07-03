using Godot;

namespace Objects.Ground;

public partial class Ground : Node2D
{
    public Vector2 LeftTop;
    public Vector2 RightBot;
    public Vector2 LeftTopGlobal;
    public Vector2 RightBotGlobal;

    public override void _Ready()
    {
        var corners = GetNode<Node>("Corners");
        LeftTop = corners.GetNode<Node2D>("LeftTop").Position;
        RightBot = corners.GetNode<Node2D>("RightBot").Position;

        LeftTopGlobal = corners.GetNode<Node2D>("LeftTop").GlobalPosition;
        RightBotGlobal = corners.GetNode<Node2D>("RightBot").GlobalPosition;

        Global.Ground = this;
    }
}