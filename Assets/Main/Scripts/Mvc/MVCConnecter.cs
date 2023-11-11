using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;

public static class MVCConnecter
{
    private static event Action<string> OnControllerUpdated;

    public static void AddController<T>(this IView view) where T : class, IController, new()
    {
        var controller = Activator.CreateInstance<T>();
        controller.AddView(view);

        if(controller is IUpdatebleController)
        {
            var updateController = controller as IUpdatebleController;

            OnControllerUpdated += updateController.Update;
        }
    }

    public static void UpdateController<T>() where T : class, IUpdatebleController => UpdateController(typeof(T).Name);

    public static void UpdateController(string tag)
    {
        OnControllerUpdated?.Invoke(tag);
    }

    public static void UpdateAllControllers() 
    {
        UpdateController("0");
    }
}