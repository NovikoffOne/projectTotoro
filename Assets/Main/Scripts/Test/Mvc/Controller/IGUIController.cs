using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IGUIController<V, M> : IController // Основной рабочий элеммент
    where V : class, IView
    where M : class, IModel, new()
{
    V View { get; }
    M Model { get; }

    void UpdateView();
}
