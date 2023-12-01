using Assets.Main.Scripts;

namespace Assets.Main.Scripts.Pool
{
    public class PoolPlayer<T> : PoolMono<T>
        where T : PlayerEnity.Player
    {
        public PoolPlayer(T prefab) : base(prefab) { }

        public override void OnSpawn(T element)
        {
            element?.Movement.ResetPosition();

            element?.gameObject.SetActive(true);
        }

        public override void OnDespawn(T element)
        {
            element?.Movement.ResetPosition();

            element?.gameObject.SetActive(false);
        }
    }
}