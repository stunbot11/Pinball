using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MapGenerator : MonoBehaviour
{
    public MoveCam cam;
    public MapHandler mapHandler;

    public List<List<GameObject>> mapNodes = new List<List<GameObject>>();
    public List<GameObject> tempNode;
    public List<MapNode> allNodes;
    public GameObject mapHolder;
    public GameObject blankMapNode;
    public GameObject nodeHost;
    public GameObject startNode;
    public GameObject endNode;
    public GameObject currentNode;
    public float disBetFloor = 2.5f;
    public float maxNodePosFromZero = 5;
    public int minL = 5;
    public int maxL = 10;
    public int minRoomsPerFloor = 2;
    public int maxRoomsPerFloor = 5;

    public List<GameObject> currentToNodes;

    private void Start()
    {
        cam = GameObject.Find("Move Cam").GetComponent<MoveCam>();
        if (GameObject.Find("Map Holder(Clone)") == null)
            generateMap();
        else
        {
            mapHandler = GameObject.Find("Map Holder(Clone)").GetComponent<MapHandler>();
            mapHandler.nodeHost.SetActive(true);
            cam.yMax = (mapHandler.floors * disBetFloor) - (disBetFloor * 1.75f);
            currentNode = mapHandler.currentNode;
            currentToNodes = mapHandler.toNodes;
            updateNodes(currentNode);
            doneGenerating = true;
        }
    }

    public void generateMap()
    {
        GameObject newHost = Instantiate(mapHolder);
        startNode = GameObject.Find("Start");
        startNode.GetComponent<MapNode>().mapGen = this;
        endNode = GameObject.Find("End");
        endNode.GetComponent<MapNode>().mapGen = this;
        nodeHost = GameObject.Find("NodeHost");

        mapHandler = newHost.GetComponent<MapHandler>();
        mapHandler.nodeHost = nodeHost;


        int length = UnityEngine.Random.Range(minL, maxL);
        newHost.GetComponent<MapHandler>().floors = length;
        cam.yMax = (length * disBetFloor) - (disBetFloor * 1.75f);
        endNode.transform.position = new Vector3(endNode.transform.position.x, startNode.transform.position.y + (length * disBetFloor) + disBetFloor, startNode.transform.position.z);
        for (int i = 0; i < length; i++)
        {
            int width = UnityEngine.Random.Range(minRoomsPerFloor, maxRoomsPerFloor);
            for (int j = 0; j <= width; j++)
            {
                GameObject newMapNode = Instantiate(blankMapNode, nodeHost.transform);
                newMapNode.transform.position = new Vector2((float)Mathf.Lerp(-maxNodePosFromZero, maxNodePosFromZero, (float)j / (float)width), endNode.transform.position.y - (disBetFloor * i) - disBetFloor);
                MapNode newNode = newMapNode.GetComponent<MapNode>();
                newNode.floor = length - i;
                newNode.mapGen = this;
                newNode.quotaMult = (float)Math.Round(UnityEngine.Random.Range(.75f, 2f), 2);
                
                newMapNode.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => updateNodes(newMapNode));
                newMapNode.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => mapHandler.hideMap(newMapNode));
                if (i > 0)
                    newNode.possibleToNodes = mapNodes[i - 1];
                else
                    newNode.toNodes.Add(endNode);
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
            currentNode = startNode;
            mapNodes.Add(lel);
            tempNode.Clear();
        }
        updateNodes(startNode);
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
        doneGenerating = true;
    }

    
    public bool doneGenerating;
    public bool tMinus;
    public Color baseColor;
    public Color currentColor;
    public Color toColor;
    public float timeToChange;
    public float time;

    private void Update()
    {
        if (doneGenerating)
        {
            currentNode.GetComponent<UnityEngine.UI.Image>().color = new Color(Mathf.Lerp(baseColor.r, currentColor.r, time / timeToChange),
                Mathf.Lerp(baseColor.g, currentColor.g, time / timeToChange), Mathf.Lerp(baseColor.b, currentColor.b, time / timeToChange));
            time += tMinus ? -Time.deltaTime : Time.deltaTime;
            if (time > timeToChange)
                tMinus = true;
            else if (time < 0)
                tMinus = false;

            for (int i = 0; i < currentToNodes.Count; i++)
            {
                currentToNodes[i].GetComponent<UnityEngine.UI.Image>().color = new Color(Mathf.Lerp(baseColor.r, toColor.r, time / timeToChange),
                Mathf.Lerp(baseColor.g, toColor.g, time / timeToChange), Mathf.Lerp(baseColor.b, toColor.b, time / timeToChange));
            }
        }
    }

    public void updateNodes(GameObject newCurrent)
    {
        currentNode.GetComponent<UnityEngine.UI.Image>().color = baseColor;
        for (int i = 0; i < currentToNodes.Count; i++)
        {
            currentToNodes[i].GetComponent<UnityEngine.UI.Image>().color = baseColor;
            currentToNodes[i].GetComponent<UnityEngine.UI.Button>().interactable = false;
        }
            
        currentToNodes.Clear();
        currentNode = newCurrent;
        for (int i = 0; i < currentNode.GetComponent<MapNode>().toNodes.Count; i++)
        {
            currentToNodes.Add(currentNode.GetComponent<MapNode>().toNodes[i]);
            currentToNodes[i].GetComponent<UnityEngine.UI.Button>().interactable = true;
        }
        mapHandler.toNodes = currentToNodes;
    }
}
