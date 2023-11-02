using Godot;
using Godot.Collections;

public partial class Character : Node3D
{
    [Export]
    private float _moveSpeed;

    private float _movementDelta;
    private NavigationAgent3D _navigationAgent;
    private bool _shouldMove = false;

    public override void _Ready()
    {
        _navigationAgent = GetNode<NavigationAgent3D>("NavigationAgent3D");
        _navigationAgent.VelocityComputed += onNavigationAgentVelocityComputed;
        _navigationAgent.NavigationFinished += onNavigationAgentNavigationFinished;
    }

    public override void _PhysicsProcess(double delta)
    {
        if (!_shouldMove)
        {
            return;
        }

        _navigationAgent.TargetPosition = _navigationAgent.TargetPosition;

        _movementDelta = _moveSpeed * (float)delta;
        Vector3 currentAgentPosition = Position;
        Vector3 nextPathPosition = _navigationAgent.GetNextPathPosition();
        Vector3 newVelocity = (nextPathPosition - currentAgentPosition).Normalized() * _movementDelta;

        _navigationAgent.Velocity = newVelocity;
    }

    public void MoveTo(Vector3 position)
    {
        _movementDelta = _moveSpeed * (float)GetProcessDeltaTime();

        _navigationAgent.TargetPosition = position;
        _navigationAgent.Velocity = (_navigationAgent.GetNextPathPosition() - Position).Normalized() * _movementDelta;
        _shouldMove = true;
    }

    private void onNavigationAgentVelocityComputed(Vector3 safeVelocity)
    {
        Position = Position.MoveToward(Position + safeVelocity, _movementDelta);
    }

    private void onNavigationAgentNavigationFinished()
    {
        _navigationAgent.TargetPosition = Position;
        _shouldMove = false;
    }
}
