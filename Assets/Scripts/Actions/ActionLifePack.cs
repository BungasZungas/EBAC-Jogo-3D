using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Itens;

public class ActionLifePack : MonoBehaviour
{
    public KeyCode keyCode = KeyCode.L;
    public SOInt soInt;

    [SerializeField] private GameObject explainText;

    private void Start()
    {
        soInt = ItemManager.Instance.GetItemByType(ItemType.LIFE_PACK).soInt;
    }

    private void RecoverLife()
    {
        if(soInt.value > 0)
        {
            ItemManager.Instance.RemoveByType(ItemType.LIFE_PACK);
            Player.Instance.healthBase.ResetLife();
            explainText.SetActive(true);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(keyCode))
        {
            RecoverLife();
        }
    }
}
