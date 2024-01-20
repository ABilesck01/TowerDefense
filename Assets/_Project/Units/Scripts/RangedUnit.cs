using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedUnit : MoveUnit
{
    [Header("Ranged")]
    [SerializeField] private Transform shootPosition;
    [SerializeField] private Bullet bulletPrefab;

    public override void HandleAttack(BaseUnit otherUnit)
    {
        var bulletInstance = Instantiate(bulletPrefab, shootPosition.position, shootPosition.rotation);
        bulletInstance.Damage = stats.GetValue(StatEnum.Damage);
    }
}
