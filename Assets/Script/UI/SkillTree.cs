using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTree : MonoBehaviour
{
    [SerializeField]
    public GameObject MousePrefab;
    [SerializeField]
    public List<RectTransform> SkiiHotKeys;

    public static SkillTree skilltree;
    public Canvas canvas;
    private void Awake()
    {
        if (skilltree == null)
        {
            skilltree = this;
           
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

}
