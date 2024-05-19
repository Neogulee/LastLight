using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Team { Player,Enemy}
public class DamageManager : MonoBehaviour
{
    [SerializeField]
    private int damageCount;
    [SerializeField]
    private GameObject damage;
    [SerializeField]
    private GameObject Cdamage;
    [SerializeField]
    private GameObject Heal;

    private int ind = 0;

    public void OnEnable()
    {
        Locator.damageManager = this;
        ShowDamage(transform,0,Team.Player);
        ShowCritDamage(transform,0,Team.Player);
        ShowHeal(transform,0,Team.Player);
    }
    public void ShowDamage(Transform T, int damage, Team team)
    {
        GameObject G = BattlePullingManager.InstantiatePulling(this.damage,transform);
        G.GetComponent<DamageEffect>().ShowDamage(T, damage, team);
    }
    public void ShowCritDamage(Transform T, int damage, Team team)
    {
        GameObject G =  BattlePullingManager.InstantiatePulling(this.Cdamage, transform);
        G.GetComponent<DamageEffect>().ShowCritDamage(T, damage,team);
    }
    public void ShowHeal(Transform T, int heal, Team team)
    {
        GameObject G = BattlePullingManager.InstantiatePulling(Heal, transform);
        G.GetComponent<DamageEffect>().ShowHeal(T, heal, team);
    }
}
