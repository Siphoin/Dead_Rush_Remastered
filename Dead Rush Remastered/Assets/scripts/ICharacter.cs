using System.Collections;
using UnityEngine;

public   interface ICharacter
    {
         void OnFire();
    void ShowAcidEffect();
    void Moving(Vector2 dir);
    IEnumerator FireEffect();
    IEnumerator AcidEffectTick();


}
