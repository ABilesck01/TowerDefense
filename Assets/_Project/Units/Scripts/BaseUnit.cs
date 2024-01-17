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


    [SerializeField] private StatSO stats;
    [Space]
    [SerializeField] private UnitView unitView;
    [SerializeField] private Vector2Int moveDirection = new Vector2Int(1, 0);
    [Space]
    public UnityEvent<UnitState> OnStateChanged;
    public UnityEvent<bool> OnLockStateChanged;

    private Transform t;
    private Rigidbody2D rb;
    private UnitState currentState;
    private UnitState lastState;
    private Vector2Int currentMoveDirection = Vector2Int.zero;
    private bool hasAttacked = false;
    private float timeBtwAttacks = 0;
    private SerializableDictionary<StatEnum, float> instanceStats;

    public StatSO GetStat() => stats;

    private void Awake()
    {
        t = transform;
        rb = GetComponent<Rigidbody2D>();
        instanceStats = stats.InstanceStats;
    }

    private void Update()
    {
        if (currentState == UnitState.dead) return;

        HandleStates();
        HandleMovement();
    }

    private void HandleStates()
    {
        if (stats.GetValue(StatEnum.LifePoints) <= 0 && currentState != UnitState.dead)
        {
            currentState = UnitState.dead;
            OnStateChanged?.Invoke(currentState);
            return;
        }

        RaycastHit2D info = Physics2D.Raycast(unitView.viewPoint.position, unitView.viewPoint.right, unitView.viewDistance, unitView.viewLayer);

        if (!info)
        {
            currentState = UnitState.run;
        }
        else if(info.transform.TryGetComponent<BaseUnit>(out BaseUnit otherUnit))
        {
            if (!hasAttacked)
            {
                if(timeBtwAttacks <= Time.time)
                {
                    currentState = UnitState.attacking;
                    hasAttacked = true;
                    timeBtwAttacks = Time.time + 1f / stats.GetValue(StatEnum.AttackRate);
                    Invoke(nameof(ResetAttacks), 0.9f);
                }
                else
                {
                    currentState = UnitState.idle;
                }
            }
        }
        else 
        {
            currentState = UnitState.idle;
        }

        if(currentState != lastState)
        {
            Debug.Log(currentState);
            lastState = currentState;
            OnStateChanged?.Invoke(currentState);
        }
    }

    private void ResetAttacks()
    {
        hasAttacked = false;
    }

    public abstract void HandleAttack();

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

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(currentMoveDirection.x * stats.GetValue(StatEnum.MoveSpeed), rb.velocity.y);
    }

    private void OnDrawGizmosSelected()
    {
        if (unitView.viewPoint == null) return;
        Gizmos.color = Color.red;
        Vector3 endPoint = unitView.viewPoint.position + new Vector3(unitView.viewDistance, 0, 0);
        Gizmos.DrawLine(unitView.viewPoint.position, endPoint);
    }

    public void TakeDamage(float amount)
    {
        instanceStats.TryGetValue(StatEnum.LifePoints, out float value);
        value -= amount;
        instanceStats.TrySetValue(StatEnum.LifePoints, value);

    }
}
