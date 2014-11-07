using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Cop : MonoBehaviour 
{
    int SpaceCount = 0;

    const int FAT_PRISONER = 0;
    const int SKINY_PRISONER = 1;
    const int MIDGET_PRISONER = 2;

    List<GameObject> CriminalsList;

    public GameObject FatCriminal;
    public GameObject SkinnyCriminal;
    public GameObject MidgetCriminal;

	void Start () 
    {
        CriminalsList = new List<GameObject>();

       
        for(int i = 0; i < 5; i++) // added 5 random prisoners
        {
            GameObject Prisoner = null;
            int PrisonerType = (Random.Range(0, 3));
            switch(PrisonerType)
            {
                case FAT_PRISONER:
                    //Debug.Log("Fat");
                    Prisoner = (GameObject)Instantiate(FatCriminal, new Vector3(3, 20, 0), Quaternion.identity);
                    break;
                case SKINY_PRISONER:
                    //Debug.Log("Skinny");
                    Prisoner = (GameObject)Instantiate(SkinnyCriminal, new Vector3(3, 20, 0), Quaternion.identity);
                    break;
                case MIDGET_PRISONER:
                    //Debug.Log("Midget");
                    Prisoner = (GameObject)Instantiate(MidgetCriminal, new Vector3(3, 20, 0), Quaternion.identity);
                    break;
            }
            AddPrisoner(Prisoner);
        }
	}
    void AddPrisoner(GameObject Prisoner)
    {
        if(Prisoner != null)
        {
            CriminalsList.Add(Prisoner);
        }
    }
	void Update () 
    {
	    if(Input.GetKeyDown("space")) // Simulates a fire
        {
            if(CriminalsList.Count > 0)
            {
                GameObject CriminalToShoot = CriminalsList[0];
                CriminalsList.Remove(CriminalToShoot);
                Debug.Log("Prisoner Fired");
                Destroy(CriminalToShoot);
            }
        }
	}
}
