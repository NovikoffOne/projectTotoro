using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ReactValue<T> : IReactValue<T>
{
    private T _value;

    public event Action<T> OnValueChanged;

    public T Value { get => _value; set => SetValue(value); }

    public ReactValue(T value)
    {
        _value = value;
        OnValueChanged?.Invoke(_value);
    }

    public void SetValue(T value)
    {
        _value = value;
        OnValueChanged?.Invoke(_value);
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}
