using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject thingToShow;

    public void OnPointerEnter(PointerEventData eventData)
    {
        thingToShow.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        thingToShow.SetActive(false);
    }
}
