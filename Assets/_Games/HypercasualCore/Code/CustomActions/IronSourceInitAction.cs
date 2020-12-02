using HutongGames.PlayMaker;
using UnityEngine;

namespace ISelf.PlayMaker.Actions
{
	[ActionCategory("Custom")]
	[HutongGames.PlayMaker.Tooltip("Init Iron Source")]
	public class IronSourceInitAction : FsmStateAction
	{
		public FsmString ApiKey;
		public FsmBool preloadInterstitial;
		public FsmBool preloadBanner;
		public IronSourceBannerPosition bannerPosition;
		
		public override void OnEnter()
		{
			// IronSourceEvents.onInterstitialAdReadyEvent += ()=>Debug.Log("[TEST] onInterstitialAdReadyEvent");
			// IronSourceEvents.onInterstitialAdLoadFailedEvent += err=>Debug.Log($"[TEST] onInterstitialAdLoadFailedEvent: {err}");
			// IronSourceEvents.onInterstitialAdShowSucceededEvent += ()=>Debug.Log("[TEST] onInterstitialAdReadyEvent");
			// IronSourceEvents.onInterstitialAdShowFailedEvent += err=>Debug.Log($"[TEST] onInterstitialAdReadyEvent: {err}");
			// IronSourceEvents.onInterstitialAdClickedEvent += ()=>Debug.Log("[TEST] onInterstitialAdClickedEvent");
			// IronSourceEvents.onInterstitialAdOpenedEvent += ()=>Debug.Log("[TEST] onInterstitialAdOpenedEvent");
			// IronSourceEvents.onInterstitialAdClosedEvent += ()=>Debug.Log("[TEST] onInterstitialAdClosedEvent");
		
			IronSource.Agent.init(ApiKey.Value);
			
			IronSource.Agent.validateIntegration();
			
			IronSource.Agent.shouldTrackNetworkState (true);
			
			if (preloadInterstitial.Value)
			{
				IronSource.Agent.loadInterstitial();
			}
			if (preloadBanner.Value)
			{
				IronSource.Agent.loadBanner(IronSourceBannerSize.SMART, bannerPosition);
			}
			
			Finish();
		}
	}
}