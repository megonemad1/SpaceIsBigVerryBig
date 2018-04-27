using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageHandler {

    IDamageHandler DealDamage(GameObject sender, float damage);
}
