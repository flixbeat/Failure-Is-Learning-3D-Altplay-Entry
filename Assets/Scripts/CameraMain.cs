using System;
using DigitalRuby.Tween;
using UnityEngine;

public class CameraMain : MonoBehaviour
{
    public static Action reset;
    
    [SerializeField] private Player player;
    
    private Vector3 initialPos;
    private Vector3 offset;
    
    void Start()
    {
        reset = ResetCam;
        initialPos = transform.position;
        offset = initialPos - player.transform.position;
    }

    private void LateUpdate()
    {
        if(player.IsActive)
            transform.position = player.transform.position + offset;
    }

    private void ResetCam()
    {
        void UpdatePos(ITween<Vector3> t)
        {
            // catches missing reference exception after changing scene
            try
            {
                Vector3 newPos = t.CurrentValue;
                transform.localPosition = newPos;
            }
            catch (Exception e) { }
        }

        void Finish(ITween t)
        {
            if (MenuBar.lifeCount == -1)
                return;
                
            player.Unfreeze();
            TouchArea.setActive.Invoke(true);
            GameManager.resumeBGM.Invoke();
        }

        Vector3 startPos = transform.position;
        Vector3 endPos = initialPos;
        
        gameObject.Tween($"{gameObject.GetInstanceID()}_reset", startPos, endPos, 1f, TweenScaleFunctions.CubicEaseInOut, UpdatePos, Finish);
        
        TouchArea.setActive.Invoke(false);
        GameManager.stopBGM.Invoke();
        GameManager.respawn.Invoke();
    }

    public void ChangeFocus(Player player)
    {
        this.player = player;
    }
}