using System;
using HutongGames.PlayMaker;
using UnityEngine;

namespace ISelf.PlayMaker.Actions
{
	public enum AdType
	{
		Rewarded,
		Interstitial
	}
	
	[ActionCategory("Custom")]
	[HutongGames.PlayMaker.Tooltip("Init Iron Source")]
	public class IronSourceShowAction : FsmStateAction
	{
		public AdType adType;
		public FsmString placement;
		public FsmEvent notReadyEvent;
		public FsmEvent successEvent;
		public FsmEvent failEvent;
		public FsmBool loadAfter;
		
		public override void OnEnter()
		{
			IronSourceEvents.onInterstitialAdShowSucceededEvent += HandleShowSuccess;
			IronSourceEvents.onInterstitialAdShowFailedEvent += HandleShowFail;
			
			if (Application.isEditor)
			{
				Debug.Log("[TEST] IronSourceShowAction skip by editor! Send event: "+notReadyEvent.Name);
				Event(notReadyEvent);
				Finish();
				return;
			}
			if (IronSource.Agent.isInterstitialReady())
			{
				Debug.Log($"[TEST] Show interstitial {placement.Value}");
				switch (adType)
				{
					case AdType.Rewarded:
						IronSource.Agent.showRewardedVideo(placement.Value);
						break;
					case AdType.Interstitial:
						IronSource.Agent.showInterstitial(placement.Value);
						break;
					default:
						Debug.Log("[TEST] Load interstitial");
						Event(failEvent);
						break;
				}
			}
			else
			{
				IronSource.Agent.loadInterstitial();
				Event(notReadyEvent);
			}
		}
		
		public override void OnExit()
		{
			IronSourceEvents.onInterstitialAdShowSucceededEvent -= HandleShowSuccess;
			IronSourceEvents.onInterstitialAdShowFailedEvent -= HandleShowFail;
		}
		
		private void HandleShowSuccess()
		{
			Debug.Log("Show Ad success!");
			
			if (loadAfter.Value &&
				adType==AdType.Interstitial &&
				IronSource.Agent.isInterstitialReady())
				IronSource.Agent.loadInterstitial();
			
			Event(successEvent);
		}
		
		private void HandleShowFail(IronSourceError err)
		{
			Debug.Log($"Show Ad failed: {err}");
			
			if (loadAfter.Value &&
				adType==AdType.Interstitial &&
				IronSource.Agent.isInterstitialReady())
				IronSource.Agent.loadInterstitial();
			
			Event(failEvent);
		}
		
		
	}
}
