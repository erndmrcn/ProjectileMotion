using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Vector3 velocity;
    public float BouncingThreshold;
    public float BallSpeed;

    public void ProjectileBall(Vector3 BallSPeed)
    {
        velocity = BallSPeed.CheckIsNaN();
    }

    public Vector3 GetVelocity(Vector3 destination, float angle, float Hmax)
    {
        Vector3 direction = destination - transform.position;
        float height = direction.y;
        direction.y = 0;
        float distance = direction.magnitude;
        // maxHeight
        direction.y = distance * Mathf.Tan(angle);
        distance += height / Mathf.Tan(angle);

        // calculate velocity
        float velocity = Mathf.Sqrt(distance * Physics.gravity.magnitude / Mathf.Sin(2 * angle));
        return velocity * direction.normalized;
    }

    // overloading
    public Vector3 GetVelocity(Vector3 destination, float angle, float Hmax, float Speed)
    {
        Vector3 direction = destination - transform.position;
        direction.y = Hmax;

        return Speed * direction.normalized;
    }

    public float GetAngle(Vector3 destination, float Hmax)
    {
        Vector3 direction = destination - transform.position;
        float height = direction.y;
        direction.y = 0;
        float distance = direction.magnitude;
        float x = distance / (4 * Hmax);
        float radian = Mathf.Atan(1 / x) + Mathf.PI;
        return radian;
    }

    // not needed
    public void DestroyBall()
    {
        GameObject.Destroy(this.gameObject);
    }

    // use this function for jumping
    public bool OnTheGround()
    {
        if (transform.position.y >= .5f)
        {
            // it means it is above cannot jump
            return false;
        }
        else
        {
            return true;
        }
    }


    private void Update()
    {
        // apply physics without rigidbody
        PhysicUpdate();
    }

    private void PhysicUpdate()
    {
        //Vector3 currFrameVelocity = (transform.position - prevPos) / Time.deltaTime;
        //prevPos = transform.position;
        //transform.position += (velocity + Physics.gravity) * Time.deltaTime;
        Vector3 pos = transform.position;
        pos.y = (transform.position.y >= .52f) ? pos.y : .5f;
        if (transform.position.y >= .52f)
        {
            // do nothing
            velocity.y -= Physics.gravity.magnitude * Time.deltaTime;
        }
        else
        {
            if (velocity.y < 0.0f)
            {
                velocity.y *= -1 * BouncingThreshold;
                BouncingThreshold *= 0.4f;
                if (BouncingThreshold <= .05f)
                {
                    BouncingThreshold = 0;
                }
            }
        }
        transform.position = (pos + (velocity * Time.deltaTime)) ;
    }
}