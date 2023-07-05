using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Infinite : MonoBehaviour
{
  
    public string keycode = string.Empty;

    [Header("�̻��� ��� ����")]
    public float m_speed = 2; // �̻��� �ӵ�.

    public GameObject m_missilePrefab; // �̻��� ������.
    public GameObject m_target; // ���� ����.
    [Space(10f)]
    public float m_distanceFromStart = 6.0f; // ���� ������ �������� �󸶳� ������.
    public float m_distanceFromEnd = 3.0f; // ���� ������ �������� �󸶳� ������.
    [Space(10f)]
    public int m_shotCount = 12; // �� �� �� �߻��Ұ���.
    [Range(0, 1)] public float m_interval = 0.15f;
    public int m_shotCountEveryInterval = 2; // �ѹ��� �� ���� �߻��Ұ���.

   

    public void KeySet(string value)
    {
        this.keycode = value;
    }
 
    private void Update()
    {
        if(this.keycode!=string.Empty)
        {
            if (Input.GetButtonDown(this.keycode))
            {
                // Shot.
                StartCoroutine(CreateMissile());
            }
        }
        
    }

    IEnumerator CreateMissile()
    {
        int _shotCount = m_shotCount;
        while (_shotCount > 0)
        {
            for (int i = 0; i < m_shotCountEveryInterval; i++)
            {
                if (_shotCount > 0)
                {
                    GameObject missile = Instantiate(m_missilePrefab);
                  //  int randomvalue = Random.RandomRange(0, player.Behind.Length);
                   // missile.GetComponent<Skill_InfiniteMissile>().Init(player.Behind[randomvalue], m_target.transform, m_speed, m_distanceFromStart, m_distanceFromEnd);

                    _shotCount--;
                }
            }
            yield return new WaitForSeconds(m_interval);
        }
        yield return null;
    }
}