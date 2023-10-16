﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental;
using Agava.YandexGames;
using System.Collections;

public class Ads : MonoBehaviour,
    IEventReceiver<ClickGameActionEvent>
{
    private void Awake()
    {
       // PlayerAccount.AuthorizedInBackground += OnAuthorizedInBackground;
        this.Subscribe<ClickGameActionEvent>();
    }

    private void OnDestroy()
    {
        //PlayerAccount.AuthorizedInBackground -= OnAuthorizedInBackground;
        this.Unsubscribe<ClickGameActionEvent>();
    }

    public void OnShowInterstitialButtonClick()
    {
        InterstitialAd.Show();
    }

    public void OnShowVideoButtonClick()
    {
        VideoAd.Show(onRewardedCallback:OnRewardedCallback);
    }

    //public void OnAuthorizeButtonClick()
    //{
    //    PlayerAccount.Authorize();
    //}

    //public void OnRequestPersonalProfileDataPermissionButtonClick()
    //{
    //    PlayerAccount.RequestPersonalProfileDataPermission();
    //}

    //public void OnGetProfileDataButtonClick()
    //{
    //    PlayerAccount.GetProfileData((result) =>
    //    {
    //        string name = result.publicName;
    //        if (string.IsNullOrEmpty(name))
    //            name = "Anonymous";
    //        Debug.Log($"My id = {result.uniqueID}, name = {name}");
    //    });
    //}

    //public void OnCallGameReadyButtonClick()
    //{
    //    YandexGamesSdk.GameReady();
    //}

    //public void OnSuggestShortcutButtonClick()
    //{
    //    Shortcut.Suggest();
    //}

    public void OnRequestReviewButtonClick()
    {
        ReviewPopup.Open();
    }

    //public void OnCanSuggestShortcutButtonClick()
    //{
    //    Shortcut.CanSuggest(result => { });
    //}

    public void OnCanRequestReviewButtonClick()
    {
        ReviewPopup.CanOpen((result, reason) => { });
    }

    public void OnEvent(ClickGameActionEvent var)
    {
        switch (var.GameAction)
        {
            case GameAction.ClickNextLevel:

            case GameAction.ClickReload:
                OnShowInterstitialButtonClick();
                Debug.Log("InterstitalAd");
                break;

            case GameAction.ClickReward:
                OnShowVideoButtonClick();
                Debug.Log("Reward");
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
        EventBus.Raise(new RewardAddGas(20));
    }
}