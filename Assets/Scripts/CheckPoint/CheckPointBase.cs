using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointBase : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public int key = 01;

    private bool checkpointActivated = false;
    private string checkpointKey = "Checkpoint Key";

    private void OnTriggerEnter(Collider other)
    {
        if(!checkpointActivated && other.transform.tag == "Player")
        {
            CheckCheckPoint();
        }
    }

    private void CheckCheckPoint()
    {
        TurnItOn();
        SaveCheckpoint();
    }

    private void TurnItOn()
    {
        meshRenderer.material.SetColor("_EmissionColor", Color.white);
    }
    private void TurnItOff()
    {
        meshRenderer.material.SetColor("_EmissionColor", Color.black);
    }

    private void SaveCheckpoint()
    {
        /*if(PlayerPrefs.GetInt(checkpointKey, 0) > key)
            PlayerPrefs.SetInt(checkpointKey, key);*/

        CheckPointManager.Instance.SaveCheckPoint(key);

        checkpointActivated = true;
    }
}
