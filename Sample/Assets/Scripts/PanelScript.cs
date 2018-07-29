using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelScript: MonoBehaviour{

    public Slider HP_bar;
    public Slider MP_bar;

    public Text HP_txt;
    public Text MP_txt;

	// Use this for initialization
    public void initiallize(int HP_max, int MP_max)
    {
        HP_bar.maxValue = HP_max;
        MP_bar.maxValue = MP_max;
    }
    public void setHP(int hp)
    {
        HP_bar.value = hp;
        match();
    }
    public void setMP(int mp)
    {
        MP_bar.value = mp;
        match();
    }
    private void match()
    {
        HP_txt.text = (int)HP_bar.value + "/" + (int)HP_bar.maxValue;
        MP_txt.text = (int)MP_bar.value + "/" + (int)MP_bar.maxValue;
    }

}