using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class VBScript : MonoBehaviour, IVirtualButtonEventHandler 
{
	VirtualButtonBehaviour[] vb;

	GameManager gameManager;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		vb = GetComponentsInChildren<VirtualButtonBehaviour>();

		foreach (VirtualButtonBehaviour v in vb)
		{
			v.RegisterEventHandler(this);
		}
	}

	void IVirtualButtonEventHandler.OnButtonPressed(VirtualButtonBehaviour vb)
	{
		if(GameObject.Find(vb.name).GetComponent<Renderer>().material.color == Color.green)
		{
			GameObject.Find(vb.name).GetComponent<Renderer>().material.color = Color.white;
			int lay = GameObject.Find(vb.name).layer - 7;

			List<GameObject> tempList = gameManager.FindList(vb.name);

			// Si on est rouge ou bleu
			if(tempList.Count > 0 && ((gameManager.joueur == 1 && lay < 13 && lay > 6) || (gameManager.joueur == 0 && lay < 7 && lay > 0 )))
			{
				gameManager.PlayMoveSeed(vb.name);
			}
		}
		else
		{
			if(gameManager.PointeurSelect != "")
			{
				GameObject.Find(gameManager.PointeurSelect).GetComponent<Renderer>().material.color = Color.white;
				GameObject.Find(vb.name).GetComponent<Renderer>().material.color = Color.green;
			}
			else
			{
				GameObject.Find(vb.name).GetComponent<Renderer>().material.color = Color.green;
			}

			gameManager.PointeurSelect = vb.name;
		}
	}

	void IVirtualButtonEventHandler.OnButtonReleased(VirtualButtonBehaviour vb)
	{

	}
}
