using UnityEngine;
using System.Collections;

public class MyAppStart : MonoBehaviour
{
	public static string uniqueUserId = "demoUserUnity";


	// Use this for initialization
	void Start ()
	{
		#if UNITY_ANDROID
        string appKey = "5404000";
		#elif UNITY_IPHONE
        string appKey = "5404001";
		#else
        string appKey = "unexpected_platform";
		#endif
		Debug.Log ("unity-script: MyAppStart Start called");

		//Dynamic config example
		IronSourceConfig.Instance.setClientSideCallbacks (true);

		string id = IronSource.Agent.getAdvertiserId ();
		Debug.Log ("unity-script: IronSource.Agent.getAdvertiserId : " + id);
		
		Debug.Log ("unity-script: IronSource.Agent.validateIntegration");
		IronSource.Agent.validateIntegration ();

		Debug.Log ("unity-script: unity version" + IronSource.unityVersion ());

		// SDK init
		Debug.Log ("unity-script: IronSource.Agent.init");
		IronSource.Agent.init (appKey);
		//IronSource.Agent.init (appKey, IronSourceAdUnits.REWARDED_VIDEO, IronSourceAdUnits.INTERSTITIAL, IronSourceAdUnits.OFFERWALL, IronSourceAdUnits.BANNER);
        //IronSource.Agent.initISDemandOnly (appKey, IronSourceAdUnits.REWARDED_VIDEO, IronSourceAdUnits.INTERSTITIAL);

		IronSource.Agent.validateIntegration();
	}
	
	// Update is called once per frame
	void Update ()
	{

	}

	void OnApplicationPause (bool isPaused)
	{
		Debug.Log ("unity-script: OnApplicationPause = " + isPaused);
		IronSource.Agent.onApplicationPause (isPaused);
	}
}
