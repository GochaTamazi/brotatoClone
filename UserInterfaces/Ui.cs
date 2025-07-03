using Godot;
using Objects;

namespace UserInterfaces;

public partial class Ui : CanvasLayer
{
    private Label _labelFps;
    private Label _labelEnemies;
    private Label _pillCount;

    public override void _Ready()
    {
        var marginContainer = GetNode<MarginContainer>("MarginContainer");
        var vboxContainer = marginContainer.GetNode<VBoxContainer>("VBoxContainer");
        _labelFps = vboxContainer.GetNode<Label>("Label FPS");
        _labelEnemies = vboxContainer.GetNode<Label>("Label Enemies");
        _pillCount = GetNode<MarginContainer>("MarginContainer6").GetNode<Label>("Pill Count");
    }

    public override void _Process(double delta)
    {
        _labelFps.Text = $"FPS: {Engine.GetFramesPerSecond()}";
        _labelEnemies.Text = $"Enemies: {Global.Enemies.GetChildCount()}";
        _pillCount.Text = $" {Global.PillCount}";
    }
}