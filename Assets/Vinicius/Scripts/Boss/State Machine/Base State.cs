using UnityEngine;

public abstract class BaseState : MonoBehaviour
{
    public bool isComplete { get; protected set; }

    protected Animator animator;
    protected Rigidbody2D rb;
    protected Transform tr;
    protected BossController controller;

    public void Setup(Rigidbody2D rb, Transform tr, Animator animator, BossController controller)
    {
        this.rb = rb;
        this.tr = tr;
        this.animator = animator;
        this.controller = controller;
    }

    public void Initialise()
    {
        isComplete = false;
    }

    public virtual void StateEnter() { }
    public virtual void StateUpdate() { }
    public virtual void StateFixedUpdate() { }
    public virtual void StateExit() { }
}