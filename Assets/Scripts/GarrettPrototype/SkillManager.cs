using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
    [SerializeField] private ServePassSimulation AservePass;
    [SerializeField] private PassSetSimulation ApassSet;
    [SerializeField] private SetAttackSimulation AsetAttack;
    [SerializeField] private AttackDefenceSimulation AattackDefence;
    [SerializeField] private Slider aSlider;

    [SerializeField] private ServePassSimulation BservePass;
    [SerializeField] private PassSetSimulation BpassSet;
    [SerializeField] private SetAttackSimulation BsetAttack;
    [SerializeField] private AttackDefenceSimulation BattackDefence;
    [SerializeField] private Slider bSlider;

    [SerializeField] [Range(75, 125)] private int aServe;
    [SerializeField] [Range(75, 125)] private int aPass;
    [SerializeField] [Range(75, 125)] private int aSet;
    [SerializeField] [Range(75, 125)] private int aAttack;
    [SerializeField] [Range(75, 125)] private int aBlock;
    [SerializeField] [Range(75, 125)] private int aDefence;

    [SerializeField] [Range(75, 125)] private int bServe;
    [SerializeField] [Range(75, 125)] private int bPass;
    [SerializeField] [Range(75, 125)] private int bSet;
    [SerializeField] [Range(75, 125)] private int bAttack;
    [SerializeField] [Range(75, 125)] private int bBlock;
    [SerializeField] [Range(75, 125)] private int bDefence;

    public GameObject M1Sliders;
    public GameObject P2Sliders;
    public GameObject SetterSliders;
    public GameObject P1Sliders;
    public GameObject M2Sliders;
    public GameObject RsSliders;

    public PlayerSkills AIP1;
    public PlayerSkills AIP2;
    public PlayerSkills AIM1;
    public PlayerSkills AIM2;
    public PlayerSkills AIS;
    public PlayerSkills AIRS;

    public PlayerSkills PlayerP1;
    public PlayerSkills PlayerP2;
    public PlayerSkills PlayerM1;
    public PlayerSkills PlayerM2;
    public PlayerSkills PlayerS;
    public PlayerSkills PlayerRS;


    public struct PlayerSkills
    {
        public float serve;
        public float pass;
        public float set;
        public float attack;
        public float block;
        public float defence;
    }


    // Start is called before the first frame update
    void Start()
    {
        AIP1.serve = 100;
        AIP1.pass = 100;
        AIP1.set = 100;
        AIP1.attack = 100;
        AIP1.block = 100;
        AIP1.defence = 100;

        AIP2.serve = 100;
        AIP2.pass = 100;
        AIP2.set = 100;
        AIP2.attack = 100;
        AIP2.block = 100;
        AIP2.defence = 100;

        AIM1.serve = 100;
        AIM1.pass = 100;
        AIM1.set = 100;
        AIM1.attack = 100;
        AIM1.block = 100;
        AIM1.defence = 100;

        AIM2.serve = 100;
        AIM2.pass = 100;
        AIM2.set = 100;
        AIM2.attack = 100;
        AIM2.block = 100;
        AIM2.defence = 100;

        AIS.serve = 100;
        AIS.pass = 100;
        AIS.set = 100;
        AIS.attack = 100;
        AIS.block = 100;
        AIS.defence = 100;

        AIRS.serve = 100;
        AIRS.pass = 100;
        AIRS.set = 100;
        AIRS.attack = 100;
        AIRS.block = 100;
        AIRS.defence = 100;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPlayersTeamSkills()
    {
        PlayerRS = GetPlayerSkills(RsSliders);
        PlayerS = GetPlayerSkills(SetterSliders);
        PlayerP1 = GetPlayerSkills(P1Sliders);
        PlayerP2 = GetPlayerSkills(P2Sliders);
        PlayerM1 = GetPlayerSkills(M1Sliders);
        PlayerM2 = GetPlayerSkills(M2Sliders);
    }

    public PlayerSkills GetPlayerSkills(GameObject SliderObject)
    {
        PlayerSkills playerSkills;
        playerSkills.pass = SliderObject.transform.Find("PassSlider").GetComponent<Slider>().value;
        playerSkills.set = SliderObject.transform.Find("SetSlider").GetComponent<Slider>().value;
        playerSkills.attack = SliderObject.transform.Find("AttackSlider").GetComponent<Slider>().value;
        playerSkills.block = SliderObject.transform.Find("BlockSlider").GetComponent<Slider>().value;
        playerSkills.defence = SliderObject.transform.Find("DefenceSlider").GetComponent<Slider>().value;
        playerSkills.serve = SliderObject.transform.Find("ServeSlider").GetComponent<Slider>().value;
        return playerSkills;
    }

    public void SetGlobalSkillValuesA()
    {
        Debug.Log("Setting A team skill values to " + aSlider.value);
        AservePass.SetServeAbility(aSlider.value);
        AservePass.SetPassAbility(aSlider.value);
        ApassSet.SetSettingAbility(aSlider.value);
        AsetAttack.SetAttackAbility(aSlider.value);
        AattackDefence.SetAttackAbility(aSlider.value);
        AattackDefence.SetBlockAbility(aSlider.value);
        AattackDefence.SetDefenceAbility(aSlider.value);
    }

    public void SetGlobalSkillValuesB()
    {
        Debug.Log("Setting B team skill values to " + bSlider.value);
        BservePass.SetServeAbility(bSlider.value);
        BservePass.SetPassAbility(bSlider.value);
        BpassSet.SetSettingAbility(bSlider.value);
        BsetAttack.SetAttackAbility(bSlider.value);
        BattackDefence.SetAttackAbility(bSlider.value);
        BattackDefence.SetBlockAbility(bSlider.value);
        BattackDefence.SetDefenceAbility(bSlider.value);
    }

    public void SetSpecificSkillValues()
    {
        Debug.Log("Setting specific team skill values");

        AservePass.SetServeAbility(aServe);
        AservePass.SetPassAbility(aPass);
        ApassSet.SetSettingAbility(aSet);
        AsetAttack.SetAttackAbility(aAttack);
        AattackDefence.SetAttackAbility(aAttack);
        AattackDefence.SetBlockAbility(aBlock);
        AattackDefence.SetDefenceAbility(aDefence);

        BservePass.SetServeAbility(bServe);
        BservePass.SetPassAbility(bPass);
        BpassSet.SetSettingAbility(bSet);
        BsetAttack.SetAttackAbility(bAttack);
        BattackDefence.SetAttackAbility(bAttack);
        BattackDefence.SetBlockAbility(bBlock);
        BattackDefence.SetDefenceAbility(bDefence);
    }

    
}
