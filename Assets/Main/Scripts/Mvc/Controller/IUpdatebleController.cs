using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IUpdatebleController
{
    string Tag { get; }

    void Update(string tag);
}
