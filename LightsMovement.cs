using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightsMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject spotLight;
    public float velocityRotation;
    float nuevaRotacionZ;
    void Start()
    {
        velocityRotation= 6f;
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 rotacionActual = spotLight.transform.rotation.eulerAngles;


        nuevaRotacionZ = spotLight.transform.rotation.z;
       
       
        if(nuevaRotacionZ < 220f)
        {
            nuevaRotacionZ = rotacionActual.z + velocityRotation * Time.deltaTime;
            
        }
        else if (nuevaRotacionZ > 240f)
        {
            nuevaRotacionZ = rotacionActual.z - velocityRotation * Time.deltaTime;
        }
        else
        {
            nuevaRotacionZ = rotacionActual.z - velocityRotation * Time.deltaTime;
        }
        spotLight.transform.rotation = Quaternion.Euler(rotacionActual.x, rotacionActual.y, nuevaRotacionZ);
          //Debug.Log(rotacionActual.z);
        //Debug.Log(rotacionActual.z);
        Debug.Log(velocityRotation);
        //Debug.Log(Time.deltaTime);


    }

}
