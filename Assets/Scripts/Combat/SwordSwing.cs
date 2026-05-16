using UnityEngine;

public class SwordSwing : MonoBehaviour
{
    [SerializeField] private float swingCooldown = 0.5f;
    private float cooldownTimer;

    private void Update()
    {
        if (cooldownTimer > 0f)
        {
            cooldownTimer -= Time.deltaTime;
        }
    }

    public bool TrySwing()
    {
        if (cooldownTimer > 0f)
        {
            return false;
        }

        cooldownTimer = swingCooldown;
        return true;
    }
}
