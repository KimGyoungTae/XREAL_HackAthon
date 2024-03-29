using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SizeChanger : MonoBehaviour
{

    //[SerializeField]
    //private ObjectManager objectManager;
    private TriggerInputDetector triggerInputDetector;
    private Creatrue creatrue;

    public float maxRotationSpeed = 100f; // 최대 회전 속도
    private float currentRotationSpeed; // 현재 회전 속도
    public float rotationSpeedChangeSpeed = 1f; // 회전 속도 변화 속도

    //public GameObject rotateTaeyop;
    //   public XRController controller;

    public float maxTime = 3f; // full-grip time
    public float average_input = 0;


    /*
    [Range(0,1)]
    public float test_LT;
    [Range(0, 1)]
    public float test_LG;
    [Range(0, 1)]
    public float test_RT;
    [Range(0, 1)]
    public float test_RG;
    */

    private void Start()
    {
        triggerInputDetector = FindObjectOfType<TriggerInputDetector>();
        creatrue = GetComponent<Creatrue>();
    }

    void Update()
    {
        //average_input = (test_LG + test_LT + test_RG + test_RT) / 4f;
        average_input = (triggerInputDetector.GetLeftGripValue + triggerInputDetector.GetRightGripValue + triggerInputDetector.GetLeftTriggerValue + triggerInputDetector.GetRightTriggerValue) / 4f;

        //if(average_input != 0 )
        //{
        //    RotateChanger(average_input);
        //}

        //if ()
        //{
        //    SizeChange(average_input);
        //}

        //if (!objectManager.activeObject && average_input == 0)
        //{
        //    objectManager.respawnObject = true;
        //}

        if (average_input != 0)
        {
            SizeChange(average_input);
        }
    }

    void SizeChange(float triggerinput)
    {
        float newScale = Mathf.Lerp(1f, 4.5f, triggerinput);
        transform.localScale = new Vector3(newScale, newScale, newScale);
     //   objectManager.activeObject.transform.rotation = controller.transform.rotation;

        if (newScale >= 4.4f)
        {
            maxTime -= Time.deltaTime;
            
            StartCoroutine(SShake());

            if (maxTime <= 0)
            {
                maxTime = 0;
                //   objectManager.destroyObject = true;
                if (creatrue != null)
                    creatrue.completed = true;
            }
        }

        else
        {
            maxTime = 3.0f;
        }
        
    }

    //void RotateChanger(float triggerinput)
    //{
    //    // 목표 회전 속도를 0에서 maxRotationSpeed 사이로 매핑
    //    float targetRotationSpeed = Mathf.Lerp(0, maxRotationSpeed, triggerinput);

    //    // 현재 회전 속도를 서서히 목표 회전 속도로 변경
    //    currentRotationSpeed = Mathf.Lerp(currentRotationSpeed, targetRotationSpeed, Time.deltaTime * rotationSpeedChangeSpeed);

    //    // 시간에 따라 Z 회전값 증가
    //    float zRotation = Time.time * currentRotationSpeed;

    //    // 오브젝트의 회전을 설정
    //    rotateTaeyop.transform.rotation = Quaternion.Euler(0, 0, zRotation);

    //}



    //IEnumerator Shake()
    //{
    //    if (!objectManager.activeObject)
    //    {
    //        yield return null;
    //    }

    //    float t = 1f;
    //    float shakePower = 0.3f;
    //    Vector3 origin = objectManager.activeObject.transform.position;

    //    while (t > 0f && objectManager.activeObject)
    //    {
    //        t -= 0.05f;
    //        objectManager.activeObject.transform.position = origin + (Vector3) Random.insideUnitCircle * shakePower * t;
    //        yield return null;
    //    }

    //    if (objectManager.activeObject)
    //    {
    //        objectManager.activeObject.transform.position = origin;
    //    }
    //}
    IEnumerator SShake()
    {
        //if (gameObject)
        //{
        //    yield return null;
        //}


        float t = 1f;
        float shakePower = 0.2f;
        Vector3 origin = gameObject.transform.position;
 
        while (t > 0f && gameObject)
        {
            t -= 0.05f;
            gameObject.transform.position = origin + (Vector3)Random.insideUnitCircle * shakePower * t;
           // Debug.Log("코루틴 실행 중..");
            yield return null;
        }

        if (gameObject)
        {
            gameObject.transform.position = origin;
        }
    }
}
