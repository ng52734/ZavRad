// See https://youtu.be/gpaq5bAjya8  for accompanying tutorial and usage!

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARTrackedImageManager))]
public class PlaceTrackedImages : MonoBehaviour
{
    // Reference to AR tracked image manager component
    private ARTrackedImageManager _trackedImagesManager;

    // List of prefabs to instantiate - these should be named the same
    // as their corresponding 2D images in the reference image library 
    public GameObject[] ArPrefabs;

    // Keep dictionary array of created prefabs
    private readonly Dictionary<string, GameObject> _instantiatedPrefabs = new Dictionary<string, GameObject>();

    // Dict of canvases
    private Dictionary<string, GameObject> canvasDictionary;

    void Awake()
    {
        // Cache a reference to the Tracked Image Manager component
        _trackedImagesManager = GetComponent<ARTrackedImageManager>();

        // prefab canvases
        canvasDictionary = new Dictionary<string, GameObject>(){
            {"Video", GameObject.FindGameObjectWithTag("ShapeCanvas") },
            {"Slike", GameObject.FindGameObjectWithTag("ImagesCanvas") }
        };

        foreach (var canvasEntry in canvasDictionary)
        {
            var canvasObject = canvasEntry.Value;
            canvasObject.SetActive(false);
        }

    }

    void OnEnable()
    {
        // Attach event handler when tracked images change
        _trackedImagesManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnDisable()
    {
        // Remove event handler
        _trackedImagesManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    // Event Handler
    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        // Loop through all new tracked images that have been detected
        foreach (var trackedImage in eventArgs.added)
        {
            // Get the name of the reference image
            var imageName = trackedImage.referenceImage.name;
            // Now loop over the array of prefabs
            foreach (var curPrefab in ArPrefabs)
            {
                // Check whether this prefab matches the tracked image name, and that
                // the prefab hasn't already been created
                if (string.Compare(curPrefab.name, imageName, StringComparison.OrdinalIgnoreCase) == 0
                    && !_instantiatedPrefabs.ContainsKey(imageName))
                {
                    // Instantiate the prefab, parenting it to the ARTrackedImage
                    var newPrefab = Instantiate(curPrefab, trackedImage.transform);
                    // Add the created prefab to our array
                    _instantiatedPrefabs[imageName] = newPrefab;

                    // Enable the canvas associated with the tracked image
                    if (canvasDictionary.ContainsKey(imageName))
                    {
                        var canvasObject = canvasDictionary[imageName];
                        canvasObject.SetActive(true);
                    }
                }
            }
        }

        // For all prefabs that have been created so far, set them active or not depending
        // on whether their corresponding image is currently being tracked
        foreach (var trackedImage in eventArgs.updated)
        {
            var imageName = trackedImage.referenceImage.name;
            if (_instantiatedPrefabs.ContainsKey(imageName))
            {
                _instantiatedPrefabs[imageName].SetActive(trackedImage.trackingState == TrackingState.Tracking);

                // Enable or disable the canvas associated with the tracked image based on tracking state
                if (canvasDictionary.ContainsKey(imageName))
                {
                    var canvasObject = canvasDictionary[imageName];
                    canvasObject.SetActive(trackedImage.trackingState == TrackingState.Tracking);
                }
            }
        }

        // If the AR subsystem has given up looking for a tracked image
        foreach (var trackedImage in eventArgs.removed)
        {
            var imageName = trackedImage.referenceImage.name;
            if (_instantiatedPrefabs.ContainsKey(imageName))
            {
                // Destroy its prefab
                Destroy(_instantiatedPrefabs[imageName]);
                // Also remove the instance from our array
                _instantiatedPrefabs.Remove(imageName);

                // Disable the canvas associated with the tracked image
                if (canvasDictionary.ContainsKey(imageName))
                {
                    var canvasObject = canvasDictionary[imageName];
                    canvasObject.SetActive(false);
                }
            }
        }
    }

}