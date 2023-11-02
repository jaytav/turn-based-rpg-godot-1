using Godot;

// Handles camera movement, rotation and zoom
public partial class CameraController : Node
{
    public enum CameraControllerMode {
        Follow,
        FollowForced,
        Manual,
    }

    public Node3D FollowNode;
    public CameraControllerMode Mode = CameraControllerMode.FollowForced;

    private Camera3D _camera;
    private Node3D _cameraContainer;

    private float _moveSpeed = 10;
    private float _rotateSpeed = 5;
    private float _zoomMax = 20;
    private float _zoomMin = 5;
    private float _zoomStrength = 1;

    public override void _Ready()
    {
        GD.Print("CameraController: Run()");
        _cameraContainer = GetNode<Node3D>("/root/Main/World/CameraContainer");
        _camera = _cameraContainer.GetNode<Camera3D>("Camera");
    }

    public override void _PhysicsProcess(double delta)
    {
        Vector3 cameraTranslation = Vector3.Zero;
        int cameraRotation = 0;

        if (Input.IsActionPressed("CameraMoveLeft"))
        {
            cameraTranslation += Vector3.Left;
        }

        if (Input.IsActionPressed("CameraMoveRight"))
        {
            cameraTranslation += Vector3.Right;
        }

        if (Input.IsActionPressed("CameraMoveUp"))
        {
            cameraTranslation += Vector3.Up;
        }

        if (Input.IsActionPressed("CameraMoveDown"))
        {
            cameraTranslation += Vector3.Down;
        }

        if (Input.IsActionPressed("CameraRotateLeft"))
        {
            cameraRotation -= 1;
        }

        if (Input.IsActionPressed("CameraRotateRight"))
        {
            cameraRotation += 1;
        }

        // when attempting to manually move, switch to Manual mode only when not FollowForced
        if (!cameraTranslation.IsZeroApprox() && Mode != CameraControllerMode.FollowForced)
        {
            Mode = CameraControllerMode.Manual;
        }

        if ((Mode == CameraControllerMode.Follow || Mode == CameraControllerMode.FollowForced) && FollowNode != null)
        {
            _cameraContainer.Position = _cameraContainer.Position.Lerp(FollowNode.Position, _moveSpeed * (float)delta);
        }
        else if (Mode == CameraControllerMode.Manual)
        {
            _cameraContainer.Translate(cameraTranslation * _moveSpeed * (float)delta);
        }

        _cameraContainer.RotateY(cameraRotation * _rotateSpeed * (float)delta);
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.IsActionPressed("CameraZoomIn"))
        {
            zoomCamera(-1);
        }
        else if (@event.IsActionPressed("CameraZoomOut"))
        {
            zoomCamera(1);
        }
    }

    private void zoomCamera(int direction)
    {
        _camera.Size = Mathf.Clamp(
            _camera.Size + (direction * _zoomStrength),
            _zoomMin,
            _zoomMax
        );
    }
}
