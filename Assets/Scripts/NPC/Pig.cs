using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Pig : WeakAnimal
{
    protected override void ReSet()
    {
        base.ReSet();
        RandomAction();
    }
    private void RandomAction()
    {

        int _random = Random.Range(0, 4);
        PlayRandomSound();

        if (_random == 0)
        {
            Wait();
        }
        if (_random == 1)
        {
            Eat();
        }
        if (_random == 2)
        {
            Peek();
        }
        if (_random == 3)
        {
            TryWalk();
        }
    }
    private void Wait()
    {
        currentTime = waitTime;
        Debug.Log("´ë±â");
    }
    private void Eat()
    {
        currentTime = waitTime;
        anim.SetTrigger("Eat");
    }
    private void Peek()
    {
        currentTime = waitTime;
        anim.SetTrigger("Peek");
    }


}
