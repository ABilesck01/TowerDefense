using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class BaseUnit : MonoBehaviour
{
    [System.Serializable]
    public struct UnitView
    {
        public Transform viewPoint;
        public float viewDistance;
        public LayerMask viewLayer;
    }

    public enum UnitState
    {
        idle,
        run,
        attacking,
        dead
    }


    [SerializeField] protected StatSO stats;
    [Space]
    [SerializeField] protected UnitView unitView;
    [SerializeField] protected float freezeTimeInAttack = 0.9f;
    [Space]
    public UnityEvent<UnitState> OnStateChanged;

    protected Transform t;
    protected Rigidbody2D rb;
    protected UnitState currentState;
    protected UnitState lastState;
    protected Vector2Int currentMoveDirection = Vector2Int.zero;
    protected bool hasAttacked = false;
    protected float timeBtwAttacks = 0;
    protected float lifePoints;

    public float GetLifePoints() => lifePoints;

    public StatSO GetStat() => stats;

    private void Awake()
    {
        t = transform;
        rb = GetComponent<Rigidbody2D>();
        lifePoints = stats.GetValue(StatEnum.LifePoints);
    }

    protected virtual void Update()
    {
        if (currentState == UnitState.dead) return;

        HandleStates();
        //HandleMovement();
    }

    protected virtual void HandleStates()
    {
        if (lifePoints <= 0 && currentState != UnitState.dead)
        {
            currentState = UnitState.dead;
            OnStateChanged?.Invoke(currentState);
            currentMoveDirection = Vector2Int.zero;
            GetComponent<Collider2D>().enabled = false;
            GetComponentInChildren<SpriteRenderer>().sortingOrder = -1;
            Destroy(gameObject, 2f);
            return;
        }


        RaycastHit2D info = Physics2D.Raycast(unitView.viewPoint.position, t.right, unitView.viewDistance, unitView.viewLayer);

        if(!info)
        {
            currentState = UnitState.idle;
        }
        else if(info.transform.TryGetComponent<BaseUnit>(out BaseUnit otherUnit))
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

        if(currentState != lastState)
        {
            Debug.Log(currentState);
            lastState = currentState;
            OnStateChanged?.Invoke(currentState);
        }
    }

    protected void ResetAttacks()
    {
        hasAttacked = false;
        currentState = UnitState.idle;
    }

    public abstract void HandleAttack(BaseUnit otherUnit);

    private void OnDrawGizmosSelected()
    {
        if (unitView.viewPoint == null) return;
        Gizmos.color = Color.red;
        Vector3 endPoint = unitView.viewPoint.position + new Vector3(unitView.viewDistance * transform.right.x, 0, 0);
        Gizmos.DrawLine(unitView.viewPoint.position, endPoint);
    }

    public void TakeDamage(float amount)
    {
        lifePoints -= amount;
    }

    public float GetUnitCost()
    {
        return stats.GetValue(StatEnum.Cost);
    }
}
