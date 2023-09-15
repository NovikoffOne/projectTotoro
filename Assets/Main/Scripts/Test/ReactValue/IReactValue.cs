using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IReactValue<T> : IDisposable
{
    public event Action<T> OnValueChanged;
    public T Value { get; set; }
}
