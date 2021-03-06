﻿using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class Ads : MonoBehaviour {

    [SerializeField] string IDAdds;
    BonusAdLayout b = new BonusAdLayout ();
    public Button button;
    Animator rand;
    Animator bonus;
    GameObject g;
    GameObject gR;
    int a;

    void Awake () {
        b.Initialize ();
        gR = GameObject.Find ("Canvas - HUD/GameOverHUD/BONUSLAYOUT/Bonus");
        g = GameObject.Find ("Canvas - HUD/GameOverHUD/BONUSLAYOUT/BonusPowerUp");
        rand = g.GetComponent<Animator> ();
        bonus = gR.GetComponent<Animator> ();
        rand.enabled = true;
        bonus.enabled = true;
        a = Random.Range (0, 3);
    }

    void Update () {
        if (!SetNuts.seenAd) {
            button.interactable = true;
        }
        else {
            button.interactable = false;
        }
    }

    public void ShowAd () {
        if (!SetNuts.seenAd) {
            rand.SetBool ("canGo", false);
            bonus.SetBool ("GoOn", false);
            ShowOptions options = new ShowOptions ();
            options.resultCallback = AdCallbackHandler;
            a = Random.Range (0, 3);
            if (Advertisement.IsReady ("rewardedVideoZone")) {
                Advertisement.Show ("rewardedVideoZone", options);
                SetNuts.seenAd = true;
            }
        }
        
    }

    void AdCallbackHandler (ShowResult result) {

        b.CallBonus ();
       

        switch (result) {
            case ShowResult.Finished:
               
                g.GetComponent<Image> ().enabled = true;
                gR.GetComponent<Image> ().enabled = true;
                print ("HEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEY");
                StartCoroutine (WaitAnimation ());
               
            break;

        }
    }


    IEnumerator WaitAnimation () {

       
        switch (a) {
            case 0:
                 SetNuts.quantHammer++;

            break;

            case 1:
                SetNuts.quantClock++;

            break;

            case 2:
                SetNuts.quantSpdd++;
            break;

        }
        rand.SetInteger ("powerUpPos", a);
        print ("YAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA" + a);
        bonus.SetBool ("GoOn", true);
        rand.SetBool ("canGo", true);

        yield return new WaitForSeconds (0.1f);

    }


}
