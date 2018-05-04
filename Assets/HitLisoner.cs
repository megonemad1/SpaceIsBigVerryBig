using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class HitLisoner:MonoBehaviour
{
    public virtual IEnumerator hit( Collider2D collision)
    {
        yield break;
    }
}