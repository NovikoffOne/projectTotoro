using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class MainMenuModel : IModel
{
    public void GetData()
    {
        
    }

    public void UpdateData()
    {
        
    }

    public void Play()
    {
        EventBus.Raise(new ClickButtonPlayInMenu());
    }

    public void LiderBoard()
    {
        EventBus.Raise(new ClickLiderBoardButtonInMenu());
    }

    public void Settings()
    {
        EventBus.Raise(new ClickSettingsButtonInMenu());
    }
}
