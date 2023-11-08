using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental;
using Agava.YandexGames;
using System.Collections;
using UnityEngine.SceneManagement;

public class Ads :
    IEventReceiver<ClickGameActionEvent>
{
    private int _amountEnergyPerReward = 20;

    public Ads()
    {
        this.Subscribe<ClickGameActionEvent>();
    }

    ~Ads()
    {
        this.Unsubscribe<ClickGameActionEvent>();
    }

    public void OnShowInterstitialButtonClick()
    {
        InterstitialAd.Show();
    }

    public void OnShowVideoButtonClick()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        OnRewardedCallback();

        return;
#endif
        VideoAd.Show(onRewardedCallback:OnRewardedCallback);
    }

    public void OnRequestReviewButtonClick()
    {
        ReviewPopup.Open();
    }

    public void OnCanRequestReviewButtonClick()
    {
        ReviewPopup.CanOpen((result, reason) => { });
    }

    public void OnEvent(ClickGameActionEvent var)
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        if (var.GameAction == GameAction.ClickReward)
            OnShowVideoButtonClick();
        
        return;
#endif
        switch (var.GameAction)
        {
            case GameAction.ClickNextLevel:

            case GameAction.ClickReload:
                OnShowInterstitialButtonClick();
                break;

            case GameAction.ClickReward:
                OnShowVideoButtonClick();
                break;

            default:
                break;
        }
    }

    private void OnAuthorizedInBackground()
    {
        Debug.Log($"{nameof(OnAuthorizedInBackground)} {PlayerAccount.IsAuthorized}");
    }

    private void OnRewardedCallback()
    {
        EventBus.Raise(new IsRewarded());
        EventBus.Raise(new RewardAddGas(_amountEnergyPerReward));
    }
}
