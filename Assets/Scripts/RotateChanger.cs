using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class RotateChanger : MonoBehaviour
{

    private TriggerInputDetector triggerInputDetector;
    private Creatrue creatrue;
    public float average_input = 0;

    public float maxRotationSpeed = 100f; // �ִ� ȸ�� �ӵ�
    public float rotationSpeedChangeSpeed = 1f; // ȸ�� �ӵ� ��ȭ �ӵ�
    public float triggerStabilityThreshold = 0.1f; // triggerInput�� ���� �ð� �̻� �����Ǵ��� Ȯ���ϱ� ���� �Ӱ谪
    private float lastTriggerInput; // ���� �������� triggerInput ��
    private float triggerStabilityTimer; // triggerInput�� �������� üũ�ϴ� Ÿ�̸�
    private float currentRotationSpeed; // ���� ȸ�� �ӵ�

    private float maxTime = 0f;

    public GameObject rotateTaeyop;



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

        if (average_input != 0)
        {
            RotateTaeyopChanger(average_input);
        }
    }


    void RotateTaeyopChanger(float triggerInput)
    {
        float zRotation = Mathf.Lerp(0, maxRotationSpeed, triggerInput);

        // ������ Quaternion���� ��ȯ
        Quaternion rotationQuaternion = Quaternion.Euler(0, 0, zRotation);

        // ���� ȸ���� Quaternion�� ���Ͽ� ���ο� ȸ���� ����
        rotateTaeyop.transform.rotation *= rotationQuaternion;


        if (zRotation == maxRotationSpeed)
        {
            maxTime += Time.deltaTime;
            StartCoroutine(Shake());

            if (maxTime >= 3)
            {
                maxTime = 3;
                StopCoroutine(Shake());
                //   objectManager.destroyObject = true;
                creatrue.completed = true;
                RotateChanger rotatechanger  = GetComponent<RotateChanger>();   
                rotatechanger.enabled = false;
              //  creatrue.state = Creatrue.State.Moving;
            }
        }

        else
        {
            maxTime = 0.0f;
        }

        /*
        // triggerInput ���� ������ ������ Ÿ�̸� ����
        if (triggerInput == lastTriggerInput)
        {
            triggerStabilityTimer += Time.deltaTime;

            // ���� �ð� �̻� �����ϰ� �����Ǹ� �ִ� ȸ�� �ӵ��� ����
            if (triggerStabilityTimer > triggerStabilityThreshold)
            {
                currentRotationSpeed = maxRotationSpeed;
               
            }
        }
        else
        {
            // triggerInput ���� ���ϸ� Ÿ�̸� �ʱ�ȭ
            triggerStabilityTimer = 0f;
        }

        // ������ �ִ� ȸ�� �ӵ��� ����
        currentRotationSpeed = Mathf.MoveTowards(currentRotationSpeed, maxRotationSpeed, Time.deltaTime * rotationSpeedChangeSpeed);

        

        // �ð��� ���� Z ȸ���� ����
        float zRotation = Time.time * currentRotationSpeed;

        // ������Ʈ�� ȸ���� ����
        rotateTaeyop.transform.rotation = Quaternion.Euler(0, 0, zRotation);

        // ���� triggerInput�� ���� ������ ����
        lastTriggerInput = triggerInput;
        */
    }


    IEnumerator Shake()
    {
        if (gameObject)
        {
            yield return null;
        }

        float t = 1f;
        float shakePower = 0.1f;
        Vector3 origin = gameObject.transform.position;

        while (t > 0f && gameObject)
        {
            t -= 0.05f;
            gameObject.transform.position = origin + (Vector3)Random.insideUnitCircle * shakePower * t;
            yield return null;
        }

        if (gameObject)
        {
            gameObject.transform.position = origin;
        }
    }
}