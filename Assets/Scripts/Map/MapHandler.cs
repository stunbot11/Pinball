using System.Collections.Generic;
using UnityEngine;

public class MapHandler : MonoBehaviour
{
    public int floors;
    public GameObject nodeHost;
    public GameObject currentNode;
    public List<GameObject> toNodes;
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void hideMap(GameObject newCurrent)
    {
        currentNode = newCurrent;
        nodeHost.SetActive(false);
    }
}
