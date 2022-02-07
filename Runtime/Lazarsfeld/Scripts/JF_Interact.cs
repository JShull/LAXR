using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
namespace JFuzz.Lazarsfeld
{
    public class JF_Interact : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public UnityEvent ClickedOnSphereEvent;
        public UnityEvent SphereEnterEvent;
        public UnityEvent SphereExitEvent;
        public GameObject ItemToHide;
        
        public void OnPointerDown(PointerEventData eventData)
        {
            ClickedOnSphereEvent.Invoke();
            if (ItemToHide != null)
            {
                if (ItemToHide.activeInHierarchy)
                {
                    ItemToHide.SetActive(false);
                }
                else
                {
                    ItemToHide.SetActive(true);
                }
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            SphereEnterEvent.Invoke();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            SphereExitEvent.Invoke();
        }
        public void DebugEventInfo(string details)
        {
            Debug.LogWarning($"{ details}");
        }
    }
}
