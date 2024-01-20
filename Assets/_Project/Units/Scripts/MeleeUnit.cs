using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeUnit : MoveUnit
{
    public override void HandleAttack(BaseUnit otherUnit)
    {
        otherUnit.TakeDamage(stats.GetValue(StatEnum.Damage));
    }
}
