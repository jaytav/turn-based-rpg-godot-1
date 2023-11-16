using Godot;

public partial class EnemyController : Node
{
    public GridCellItem Enemy
    {
        get { return _enemy; }
        set { setEnemy(value); }
    }

    private GridCellItem _enemy;

    private void setEnemy(GridCellItem item)
    {
        _enemy = item;
        Enemy.TreeExited += onEnemyTreeExited;
    }

    private void onEnemyTreeExited()
    {
        GD.Print("You Win!");
    }
}
