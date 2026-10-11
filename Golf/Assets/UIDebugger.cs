using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class UIDebugger : MonoBehaviour
{
    void Update()
    {
        // Detects left mouse click (or mobile touch)
        if (Input.GetMouseButtonDown(0))
        {
            PointerEventData pointerData = new PointerEventData(EventSystem.current)
            {
                position = Input.mousePosition
            };

            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, results);

            if (results.Count > 0)
            {
                // The first item in the list is the topmost UI element hit
                Debug.Log($"<color=green>Clicked UI Element:</color> <b>{results[0].gameObject.name}</b> (Path: {GetGameObjectPath(results[0].gameObject)})", results[0].gameObject);
                
                // Optional: Print all elements stacked beneath it if overlapping
                if (results.Count > 1)
                {
                    string stacked = "Elements underneath: ";
                    for (int i = 1; i < results.Count; i++)
                    {
                        stacked += results[i].gameObject.name + ", ";
                    }
                    Debug.Log(stacked);
                }
            }
            else
            {
                Debug.Log("<color=red>Clicked empty space:</color> No UI Raycast Target found under mouse.");
            }
        }
    }

    // Helper to print the full hierarchy path so you know exactly which panel/child it is
    string GetGameObjectPath(GameObject obj)
    {
        string path = obj.name;
        Transform parent = obj.transform.parent;
        while (parent != null)
        {
            path = parent.name + "/" + path;
            parent = parent.parent;
        }
        return path;
    }
}