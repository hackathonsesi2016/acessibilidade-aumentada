using System;
using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using Vuforia;


public class CustomTrackableEventHandler : MonoBehaviour,
                                            ITrackableEventHandler
{
    private TrackableBehaviour mTrackableBehaviour;
    public CustomEventString OnFoundTrack;
    public CustomEventString OnLostTrack;
	TrackableBehaviour.Status oldStatus = TrackableBehaviour.Status.NOT_FOUND;

    void Start()
    {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }
    }

    public void OnTrackableStateChanged(
                                    TrackableBehaviour.Status previousStatus,
                                    TrackableBehaviour.Status newStatus)
    {
		
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            OnTrackingFound();
        }
		else if(oldStatus == TrackableBehaviour.Status.DETECTED ||
			oldStatus == TrackableBehaviour.Status.TRACKED ||
			oldStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            OnTrackingLost();
        }
		oldStatus = newStatus;
    }

    private void OnTrackingFound()
    {
        Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
        Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

        // Enable rendering:
        foreach (Renderer component in rendererComponents)
        {
            component.enabled = true;
        }

        // Enable colliders:
        foreach (Collider component in colliderComponents)
        {
            component.enabled = true;
        }

        Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");
        OnFoundTrack.Invoke(mTrackableBehaviour.TrackableName);
    }


    private void OnTrackingLost()
    {
        Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
        Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

        // Disable rendering:
        foreach (Renderer component in rendererComponents)
        {
            component.enabled = false;
        }

        // Disable colliders:
        foreach (Collider component in colliderComponents)
        {
            component.enabled = false;
        }

        Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
        OnLostTrack.Invoke(mTrackableBehaviour.TrackableName);
    }


}

[Serializable]
public class CustomEventString : UnityEvent<string>
{

}