using UnityEngine;
using UnityEngine.UI;

public class MoveCam : MonoBehaviour
{
    private GameObject cam;
    private Scrollbar bar;
    public float yMin;
    public float yMax;

    private void Start()
    {
        cam = GameObject.Find("Main Camera");
        bar = GetComponent<Scrollbar>();
    }

    public void updateCamPos()
    {
        cam.transform.position = new Vector3(cam.transform.position.x, Mathf.Lerp(yMin, yMax, bar.value), cam.transform.position.z);
    }
}
