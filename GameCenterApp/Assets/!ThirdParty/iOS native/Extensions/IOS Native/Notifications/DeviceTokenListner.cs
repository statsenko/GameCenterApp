////////////////////////////////////////////////////////////////////////////////
//  
// @module IOS Native Plugin for Unity3D 
// @author Osipov Stanislav (Stan's Assets) 
// @support stans.assets@gmail.com 
//
////////////////////////////////////////////////////////////////////////////////



using UnityEngine;
using System.Collections;

public class DeviceTokenListner : MonoBehaviour {
	
	

	//--------------------------------------
	// INITIALIZE
	//--------------------------------------

	public static void Create() {
		 new GameObject ("DeviceTockenListner").AddComponent<DeviceTokenListner> ();
	}


	void Awake() {
		DontDestroyOnLoad (gameObject);
	}


	//--------------------------------------
	//  PUBLIC METHODS
	//--------------------------------------

	/*
	  
	 
    #if UNITY_IPHONE
	
	private bool tokenSent = false;
	#endif
	void  FixedUpdate () {
		#if UNITY_IPHONE
		
		if (!tokenSent) {

			byte[] token   = NotificationServices.deviceToken;
			if(token != null) {

				IOSNotificationDeviceToken t = new IOSNotificationDeviceToken(token);
				IOSNotificationController.instance.OnDeviceTockeReceived (t);
				Destroy (gameObject);
			}
		}
		
		#endif

	}
	
	*/
	
	//--------------------------------------
	//  GET/SET
	//--------------------------------------
	
	//--------------------------------------
	//  EVENTS
	//--------------------------------------
	
	//--------------------------------------
	//  PRIVATE METHODS
	//--------------------------------------
	
	//--------------------------------------
	//  DESTROY
	//--------------------------------------

}
