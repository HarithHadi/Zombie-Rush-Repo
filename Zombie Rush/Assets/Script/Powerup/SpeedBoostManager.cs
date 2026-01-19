using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoostManager : MonoBehaviour
{
    public static SpeedBoostManager instance;
    private static bool isBoostActive = false;
    private static int currentBoostMultiplier = 1;
    public GameObject pickupEffect;
    private GameObject activeBoostEffect;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else 
        {
            Destroy(gameObject);
        }
    }

    public void ActiveSpeedBoost(int multiplier, float duration, Collider player) 
    {
        StopAllCoroutines();
        StartCoroutine(SpeedBoostCoroutine(multiplier, duration, player));

    }

    IEnumerator SpeedBoostCoroutine(int multiplier, float duration, Collider player) 
    {
        Debug.Log("Start speed");
        isBoostActive = true;
        currentBoostMultiplier = multiplier;
        Move[] movescripts = FindObjectsOfType<Move>();
        foreach (Move move in movescripts) 
        {
            if (move != null) 
            {
                move.setSpeed(multiplier);
            }
        }

        PlayerMovement pc = player.GetComponent<PlayerMovement>();
        if (pc != null) 
        {
            //set lane speed
        }

        //-----For Effects ------------------

        Transform playerpos = player.transform;
        Transform boostpos = playerpos.Find("BoostPos");
        Vector3 firepos = boostpos.position;
        // cool effect 
        if (activeBoostEffect == null)
        {
            AudioManager.Instance.Play(AudioManager.SoundType.Speed);
            activeBoostEffect = Instantiate(pickupEffect, boostpos.transform);
            activeBoostEffect.transform.SetParent(playerpos, true);
        }

        //-----For Effects ------------------

        yield return new WaitForSeconds(duration);

        isBoostActive = false;
        currentBoostMultiplier = 1;


        if (activeBoostEffect != null)
        {
            Destroy(activeBoostEffect);
            activeBoostEffect = null;
        }

        //Reset speed for all maps
        Move[] allMoveScripts = FindObjectsOfType<Move>();
        foreach (Move move in allMoveScripts) 
        {
            if(move != null) 
            {
                move.setSpeed(1);
            }
        }
    }
    public static int getCurrentSped() 
    {
        return isBoostActive ? currentBoostMultiplier : 1;
    }

    public bool IsBoostActive() 
    {
        return isBoostActive;
    }
}
