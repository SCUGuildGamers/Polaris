using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShield : MonoBehaviour
{
    public Shield shield;
    public int ShieldHealth = 3;
    private int _currentShield;

    void Start()
    {
        _currentShield = 0;
    }

    void Update()
    {
        if (!PauseMenu.GameIsPaused)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                shield.gameObject.SetActive(true);
                _currentShield = 3;
            }

            if (_currentShield <= 0 && shield.gameObject.activeSelf)
                shield.gameObject.SetActive(false); 
        }
    }

    public void ReduceShield()
    {
        _currentShield--;
    }
}
