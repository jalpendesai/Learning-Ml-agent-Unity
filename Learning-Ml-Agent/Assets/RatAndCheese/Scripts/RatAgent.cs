using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class RatAgent : Agent
{
    Rigidbody rbody;

    public float speed = 10.0f;
    public float turnSpeed = 5.0f;

    void Start()
    {
        rbody = GetComponent<Rigidbody>();
    }

    public override void AgentReset()
    {
        int rotation = Random.Range(0,4);
        float rotateAngle = rotation * 90f;
        
        this.transform.position = new Vector3(0f, 0.5f, 0f);
        rbody.velocity = Vector3.zero;
        rbody.angularVelocity = Vector3.zero;
    }

    public void MoveAgent(float[] act)
    {
        Vector3 dirToGo = Vector3.zero;
        Vector3 rotateDir = Vector3.zero;

        int action = Mathf.FloorToInt(act[0]);

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
        transform.Rotate(rotateDir, Time.fixedDeltaTime * 200f);
        rbody.AddForce(dirToGo * 1f, ForceMode.VelocityChange);
    }

    public override void AgentAction(float[] vectorAction, string textAction)
    {
        MoveAgent(vectorAction);

        AddReward(-1f / agentParameters.maxStep);

        if(this.transform.position.y < 0){
            Done();
        }
    }
}
