using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class BasePartner : MonoBehaviour
{
    [SerializeField] protected Animator _anim;
    [SerializeField] protected Rigidbody2D _rb;
    protected EnemyManager _enemyManager;
    protected EnemyData _currentEnemy;
    protected PlayerControler _player; 
    protected PartnerManager _manager;
    protected PartnerState _currentState = new PartnerState();
    protected float _followOffset;
    protected bool _canMove;
    protected float _distance => Vector2.Distance(transform.position, _target.position);
    protected float _rayDistance = 2f;
    protected float avoidStrength = 0.5f;
    public LayerMask _enemyLayer, _obstacleLayer;


    [Header("BaseStats")]
    [SerializeField] protected float _normalAttackRadius;
    protected float _moveSpeed;
    public int _attackDamage;
    public float _attackRange;
    public Transform attackPoint;

    protected Transform _target;
    protected Vector2 _dir;
    protected Vector3 _lastMoveDir;
    protected Vector3? _lastKnownPos = null;
    protected virtual void Update()
    {

        if (_enemyManager != null && _enemyManager.enemyDatas.Count > 0)
            CheckAttack();

        switch (_currentState)
        {
            case PartnerState.Idle:
                IdleState();
                break;
            case PartnerState.MoveToTarget:
                MoveToTargetState(); 
                break;
            case PartnerState.Attack:
                UpdateAttack();
                break;
        }
    }

    protected virtual void FixedUpdate()
    {
        if (!_canMove) return;

        _rb.MovePosition(_rb.position + _dir.normalized * _moveSpeed * Time.fixedDeltaTime);
    }
    public void Init(PartnerStats stats, PlayerControler player, PartnerManager manager, float offset)
    {
        _player = player;
        _manager = manager;
        _moveSpeed = stats.speed;
        _attackDamage = stats.attack;
        _followOffset = offset;
        _enemyManager = FindObjectOfType<EnemyManager>();
        if (_enemyManager != null) _currentEnemy = CheckNearestEnemy(_enemyManager.enemyDatas);
        _target = _player.transform;
        var colA = _player.GetComponent<Collider2D>();
        var colB = GetComponent<Collider2D>();
        Physics2D.IgnoreCollision(colA, colB);
        StartState();
    }

    protected void ChangeState(PartnerState newState)
    {
        if (newState == _currentState) return;

        //Exit State
        switch (_currentState)
        {
            case PartnerState.Idle:
                break;
            case PartnerState.MoveToTarget:
                break;
            case PartnerState.Attack:
                ExitAttack();
                break;
        }
        _currentState = newState;
        //EnterState
        switch (newState)
        {
            case PartnerState.Start:
                StartState();
                break;
            case PartnerState.Idle:
                _canMove = false;
                break;
            case PartnerState.MoveToTarget:
                _canMove = true;
                break;
            case PartnerState.Attack:
                EnterAttack();
                break;
        }
       
    }

    private void StartState()
    {
        _lastMoveDir = _player.PlayerMovement.VectorDirPlayer();
        _anim.SetFloat("Horizontal", _lastMoveDir.x);
        _anim.SetFloat("Vertical", _lastMoveDir.y);
        ChangeState(PartnerState.Idle);
    }
    #region Attack_State
    protected abstract void CheckAttack();

    protected abstract void ExitAttack();

    protected abstract void UpdateAttack();
    protected abstract void EnterAttack();
    protected void HitBoxAttack(Vector3 dir, float range, float radius)
    {
        var hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position + dir * range, radius, _enemyLayer);
        foreach (var enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyData>().TakeDamage(_attackDamage);
        }
    }
    protected EnemyData CheckNearestEnemy(List<EnemyData> _allEnemies)
    {
        return _allEnemies
            .Where(e => e != null)
            .OrderBy(e => Vector3.Distance(transform.position, e.transform.position))
            .FirstOrDefault();
    }
    protected Vector3 AbsDir(Vector3 dir)
    {
        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
        {
            return dir.x > 0 ? Vector2.right : Vector2.left;
        }
        else
        {
            return dir.y > 0 ? Vector2.up : Vector2.down;
        }
    }
    #endregion

    private void MoveToTargetState()
    {   
        Vector2 dirToTarget = (_target.position - transform.position).normalized;

        //Ray Center
        var hitForward = Physics2D.Raycast(transform.position, dirToTarget, _rayDistance, _obstacleLayer);
        //Ray Left
        Vector2 leftDir = Quaternion.Euler(0, 0, 45) * dirToTarget;
        var hitLeft = Physics2D.Raycast(transform.position, leftDir, _rayDistance, _obstacleLayer);
        //Ray Right
        Vector2 rightDir = Quaternion.Euler(0, 0, -45) * dirToTarget;
        var hitRight = Physics2D.Raycast(transform.position, rightDir, _rayDistance, _obstacleLayer);

        Debug.DrawRay(transform.position, dirToTarget * _rayDistance, Color.red);
        Debug.DrawRay(transform.position, leftDir * _rayDistance, Color.yellow);
        Debug.DrawRay(transform.position, rightDir * _rayDistance, Color.green);

        _dir = dirToTarget;

        if (hitForward.collider != null && hitLeft.collider != null && hitRight.collider != null)
        {
            Debug.Log("back duoc");
            _dir = -dirToTarget * avoidStrength;
        }
        else if (hitLeft.collider != null && hitRight.collider == null)
            _dir += (Vector2)(Quaternion.Euler(0, 0, -90) * dirToTarget) * avoidStrength;
        else if (hitRight.collider != null && hitLeft.collider == null)
            _dir += (Vector2)(Quaternion.Euler(0, 0, 90) * dirToTarget) * avoidStrength;

        _lastMoveDir = _dir;
        _anim.SetFloat("Horizontal", _dir.x);
        _anim.SetFloat("Vertical", _dir.y);
        _anim.SetFloat("MoveMagnitude", _dir.magnitude);
        if (_distance <= _followOffset)
        {
            _anim.SetFloat("MoveMagnitude", 0);
            ChangeState(PartnerState.Idle);
            return;
        }
    }

    private void IdleState()
    {
        _anim.SetFloat("Horizontal", _lastMoveDir.x);
        _anim.SetFloat("Vertical", _lastMoveDir.y);
        _anim.SetFloat("MoveMagnitude", 0);
        if (_distance > _followOffset) ChangeState(PartnerState.MoveToTarget);
    }
}

public enum PartnerState
{   
    Start,
    Idle,
    MoveToTarget,
    Attack
}
