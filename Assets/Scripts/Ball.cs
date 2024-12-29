using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class Ball : MonoBehaviour
{
    public static Ball Instance;
    public bool hasStarted = false;
    bool isSpeed;
    bool pick;
    Rigidbody rb;


    [Header("Object Parameters")]
    public string colorName;
    public bool isBigger;
    public bool isCombo;
    public bool isColorCombo;

    [Header("VFX")]
    public GameObject VFX;
    [SerializeField] GameObject VFXCollision;
    [SerializeField] GameObject VFXHealth;
    [SerializeField] GameObject VFXShip;
    [SerializeField] GameObject VFXFreeze;
    [SerializeField] Transform VFXContainer;
    GameObject freeze;


    [Header("Other")]
    [SerializeField] FeaturesTop Features;
    Vector3 smallSize;
    Vector3 originalSize;
    [SerializeField] GameObject effectColors;
    public GameObject characterModel;

    [Header("Character")]
    public bool isShip;
    Animator animator;
    void Awake(){
        Instance = this;
    }

    void Start()
    {
       characterModel = gameObject.transform.GetChild(0).gameObject;
        rb = GetComponent<Rigidbody>();
        isSpeed = false;
        colorName = characterModel.GetComponent<MeshRenderer>().material.name.Replace(" (Instance)", "");
        smallSize = new Vector3(0.5f, 0.1f, 0.5f);
        originalSize = new Vector3(0.5f, 0.5f, 0.5f);
        transform.localScale = originalSize;
        animator = characterModel.GetComponent<Animator>();
    }


    public void GetModel()
    {
        characterModel = gameObject.transform.GetChild(0).gameObject;
        Material m = Wall.Instance.ColorsBlock[0];
        characterModel.GetComponent<Renderer>().material = m;
        colorName = m.name.Replace(" (Instance)", "");
        GetComponent<Rigidbody>().isKinematic = false;
        gameObject.SetActive(true);
        if (isColorCombo)
        {
            unFreeze();
        }
        isShip = PlayerPrefs.GetInt("ship") == 1 ? true : false;
    }


    void GetColor()
    {
        characterModel = gameObject.transform.GetChild(0).gameObject;
        int r = Random.Range(0, Wall.Instance.ColorsBlock.Length);
        Material m = Wall.Instance.ColorsBlock[r];
        characterModel.GetComponent<Renderer>().material = m;
        colorName = m.name.Replace(" (Instance)", "");
    }

     private void OnTriggerEnter(Collider collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Size":
                Features.Show(3);
                StartCoroutine(Grayscale());
                Character.Instance.SoundCollision();
                GameObject vSize = Instantiate(VFXCollision, transform.position, transform.rotation);
                break;
            case "Apple":
                Features.Show(4);
                GameStatus.Instance.LivesAdd();
                Character.Instance.SoundHealthUp();
                PopManager.Instance.showHealth();
                GameObject vhealth = Instantiate(VFXHealth, transform.position, transform.rotation);
                break;
            case "Battery":
                Features.Show(0);
                StartCoroutine(speedUp(2f));
                Character.Instance.SoundCollision();
                GameObject vbattery = Instantiate(VFXCollision, transform.position, transform.rotation);
                break;
            case "startJump":
                Features.Show(0);
                StartCoroutine(speedUp(1.5f));
                Character.Instance.SoundCollision();
                GameObject vBattery = Instantiate(VFXCollision, transform.position, transform.rotation);
                break;
            case "crystal":
                Features.Show(2);
                GameObject vcrystal = Instantiate(VFXCollision, transform.position, transform.rotation);
                Material diamondColor = collision.gameObject.transform.parent.gameObject.GetComponent<MeshRenderer>().material;
                characterModel.GetComponent<MeshRenderer>().material = diamondColor;
                colorName = diamondColor.name.Replace(" (Instance)", "");
                Character.Instance.SoundCrystal();
                break;
            case "bill" when isSpeed == false:
                Features.Show(7);
                StartCoroutine(speedUp(0.5f));
                Character.Instance.SoundCollision();
                PopManager.Instance.showSlow();
                GameObject vBill = Instantiate(VFXCollision, transform.position, transform.rotation);
                break;
            case "ColorCombo" when isColorCombo != true:
                Features.Show(5);
                StartCoroutine(ColorCombo());
                Character.Instance.SoundCollision();
                GameObject vColorCombo = Instantiate(VFXCollision, transform.position, transform.rotation);
                break;
            case "timer":
                Features.Show(1);
                GameStatus.Instance.TimerAdd();
                PopManager.Instance.showTime();
                Character.Instance.SoundCollision();
                GameObject vtimer = Instantiate(VFXCollision, transform.position, transform.rotation);
                break;
            case "Ship" when isShip != true:
                Features.Show(8);
                PopManager.Instance.showTakes();
                GameStatus.Instance.LivesRemove();
                Debug.Log("Ship");
                isBigger = false;
                Character.Instance.SoundHealthDown();
                GameObject vship = Instantiate(VFXShip, transform.position, transform.rotation);
                break;
            case "Bowling":
                Features.Show(6);
                GameStatus.Instance.LivesRemove();
                Debug.Log("Bowling");
                Character.Instance.SoundHealthDown();
                isBigger = false;
                PopManager.Instance.showTakes();
                GameObject vShip = Instantiate(VFXShip, transform.position, transform.rotation);
                break;
        }
    }

    public void OnColl()
    {
        if (isColorCombo)
        {
            GetColor();
        }
        StartCoroutine(PickUp());
        VFXColl();
        PlayAnim();
    }

    void PlayAnim()
    {
        animator.Rebind();
        animator.SetInteger("JumpIndex", Random.Range(0,4));
        animator.SetTrigger("Jump");
    }

    private IEnumerator ColorCombo()
    {
        isColorCombo = true;
        VFXFreezeCol();
        yield return new WaitForSeconds(5f);
        unFreeze();
    }

    void unFreeze()
    {
        VFXFreezeColRemove();
        isColorCombo = false;
    }

    public IEnumerator Grayscale()
    {
        effectColors.GetComponent<PostProcessVolume>().enabled = true;
        yield return new WaitForSeconds(4f);
        effectColors.GetComponent<PostProcessVolume>().enabled = false;
    }

    private IEnumerator speedUp(float i)
    {
        isSpeed = true;
        GameControllerButton.Instance.speedUp = true;
        GameControllerButton.Instance.speed *= i;
        yield return new WaitForSeconds(5f);
        GameControllerButton.Instance.speedUp = false;
        GameControllerButton.Instance.speed = GameControllerButton.Instance.levelSpeed;
        isSpeed = false;
    }

    public void Water()
    {
        Character.Instance.GoToWater();
    }

    private IEnumerator PickUp()
    {
        float timeElapsed = 0;
        float duration = 0.5f; 
        Vector3 startScale = transform.localScale;
        Vector3 endScale = originalSize;

        while (timeElapsed < duration)
        {
            transform.localScale = Vector3.Lerp(startScale, endScale, timeElapsed / duration);
            timeElapsed += Time.deltaTime;

            yield return null;
        }
    }

    public void VFXColl()
    {
        GameObject sparkles = Instantiate(VFXCollision, transform.position, transform.rotation);
    }
    public void VFXHealthCol()
    {
        GameObject sparkles = Instantiate(VFXShip, transform.position, transform.rotation);
    }
    public void VFXFreezeCol()
    {
        freeze = Instantiate(VFXFreeze, transform.position, transform.rotation);
        freeze.transform.parent = VFXContainer;
    }
    public void VFXFreezeColRemove()
    {
        Destroy(freeze);
    }


}
