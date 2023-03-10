using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using static ExtensionMethods.ExtensionMethods;

public class Torch : MonoBehaviour
{
    public DayCycle dayCycle;
    private Light2D light2D;
    public Sprite torchParticle;
    private ParticleSystem particle;
    public Material material;
    private void Start()
    {
        this.gameObject.AddComponent<ParticleSystem>();
        light2D = GetComponent<Light2D>();
        light2D.enabled = false;
        light2D.pointLightOuterRadius = 2.111f;
        light2D.intensity = 0.56f;

        particle = GetComponent<ParticleSystem>();
        IntitializeParticleComponent();
    }
    private void Update() => ToggleLight();
    private void ToggleLight() => light2D.enabled = !dayCycle.isDay;
    private void IntitializeParticleComponent()
    {
        particle.Stop();

        ParticleSystem.MainModule _main = particle.main;
        _main.duration = 0.1f;
        _main.startSpeed = 1.28f;
        _main.startSize = 0.7f;
        _main.maxParticles = 30;
        _main.startLifetime = 2;
        _main.simulationSpace = ParticleSystemSimulationSpace.World;
        

        particle.Play();

        ParticleSystem.EmissionModule _emission = particle.emission;
        _emission.enabled = true;
        _emission.rateOverTime = 3;

        ParticleSystem.ShapeModule _shape = particle.shape;
        _shape.enabled = true;
        _shape.shapeType = ParticleSystemShapeType.SingleSidedEdge;
        _shape.radius = 0.4f;
        _shape.sprite = torchParticle;

        ParticleSystem.SizeOverLifetimeModule _sizeOverLifeTime = particle.sizeOverLifetime;
        _sizeOverLifeTime.enabled = true;
        _sizeOverLifeTime.size = new ParticleSystem.MinMaxCurve(0.2f, 0.8f);

        ParticleSystem.TextureSheetAnimationModule _textureSheet = particle.textureSheetAnimation;
        _textureSheet.enabled = true;
        _textureSheet.mode = ParticleSystemAnimationMode.Sprites;
        _textureSheet.AddSprite(torchParticle);
        _textureSheet.timeMode = ParticleSystemAnimationTimeMode.Lifetime;

        ParticleSystemRenderer _renderer = particle.GetComponent<ParticleSystemRenderer>();
        _renderer.sortingOrder = 1;
        _renderer.material = material;
    }
}
