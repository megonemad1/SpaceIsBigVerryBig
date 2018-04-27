using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeppon{


    void StartFire(Action<GameObject> notify);
    int GetAmmo();

}
