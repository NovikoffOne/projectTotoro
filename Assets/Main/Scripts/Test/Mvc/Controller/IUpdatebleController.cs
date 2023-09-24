using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IUpdatebleController // Представляет рабочий интерфейс котроллера обновляющийся по тегу
{
    string Tag { get; }

    void UpdateController(string tag);
}
