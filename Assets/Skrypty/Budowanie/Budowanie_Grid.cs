using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class Budowanie_Grid : MonoBehaviour
{
    public static Budowanie_Grid Instance { get; private set; }
    #region Deklaracje zmiennych
    public Grid grid;
    public Tilemap Glowny_TileMap;
    public Tilemap Dodatkowy_TileMap;
    public TileBase podswietlony_Tile;
    public TileBase Blokujacy_Tile;
    public TileBase poprzedniTile;
    public float rozmiarGridu = 5.0f;
    public Boolean once;
    private Boolean trybBudowania;
    [SerializeField] private Konverter sprite_wiezy_Konverter;
    [SerializeField]
    private Transform Wieza_Transform;
    private Vector3Int PodswietlonaPozycja_Vector3;
    private bool podswietlono;
    private Wieze_SO holder_Wieza;
    #endregion
    #region zmienne timera do oszczedzenia zasobów 
    private float timer=0;
    private float timermax=0.2f;
    #endregion
    private void Awake()
    {
        sprite_wiezy_Konverter = new Konverter();
        sprite_wiezy_Konverter.sprite = Resources.Load<Sprite>("weapons");
        podswietlony_Tile = sprite_wiezy_Konverter;
        once = true;
        Instance= this; // przypisanie aktualnej instancji 
        trybBudowania= false;
    }

    private void Instance_OnActiveBuildingTypeChanged(object sender, MechanikaBudowania.OnActiveBuildingTypeChangedEventArgs e)
    {
        //Debug.Log("wywolano" + e.aktywna_wieza_so.wieza_Transform);
        if (e.aktywna_wieza_so != null)
        {
            holder_Wieza = e.aktywna_wieza_so;
            Ustaw_aktywna_wieze(e.aktywna_wieza_so.wieza_Transform);
            AktywujBudowanie();
        }

    }
    #region Metody Budowanie
    public bool czyMenuBudowaniaOtwarte()
    {
        return trybBudowania;
    }
    private void Start()
    {
        MechanikaBudowania.Instance.OnActiveBuildingTypeChanged += Instance_OnActiveBuildingTypeChanged;
    }

    private void Update()
    {
        if (trybBudowania)
        {
            timer += Time.deltaTime;
            if (timer > timermax)
            {
                Podswietl_kratke();
                timer = 0;
            }
            if (Input.GetMouseButtonDown(0))
            {
                StawiajNaGridzie();
            }
        }
    }

    public void AktywujBudowanie()
    {
        trybBudowania = true;
    }
    public void DeaktywujBudowanie()
    {
        trybBudowania = false;
        WylaczPodswietlenie();
    }
    private void Ustaw_aktywna_wieze(Transform wieza_aktualna)
    {
        Wieza_Transform = wieza_aktualna;
    }
    private Vector3Int PozycjaKursoraNaGridzie()
    {
        Vector3 pozycja = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int pozycja_grid = Glowny_TileMap.WorldToCell(pozycja);
        pozycja_grid.z = 0;
        return pozycja_grid;
    }
    void StawiajNaGridzie()
    {
        Vector3 clickPosition = Uzyteczne.GetMouseWorldPosition();
        // Pozyskaj pozycjê œrodka komórki na siatce
        Vector3Int roundedPosition_Vector3 = grid.WorldToCell(clickPosition);
        // SprawdŸ, czy na danej komórce ju¿ jest obiekt
        if (MechanikaEkonomi.Instance.CzyStac(holder_Wieza.koszt_StartowaIloscSur_Lista))
        {
            if (grid != null && Dodatkowy_TileMap != null)
            {
                // SprawdŸ, czy komórka jest pusta
                if (Dodatkowy_TileMap.GetTile(roundedPosition_Vector3) == null)
                {
                    // Twórz kopiê prefabu na pozycji œrodka komórki
                    Transform newObject = Instantiate(Wieza_Transform, grid.GetCellCenterWorld(roundedPosition_Vector3), Quaternion.identity);
                    // Ustaw odpowiedni tile na dodatkowej TileMapie
                    Dodatkowy_TileMap.SetTile(roundedPosition_Vector3, Blokujacy_Tile);
                    MechanikaEkonomi.Instance.WydajSurowce(holder_Wieza.koszt_StartowaIloscSur_Lista);
                    MechanikaStatystyk.instance.IncreaseBuilded();
                }
            }
        }

    }
    private void WylaczPodswietlenie()
    {
        if (grid != null && Glowny_TileMap != null)
        {
                Glowny_TileMap.SetTile(PodswietlonaPozycja_Vector3, poprzedniTile);
        }
    }
    private void Podswietl_kratke()
    {
        Vector3Int pozycja_grid = PozycjaKursoraNaGridzie(); // pobierz pozycje na gridzie 
        if (grid != null) // sprawdz czy grid jest zainicjowany 
        {
            if (Dodatkowy_TileMap.GetTile(PozycjaKursoraNaGridzie()) == null) // sprawdz czy kursor jest w granicy budowania 
            {
                if (PodswietlonaPozycja_Vector3 != pozycja_grid )
                {
                    if(PodswietlonaPozycja_Vector3 != Vector3Int.zero) {
                        Glowny_TileMap.SetTile(PodswietlonaPozycja_Vector3, poprzedniTile);
                    }

                    TileBase tile = Glowny_TileMap.GetTile(pozycja_grid);
                    if (tile)
                    {
                        poprzedniTile = Glowny_TileMap.GetTile(pozycja_grid);
                        Glowny_TileMap.SetTile(pozycja_grid, podswietlony_Tile);
                        podswietlony_Tile = Glowny_TileMap.GetTile(pozycja_grid); 
                        PodswietlonaPozycja_Vector3 = pozycja_grid;
                    }
                    else
                    {
                        WylaczPodswietlenie();
                    }
                }
                else
                {
                    Debug.Log("wykryto zmiane pola");
                }
            }
            else
            {
               WylaczPodswietlenie();
            }
        }
    }
    #endregion



}
