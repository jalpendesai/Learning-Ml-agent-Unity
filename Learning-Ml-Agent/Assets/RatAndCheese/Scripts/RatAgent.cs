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
      this.transform.position = new Vector3(0f,0.5f,0f);
      rbody.AddForce(Vector3.zero);
    }

    public override void AgentAction(float[] vectorAction, string textAction)
    {
        float absDir = 0f;
        Vector3 dir = Vector3.zero;
        Vector3 rotateDir = Vector3.zero;

        if (brain.brainParameters.vectorActionSpaceType == SpaceType.continuous)
        {
            absDir = Mathf.Clamp(vectorAction[0], -1f, 1f);
            // dir += new Vector3(this.transform.position.x * absDir,0,0); 

            rotateDir = transform.up * Mathf.Clamp(vectorAction[1], -1f, 1f);
            dir = new Vector3(absDir * speed,0.0f,0.0f);
            
        }
        // else
        // {
        //     switch ((int)(vectorAction[0]))
        //     {
        //         case 1:
        //             dir = transform.forward;
        //             break;
        //         case 2:
        //             rotateDir = -transform.up;
        //             break;
        //         case 3:
        //             rotateDir = transform.up;
        //             break;
        //     }
        // }
        rbody.AddForce(dir * speed, ForceMode.VelocityChange);
        Vector3 localVel = transform.InverseTransformDirection(rbody.velocity);
        if (localVel.z < 0)
        {
            // TODO: Penalize if player goes backward
        }
        transform.Rotate(rotateDir, Time.fixedDeltaTime * turnSpeed);

        if(this.transform.position.y < 0.3){
          Done();
      }
    }
}
