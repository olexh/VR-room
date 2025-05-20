using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstTask : MonoBehaviour
{
    [SerializeField] private ElectricManager electricManager;
    [SerializeField] private GameObject lampSpotlight;
    [SerializeField] private SwitchManager switchManager;
    public void ToggleSwitch()
    {
        if (electricManager.IsConnectionsCorrect())
        {
            switchManager.ToggleSwitch();
            lampSpotlight.SetActive(switchManager.isOn);
        }
        else
        {
            lampSpotlight.SetActive(false);
        }
    }
}
