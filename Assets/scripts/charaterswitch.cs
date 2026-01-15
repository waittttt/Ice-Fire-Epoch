using UnityEngine;
using UnityEngine.UI;

public class CharacterSwitcher : MonoBehaviour
{
    public GameObject fireCharacter;  // 火人
    public GameObject waterCharacter; // 水人

    public Image avatarImage;
    public Image IconImage;
    public Sprite fireAvatar;
    public Sprite waterAvatar;
    public Sprite fireIcon;
    public Sprite waterIcon;

    public camerafollow cameraFollow; // 摄像机跟随脚本

    public GameObject switchToFireEffectPrefab;
    public GameObject switchToWaterEffectPrefab;

    private GameObject activeCharacter; // 当前激活的角色
    private AudioSource audioSource;
    public AudioClip switchSound;

    private void Start()
    {
        // 默认激活火人，禁用水人
        fireCharacter.SetActive(true);
        waterCharacter.SetActive(false);
        activeCharacter = fireCharacter;
        UpdateAvatarImage(fireAvatar);
        UpdateIconImage(fireIcon);

        // 设置摄像机的目标为火人
        if (cameraFollow != null)
        {
            cameraFollow.target = fireCharacter.transform;
        }
        audioSource=GetComponent<AudioSource>();
    }

    private void Update()
    {
        // 检测按下Q键
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SwitchCharacter();
        }
    }

    private void SwitchCharacter()
    {
        PlaySwitchSound();
        // 获取当前激活角色的位置和旋转
        Vector3 currentPosition = activeCharacter.transform.position;
        Quaternion currentRotation = activeCharacter.transform.rotation;

        // 切换角色
        if (activeCharacter == fireCharacter)
        {
            PlaySwitchEffect(currentPosition,switchToWaterEffectPrefab);
            fireCharacter.SetActive(false);
            waterCharacter.SetActive(true);
            activeCharacter = waterCharacter;
            UpdateAvatarImage(waterAvatar);
            UpdateIconImage(waterIcon);
        }
        else
        {
            PlaySwitchEffect(currentPosition,switchToFireEffectPrefab);
            waterCharacter.SetActive(false);
            fireCharacter.SetActive(true);
            activeCharacter = fireCharacter;
            UpdateAvatarImage(fireAvatar);
            UpdateIconImage(fireIcon);
        }

        // 将新角色的位置和旋转设置为当前的位置和旋转
        activeCharacter.transform.position = currentPosition;
        activeCharacter.transform.rotation = currentRotation;

        // 更新摄像机跟随目标
        if (cameraFollow != null)
        {
            cameraFollow.target = activeCharacter.transform;
        }
    }
    private void PlaySwitchEffect(Vector3 position,GameObject switchEffectPrefab){
        if(switchEffectPrefab!=null){
            GameObject effect=Instantiate(switchEffectPrefab,position,Quaternion.identity);

            Destroy(effect,2f);
        }
    }
    private void PlaySwitchSound(){
        Debug.Log("PlaySwitchSound");
        Debug.Log("switchSound:"+switchSound+",audioSource:"+audioSource);
        if(switchSound!=null&&audioSource!=null){
            audioSource.clip = switchSound;
            audioSource.loop=false;
            audioSource.Play();
        }
    }
    void UpdateAvatarImage(Sprite newAvatar){
        if(avatarImage!=null){
            avatarImage.sprite=newAvatar;
        }
    }
    void UpdateIconImage(Sprite newIcon){
        if(IconImage!=null){
            IconImage.sprite=newIcon;
        }
    }
}
