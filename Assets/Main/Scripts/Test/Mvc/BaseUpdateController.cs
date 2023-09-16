using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public abstract class BaseUpdateController<V, M> : BaseController<V, M>, IUpdatebleController
        where V : class, IView
        where M : class, IModel, new()
{
    public string Tag => GetType().Name;

    public void UpdateController(string tag)
    {
        if (!string.IsNullOrEmpty(tag) & tag.Equals("0"))
            UpdateView();

        if (!string.IsNullOrEmpty(tag) & !Tag.Equals(tag))
            return;

        UpdateView();
    }
}
