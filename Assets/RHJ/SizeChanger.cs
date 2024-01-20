using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SizeChanger : MonoBehaviour
{

    [SerializeField]
    private ObjectManager objectManager;
    [SerializeField]
    private TriggerInputDetector triggerInputDetector;
    [SerializeField]
    private Creatrue creatrue;

    public float maxRotationSpeed = 100f; // �ִ� ȸ�� �ӵ�
    private float currentRotationSpeed; // ���� ȸ�� �ӵ�
    public float rotationSpeedChangeSpeed = 1f; // ȸ�� �ӵ� ��ȭ �ӵ�

    public GameObject rotateTaeyop;
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
        creatrue = GetComponent<Creatrue>();
    }

    void Update()
    {
        //average_input = (test_LG + test_LT + test_RG + test_RT) / 4f;
        average_input = (triggerInputDetector.GetLeftGripValue + triggerInputDetector.GetRightGripValue + triggerInputDetector.GetLeftTriggerValue + triggerInputDetector.GetRightTriggerValue) / 4f;

        if(average_input != 0 )
        {
            RotateChanger(average_input);
        }

        if (objectManager.activeObject)
        {
            SizeChange(average_input);
        }

        if (!objectManager.activeObject && average_input == 0)
        {
            objectManager.respawnObject = true;
        }
    }

    void SizeChange(float triggerinput)
    {
        float newScale = Mathf.Lerp(0.05f, 1.0f, triggerinput);
        objectManager.activeObject.transform.localScale = new Vector3(newScale, newScale, newScale);
     //   objectManager.activeObject.transform.rotation = controller.transform.rotation;

        if (newScale == 1.0f)
        {
            maxTime -= Time.deltaTime;
            StartCoroutine(Shake());
         
            if (maxTime <= 0)
            {
                maxTime = 0;
             //   objectManager.destroyObject = true;
             creatrue.completed = true;
            }
        }

        else
        {
            maxTime = 3.0f;
        }
        
    }

    void RotateChanger(float triggerinput)
    {
        // ��ǥ ȸ�� �ӵ��� 0���� maxRotationSpeed ���̷� ����
        float targetRotationSpeed = Mathf.Lerp(0, maxRotationSpeed, triggerinput);

        // ���� ȸ�� �ӵ��� ������ ��ǥ ȸ�� �ӵ��� ����
        currentRotationSpeed = Mathf.Lerp(currentRotationSpeed, targetRotationSpeed, Time.deltaTime * rotationSpeedChangeSpeed);

        // �ð��� ���� Z ȸ���� ����
        float zRotation = Time.time * currentRotationSpeed;

        // ������Ʈ�� ȸ���� ����
        rotateTaeyop.transform.rotation = Quaternion.Euler(0, 0, zRotation);

    }



    IEnumerator Shake()
    {
        if (!objectManager.activeObject)
        {
            yield return null;
        }

        float t = 1f;
        float shakePower = 0.3f;
        Vector3 origin = objectManager.activeObject.transform.position;
        
        while (t > 0f && objectManager.activeObject)
        {
            t -= 0.05f;
            objectManager.activeObject.transform.position = origin + (Vector3) Random.insideUnitCircle * shakePower * t;
            yield return null;
        }

        if (objectManager.activeObject)
        {
            objectManager.activeObject.transform.position = origin;
        }
    }
}
