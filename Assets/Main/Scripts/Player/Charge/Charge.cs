using UnityEngine;

namespace Assets.Main.Scripts.PlayerEnity
{
    public class Charge : MonoBehaviour
    {
        private int _index;

        public int Index => _index;

        public void Init(int index = 0)
        {
            _index = index;
        }

        public void Move(Transform transformParent)
        {
            transform.parent = transformParent;

            transform.position = transformParent.position;
        }
    }
}