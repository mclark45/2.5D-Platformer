using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    [SerializeField] private Text _score;

    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("UIManager is Null");
            }
            return _instance;
        }
    }

    public void Awake()
    {
        _instance = this;
    }

    public void CollectableCoins(int Coins)
    {
        _score.text = "Coins: " + Coins;
    }
}
