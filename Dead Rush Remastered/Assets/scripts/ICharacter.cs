using System.Collections;
using UnityEngine;

public   interface ICharacter
    {
         void OnFire();
    void ShowAcidEffect();
    IEnumerator FireEffect();
    IEnumerator AcidEffectTick();

}
