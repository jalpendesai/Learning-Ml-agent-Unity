using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class RollerAgent : Agent
{
    public Transform Target;

    private Rigidbody rBody;

    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody>();
    }

    public override void AgentReset()
    {
        if (this.transform.position.y < 0)
        {
            this.rBody.angularVelocity = Vector3.zero;
            this.rBody.velocity = Vector3.zero;
            this.transform.position = new Vector3(0, 0.5f, 0);
        }

        Target.position = new Vector3(Random.value * 8 - 4, 0.5f, Random.value * 8 - 4);
    }

    public override void CollectObservations()
    {
        //  Target and Agent Positions
        AddVectorObs(Target.position);
        AddVectorObs(this.transform.position);

        //  Agent Velocity
        AddVectorObs(rBody.velocity.x);
        AddVectorObs(rBody.velocity.z);
    }

    //  --------Actions & Rewards ------------  //    
    public float speed = 10;
    public override void AgentAction(float[] vectorAction, string textAction)
    {
        //  Actions, size = 2
        Vector3 controlSignal = Vector3.zero;
        controlSignal.x = vectorAction[0];
        controlSignal.z = vectorAction[1];
        rBody.AddForce(controlSignal * speed);

        //  Rewards
        float distanceToTarget = Vector3.Distance(this.transform.position, Target.position);

        //  Reached Targer?
        if(distanceToTarget < 1.42f){
            SetReward(1.0f);
            Done();
        }

        //  Fell off Platform?
        if(this.transform.position.y < 0){
            Done();
        }
    }
}
