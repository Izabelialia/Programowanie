using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackDelay = 0.1f;
    public float attackRange = 0.5f;
    public float chargeTime = 2f;
    private bool canAttack = true;
    private float buttonHoldTime = 0f;
    private bool isHolding = false;
    
    public PlayerClasses playerClasses;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && canAttack)
        {
            isHolding = true;
            buttonHoldTime = 0f;
        }

        if (isHolding)
        {
            buttonHoldTime += Time.deltaTime;
        }

        if (Input.GetKeyUp(KeyCode.Mouse0) && canAttack)
        {
            Attack();
            isHolding = false;
        }
    }

    void Attack()
    {
        canAttack = false;

        int finalDamage = playerClasses.attack;

        if (buttonHoldTime >= chargeTime)
        {
            finalDamage *= 2;
        }

        RaycastHit hit;
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

        if (Physics.Raycast(ray, out hit, attackRange))
        {
            Enemy enemy = hit.collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(finalDamage);
            }
        }

        Invoke(nameof(ResetAttack), attackDelay);
    }

    void ResetAttack()
    {
        canAttack = true;
    }
}

