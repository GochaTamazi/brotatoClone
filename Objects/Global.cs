using Godot;

namespace Objects;

internal static class Global
{
    public const int EnemyCollisionLayer = 2;
    public const int RaycastEnemyCollisionLayer = 8;
    public static Node2D GroundObjects;
    public static Node2D Pills;
    public static Node2D Enemies;
    public static Characters.Player.Player Player;
    public static Ground.Ground Ground;
    public static uint PillCount { set; get; } = 0;
    public static float Speed { get; set; } = 35;
}