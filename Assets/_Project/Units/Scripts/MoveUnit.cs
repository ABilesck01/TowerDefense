using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditorInternal.VersionControl.ListControl;

public abstract class MoveUnit : BaseUnit
{
    [SerializeField] private Vector2Int moveDirection = new Vector2Int(1, 0);

    public Vector2Int MoveDirection
    {
        get => moveDirection;
        set
        {
            moveDirection = value;
            currentMoveDirection = value;
        }
    }

    protected override void HandleStates()
    {
        if (lifePoints <= 0 && currentState != UnitState.dead)
        {
            currentState = UnitState.dead;
            OnStateChanged?.Invoke(currentState);
            currentMoveDirection = Vector2Int.zero;
            moveDirection = Vector2Int.zero;
            GetComponent<Collider2D>().enabled = false;
            GetComponentInChildren<SpriteRenderer>().sortingOrder = -1;
            Destroy(gameObject, 2f);
            return;
        }


        RaycastHit2D info = Physics2D.Raycast(unitView.viewPoint.position, t.right, unitView.viewDistance, unitView.viewLayer);

        if (!info)
        {
            if (!hasAttacked)
                currentState = UnitState.run;
        }
        else if (info.transform.TryGetComponent<BaseUnit>(out BaseUnit otherUnit))
        {
            if (!hasAttacked && Time.time >= timeBtwAttacks)
            {
                currentState = UnitState.attacking;
                hasAttacked = true;
                timeBtwAttacks = Time.time + 1f / stats.GetValue(StatEnum.AttackRate);
                HandleAttack(otherUnit);
                Invoke(nameof(ResetAttacks), freezeTimeInAttack);
            }
        }
        else
        {
            currentState = UnitState.idle;
        }

        if (currentState != lastState)
        {
            Debug.Log(currentState);
            lastState = currentState;
            OnStateChanged?.Invoke(currentState);
        }
    }

    private void HandleMovement()
    {
        if (currentState == UnitState.run)
        {
            currentMoveDirection = moveDirection;
        }
        else
        {
            currentMoveDirection = Vector2Int.zero;
        }
    }

    protected override void Update()
    {
        if (currentState == UnitState.dead) return;

        HandleStates();
        HandleMovement();
    }

    //private void Update()
    //{
    //    if (currentState == UnitState.dead) return;

    //    HandleStates();
    //    HandleMovement();
    //}

    private void FixedUpdate()
    {
        if (currentState == UnitState.dead)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        rb.velocity = new Vector2(currentMoveDirection.x * stats.GetValue(StatEnum.MoveSpeed), rb.velocity.y);
    }
}
