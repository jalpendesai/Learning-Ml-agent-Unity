using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class RatAgent : Agent
{
    Rigidbody rbody;
    RayPerception rayPerception;
    public float speed = 5.0f;
    public Transform Target;
    void Start()
    {
        rbody = GetComponent<Rigidbody>();
        rayPerception = GetComponent<RayPerception>();
    }

    public override void AgentReset()
    {
        int rotation = Random.Range(0, 4);
        float rotateAngle = rotation * 90f;

        this.transform.position = new Vector3(Random.value * 35, 0.5f, Random.value * 35);
        rbody.velocity = Vector3.zero;
        rbody.angularVelocity = Vector3.zero;

        Target.position = new Vector3(Random.value * 37, 1f, Random.value * 37);
    }

    public override void CollectObservations()
    {
        var rayDistance = 30f;
        float[] rayAngles = { 0f, 45f, 90f, 135f, 180f, 225f, 270f, 315f };
        var detectableObjects = new[] { "goal" };
        AddVectorObs(rayPerception.Perceive(rayDistance, rayAngles, detectableObjects, 0f, 0f));
        AddVectorObs(rayPerception.Perceive(rayDistance, rayAngles, detectableObjects, 1.5f, 0f));

        AddVectorObs(Target.position);
        AddVectorObs(this.transform.position);
        AddVectorObs(rbody.velocity.x);
        AddVectorObs(rbody.velocity.z);
    }
    public void MoveAgent(float[] act)
    {
        // Vector3 dirToGo = Vector3.zero;
        // Vector3 rotateDir = Vector3.zero;

        // int action = Mathf.FloorToInt(act[0]);

        // switch (action)
        // {
        //     case 1:
        //         dirToGo = transform.forward * 1f;
        //         break;
        //     case 2:
        //         dirToGo = transform.forward * -1f;
        //         break;
        //     case 3:
        //         rotateDir = transform.up * 1f;
        //         break;
        //     case 4:
        //         rotateDir = transform.up * -1f;
        //         break;
        //     case 5:
        //         dirToGo = transform.right * -0.75f;
        //         break;
        //     case 6:
        //         dirToGo = transform.right * 0.75f;
        //         break;
        // }
        // this.transform.Rotate(rotateDir, Time.fixedDeltaTime * 200f);
        // rbody.AddForce(dirToGo * 10f, ForceMode.VelocityChange);
    }

    public override void AgentAction(float[] vectorAction, string textAction)
    {
        // MoveAgent(vectorAction);

        Vector3 dirToGo = Vector3.zero;
        Vector3 rotateDir = Vector3.zero;

        int action = Mathf.FloorToInt(vectorAction[0]);

        switch (action)
        {
            case 1:
                dirToGo = transform.forward * 1f;
                break;
            case 2:
                dirToGo = transform.forward * -1f;
                break;
            case 3:
                rotateDir = transform.up * 1f;
                break;
            case 4:
                rotateDir = transform.up * -1f;
                break;
            case 5:
                dirToGo = transform.right * -0.75f;
                break;
            case 6:
                dirToGo = transform.right * 0.75f;
                break;
        }
        this.transform.Rotate(rotateDir, Time.fixedDeltaTime * 200f);
        rbody.AddForce(dirToGo * speed, ForceMode.VelocityChange);

        // AddReward(-1f / agentParameters.maxStep);

        if (this.transform.position.y < 0)
        {
            AddReward(-1f);
            Done();
        }

        float distanceToTarget = Vector3.Distance(this.transform.position, Target.position);
        if (distanceToTarget < 1.42f)
        {
            AddReward(5f);
            Done();
        }

    }
}
