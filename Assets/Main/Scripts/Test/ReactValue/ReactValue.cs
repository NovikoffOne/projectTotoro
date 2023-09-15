using System;

public class ReactValue<T> : IReactValue<T>
{
    private T _value;

    public event Action<T> OnValueChanged;

    public T Value { get => _value;
        set
        {
            _value = value;
            OnValueChanged?.Invoke(_value);
        }
    }

    public ReactValue(T value)
    {
        _value = value;
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}
