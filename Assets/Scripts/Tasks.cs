using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
public class ActionTask : Task
{
    public Action Action { get; private set; }
    public ActionTask(Action action)
    {
        Action = action;
    }

    protected override void Init()
    {
        Action();
        SetStatus(TaskStatus.Success);
    }
}
*/

public abstract class TimedTask : Task
{
    public float Duration { get; private set; }
    public float StartTime { get; private set; }

    protected TimedTask(float duration)
    {
        Duration = duration;
    }

    protected override void Init()
    {
        StartTime = Time.time;
    }

    internal override void Update()
    {
        var now = Time.time;
        var elapsed = now - StartTime;
        var t = Mathf.Clamp01(elapsed / Duration);
        if (t >= 1)
        {
            OnElapsed();
        }
        else
        {
            OnTick(t);
        }
    }

    protected virtual void OnTick(float t) { }

    protected virtual void OnElapsed()
    {
        SetStatus(TaskStatus.SUCCESS);
    }
}

public class Wait : TimedTask
{
    public Wait(float duration) : base(duration) { }
}

public abstract class MoveTask : Task
{
    protected readonly GameObject gameObject;
    protected MoveTask(GameObject gameObject)
    {
        this.gameObject = gameObject;
    }
}

public abstract class TimedObjectTask : TimedTask
{
    protected readonly GameObject gameObject;
    protected TimedObjectTask(GameObject gameObject, float duration) : base(duration)
    {
        this.gameObject = gameObject;
    }
}

public class Teleport : MoveTask
{
    private readonly Vector3 targetPos;
    public Teleport(GameObject gameObject, Vector3 pos) : base(gameObject)
    {
        targetPos = pos;
    }

    protected override void Init()
    {
        gameObject.transform.position = targetPos;
        SetStatus(TaskStatus.SUCCESS);
    }
}

public class Transport : TimedObjectTask
{
    public Vector3 StartPos { get; private set; }
    public Vector3 EndPos { get; private set; }

    public Transport(GameObject gameObject, Vector3 start, Vector3 end, float duration)
        : base(gameObject, duration)
    {
        StartPos = start;
        EndPos = end;
    }

    protected override void OnTick(float t)
    {
        gameObject.transform.position = Vector3.Lerp(StartPos, EndPos, t);
    }
}

public class Scale : TimedObjectTask
{
    public Vector3 StartScale { get; private set; }
    public Vector3 EndScale { get; private set; }

    public Scale(GameObject gameObject, Vector3 start, Vector3 end, float duration) :
        base(gameObject, duration)
    {
        StartScale = start;
        EndScale = end;
    }

    protected override void OnTick(float t)
    {
        gameObject.transform.localScale = Vector3.Lerp(StartScale, EndScale, t);
    }
}