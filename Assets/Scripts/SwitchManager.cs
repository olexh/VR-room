using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;

public class SwitchManager : MonoBehaviour
{
    public bool isOn = false;
    [SerializeField] private GameObject switchRotator;
    [SerializeField] private GameObject switchButton;

    [Tooltip("Event triggered when switch is turned on")]
    public UnityEvent onSwitchOn;

    [Tooltip("Event triggered when switch is turned off")]
    public UnityEvent onSwitchOff;

    public void ToggleSwitch()
    {
        isOn = !isOn;

        RotateSwitch();

        // Invoke the appropriate event based on the switch state
        if (isOn)
        {
            onSwitchOn?.Invoke();
        }
        else
        {
            onSwitchOff?.Invoke();
        }
    }

    public void RotateSwitch()
    {
        if (isOn)
        {
            switchRotator.transform.Rotate(-45, 0, 0);
        }
        else
        {
            switchRotator.transform.Rotate(45, 0, 0);
        }
    }
}