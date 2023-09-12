using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class TestReactValue : MonoBehaviour
{
    public IReactValue<int> _reactValueInt;
    public IReactValue<string> _reactValueString;

    private void Awake()
    {
        _reactValueInt = new ReactValue<int>(default);
        _reactValueInt.OnValueChanged += ReactInt;

        _reactValueString = new ReactValue<string>(default);
        _reactValueString.OnValueChanged += ReactString;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            var reactValueStr = ReactValueHolder<string>.GetReactValue("str");
            var reactValueInt = ReactValueHolder<int>.GetReactValue("int");

            reactValueInt.SetValue(reactValueInt.Value += 10);

            reactValueStr.SetValue($"{reactValueInt.Value}");
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            _reactValueInt.Value += 10;
            _reactValueString.Value = $"string {_reactValueInt.Value}";
        }
    }

    private void OnDestroy()
    {
        _reactValueInt.Dispose();
        _reactValueString.Dispose();
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
