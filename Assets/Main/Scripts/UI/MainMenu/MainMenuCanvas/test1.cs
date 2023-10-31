using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Main.Scripts.UI.MainMenu.MainMenuCanvas
{
    internal class test1 : MonoBehaviour
    {
        public enum KEY
        {
            Key1,
            Key2,
            Key3,
            Key4
        }


        

        public string GetRandomValue()
        {
            Dictionary<string, string> strings = new Dictionary<string, string>();

            strings["Key0"] = "Value 0";
            strings["Key1"] = "Value 1";
            strings["Key2"] = "Value 2";
            strings["Key3"] = "Value 3";
            strings["Key4"] = "Value 4";

            var value = UnityEngine.Random.Range(0, strings.Count);

            int temp = 0;


            foreach (var _string in strings.Values)
            {
                temp++;

                if(temp == value)
                {
                    return _string;
                }
            }

            return "";
        }
    }
}
