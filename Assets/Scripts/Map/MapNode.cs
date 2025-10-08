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
    private int[] weight = {1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 3, 3, 3, 4, 4, 5};
    private int[] weightToAdd = {5, 11, 14, 16, 17};
    public bool lastFloor;
    public int floor;

    public void Start()
    {
        //112223345
        //25789
        if (!lastFloor)
        {
            path = GetComponent<LineRenderer>();

            if (floor < mapGen.mapNodes.Count) //sets toNodes
            {
                for (int i = 0; i < possibleToNodes.Count; i++)
                {
                    test.Add(possibleToNodes[i]);
                }

                //int ranNum = Random.Range(1, test.Count);
                int ranNum = weight[Random.Range(1, weightToAdd[test.Count - 1])];
                if (gameObject.name == "Start")
                    ranNum = test.Count;
                for (int i = 0; i < ranNum; i++)
                {
                    int nodeToAdd = Random.Range(0, test.Count);
                    toNodes.Add(test[nodeToAdd]);
                    test.RemoveAt(nodeToAdd);
                }
            }

            path.positionCount = toNodes.Count * 2;
            for (int i = 0; i < toNodes.Count; i++) //sets paths
            {
                path.SetPosition(i * 2, transform.position);
                path.SetPosition((i * 2) + 1, toNodes[i].transform.position);
                toNodes[i].GetComponent<MapNode>().fromNodes.Add(this.gameObject);
                toNodes[i].gameObject.SetActive(true);
            }
        }
    }
}

/*
        int maxPyNum = 40;
        int currentNum = 1;
        int prevNum = 0;

        for (int i = 0; i < maxPyNum; i++) {
            int tempNum = currentNum;
            currentNum += prevNum;
            prevNum = tempNum;
            System.out.println(i + " pyNum = " + currentNum);
        }
 */