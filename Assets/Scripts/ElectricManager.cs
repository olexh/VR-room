using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;

public class ElectricManager : MonoBehaviour
{
    [Tooltip("The text that displays debug information.")]
    public TextMeshProUGUI debugText;
    private Dictionary<string, GameObject> connections = new Dictionary<string, GameObject>();

    [Tooltip("The keys of the schema.")]
    [SerializeField] private List<GameObject> keys = new List<GameObject>();

    [Tooltip("The values of the schema.")]
    [SerializeField] private List<GameObject> values = new List<GameObject>();
    public void Connect(SelectEnterEventArgs args)
    {
        GameObject point = args.interactableObject.transform.gameObject;
        GameObject interactor = args.interactorObject.transform.gameObject;

        GameObject wire = point.GetComponent<WirePointManager>().wire;

        connections[interactor.name] = wire;

        // UpdateDebugText();
    }

    public void Disconnect(SelectExitEventArgs args)
    {
        GameObject point = args.interactableObject.transform.gameObject;
        GameObject interactor = args.interactorObject.transform.gameObject;

        GameObject wire = point.GetComponent<WirePointManager>().wire;

        connections.Remove(interactor.name);

        // UpdateDebugText();
    }

    private void UpdateDebugText()
    {
        string text = "Connections:\n";
        foreach (var kvp in connections)
        {
            string pointName = kvp.Value != null ? kvp.Value.name : "null";
            text += $"{kvp.Key}: [{pointName}]\n";
        }

        if (IsConnectionsCorrect())
        {
            text = "Connections are correct.";
        }

        debugText.text = text;
    }

    /// <summary>
    /// Checks if all connections are correctly made according to the schema.
    /// </summary>
    /// <returns>True if all connections are correctly made, false otherwise.</returns>
    public bool IsConnectionsCorrect()
    {
        // Check if we have all required connections
        if (connections.Count < keys.Count)
        {
            return false; // Not all connections are made
        }

        // Create a dictionary to track which wires are connected to which keys
        Dictionary<string, List<string>> wireConnections = new Dictionary<string, List<string>>();

        // Collect all connections by wire
        foreach (var kvp in connections)
        {
            string interactorName = kvp.Key;
            GameObject wire = kvp.Value;

            if (wire == null) continue;

            if (!wireConnections.ContainsKey(wire.name))
            {
                wireConnections[wire.name] = new List<string>();
            }

            wireConnections[wire.name].Add(interactorName);
        }

        // For each key-value pair in our schema, check if they're connected by the same wire
        for (int i = 0; i < keys.Count && i < values.Count; i++)
        {
            string keyName = keys[i].name;
            string valueName = values[i].name;

            // Check if both key and value exist in any wire connection
            bool found = false;

            foreach (var wirePair in wireConnections)
            {
                if (wirePair.Value.Contains(keyName) && wirePair.Value.Contains(valueName))
                {
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                return false; // This key-value pair is not properly connected
            }
        }

        return true; // All connections are correct
    }
}
