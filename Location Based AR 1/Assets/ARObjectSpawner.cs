using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class ARObjectSpawner : MonoBehaviour
{
    public GameObject objectToSpawn; // Assign the prefab to be spawned in the inspector
    public float spawnDistance = 1.0f; // Distance in front of the camera
    public Button spawnButton; // Assign the UI Button in the inspector

    private Camera arCamera;

    void Start()
    {
        // Get the AR Camera
        arCamera = Camera.main;
        if (arCamera == null)
        {
            Debug.LogError("AR Camera not found.");
        }

        // Add listener to the button
        if (spawnButton != null)
        {
            spawnButton.onClick.AddListener(SpawnObject);
        }
        else
        {
            Debug.LogError("Spawn button not assigned.");
        }

        // Debug log to confirm initialization
        Debug.Log("ARObjectSpawner initialized.");
    }

    void SpawnObject()
    {
        // Check if objectToSpawn is assigned
        if (objectToSpawn == null)
        {
            Debug.LogError("Object to spawn is not assigned.");
            return;
        }

        // Check if arCamera is assigned
        if (arCamera == null)
        {
            Debug.LogError("AR Camera is not assigned.");
            return;
        }

        // Calculate the spawn position in front of the AR camera
        Vector3 spawnPosition = arCamera.transform.position + arCamera.transform.forward * spawnDistance;

        // Instantiate the object at the spawn position with the same rotation as the camera
        Instantiate(objectToSpawn, spawnPosition, arCamera.transform.rotation);

        // Debug log to confirm object spawning
        Debug.Log("Object spawned at position: " + spawnPosition);
    }
}