using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapNode : MonoBehaviour
{
    public MapGenerator mapGen;
    private LineRenderer path;
    public List<GameObject> toNodes = new();
    public List<GameObject> fromNodes = new();
    public List<GameObject> possibleToNodes;
    
    //private int[] weight = {1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 3, 3, 3, 4, 4, 5};
    //private int[] weightToAdd = {5, 11, 14, 16, 17};
    public bool lastFloor;
    public bool isCurrent;
    public int floor;

    public float quotaMult;

    public int playableScenes;
    public string scene;

    public void Start()
    {
        //112223345
        //25789
        scene = "Board " + Random.Range(1, playableScenes + 1);
        if (!lastFloor)
        {
            path = GetComponent<LineRenderer>();
            GetToNodes();
            path.positionCount = toNodes.Count * 2;
            SetPaths();
            GetComponentInChildren<TextMeshProUGUI>().text = quotaMult.ToString();
        }
    }

    public void GetToNodes()
    {
        if (floor < mapGen.mapNodes.Count) //sets toNodes
        {
            List<GameObject> test = new();
            for (int i = 0; i < possibleToNodes.Count; i++)
            {
                test.Add(possibleToNodes[i]);
            }

            //int ranNum = Random.Range(1, test.Count);
            //int ranNum = weight[Random.Range(1, weightToAdd[test.Count - 1])];
            int ranNum = Random.Range(1, 3);
            if (gameObject.name == "Start")
                ranNum = test.Count;
            for (int i = 0; i < ranNum; i++)
            {
                int nodeToAdd = Random.Range(0, test.Count);
                toNodes.Add(test[nodeToAdd]);
                test.RemoveAt(nodeToAdd);
            }
        }
    }

    public void SetPaths()
    {
        for (int i = 0; i < toNodes.Count; i++) //sets paths
        {
            path.SetPosition(i * 2, transform.position);
            path.SetPosition((i * 2) + 1, toNodes[i].transform.position);
            toNodes[i].GetComponent<MapNode>().fromNodes.Add(this.gameObject);
            toNodes[i].gameObject.SetActive(true);
        }
    }

    public void loadScene()
    {
        PlayerStats stats = GameObject.Find("PlayerStats").GetComponent<PlayerStats>();
        stats.floor = floor;
        stats.quotaMult = quotaMult;
        SceneManager.LoadScene(scene);
    }
}