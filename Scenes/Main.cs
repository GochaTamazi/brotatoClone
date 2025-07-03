using Godot;
using Objects;
using Objects.Characters.Player;

namespace Scenes;

public partial class Main : Node2D
{
    public override void _Ready()
    {
        Global.GroundObjects = GetNode<Node2D>("GroundObjects");
        Global.Pills = GetNode<Node2D>("Pills");

        Global.Enemies = GetNode<Node2D>("Movable").GetNode<Node2D>("Enemies");
        Global.Player = GetNode<Node2D>("Movable").GetNode<Player>("Player");
    }
}