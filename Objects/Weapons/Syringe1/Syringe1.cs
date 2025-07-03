using System;
using Godot;

namespace Objects.Weapons.Syringe1;

public partial class Syringe1 : Weapon
{
    private float _attackCooldown = 0.5f;
    private AnimatedSprite2D _animatedSprite;
    private Area2D _attackArea;
    private bool _isAttacking = false;

    public override void _Ready()
    {
        _animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        _animatedSprite.FlipV = (GD.Randi() % 2 == 0);

        _attackArea = GetNode<Area2D>("Area2D");
        _attackArea.AreaEntered += Area2DOnAreaEntered;

        Position = new Vector2(GD.RandRange(-10, 10), GD.RandRange(-10, 10));
        RotationDegrees = GD.RandRange(-360, 360);
    }

    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("Attack"))
        {
            Vector2 attackDirection = GetGlobalMousePosition();
            //Console.WriteLine(attackDirection);
            Attack(attackDirection);
        }
    }

    public void Attack(Vector2 direction)
    {
        if (_isAttacking) return;

        _isAttacking = true;
        _animatedSprite.Play("Attack"); // Запускаем анимацию атаки

        // Устанавливаем позицию области атаки
        //_attackArea.GlobalPosition = GlobalPosition + direction.Normalized() * AttackRange;

        //Position = Vector2.Zero;
        MoveAndCollide(new Vector2(GD.RandRange(-10, 10), GD.RandRange(-10, 10)));

        Vector2 mousePosition = GetGlobalMousePosition();
        Vector2 direction1 = (mousePosition - GlobalPosition); //.Normalized();
        Rotation = direction1.Angle();

        Console.WriteLine(Position);

        // После завершения анимации возвращаемся в состояние покоя
        var timer = new Timer();
        timer.WaitTime = _attackCooldown;
        timer.OneShot = true;
        timer.Timeout += OnAttackCooldownComplete;
        AddChild(timer);
        timer.Start();
    }

    private void Area2DOnAreaEntered(Area2D area)
    {
    }

    private void OnAttackCooldownComplete()
    {
        _isAttacking = false; // Сбрасываем флаг атаки
        _animatedSprite.Play("Default"); // Возвращаемся к состоянию покоя
    }
}