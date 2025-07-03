using System.Collections.Generic;
using Godot;

namespace Objects.Characters.Player;

public partial class Player : Character
{
    private List<PackedScene> _weapons = new();

    public override void _Ready()
    {
        Global.Player = this;
        Sprite = GetNode<Sprite2D>("Sprite2D");
        MaximumSpeed = Global.Speed * 10;
        Global.Player = this;
    }

    public override void _Process(double delta)
    {
        InputDirection = Input.GetVector("Left", "Right", "Up", "Down");
        FlipSprite();

        Move(delta);
    }

    public void AddNewWeapon()
    {
        PackedScene[] weapons = new[]
        {
            ResourceLoader.Load<PackedScene>("res://Objects/Weapons/Syringe1/Syringe1.tscn")
        };

        var weaponScene = weapons[GD.Randi() % weapons.Length];
        dynamic weapon = weaponScene.Instantiate();
        
        // Получить позиции с PlaceObjectsOnCircle по циклу
        // добавить кудато на другой слой
        
        weapon.Position = new Vector2(Position.X, Position.Y);
        //Global.Enemies.AddChild(weapon);
    }

    public static Vector2[] PlaceObjectsOnCircle(Vector2 center, float radius, int objectCount)
    {
        var positions = new Vector2[objectCount];
        var angleStep = 360.0f / objectCount;

        for (var i = 0; i < objectCount; i++)
        {
            var angleInRadians = Mathf.DegToRad(angleStep * i);
            positions[i] = new Vector2(
                center.X + radius * Mathf.Cos(angleInRadians),
                center.Y + radius * Mathf.Sin(angleInRadians)
            );
        }

        return positions;
    }
}