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
    [SerializeField] private UnitView unitView;
    [SerializeField] private float freezeTimeInAttack = 0.9f;
    [SerializeField] private Vector2Int moveDirection = new Vector2Int(1, 0);
    [Space]
    public UnityEvent<UnitState> OnStateChanged;

    private Transform t;
    private Rigidbody2D rb;
    private UnitState currentState;
    private UnitState lastState;
    private Vector2Int currentMoveDirection = Vector2Int.zero;
    private bool hasAttacked = false;
    private float timeBtwAttacks = 0;
    private float lifePoints;

    public Vector2Int MoveDirection 
    { 
        get => moveDirection;
        set
        {
            moveDirection = value;
            currentMoveDirection = value;
        }
    }

    public StatSO GetStat() => stats;

    private void Awake()
    {
        t = transform;
        rb = GetComponent<Rigidbody2D>();
        lifePoints = stats.GetValue(StatEnum.LifePoints);
    }

    private void Update()
    {
        if (currentState == UnitState.dead) return;

        HandleStates();
        HandleMovement();
    }

    private void HandleStates()
    {
        if (lifePoints <= 0 && currentState != UnitState.dead)
        {
            currentState = UnitState.dead;
            OnStateChanged?.Invoke(currentState);
            currentMoveDirection = Vector2Int.zero;
            moveDirection = Vector2Int.zero;
            GetComponent<Collider2D>().enabled = false;
            //this.enabled = false;
            return;
        }


        RaycastHit2D info = Physics2D.Raycast(unitView.viewPoint.position, t.right, unitView.viewDistance, unitView.viewLayer);

        if (!info)
        {
            if(!hasAttacked)
                currentState = UnitState.run;
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
        currentState = UnitState.idle;
    }

    public abstract void HandleAttack(BaseUnit otherUnit);

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
        if(currentState == UnitState.dead)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        rb.velocity = new Vector2(currentMoveDirection.x * stats.GetValue(StatEnum.MoveSpeed), rb.velocity.y);
    }

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
}
