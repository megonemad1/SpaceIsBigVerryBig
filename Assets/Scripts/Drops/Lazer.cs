using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : MonoBehaviour, IWeppon, IDrop
{
    Action<GameObject> notify = null;
    public Sprite LazerSprite;
    LineRenderer lr;
    public GunState gun_state = GunState.ready;
    public float TotalFireDuration, TotalGunCoolDownTime, WepponDist, WepponDamage,ColorVarence;
    public float CurrentFireDuration, CurrentGunCoolDownTime;
    public Color StartColor, EndColor;

    public enum GunState
    {
        ready = 0,
        fire = 1,
        cooldown = 2

    }
    public int ammo = 10;
    // Use this for initialization
    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }
    void FixedUpdate()
    {
        switch (gun_state)
        {
            case GunState.ready:
                break;

            case GunState.fire:
                float[] hsv = new float[3];
                Color.RGBToHSV(StartColor, out hsv[0], out hsv[1], out hsv[2]);
                lr.startColor = UnityEngine.Random.ColorHSV(hsv[0] - ColorVarence, hsv[0] + ColorVarence);
                Color.RGBToHSV(EndColor, out hsv[0], out hsv[1], out hsv[2]);
                lr.endColor = UnityEngine.Random.ColorHSV(hsv[0] - ColorVarence, hsv[0] + ColorVarence);
                lr.SetPosition(0, this.transform.position);
                lr.SetPosition(1, this.transform.position + this.transform.up * WepponDist);
                this.transform.parent.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
                RaycastHit2D hit = Physics2D.Raycast(this.transform.position, this.transform.up, WepponDist);
                this.transform.parent.gameObject.layer = LayerMask.NameToLayer("Default");
                if (hit)
                {

                    lr.SetPosition(1, this.transform.position + this.transform.up * hit.distance); 
                    if (notify != null)
                    {
                        notify.Invoke(hit.transform.gameObject);
                        IDamageHandler d = hit.transform.gameObject.GetComponent<IDamageHandler>();
                        if (d != null)
                        {
                            d.DealDamage(this.gameObject, WepponDamage);
                        }
                    }
                }
                CurrentFireDuration -= Time.deltaTime;
                if (CurrentFireDuration < 0)
                {
                    gun_state = GunState.cooldown;
                    CurrentGunCoolDownTime = TotalGunCoolDownTime;
                }
                break;

            case GunState.cooldown:
                CurrentGunCoolDownTime -= Time.deltaTime;
                if (CurrentGunCoolDownTime < 0)
                {
                    gun_state = GunState.ready;
                }
                break;
        }
    }
    // Update is called once per frame
    void Update()
    {
        switch (gun_state)
        {
            case GunState.ready:
                break;

            case GunState.fire:
                lr.enabled = true;
                break;

            case GunState.cooldown:
                lr.enabled = false;
                break;
        }
    }

    public void StartFire(Action<GameObject> notify)
    {
        if (gun_state == GunState.ready && GetAmmo() > 0)
        {
            this.notify = notify;
            this.gun_state = GunState.fire;
            CurrentFireDuration = TotalFireDuration;
            this.ammo--;
        }
    }

    public int GetAmmo()
    {
        return ammo;
    }

    public Sprite GetIcon()
    {
        return LazerSprite;
    }

    public GameObject GetDroppedItem()
    {
        return this.gameObject;
    }
}
