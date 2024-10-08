using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth Instance {  get; private set; }

    public static int Maxhealth;
    public int Currenthealth;

    public PlayerHealthUI healthUI;

    // Tudo sobre o shield e sua relacao com a vida
    public static bool hasShildUp, canShield, shieldBroken;
    public GameObject shield;
    float TimeToReDo;
    public static float TimeToShieldRemake = 20f;

    // tudo sobre o escudo e sua realcao com a vida
    public static bool hasArmorUp;
    public static int percentOfProtection;

    // tudo sobre o espelho e sua relacao com a vida
    public static bool hasMirrorUp;

    // Tudo sobre Patua e como ele desvia o dano do adversario
    bool hasPatuaUP;
    public static int chanceForLiving;

    // Tudo sobre Stamina e sua relacao com a vida
    public static bool deadByStamina;

    // Onde se encaixa o flashSprite
    public FleashMaterial fleashMaterialScript;


    // freeze no momento do Dano
    [Space]
    [Header("Freeze When Attacked")]
    public float durationFreeze;
    bool _isFrozen = false;
    float _pendingFreezeDuration = 0f;
    bool _isThereMonsters;

    //CineMachineCamera
    [Space]
    [Header("Camera Shake")]
    public CinemachineVirtualCamera cinemachineVirtualCamera;
    private float shakeTimer;
    private CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin;
    public float intencidadeDoShake;
    public float duracaoDoShake;
    public static bool shackCamera;

    // bool que indica se foi atingido ou nem
    public static bool _HasbeenHit;


    public static bool isAlive;

    [Space]
    public GameObject DeathPanel;

    public static int HealthRegen;
    public float TimetoRegenarateHealth;

   
    bool canTakeaDamage = true;
    public float TimeToTakeDamage;

    public static bool setMaxHealth;

    [Space]
    public Animator anim;

    [Space]
    public float timeOfPoison;
    public bool poisoned;
    public bool hasbeenPoisoned;
    public int HitsPoisoned;
    

    void Start()
    {
        Instance = this;
        Maxhealth = GameManager.PlayerMaxhealth;
        Currenthealth = Maxhealth;
        HealthRegen = 2;
        TimetoRegenarateHealth = 3.0f;
        healthUI.SetMaxHealth(Maxhealth);
        isAlive = true;
    }

    
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.R) && canShield && !shieldBroken) 
        { 
          hasShildUp = true;
        }

        if(hasShildUp)
        {
            shield.SetActive(true);
        }
        if(!hasShildUp)
        {
            shield.SetActive(false);
        }

        if(shieldBroken) 
        { 
            TimeToReDo += Time.deltaTime;

            if(TimeToReDo >= TimeToShieldRemake)
            {
                shieldBroken = false;
                TimeToReDo = 0;
            }
        }

       if(Currenthealth <= 0)
        {
            Dead();
        }

       if(isAlive)
        {
            TimetoRegenarateHealth -= Time.deltaTime;
            if(TimetoRegenarateHealth <= 0f && Currenthealth < Maxhealth)
            {
                Currenthealth += HealthRegen;
                if(Currenthealth > Maxhealth) { Currenthealth = Maxhealth; }
                healthUI.SetHealth(Currenthealth);
                TimetoRegenarateHealth = 3.5f;
            }
        }

       if(!canTakeaDamage)
        {
            TimeToTakeDamage += Time.deltaTime;

            if(TimeToTakeDamage >= 1f)
            {
                canTakeaDamage = true;
            }
        }

       if(setMaxHealth)
        {
            healthUI.SetMaxHealth(Maxhealth);
            setMaxHealth = false;
        }

       if(deadByStamina && PlayerMovement.isGrounded)
        {
            Dead();
        }

        if (_pendingFreezeDuration > 0f && !_isFrozen)
        {

            StartCoroutine(SetTimeForAtackEffect());
        }

        if(shackCamera)
        {
            PlayerHealth.Instance.StartCoroutine(ShackCamera(5, 0.5f));
            shackCamera = false;
        }

        if (poisoned) StartCoroutine(Poisoned());
    }

    IEnumerator Poisoned()
    {
        int originalhitpoisoned = HitsPoisoned;
        int damage = 2;
        poisoned = false;
        TakeDamageFromPoison(damage);

        yield return new WaitForSeconds(timeOfPoison);

        if (HitsPoisoned > originalhitpoisoned)
        {
            damage += 3;
            originalhitpoisoned = HitsPoisoned;
        }
        TakeDamageFromPoison(damage);

        yield return new WaitForSeconds(timeOfPoison);

        if (HitsPoisoned > originalhitpoisoned)
        {
            damage += 3;
            originalhitpoisoned = HitsPoisoned;
        }
        TakeDamageFromPoison(damage);

        yield return new WaitForSeconds(timeOfPoison);

        if (HitsPoisoned > originalhitpoisoned)
        {
            damage += 3;
            originalhitpoisoned = HitsPoisoned;
        }
        TakeDamageFromPoison(damage);
        damage = 2;
        HitsPoisoned = 0;
        hasbeenPoisoned = false;
    }

    public void TakeDamageFromPoison(int damage)
    {
        Debug.Log("POsion : " + damage);
        Currenthealth -= damage;
        healthUI.SetHealth(Currenthealth);
        canTakeaDamage = false;
        fleashMaterialScript.FlashPoison();
    }

    public void TakeDamage(int damage)
    {
        if (!Downdash._isDownDash)
        {
            int chanceOfLive = Random.Range(0, 100);
            if (chanceOfLive <= chanceForLiving) hasPatuaUP = true;
            _HasbeenHit = true;

            if (canTakeaDamage && !hasPatuaUP)
            {
                anim.SetBool("Attack1", false);
                anim.SetBool("Attack2", false);
                if (hasArmorUp)
                {
                    int damageReflet = (damage / 100) * percentOfProtection;
                    damage -= damageReflet;
                    Currenthealth -= damage;
                    healthUI.SetHealth(Currenthealth);
                    canTakeaDamage = false;
                }
                else
                {
                    Currenthealth -= damage;
                    healthUI.SetHealth(Currenthealth);
                    canTakeaDamage = false;
                }
                fleashMaterialScript.Flash();
            }
            hasPatuaUP = false;
            PlayerHealth.Instance.StartCoroutine(ShackCamera(intencidadeDoShake, duracaoDoShake));
            Freeze();
            PlayerAttack.canAtack = true;
        }
    }

    public void Freeze()
    {
        _pendingFreezeDuration = durationFreeze;
    }

    IEnumerator SetTimeForAtackEffect()
    {
        _isFrozen = true;
        var original = Time.timeScale;
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(durationFreeze);
        Time.timeScale = original;
        _pendingFreezeDuration = 0f;
        _isFrozen = false;
    }

    IEnumerator ShackCamera(float intencity, float time)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intencity;
        shakeTimer = time;
        yield return new WaitForSeconds(shakeTimer);
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
    }

    public static void LibertarKiumbas()
    {
        Maxhealth += 25;
        setMaxHealth = true;
    }

    public static void ArmorUP()
    {
        hasArmorUp = true;
        percentOfProtection += 2;
    }

    public static void SetHabilitStatus()
    {
        float TimePercentOfShield = (TimeToShieldRemake / 100) * 10;

        if(TimeToShieldRemake > 10f) TimeToShieldRemake -= TimePercentOfShield;

        Debug.Log("tempo do shield: " +  TimeToShieldRemake);
    }

    public static void SetMirror()
    {
        hasMirrorUp = true;
        EnemyDamage.PercentOfDamage += 5;
    }

    public static void ChanceOfPatua()
    {
        chanceForLiving += 2;
    }


    public void CurarPorRespown()
    {
        Currenthealth = Maxhealth;
        healthUI.SetHealth(Currenthealth);
    }

    public void Dead()
    {
        isAlive = false;
        GameManager.IsInMainScene = true;
        StartCoroutine(ShowUI("Terreiro"));
        GameManager.MapsPassed = 0;
    }

    public IEnumerator ShowUI(string scene)
    {
        yield return new WaitForSeconds(1f);
        FadeScript.ShowUI();
        yield return new WaitForSeconds(1f);
        PlayerMovement.isInFinalScene = true; 
        deadByStamina = false;
        SceneChange.SceneToChangeMusic = scene;
        AudioManager.hasChangedscene = true;
        Destroy(gameObject);
        SceneManager.LoadScene(scene);
    }
}
