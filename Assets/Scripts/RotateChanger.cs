using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateChanger : MonoBehaviour
{

    [SerializeField]
    private TriggerInputDetector triggerInputDetector;
    public float average_input = 0;

    public float maxRotationSpeed = 100f; // �ִ� ȸ�� �ӵ�
    public float rotationSpeedChangeSpeed = 1f; // ȸ�� �ӵ� ��ȭ �ӵ�
    public float triggerStabilityThreshold = 0.1f; // triggerInput�� ���� �ð� �̻� �����Ǵ��� Ȯ���ϱ� ���� �Ӱ谪
    private float lastTriggerInput; // ���� �������� triggerInput ��
    private float triggerStabilityTimer; // triggerInput�� �������� üũ�ϴ� Ÿ�̸�
    private float currentRotationSpeed; // ���� ȸ�� �ӵ�


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
}
