using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class TestReactValue2 : MonoBehaviour
{
    IReactValue<int> _reactValueInt;
    IReactValue<string> _reactValueStr;

    private void Awake()
    {
        ReactValueHolder<int>.Add("int", new ReactValue<int>(default));

        _reactValueInt = ReactValueHolder<int>.GetReactValue("int");

        _reactValueInt.OnValueChanged += ReactInt;

        ReactValueHolder<string>.Add("str", new ReactValue<string>(default));

        _reactValueStr = ReactValueHolder<string>.GetReactValue("str");

        _reactValueStr.OnValueChanged += ReactStr;
    }

    private void OnDestroy()
    {
        _reactValueInt.Dispose();
        _reactValueStr.Dispose();
    }

    private void ReactStr(string var)
    {
        Debug.Log($"TEST2 STR - {var}");
    }

    private void ReactInt(int var)
    {
        Debug.Log($"TEST2 INT - {var}");
    }
}
