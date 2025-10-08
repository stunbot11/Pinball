using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public MoveCam cam;
    public List<List<GameObject>> mapNodes = new List<List<GameObject>>();
    public List<GameObject> tempNode;
    public List<MapNode> allNodes;
    public GameObject blankMapNode;
    public GameObject nodeHost;
    public GameObject startNode;
    public GameObject endNode;
    public float disBetFloor = 2.5f;
    public float maxNodePosFromZero = 5;
    public int minL = 5;
    public int maxL = 10;
    public int minRoomsPerFloor = 2;
    public int maxRoomsPerFloor = 5;

    private void Start()
    {
        cam = GameObject.Find("Move Cam").GetComponent<MoveCam>();
        generateMap();
    }

    public void generateMap()
    {
        int length = Random.Range(minL, maxL);
        cam.yMax = (length * disBetFloor) - (disBetFloor * 1.75f);
        endNode.transform.position = new Vector3(endNode.transform.position.x, startNode.transform.position.y + (length * disBetFloor) + disBetFloor, startNode.transform.position.z);
        for (int i = 0; i < length; i++)
        {
            int width = Random.Range(minRoomsPerFloor, maxRoomsPerFloor);
            for (int j = 0; j <= width; j++)
            {
                
                GameObject newMapNode = Instantiate(blankMapNode, nodeHost.transform);
                newMapNode.transform.position = new Vector2((float)Mathf.Lerp(-maxNodePosFromZero, maxNodePosFromZero, (float)j / (float)width), endNode.transform.position.y - (disBetFloor * i) - disBetFloor);
                newMapNode.GetComponent<MapNode>().floor = length - i;
                newMapNode.GetComponent<MapNode>().mapGen = this;
                if (i > 0)
                    newMapNode.GetComponent<MapNode>().possibleToNodes = mapNodes[i - 1];
                else
                    newMapNode.GetComponent<MapNode>().toNodes.Add(endNode);
                tempNode.Add(newMapNode);
                allNodes.Add(newMapNode.GetComponent<MapNode>());
            }
            List<GameObject> lel = new List<GameObject>();

            for (int j = 0; j < tempNode.Count; j++)
            {
                lel.Add(tempNode[j]);
                if (i == length - 1)
                {
                    startNode.GetComponent<MapNode>().possibleToNodes.Add(tempNode[j]);
                    startNode.GetComponent<MapNode>().Start();
                }
            }
            mapNodes.Add(lel);
            tempNode.Clear();
        }
        StartCoroutine(deletasuarisrex());
    }

    bool hasFrom;
    IEnumerator deletasuarisrex()
    {
        yield return new WaitForSeconds(.001f);
        for (int i = allNodes.Count - 1; i >= 0; i--)
        {
            for (int j = 0; j < allNodes[i].fromNodes.Count; j++)
            {
                if (allNodes[i].fromNodes[j] != null)
                    hasFrom = true;
            }

            if (!hasFrom)
                Destroy(allNodes[i].gameObject);

            if (allNodes[i].fromNodes.Count == 0)
                Destroy(allNodes[i].gameObject);
            hasFrom = false;
            yield return new WaitForSeconds(.001f);
        }
    }
}
