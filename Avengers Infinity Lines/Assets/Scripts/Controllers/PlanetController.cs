using System.Collections.Generic;
using UnityEngine;

public class PlanetController : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer planetRenderer;

    [SerializeField]
    private List<Sprite> planetBackgroundList;

    public Planet PlanetName { get; set; }

    void Start()
    {
        this.RevealPlanet(this.PlanetName);
    }

    private void RevealPlanet(Planet planetName)
    {
        switch (planetName)
        {
            case Planet.Earth:
                this.planetRenderer.sprite = this.planetBackgroundList[0];
                break;
            case Planet.Xander:
                this.planetRenderer.sprite = this.planetBackgroundList[1];
                break;
            case Planet.Vormir:
                this.planetRenderer.sprite = this.planetBackgroundList[2];
                break;
            case Planet.Asgard:
                this.planetRenderer.sprite = this.planetBackgroundList[3];
                break;
        }
    }
}
