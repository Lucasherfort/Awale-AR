using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour 
{
	public List<GameObject> attic1 = new List<GameObject>();
	public List<GameObject> attic2 = new List<GameObject>();
	public List<GameObject> attic3 = new List<GameObject>();
	public List<GameObject> attic4 = new List<GameObject>();
	public List<GameObject> attic5 = new List<GameObject>();
	public List<GameObject> attic6 = new List<GameObject>();
	public List<GameObject> attic7 = new List<GameObject>();
	public List<GameObject> attic8 = new List<GameObject>();
	public List<GameObject> attic9 = new List<GameObject>();
	public List<GameObject> attic10 = new List<GameObject>();
	public List<GameObject> attic11 = new List<GameObject>();
	public List<GameObject> attic12 = new List<GameObject>();

	public GameObject[] atticText;

	private List<List<GameObject>> ListeDesListesBoard = new List<List<GameObject>>();

	[SerializeField]
	private GameObject[] spawnSeed;

	public string PointeurSelect;

	RaycastHit hit;

	// Joueur bleu = 0 et joueur rouge = 1
	public int joueur = 0;
	public Text TxtJoueur;

	public int scoreBleu = 0;
	public int scoreRouge = 0;

	public Text scoreBlueTxt;
	public Text scoreRedTxt;

	public bool isFinish = false;
	public GameObject PanelFinish;
	public Text finishTxt;

	private void Start()
	{
		PanelFinish.SetActive(false);
		TxtJoueur.text = "BLUE";
		TxtJoueur.color = Color.blue;

		scoreBlueTxt.text = "0";
		scoreRedTxt.text = "0";
		SetupListAttic();
		PointeurSelect = "";
	}

	private void SetupListAttic()
	{
		ListeDesListesBoard.Add(attic1);
		ListeDesListesBoard.Add(attic2);
		ListeDesListesBoard.Add(attic3);
		ListeDesListesBoard.Add(attic4);
		ListeDesListesBoard.Add(attic5);
		ListeDesListesBoard.Add(attic6);
		ListeDesListesBoard.Add(attic7);
		ListeDesListesBoard.Add(attic8);
		ListeDesListesBoard.Add(attic9);
		ListeDesListesBoard.Add(attic10);
		ListeDesListesBoard.Add(attic11);
		ListeDesListesBoard.Add(attic12);

		attic1.Add(GameObject.Find("seed_1"));
		attic1.Add(GameObject.Find("seed_2"));
		attic1.Add(GameObject.Find("seed_3"));
		attic1.Add(GameObject.Find("seed_4"));

		attic2.Add(GameObject.Find("seed_5"));
		attic2.Add(GameObject.Find("seed_6"));
		attic2.Add(GameObject.Find("seed_7"));
		attic2.Add(GameObject.Find("seed_8"));

		attic3.Add(GameObject.Find("seed_9"));
		attic3.Add(GameObject.Find("seed_10"));
		attic3.Add(GameObject.Find("seed_11"));
		attic3.Add(GameObject.Find("seed_12"));

		attic4.Add(GameObject.Find("seed_13"));
		attic4.Add(GameObject.Find("seed_14"));
		attic4.Add(GameObject.Find("seed_15"));
		attic4.Add(GameObject.Find("seed_16"));

		attic5.Add(GameObject.Find("seed_17"));
		attic5.Add(GameObject.Find("seed_18"));
		attic5.Add(GameObject.Find("seed_19"));
		attic5.Add(GameObject.Find("seed_20"));

		attic6.Add(GameObject.Find("seed_21"));
		attic6.Add(GameObject.Find("seed_22"));
		attic6.Add(GameObject.Find("seed_23"));
		attic6.Add(GameObject.Find("seed_24"));

		attic12.Add(GameObject.Find("seed_25"));
		attic12.Add(GameObject.Find("seed_26"));
		attic12.Add(GameObject.Find("seed_27"));
		attic12.Add(GameObject.Find("seed_28"));

		attic11.Add(GameObject.Find("seed_29"));
		attic11.Add(GameObject.Find("seed_30"));
		attic11.Add(GameObject.Find("seed_31"));
		attic11.Add(GameObject.Find("seed_32"));

		attic10.Add(GameObject.Find("seed_33"));
		attic10.Add(GameObject.Find("seed_34"));
		attic10.Add(GameObject.Find("seed_35"));
		attic10.Add(GameObject.Find("seed_36"));

		attic9.Add(GameObject.Find("seed_37"));
		attic9.Add(GameObject.Find("seed_38"));
		attic9.Add(GameObject.Find("seed_39"));
		attic9.Add(GameObject.Find("seed_40"));

		attic8.Add(GameObject.Find("seed_41"));
		attic8.Add(GameObject.Find("seed_42"));
		attic8.Add(GameObject.Find("seed_43"));
		attic8.Add(GameObject.Find("seed_44"));

		attic7.Add(GameObject.Find("seed_45"));
		attic7.Add(GameObject.Find("seed_46"));
		attic7.Add(GameObject.Find("seed_47"));
		attic7.Add(GameObject.Find("seed_48"));
	}

	public void PlayMoveSeed(string atticName)
	{
		List<GameObject> currentList = FindList(atticName);
		int nbseed = currentList.Count;

		for(int i = 0; i < currentList.Count ; i++)
		{
			currentList[i].SetActive(false);
		}

		AnimationPutSeed(currentList);
	}

	private void AnimationPutSeed(List<GameObject> StartList)
	{
		int Index = ListeDesListesBoard.IndexOf(StartList);

		int depart = Index;

		atticText[Index].GetComponent<Text>().text = "0";

		int start = Index + 1;

		List<GameObject> temp = new List<GameObject>(StartList);

		StartList.Clear();

		while (temp.Count != 0)
		{
			if(start == ListeDesListesBoard.Count)
			{
				start = 0;
			}

			if(start == depart)
			{
				start++;
			}

			List<GameObject> nList = ListeDesListesBoard[start];

			nList.Add(temp[0]);
			atticText[start].GetComponent<Text>().text = nList.Count.ToString();

			temp[0].transform.position = spawnSeed[start].transform.position;

			temp[0].SetActive(true);

			start++;

			temp.Remove(temp[0]);

		}

		int arrive = start - 1;


		// On regarde si on peut prendre des graines
		
		// Si il y a 2 ou 3 graines sur le dernier grenier
		// Et que se grenier est coté de l'adversaire
		// c'est ok!

		if(joueur == 0)
		{
			IncreaseScoreBlue(depart,arrive);
		}
		else
		{
			IncreaseScoreRed(depart,arrive);
		}
	}

	private void IncreaseScoreBlue(int d, int a)
	{
		while(a > 5 && a < 12 && (ListeDesListesBoard[a].Count == 2 || ListeDesListesBoard[a].Count == 3)  && d != a)
		{
			scoreBleu += ListeDesListesBoard[a].Count;
			scoreBlueTxt.text = scoreBleu.ToString();

			for(int i = 0 ; i < ListeDesListesBoard[a].Count ; i++)
			{
				// On détruit les gameObject des listes
				Destroy(ListeDesListesBoard[a][i]);
			}

			atticText[a].GetComponent<Text>().text = "0";
			ListeDesListesBoard[a].Clear();
			a--;
		}

		// On vérifie que le joueur rouge peut jouer
		if(attic7.Count == 0 && attic8.Count == 0 && attic9.Count == 0 && attic10.Count == 0 && attic11.Count == 0 && attic12.Count == 0)
		{
			isFinish = true;
			if(scoreBleu > scoreRouge)
			{
				FinishGame("bleus");
			}
			else if (scoreBleu < scoreRouge)
			{
				FinishGame("rouges");
			}
			else
			{
				FinishGame("");
			}
		}

		joueur = 1;
		TxtJoueur.text = "RED";
		TxtJoueur.color = Color.red;
	}

	private void IncreaseScoreRed(int d, int a)
	{
		while(a >= 0 && a < 6 && (ListeDesListesBoard[a].Count == 2 || ListeDesListesBoard[a].Count == 3)  && d != a)
		{
			scoreRouge += ListeDesListesBoard[a].Count;
			scoreRedTxt.text = scoreRouge.ToString();

			for(int i = 0 ; i < ListeDesListesBoard[a].Count ; i++)
			{
				// On détruit les gameObject des listes
				Destroy(ListeDesListesBoard[a][i]);
			}

			atticText[a].GetComponent<Text>().text = "0";
			ListeDesListesBoard[a].Clear();
			a--;
		}

		// On vérifie que le joueur bleu peut joueur
		if(attic1.Count == 0 && attic2.Count == 0 && attic3.Count == 0 && attic4.Count == 0 && attic5.Count == 0 && attic6.Count == 0)
		{
			isFinish = true;
			if(scoreBleu > scoreRouge)
			{
				FinishGame("bleus");
			}
			else if (scoreBleu < scoreRouge)
			{
				FinishGame("rouges");
			}
			else
			{
				FinishGame("");
			}
		}

		joueur = 0;
		TxtJoueur.text = "BLUE";
		TxtJoueur.color = Color.blue;
	}

	private void FinishGame(string color)
	{
		PanelFinish.SetActive(true);

		if(color == "")
		{
			finishTxt.text = "Egalité";
		}
		else
		{
			finishTxt.text = "Les "+color+" remportent la partie";
		}
		StartCoroutine(GoToMenu());
	}

	private IEnumerator GoToMenu()
	{
		yield return new WaitForSeconds(5f);
		SceneManager.LoadScene("Menu");
	}

	public List<GameObject> FindList(string atticName)
	{
		List<GameObject> result = new List<GameObject>();

		switch (atticName)
		{
			case "attic1":
			result = attic1;
			break;

			case "attic2":
			result = attic2;
			break;

			case "attic3":
			result = attic3;
			break;

			case "attic4":
			result = attic4;
			break;
			
			case "attic5":
			result = attic5;
			break;

			case "attic6":
			result = attic6;
			break;

			case "attic7":
			result = attic7;
			break;

			case "attic8":
			result = attic8;
			break;

			case "attic9":
			result = attic9;
			break;

			case "attic10":
			result = attic10;
			break;

			case "attic11":
			result = attic11;
			break;

			case "attic12":
			result = attic12;
			break;
		}

		return result;
	}
	
	private void Update () 
	{
		Vector2 center = new Vector2(Screen.width/2, Screen.height/2);
		Ray ray = Camera.main.ScreenPointToRay(center);

		if(Input.GetButtonDown("Fire1") && !isFinish)
		{
			if(Physics.Raycast(ray, out hit, Mathf.Infinity))
			{
				if(hit.transform.tag == "Grenier")
				{
					if(hit.collider.gameObject.GetComponent<Renderer>().material.color == Color.green)
					{
						hit.collider.gameObject.GetComponent<Renderer>().material.color = Color.white;
						int lay = hit.transform.gameObject.layer - 7;

						List<GameObject> tempList = FindList(hit.transform.name);

						// Si on est rouge ou bleu
						if(tempList.Count > 0 && ((joueur == 1 && lay < 13 && lay > 6) || (joueur == 0 && lay < 7 && lay > 0 )))
						{
							PlayMoveSeed(hit.transform.name);
						}
					}
					else
					{
						if(PointeurSelect != "")
						{
							GameObject.Find(PointeurSelect).GetComponent<Renderer>().material.color = Color.white;
							hit.collider.gameObject.GetComponent<Renderer>().material.color = Color.green;
						}
						else
						{
							hit.collider.gameObject.GetComponent<Renderer>().material.color = Color.green;
						}

						PointeurSelect = hit.transform.name;
					}
				}
			}
		}
	}
}
