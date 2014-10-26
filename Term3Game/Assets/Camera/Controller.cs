using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Controller : MonoBehaviour 
{
    Player Player;

	void Start () 
	{
		GameObject PlayerGameObject = GameObject.Find ("Player");
		Player = (Player)PlayerGameObject.GetComponent (typeof(Player));
		Debug.Log("From CONTROLLLLLER????" + Player.ToString ());
	}
    void Update()
    {
    }
}
