using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class TRackableBoard : MonoBehaviour, ITrackableEventHandler 
{
	[SerializeField]
	private GameObject PanelTracking;

	[SerializeField]
	private GameObject Tour;

	[SerializeField]
	private GameObject CanvasBoard;

	[SerializeField]
	private GameObject PanelScore;

	[SerializeField]
	private GameObject Viseur;

	private GameObject[] Seeds;

	TrackableBehaviour mTrackableBehaviour;

	// Use this for initialization
	void Start () 
	{
		SetupSeed();
		mTrackableBehaviour = GetComponent<TrackableBehaviour>();
		mTrackableBehaviour.RegisterTrackableEventHandler(this);
		PanelTracking.SetActive(true);
		Viseur.SetActive(false);
		PanelScore.SetActive(false);
		CanvasBoard.SetActive(false);
		Tour.SetActive(false);
	}

	private void SetupSeed()
	{
		Seeds = GameObject.FindGameObjectsWithTag("Seed");
	}

	void ITrackableEventHandler.OnTrackableStateChanged(TrackableBehaviour.Status previousStatus,TrackableBehaviour.Status newStatus)
	{
		if(newStatus == TrackableBehaviour.Status.TRACKED)
		{
			PanelScore.SetActive(true);
			PanelTracking.SetActive(false);
			Viseur.SetActive(true);
			CanvasBoard.SetActive(true);
			Tour.SetActive(true);

			SetupSeed();
			for(int i = 0; i < Seeds.Length ; i++)
			{
				Seeds[i].GetComponent<Rigidbody>().useGravity = true;
			}
		}
		else 
		{
			PanelScore.SetActive(false);
			Viseur.SetActive(false);
			PanelTracking.SetActive(true);
			CanvasBoard.SetActive(false);
			Tour.SetActive(false);
			
			for(int i = 0; i < Seeds.Length ; i++)
			{
				Seeds[i].GetComponent<Rigidbody>().useGravity = false;
			}
		}
	}
}
