using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;

public class MapNode : MonoBehaviour
{
    public MapGenerator mapGen;
    private LineRenderer path;
    public List<GameObject> toNodes = new();
    public List<GameObject> fromNodes = new();
    public List<GameObject> possibleToNodes;
    public List<GameObject> test = new();
    public bool lastFloor;
    public int floor;

    public void Start()
    {
        if (!lastFloor)
        {
            path = GetComponent<LineRenderer>();

            if (floor < mapGen.mapNodes.Count)
            {
                for (int i = 0; i < possibleToNodes.Count; i++)
                {
                    test.Add(possibleToNodes[i]);
                }

                int ranNum = Random.Range(1, test.Count);
                print(ranNum);
                for (int i = 0; i < ranNum; i++)
                {
                    int nodeToAdd = Random.Range(0, test.Count);
                    toNodes.Add(test[nodeToAdd]);
                    test.RemoveAt(nodeToAdd);
                }
            }

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
}