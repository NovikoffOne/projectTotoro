using System.Collections.Generic;
using UnityEngine;

namespace Assets.Main.Scripts.PlayerEnity
{
    public class ChargeColor : MonoBehaviour
    {
        [SerializeField] private List<Charge> _charges;

        public Charge GetMaterial(int index)
        {
            return _charges[index];
        }
    }
}