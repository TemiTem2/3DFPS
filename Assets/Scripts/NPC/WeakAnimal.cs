using UnityEngine;

public class WeakAnimal : Animal
{
    public void Run(Vector3 _targetPos)
    {
        destination = new Vector3(transform.position.x - _targetPos.x, 0, transform.position.z - _targetPos.z).normalized;

        currentTime = runTime;
        navAgent.speed = runSpeed;
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
