using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Plane Ground;
    public GroundController GroundObject;
    public PoolingController PoolingController;
    public ScreenController ScreenController;
    public Ball currentBall;
    public GameObject StartingPoint;
    [Range(1.0f, 100.0f)] public float Hmax;
    public int DoubleClicked;

    // by using Ballspeed and Hmax calculate angle and horizontal/vertical velocity
    private void Start()
    {
        PoolingController.Initialize();
        ScreenController.Initialize();
        //InitializeSliders();
        DoubleClicked = 1;
    }

    public void UpdateBallSpeed()
    {
        foreach (GameObject item in PoolingController.items)
        {
            item.GetComponent<Ball>().BallSpeed = ScreenController.BallSpeedSlider.value;
        }  
    }

    public void UpdateBouncingThreshold()
    {
        foreach (GameObject item in PoolingController.items)
        {
            item.GetComponent<Ball>().BouncingThreshold = ScreenController.BouncinessSlider.value / 10.0f;
        }
    }

    void Update()
    {
        //SlidersUpdate();
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 pos = Input.mousePosition;
            ChangeState(pos);
        }
    }

    // to switch between
    // selecting starting point and 
    // throwing the ball
    private void ChangeState(Vector3 pos)
    {
        DoubleClicked += 1;
        DoubleClicked %= 2;
        switch (DoubleClicked)
        {
            case 0:
                Spawn(pos);
                break;
            case 1:
                Throw(pos);
                break;
            default:
                break;
        }
    }


    private Ray CreateRay(Vector3 pos)
    {
        Ray ray = Camera.main.ScreenPointToRay(pos);
        Debug.Log(ray.origin + ", " + ray.direction);
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.green);
        return ray;
    }

    private void Spawn(Vector3 pos)
    {
        // means choosing starting point state
        // spawn balls from the clicked point
        // pooling manager get item
        Ray ray = CreateRay(pos);

        // Check if it hits the ground
        RaycastHit hitInfo;
        if (GroundObject.CheckRayHit(ray, out hitInfo))
        {
            StartingPoint.transform.position = hitInfo.point + new Vector3(.0f, .5f, .0f);
            //if (currentBall)
            //{
            //    currentBall.DestroyBall();
            //}
            GameObject temp = PoolingController.GetItem();
            if (!temp)
            {
                ScreenController.ShowWarning();
            }
            else
            {
                currentBall = temp.GetComponent<Ball>();
                currentBall.transform.position = StartingPoint.transform.position;
                currentBall.gameObject.SetActive(true);
            }
        }
        else DoubleClicked--;
    }

    private void Throw(Vector3 pos)
    {
        // means choose destination
        // throw ball to the clicked position
        // calculate angle and velocity
        Ray ray = CreateRay(pos);

        // Check if it hits the ground
        RaycastHit hitInfo;
        if (GroundObject.CheckRayHit(ray, out hitInfo))
        {
            Vector3 position = hitInfo.point;
            position.y = .5f;
            if(currentBall)
            {
                float TempAngle = currentBall.GetAngle(position, Hmax);
                Vector3 velocity = currentBall.GetVelocity(position, TempAngle, Hmax);
                Debug.Log("Throwing at " + hitInfo.point + " with velocity " + velocity);
                currentBall.ProjectileBall(velocity);
            }
        }
        else DoubleClicked--;
    }
}
