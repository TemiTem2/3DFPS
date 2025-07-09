using UnityEngine;

public class WeakAnimal : Animal
{
    public void Run(Vector3 _targetPos)
    {
        direction = Quaternion.LookRotation(transform.position - _targetPos).eulerAngles;

        currentTime = runTime;
        applySpeed = runSpeed;
        isWalking = false;
        isRunning = true;
        anim.SetBool("Running", isRunning);
    }
    public override void Damage(int _damaga, Vector3 _targetPos)
    {
        base.Damage(_damaga, _targetPos);
        if(!isDead)
            Run(_targetPos);
    }
}
