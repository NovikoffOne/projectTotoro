using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Test
{
    public int testInt;
}

public class PlayerStar : MonoBehaviour
{
    public static PlayerStar Instance;

    private int _count;

    public int Count => _count;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

            Testing(new Test(), 5);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            var test2 = new Test();

            Testing2(ref test2, 5);
        }
    }

    private void Testing(Test t, int count)
    {
        if (count > 0) 
        {
            t.testInt++;

            Debug.Log(t.GetHashCode());

            Testing(t, --count);
        }
        else
        {
            return;
        }
    }

    private void Testing2(ref Test t, int count)
    {
        if (count > 0)
        {
            t.testInt++;

            Debug.Log(t.GetHashCode());

            Testing2(ref t, --count);
        }
        else
        {
            return;
        }
    }

    public void SetStar(int count) => this._count = count;

    public void OnAdded(int count) => _count += count;
}
