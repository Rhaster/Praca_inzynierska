using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
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
    
    [SerializeField]
    private Transform Wieza_Transform;
    private Wieze_SO holder_Wieza;
    #endregion
    #region zmienne timera do oszczedzenia zasobów 
    private float timer=0;
    private float timermax=0.2f;
    #endregion
    private void Awake()
    {

        once = true;
        Instance= this; // przypisanie aktualnej instancji 
        trybBudowania= false;
    }

    private void Instance_OnActiveBuildingTypeChanged(object sender, MechanikaBudowania.Holder_Typu_Budowli e)
    {
        //Debug.Log("wywolano" + e.aktywna_wieza_so.wieza_Transform);
        if (e.aktywna_wieza_so != null)
        {
            holder_Wieza = e.aktywna_wieza_so;
            Ustaw_aktywna_wieze(e.aktywna_wieza_so.wieza_Transform);
            AktywujBudowanie();
        }
        else
        {
            holder_Wieza =null;
            Ustaw_aktywna_wieze(null);
            DeaktywujBudowanie();
        }

    }
    #region Metody Budowanie
    public bool czyMenuBudowaniaOtwarte()
    {
        return trybBudowania;
    }
    private void Start()
    {
        MechanikaBudowania.Instance.Zmiana_aktywnego_typu_wiezy += Instance_OnActiveBuildingTypeChanged;
    }

    private void Update()
    {
        if (trybBudowania)
        {
            timer += Time.deltaTime;
            if (timer > timermax)
            {
                timer = 0;
            }
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
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
   
    
    #endregion



}
