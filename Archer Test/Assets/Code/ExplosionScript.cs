using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour {

    public GameObject[] healthOrbs;
    bool[] started;

    // Use this for initialization
    void Start ()
    {
        healthOrbs = new GameObject[transform.childCount];
        started = new bool[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            float randNum = (int)Random.Range(1, 6);
            randNum *= 0.1667f;

            healthOrbs[i] = transform.GetChild(i).gameObject;
            started[i] = false;
            healthOrbs[i].GetComponent<Animator>().SetFloat("offset", randNum);

            healthOrbs[i].SetActive(false);
        }

        EventManager.AddListener("GAMEOVER", PlayExplosion);
    }

    // Update is called once per frame
    void Update ()
    {
        //GetComponent<Animator>().StartPlayback();
    }

    public void PlayExplosion()
    {
        for (int i = 0; i < transform.childCount; i++)
        {


            healthOrbs[i].SetActive(true);

            if (started[i] == false)
            {
                float randNum = (int)Random.Range(1, 6);
                randNum *= 0.1667f;
                started[i] = true;
                healthOrbs[i].GetComponent<Animator>().SetFloat("offset", randNum);
            }
        }
    }
}
