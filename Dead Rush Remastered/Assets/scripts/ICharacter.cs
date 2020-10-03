using System.Collections;
using UnityEngine;

public interface ICharacter
{
    void OnFire();
    void ShowAcidEffect();
    void ShowFireEffect();
    void ShowDamageEffect();
    void Moving(Vector2 dir);
    IEnumerator FireEffect();
    IEnumerator AcidEffectTick();
    IEnumerator FireEffectTick();
    IEnumerator DamageEffectTick();

}
