using System;

namespace Assets.Main.Scripts.SaveLoader
{
    public class PlayerStarData : IData
    {
        public int Count;
        public int LevelIndex;

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}