using Godot;

namespace Objects.Spawners.Spawner2;

public partial class Spawner2 : Spawner
{
    public override void _Ready()
    {
        EnemyScene = ResourceLoader.Load<PackedScene>("res://Objects/Characters/Enemies/Mob2/Mob2.tscn");
        Sprite = GetNode<Sprite2D>("Sprite2D");

        Sprite.Rotation = GD.RandRange(-360, 360);
        Sprite.FlipH = (GD.Randi() % 2 == 0);

        _totalTime = WaitTime = GD.RandRange(2.0f, 3.0f); // 5

        base._Ready();
        _originalColor = Sprite.Modulate;
        _shaderMaterial = Sprite.Material as ShaderMaterial;
    }

    private double _totalTime = 2.0;
    private double _elapsedTime = 0.0;
    private float _maxBlinkSpeed = 10.0f;
    private Color _originalColor;
    private ShaderMaterial _shaderMaterial;

    public override void _Process(double delta)
    {
        _elapsedTime += delta;
        var t = (float)Mathf.Clamp(_elapsedTime / _totalTime, 0, 1);
        //var alpha = Mathf.Lerp(1.0f, 0.0f, t);
        var alpha = 1.0f - t;
        var blinkSpeed = Mathf.Lerp(1.0f, _maxBlinkSpeed, t);


        Color modulatedColor;
        if (Mathf.FloorToInt(_elapsedTime * blinkSpeed) % 2 == 0)
            modulatedColor = new Color(_originalColor.R, _originalColor.G, _originalColor.B, alpha);
        else
            modulatedColor = new Color(_originalColor.R, _originalColor.G, _originalColor.B, _originalColor.A);

        // Передаем цвет в шейдер
        _shaderMaterial.SetShaderParameter("modulate_color", modulatedColor);
    }
}