using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cloth;

public class HealthBase : MonoBehaviour, IDamageable
{
    public float startLife;
    public bool destroyOnKill = false;
    public float currentLife;

    public Action<HealthBase> OnDamage;
    public Action<HealthBase> OnKill;

    public List<UIFillUpdater> uiGunUpdater;

    public float damageMultiply = 1f;

    private void Awake()
    {
        Init();
        UpdateUI();
    }

    public void Init()
    {
        SaveManager.Instance.InitializePlayerLife();
    }

    public void ResetLife()
    {
        currentLife = startLife;
        UpdateUI();
    }

    protected virtual void Kill()
    {
        if(destroyOnKill)
            Destroy(gameObject, 3f);

        OnKill?.Invoke(this);
    }

    [NaughtyAttributes.Button]
    public void Damage()
    {
        Damage(5);
        ShakeCamera.Instance.Shake();
    }

    public void Damage(float f)
    {
        currentLife -= f * damageMultiply;

        if(currentLife <= 0)
        {
            Kill();
        }
        UpdateUI();
        OnDamage?.Invoke(this);
    }

    public void Damage(float damage, Vector3 dir)
    {
        Damage(damage);
    }

    private void UpdateUI()
    {
        if(uiGunUpdater != null)
        {
            uiGunUpdater.ForEach(i => i.UpdateValue((float)currentLife / startLife));
        }
    }

    public void ChangeDamageMultiply(float damage, float duration)
    {
        StartCoroutine(ChangeDamageMultiplyCoroutine(damageMultiply, duration));
    }

    IEnumerator ChangeDamageMultiplyCoroutine(float damageMultiply, float duration)
    {
        this.damageMultiply = damageMultiply;
        yield return new WaitForSeconds(duration);
        this.damageMultiply = 1;
    }
}
