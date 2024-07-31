using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShootLimit : GunBase
{
    public List<UIFillUpdater> uiGunUpdaters;
    public float maxShoot = 5f;
    public float timeToRecharge = 1f;

    private float _currentShoots;
    private bool _recharging = false;

    private void Awake()
    {
        GetAllUIs();
    }

    protected override IEnumerator ShootCoroutine()
    {
        if (_recharging) yield break;

        while (true)
        {
            if (_currentShoots < maxShoot && Time.time >= lastShootTime + timeBetweenShoot)
            {
                Shoot();
                _currentShoots++;
                lastShootTime = Time.time;
                CheckRecharge();
                UpdateUI();
                yield return new WaitForSeconds(timeBetweenShoot);
            }
            else if (_currentShoots >= maxShoot)
            {
                yield break;
            }
            else
            {
                yield return null; 
            }
        }
    }

    private void CheckRecharge()
    {
        if (_currentShoots >= maxShoot && !_recharging)
        {
            StopShoot();
            StartRecharge();
        }
    }

    private void StartRecharge()
    {
        _recharging = true;
        StartCoroutine(RechargeCoroutine());
    }

    private IEnumerator RechargeCoroutine()
    {
        float time = 0;
        while (time < timeToRecharge)
        {
            time += Time.deltaTime;
            uiGunUpdaters.ForEach(i => i.UpdateValue(time / timeToRecharge));
            yield return new WaitForEndOfFrame();
        }

        _currentShoots = 0;
        _recharging = false;
    }

    private void UpdateUI()
    {
        uiGunUpdaters.ForEach(i => i.UpdateValue(maxShoot, _currentShoots));
    }

    private void GetAllUIs()
    {
        uiGunUpdaters.Clear();
        UIFillUpdater gunUI = GameObject.Find("Reload UI").GetComponent<UIFillUpdater>();
        uiGunUpdaters.Add(gunUI);
    }
}
