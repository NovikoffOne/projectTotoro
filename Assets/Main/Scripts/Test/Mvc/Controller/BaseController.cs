using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public abstract class BaseController<V, M> : IGUIController<V, M>
    where V : class, IView
    where M : class, IModel, new()
{
    public BaseController()
    {
        Model = Activator.CreateInstance<M>();
    }

    public V View { get; private set; }

    public M Model { get; }

    public abstract void UpdateView();
    public abstract void HidePanel();

    void IController.AddView<T>(T view)
    {
        if (View != null)
            return;

        View = view as V;

        OnShow();
    }

    protected abstract void OnShow();
}
