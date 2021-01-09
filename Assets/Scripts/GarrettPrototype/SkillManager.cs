using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
    [SerializeField] private ServePassSimulation AservePass = null;
    [SerializeField] private PassSetSimulation ApassSet = null;
    [SerializeField] private SetAttackSimulation AsetAttack = null;
    [SerializeField] private AttackDefenceSimulation AattackDefence = null;
    [SerializeField] private Slider aSlider = null;

    [SerializeField] private ServePassSimulation BservePass = null;
    [SerializeField] private PassSetSimulation BpassSet = null;
    [SerializeField] private SetAttackSimulation BsetAttack = null;
    [SerializeField] private AttackDefenceSimulation BattackDefence = null;
    [SerializeField] private Slider bSlider = null;

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
        PlayerP1.serve = DifficultyTracker.currentDifficulty.player;
        PlayerP1.pass = DifficultyTracker.currentDifficulty.player;
        PlayerP1.set = DifficultyTracker.currentDifficulty.player;
        PlayerP1.attack = DifficultyTracker.currentDifficulty.player;
        PlayerP1.block = DifficultyTracker.currentDifficulty.player;
        PlayerP1.defence = DifficultyTracker.currentDifficulty.player;

        PlayerP2.serve = DifficultyTracker.currentDifficulty.player;
        PlayerP2.pass = DifficultyTracker.currentDifficulty.player;
        PlayerP2.set = DifficultyTracker.currentDifficulty.player;
        PlayerP2.attack = DifficultyTracker.currentDifficulty.player;
        PlayerP2.block = DifficultyTracker.currentDifficulty.player;
        PlayerP2.defence = DifficultyTracker.currentDifficulty.player;

        PlayerM1.serve = DifficultyTracker.currentDifficulty.player;
        PlayerM1.pass = DifficultyTracker.currentDifficulty.player;
        PlayerM1.set = DifficultyTracker.currentDifficulty.player;
        PlayerM1.attack = DifficultyTracker.currentDifficulty.player;
        PlayerM1.block = DifficultyTracker.currentDifficulty.player;
        PlayerM1.defence = DifficultyTracker.currentDifficulty.player;

        PlayerM2.serve = DifficultyTracker.currentDifficulty.player;
        PlayerM2.pass = DifficultyTracker.currentDifficulty.player;
        PlayerM2.set = DifficultyTracker.currentDifficulty.player;
        PlayerM2.attack = DifficultyTracker.currentDifficulty.player;
        PlayerM2.block = DifficultyTracker.currentDifficulty.player;
        PlayerM2.defence = DifficultyTracker.currentDifficulty.player;

        PlayerS.serve = DifficultyTracker.currentDifficulty.player;
        PlayerS.pass = DifficultyTracker.currentDifficulty.player;
        PlayerS.set = DifficultyTracker.currentDifficulty.player;
        PlayerS.attack = DifficultyTracker.currentDifficulty.player;
        PlayerS.block = DifficultyTracker.currentDifficulty.player;
        PlayerS.defence = DifficultyTracker.currentDifficulty.player;

        PlayerRS.serve = DifficultyTracker.currentDifficulty.player;
        PlayerRS.pass = DifficultyTracker.currentDifficulty.player;
        PlayerRS.set = DifficultyTracker.currentDifficulty.player;
        PlayerRS.attack = DifficultyTracker.currentDifficulty.player;
        PlayerRS.block = DifficultyTracker.currentDifficulty.player;
        PlayerRS.defence = DifficultyTracker.currentDifficulty.player;

        AIP1.serve = DifficultyTracker.currentDifficulty.ai;
        AIP1.pass = DifficultyTracker.currentDifficulty.ai;
        AIP1.set = DifficultyTracker.currentDifficulty.ai;
        AIP1.attack = DifficultyTracker.currentDifficulty.ai;
        AIP1.block = DifficultyTracker.currentDifficulty.ai;
        AIP1.defence = DifficultyTracker.currentDifficulty.ai;

        AIP2.serve = DifficultyTracker.currentDifficulty.ai;
        AIP2.pass = DifficultyTracker.currentDifficulty.ai;
        AIP2.set = DifficultyTracker.currentDifficulty.ai;
        AIP2.attack = DifficultyTracker.currentDifficulty.ai;
        AIP2.block = DifficultyTracker.currentDifficulty.ai;
        AIP2.defence = DifficultyTracker.currentDifficulty.ai;

        AIM1.serve = DifficultyTracker.currentDifficulty.ai;
        AIM1.pass = DifficultyTracker.currentDifficulty.ai;
        AIM1.set = DifficultyTracker.currentDifficulty.ai;
        AIM1.attack = DifficultyTracker.currentDifficulty.ai;
        AIM1.block = DifficultyTracker.currentDifficulty.ai;
        AIM1.defence = DifficultyTracker.currentDifficulty.ai;

        AIM2.serve = DifficultyTracker.currentDifficulty.ai;
        AIM2.pass = DifficultyTracker.currentDifficulty.ai;
        AIM2.set = DifficultyTracker.currentDifficulty.ai;
        AIM2.attack = DifficultyTracker.currentDifficulty.ai;
        AIM2.block = DifficultyTracker.currentDifficulty.ai;
        AIM2.defence = DifficultyTracker.currentDifficulty.ai;

        AIS.serve = DifficultyTracker.currentDifficulty.ai;
        AIS.pass = DifficultyTracker.currentDifficulty.ai;
        AIS.set = DifficultyTracker.currentDifficulty.ai;
        AIS.attack = DifficultyTracker.currentDifficulty.ai;
        AIS.block = DifficultyTracker.currentDifficulty.ai;
        AIS.defence = DifficultyTracker.currentDifficulty.ai;

        AIRS.serve = DifficultyTracker.currentDifficulty.ai;
        AIRS.pass = DifficultyTracker.currentDifficulty.ai;
        AIRS.set = DifficultyTracker.currentDifficulty.ai;
        AIRS.attack = DifficultyTracker.currentDifficulty.ai;
        AIRS.block = DifficultyTracker.currentDifficulty.ai;
        AIRS.defence = DifficultyTracker.currentDifficulty.ai;
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

    
}
