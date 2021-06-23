using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopOfLadder : MonoBehaviour
{
    private Ladder _ladder;
    void Start()
    {
        _ladder = GameObject.FindGameObjectWithTag("Ladder").GetComponent<Ladder>();
        
        if (_ladder == null)
        {
            Debug.LogError("Ladder Script is Null");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _ladder.TurnLadderOff(false);
        }
    }
}
