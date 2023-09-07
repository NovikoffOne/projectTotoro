using System;

namespace Assets.Main.DataSaver
{
    [Serializable]
    public class PlayerStarData : IData
    {
        public int Count;

        public PlayerStarData() { } 

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
