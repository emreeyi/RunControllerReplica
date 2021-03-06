using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using namespace1;

public class GameManager : MonoBehaviour
{
    public static int AnlikKarakterSayisi = 1;
    public GameObject VarisNoktasi;
    public List<GameObject> Karakterler;
    public List<GameObject> OlusmaEfektleri;
    public List<GameObject> YokOlmaEfektleri;
    public List<GameObject> AdamLekesiEfektleri;
    [Header("LEVEL VER?LER?")]
    public List<GameObject> Dusmanlar;
    public int KacDusmanOlsun;
    public GameObject _AnaKarakter;
    public bool OyunBittimi;
    // Start is called before the first frame update
    void Start()
    {
        DusmanlariOlustur();
    }
    public void DusmanlariOlustur()
    {
        for (int i = 0; i < KacDusmanOlsun; i++)
        {
            Dusmanlar[i].SetActive(true);
        }
    }
    public void DusmanlariTetikle()
    {
        foreach (var item in Dusmanlar)
        {
            if (item.activeInHierarchy)
            {
                item.GetComponent<Dusman>().AnimasyonTetikle();
            }
        }
    }
// Update is called once per frame
void Update()
    {

    }
    void SavasDurumu()
    {
        if (AnlikKarakterSayisi==1||KacDusmanOlsun==0)
        {
            OyunBittimi = true;
            foreach (var item in Dusmanlar)
            {
                if (item.activeInHierarchy)
                {
                    item.GetComponent<Animator>().SetBool("Saldir", false);
                }
            }
            foreach (var item in Karakterler)
            {
                if (item.activeInHierarchy)
                {
                    item.GetComponent<Animator>().SetBool("Saldir", false);
                }
            }
            _AnaKarakter.GetComponent<Animator>().SetBool("Saldir", false);
            if (AnlikKarakterSayisi<KacDusmanOlsun||AnlikKarakterSayisi==KacDusmanOlsun)
            {
                Debug.Log("kaybettin");
            }
            else
            {
                Debug.Log("kazand?n");
            }
        }
    }
    public void AdamYonetim(string islemturu, int GelenSayi, Transform Pozisyon)
    {
        switch (islemturu)
        {
            case "Carpma":
                Matematiksel_islemler.Carpma(GelenSayi, Karakterler, Pozisyon,OlusmaEfektleri);
                break;
            case "Toplama":
                Matematiksel_islemler.Toplama(GelenSayi, Karakterler, Pozisyon,OlusmaEfektleri);
                break;
            case "Cikartma":
                Matematiksel_islemler.Cikartma(GelenSayi, Karakterler,YokOlmaEfektleri);
                break;
            case "Bolme":
                Matematiksel_islemler.Bolme(GelenSayi, Karakterler,YokOlmaEfektleri);
                break;
        }
    }
    public void YokOlmaEfektiOlustur(Vector3 Pozisyon,bool Balyoz=false,bool Durum=false)
    {
        foreach (var item in YokOlmaEfektleri)
        {
            if (!item.activeInHierarchy)
            {
                item.SetActive(true);
                item.transform.position = Pozisyon;
                item.GetComponent<ParticleSystem>().Play();
                if (!Durum)
                {
                    AnlikKarakterSayisi--;
                }
                else
                {
                    KacDusmanOlsun--;
                }
                break;
            }
        }
        if (Balyoz)
        {
            Vector3 yeniPoz = new Vector3(Pozisyon.x, 0.005f, Pozisyon.z);
            foreach (var item in AdamLekesiEfektleri)
            {
                if (!item.activeInHierarchy)
                {
                    item.SetActive(true);
                    item.transform.position = yeniPoz;
                    break;
                }
            }
        }
        if (!OyunBittimi)
        {
            SavasDurumu();
        }
    }
}
