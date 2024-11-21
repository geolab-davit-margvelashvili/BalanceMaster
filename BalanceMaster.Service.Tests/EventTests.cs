namespace BalanceMaster.Service.Tests;

public class SomeClass
{
    public event EventHandler<int>? ValueChanged;
    private int _value;

    public int Value
    {
        get => _value;
        set
        {
            _value = value;
            ValueChanged?.Invoke(this, _value);
        }
    }
}

public class EventTests
{
    [Fact]
    public void TestDelegateCall()
    {
        var someClass = new SomeClass();

        someClass.ValueChanged += Function1;
        someClass.ValueChanged += Function2;

        someClass.Value = 10;

        someClass.ValueChanged -= Function2;

        someClass.Value += 20;
    }

    private void Function1(object sender, int parameter)
    {
    }

    private void Function2(object sender, int parameter)
    {
    }
}