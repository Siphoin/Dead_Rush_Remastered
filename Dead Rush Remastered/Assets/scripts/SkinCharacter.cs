using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.scripts
{
 public   class SkinCharacter
    {
        private string name_skin;

        public SkinCharacter(string nameSkin)
        {
            name_skin = nameSkin;
        }

        public Sprite GetSkinState (StateCharacterType state)
        {
            string state_str = state.ToString();
            string path = "Skins/" + name_skin + "_" + state_str;
            return Resources.Load<Sprite>(path);
        }
    }
}
