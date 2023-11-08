using Agava.YandexGames;
using UnityEngine;

public class Ads :
    IEventReceiver<GameActionEvent>
{
    private int _amountEnergyPerReward = 20;

    public Ads()
    {
        this.Subscribe<GameActionEvent>();
    }

    ~Ads()
    {
        this.Unsubscribe<GameActionEvent>();
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
        VideoAd.Show(onRewardedCallback: OnRewardedCallback);
    }

    public void OnRequestReviewButtonClick()
    {
        ReviewPopup.Open();
    }

    public void OnCanRequestReviewButtonClick()
    {
        ReviewPopup.CanOpen((result, reason) => { });
    }

    public void OnEvent(GameActionEvent var)
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
