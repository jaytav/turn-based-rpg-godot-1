using Godot;

[Tool]
public partial class CharacterStatData : Resource
{
    [Signal]
    public delegate void ValueUpdatedEventHandler(int value);

    [Signal]
    public delegate void MaxValueUpdatedEventHandler(int maxValue);

    [Export]
    public int Value
    {
        get { return _value; }
        set { setValue(value); }
    }

    [Export]
    public int MaxValue
    {
        get { return _maxValue; }
        set { setMaxValue(value); }
    }

    private int _value;
    private int _maxValue;

    private void setValue(int value)
    {
        // only clamp Value using MaxValue if more than 0
        if (MaxValue > 0)
        {
            value = Mathf.Clamp(value, 0, MaxValue);
        }
        else
        {
            value = Mathf.Max(value, 0);
        }

        // prevent ValueUpdated signal if Value is unchanged
        if (Value == value)
        {
            return;
        }

        _value = value;

        EmitSignal(nameof(ValueUpdated), Value);
    }

    private void setMaxValue(int maxValue)
    {
        maxValue = Mathf.Max(maxValue, 0);

        if (MaxValue == maxValue)
        {
            return;
        }

        _maxValue = maxValue;

        // update Value if more than updated MaxValue
        Value = Value;

        EmitSignal(nameof(MaxValueUpdated), MaxValue);
    }
}
