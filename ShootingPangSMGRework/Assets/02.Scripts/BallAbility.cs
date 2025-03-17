using UnityEngine;

public abstract class BallAbility : MonoBehaviour
{
    protected bool active;
    public bool isActive
    {
        get { return active; }
    }

    public abstract void StopAbility();
}
