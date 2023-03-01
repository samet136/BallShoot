using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("----TOP AYARLARI")]
    [SerializeField] private GameObject[] Toplar;
    [SerializeField] private GameObject FirePoint;
    [SerializeField] private float TopGucu;
    int AktifTopIndex;
    [SerializeField] private Animator _TopAtar;
    [SerializeField] private ParticleSystem TopAtmaEfekt;
    [SerializeField] private ParticleSystem[] TopEfektleri;
    int AktifTopEfektIndex;
    [SerializeField] private AudioSource[] TopSesleri;
    int AktifTopSesIndex;

    [Header("----LEVEL AYARLARI")]
    [SerializeField] private int HedefTopSayisi;
    [SerializeField] private int MevcutTopSayisi;
    int GirenTopSayisi;
    [SerializeField] private Slider LevelSlider;
    [SerializeField] private TextMeshProUGUI KalanTopSayisi_Text;

    [Header("----UI AYARLARI")]
    [SerializeField] private GameObject[] Paneller;
    [SerializeField] private TextMeshProUGUI YildizSayisi;
    [SerializeField] private TextMeshProUGUI Kazandin_LevelSayisi;
    [SerializeField] private TextMeshProUGUI Kaybettin_LevelSayisi;

    [Header("----DİGER AYARLAR")]
    [SerializeField] private Renderer KovaSeffaf;
    float KovaninBaslangicDegeri;
    float KovaStepDegeri;
    [SerializeField] private AudioSource[] DigerSesler;

    string LevelAd;
    void Start()
    {
        AktifTopEfektIndex = 0;
        AktifTopSesIndex = 0;
        LevelAd = SceneManager.GetActiveScene().name;

        KovaninBaslangicDegeri = .5f;
        KovaStepDegeri = .25f / HedefTopSayisi;

        LevelSlider.maxValue = HedefTopSayisi;
        KalanTopSayisi_Text.text = MevcutTopSayisi.ToString();

    }
    public void TopGirdi()
    {
        GirenTopSayisi++;
        LevelSlider.value = GirenTopSayisi;

        KovaninBaslangicDegeri -= KovaStepDegeri;
        KovaSeffaf.material.SetTextureScale("_MainTex", new Vector2(1f, KovaninBaslangicDegeri));

        TopSesleri[AktifTopSesIndex].Play();
        AktifTopSesIndex++;

        if (AktifTopSesIndex == TopSesleri.Length - 1)
            AktifTopSesIndex = 0;

        if (GirenTopSayisi == HedefTopSayisi)
        {
            Time.timeScale = 0;
            DigerSesler[1].Play();
            PlayerPrefs.SetInt("Level", SceneManager.GetActiveScene().buildIndex + 1);
            PlayerPrefs.SetInt("Yildiz", PlayerPrefs.GetInt("Yildiz") + 15);
            YildizSayisi.text = PlayerPrefs.GetInt("Yildiz").ToString();
            Kazandin_LevelSayisi.text = "LEVEL : " + LevelAd;
            Paneller[1].SetActive(true);
        }
        int sayi = 0;
        foreach (var item in Toplar)
        {
            if (item.activeInHierarchy)
                sayi++;
        }

        if (sayi==0)
        {
            if (MevcutTopSayisi == 0 && GirenTopSayisi != HedefTopSayisi)
            {
                Kaybettin();
            }
            if ((MevcutTopSayisi + GirenTopSayisi) < HedefTopSayisi)
            {
                Kaybettin();
            }

        }        
    }
    public void TopGirmedi()
    {
        /* TopSesleri[AktifTopSesIndex].Play();
         AktifTopSesIndex++;

         if (AktifTopSesIndex == TopSesleri.Length - 1)
             AktifTopSesIndex = 0;*/

        int sayi = 0;
        foreach (var item in Toplar)
        {
            if (item.activeInHierarchy)
                sayi++;
        }

        if (sayi == 0)
        {
            if (MevcutTopSayisi == 0)
            {
                Kaybettin();
            }
            if ((MevcutTopSayisi + GirenTopSayisi) < HedefTopSayisi)
            {
                Kaybettin();
            }
        }
    }
   
    public void OyunuDurdur()
    {
        Paneller[0].SetActive(true);
        Time.timeScale = 0;
    }
    public void PanellericinButonislemi(string islem)
    {
        switch (islem)
        {
            case "Devamet":
                Time.timeScale = 1;
                Paneller[0].SetActive(false);
                break;
            case "Cikis":
                Application.Quit(); // runcontrolde panel ile sorgu olayını yapmıştık
                break;
            case "Ayarlar":
                // BURAYI SANA BIRAKIYORUM
                break;
            case "Tekrar":
                Time.timeScale = 1;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                break;
            case "Birsonraki":
                Time.timeScale = 1;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                break;


        }
    }
    public void ParcEfekt(Vector3 Pozisyon, Color Renk)
    {

        TopEfektleri[AktifTopEfektIndex].transform.position = Pozisyon;
        var main = TopEfektleri[AktifTopEfektIndex].main;
        main.startColor = Renk;
        TopEfektleri[AktifTopEfektIndex].gameObject.SetActive(true);
        AktifTopEfektIndex++;

        if (AktifTopEfektIndex == TopEfektleri.Length - 1)
            AktifTopEfektIndex = 0;

    }
    void Kaybettin()
    {
        Time.timeScale = 0;
        DigerSesler[0].Play();
        Kaybettin_LevelSayisi.text = "LEVEL : " + LevelAd;
        Paneller[2].SetActive(true);
    }

    public void TopAt()
    {

        if (Time.timeScale != 0)
        {            
                MevcutTopSayisi--;
                KalanTopSayisi_Text.text = MevcutTopSayisi.ToString();
                _TopAtar.Play("TopAtar");
                TopAtmaEfekt.Play();
                DigerSesler[2].Play();
                Toplar[AktifTopIndex].transform.SetPositionAndRotation(FirePoint.transform.position, FirePoint.transform.rotation);
                Toplar[AktifTopIndex].SetActive(true);
                Toplar[AktifTopIndex].GetComponent<Rigidbody>().AddForce(Toplar[AktifTopIndex].transform.TransformDirection(90, 90, 0) * TopGucu, ForceMode.Force);

                if (Toplar.Length - 1 == AktifTopIndex)
                    AktifTopIndex = 0;
                else
                    AktifTopIndex++;            
        }
    }
}
