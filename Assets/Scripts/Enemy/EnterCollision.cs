using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZUNGAS.StateMachine;
using DG.Tweening;

public class EnterCollision : MonoBehaviour
{
    [SerializeField] private GameObject Enemy;
    [SerializeField] private GameObject trigger;
    [SerializeField] private string tagToCheck = "Player";

    public bool startWithAnimation = true;

    [SerializeField] private float timeToScale = .2f;
    private Ease scaleEase = Ease.OutBounce;

    private void Awake()
    {
        Enemy.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tagToCheck))
        {
            Enemy.SetActive(true);
            trigger.SetActive(false);

            if (startWithAnimation == true)
            {
                Enemy.transform.localScale = Vector3.zero;
                Enemy.transform.DOScale(Vector3.one, timeToScale).SetEase(scaleEase);
            }
        }
    }
}
