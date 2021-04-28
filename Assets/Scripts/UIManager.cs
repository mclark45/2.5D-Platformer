using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    [SerializeField]
    private Text _score;

    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("UIManger is Null");
            }
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    public void CoinsCollectedText(int coins)
    {
        _score.text = "Coins: " + coins;
    }
}
