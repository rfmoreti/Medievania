using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEyeController : Enemy
{
    public override void JumpedOn()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        base.JumpedOn();
    }
}
