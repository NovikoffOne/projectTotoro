using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class TestReactValue2<T, T2> : MonoBehaviour, IObserverReactValue<T>, IObserveableReactValue<T>
{
    public ReactValueHolder<T, T2> ReactValueHolder;

    private void Awake()
    {
        ReactValueHolder.Add(this, new TesterReactValue<T2>(default));

        //ReactValueHolder.GetReactValue(this, new TesterReactValue<T2>(default));
    }

    private void ReactInt(int var)
    {
        Debug.Log($"INT - {var}");
    }

    private void ReactString(string var)
    {
        Debug.Log($"STR - {var}");
    }
}
