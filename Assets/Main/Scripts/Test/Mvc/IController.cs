using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public interface IController
{
    // Используется для связи с моделью

    void AddView<T>(T view) where T : class, IView;
}

public interface IGUIController<V, M> : IController // Основной рабочий элеммент
    where V : class, IView
    where M : class, IModel, new()
{
    V View { get; }
    M Model { get; }

    void UpdateView();
}
