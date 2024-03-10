using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddButton : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Transform panel;
    [SerializeField]
    private GameObject Button;
    GameObject btn;
    void Awake()
    {
        for (int i = 0; i<8 ; i++)
        {
           //tao 8 cai button va gan vao panel
            btn = Instantiate(Button);
            btn.name = "" + i;
            btn.transform.SetParent(panel, false); //thanh phan con lay transform cua cha, neu true thi giu transform cua no
        }
    }
}
