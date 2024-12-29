using System.Collections;
using UnityEngine;

#if UNITY_IOS
using Unity.Advertisement.IosSupport;
#endif
 
public class AttPermissionRequest : MonoBehaviour {
    void Awake() {
#if UNITY_IOS
        // Check the user's consent status.
        // If the status is undetermined, display the request request:
        if(ATTrackingStatusBinding.GetAuthorizationTrackingStatus() == ATTrackingStatusBinding.AuthorizationTrackingStatus.NOT_DETERMINED) {
            ATTrackingStatusBinding.RequestAuthorizationTracking();
        }
#endif
    }

}