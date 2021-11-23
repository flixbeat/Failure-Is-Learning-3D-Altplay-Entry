using System;
using System.Collections;
using System.Collections.Generic;
using DigitalRuby.Tween;
using UnityEngine;

public class CameraMain : MonoBehaviour
{
    public static Action zoomOut;
    
    [SerializeField] private Player player;
    private readonly Vector3 zoomOutPoint = new Vector3(45, 69, -185);
    private Vector3 initialPos;
    private Vector3 offset;
    
    void Start()
    {
        zoomOut = ZoomOut;
        initialPos = transform.position;
        offset = initialPos - player.transform.position;
    }

    private void LateUpdate()
    {
        if(player.isActive)
            transform.position = player.transform.position + offset;
    }

    private void ZoomOut()
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = zoomOutPoint;

        ZoomOut(startPos, endPos, false);
    }

    private void ZoomOut(Vector3 startPos, Vector3 endPos, bool isSecondZoom)
    {
        print("isSecondZoom: " + isSecondZoom);
        Func<float, float> tsf = isSecondZoom ? TweenScaleFunctions.SineEaseIn : TweenScaleFunctions.SineEaseOut;
        float duration = isSecondZoom ? 0.6f : 2f;

        void UpdateZ(ITween<Vector3> t)
        {
            Vector3 newPos = t.CurrentValue;
            transform.localPosition = newPos;
        }

        void FinishZoom(ITween t)
        {
            if (isSecondZoom)
            {
                player.Unfreeze();
                return;
            }
            
            GameManager.respawn.Invoke();
        }

        gameObject.Tween($"{gameObject.GetInstanceID()}_zoom", startPos, endPos, duration, tsf, UpdateZ, FinishZoom);
    }

    public void ChangeFocus(Player player)
    {
        this.player = player;
        Vector3 startPos = Vector2.zero;
        Vector3 endPos = initialPos;
        ZoomOut(startPos, endPos, true);
    }
}