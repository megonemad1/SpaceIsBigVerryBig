using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitLisoner:MonoBehaviour
{
    public virtual IEnumerator hit( Collider2D collision, BulletHandeler bullet)
    {
        yield break;
    }
}