using Godot;
using Godot.Collections;

public partial class MouseController : Node
{
    public Vector3 PositionInWorld = new();

    public override void _Process(double delta)
    {
        PositionInWorld = getMousePositionInWorld();
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.IsActionPressed("ActionPrimary"))
        {
            GD.Print($"MouseController: _UnhandledInput(): PositionInWorld: {PositionInWorld}");
        }
    }

    private Vector3 getMousePositionInWorld()
    {
        PhysicsDirectSpaceState3D spaceState = GetViewport().World3D.DirectSpaceState;
        Vector2 mousePosition = GetViewport().GetMousePosition();
        Camera3D camera = GetTree().Root.GetCamera3D();
        Vector3 rayOrigin = camera.ProjectRayOrigin(mousePosition);
        Vector3 rayEnd = rayOrigin + camera.ProjectRayNormal(mousePosition) * 2000;
        Dictionary intersectRay = spaceState.IntersectRay(PhysicsRayQueryParameters3D.Create(
            rayOrigin,
            rayEnd
        ));

        if (intersectRay.ContainsKey("position"))
        {
            return (Vector3)intersectRay["position"];
        }

        return Vector3.Zero;
    }
}
