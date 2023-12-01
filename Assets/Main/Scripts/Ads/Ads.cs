using Agava.YandexGames;
using Assets.Main.Scripts.Events;
using Assets.Main.Scripts.Events.GameEvents;
using UnityEngine;

namespace Assets.Main.Scripts.Ads
{
    public class Ads :
        IEventReceiver<GameActionEvent>
    {
        private int _amountEnergyPerReward = 20;

        public Ads()
        {
            this.Subscribe();
        }

        ~Ads()
        {
            this.Unsubscribe();
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

        private void OnRewardedCallback()
        {
            EventBus.Raise(new IsRewarded());
            EventBus.Raise(new RewardGasAdded(_amountEnergyPerReward));
        }
    }
}