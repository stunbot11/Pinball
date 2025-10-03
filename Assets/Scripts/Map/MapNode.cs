using System.Collections.Generic;
using UnityEngine;

public class MapNode : MonoBehaviour
{
    private LineRenderer path;
    public List<GameObject> toNodes;
    public List<GameObject> fromNodes;

    public void Start()
    {
        path = GetComponent<LineRenderer>();
        path.positionCount = toNodes.Count * 2;

        for (int i = 0; i < toNodes.Count; i++)
        {
            path.SetPosition(i * 2, transform.position);
            path.SetPosition((i * 2) + 1, toNodes[i].transform.position);
            toNodes[i].GetComponent<MapNode>().fromNodes.Add(this.gameObject);
            toNodes[i].gameObject.SetActive(true);
        }
    }
}
