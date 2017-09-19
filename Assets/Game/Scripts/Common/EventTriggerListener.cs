using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

    public class EventTriggerListener : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
    {
        public delegate void VoidDelegate(GameObject go);
        public VoidDelegate onClick;
        public VoidDelegate onDown;
        public VoidDelegate onUp;

        static public EventTriggerListener Get(GameObject go)
        {
            EventTriggerListener listener = go.GetComponent<EventTriggerListener>();
            if (listener == null) listener = go.AddComponent<EventTriggerListener>();
            return listener;
        }

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            if (onClick != null) onClick(gameObject);
        }

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            if (onDown != null) onDown(gameObject);
        }

        public virtual void OnPointerUp(PointerEventData eventData)
        {
            if (onUp != null) onUp(gameObject);
        }
    }