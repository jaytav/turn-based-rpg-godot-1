using Godot;
using Godot.Collections;

public partial class MouseController : Node
{
    private Dictionary _intersectRay = new();

    public override void _Process(double delta)
    {
        setIntersectRay();
    }

    public Vector3 GetMouseWorldPosition()
    {
        if (!_intersectRay.ContainsKey("position"))
        {
            return Vector3.Zero;
        }

        return (Vector3)_intersectRay["position"];
    }

    private void setIntersectRay()
    {
        PhysicsDirectSpaceState3D spaceState = GetViewport().World3D.DirectSpaceState;
        Vector2 mousePosition = GetViewport().GetMousePosition();
        Camera3D camera = GetTree().Root.GetCamera3D();
        Vector3 rayOrigin = camera.ProjectRayOrigin(mousePosition);
        Vector3 rayEnd = rayOrigin + camera.ProjectRayNormal(mousePosition) * 2000;

        _intersectRay = spaceState.IntersectRay(PhysicsRayQueryParameters3D.Create(
            rayOrigin,
            rayEnd
        ));
    }
}
