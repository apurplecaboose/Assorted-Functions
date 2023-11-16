using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks; // MAKE SURE YOU INCLUDE THIS

public class AngleLerp : MonoBehaviour
{
    //FOR TESTING USE ONLY
    //[SerializeField] float _deltaAngle, _rotTime;
    //void Update()
    //{
    //    if(Input.GetKeyDown(KeyCode.Space))
    //    {
    //        AngleLerping(this.gameObject, _deltaAngle, _rotTime);
    //    }
    //}

    public async void AngleLerping(GameObject objRef, float anglechange, float targetTime) 
    {
        float local_dTime = 0;
        Quaternion quatA = objRef.transform.rotation; //set starting rotation
        //set end rotation aka plus how many degrees I want to rotate
        //Quaternion.Euler is to change it to Euler angles
        Quaternion quatB = Quaternion.Euler(new Vector3(0, 0, anglechange)) * objRef.transform.rotation; // multiply quaternions to add them

        //while loop will keep looping until the target time is reached 
        while (local_dTime <= targetTime)
        {
            local_dTime += Time.deltaTime; // increase the deltatime over time 
            objRef.transform.rotation = Quaternion.Lerp(quatA, quatB, local_dTime / targetTime); // change the rotation
            await Task.Yield(); //used by async function to wait for the while condition
        }
    }
}
