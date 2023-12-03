using JaLoader;
using System.Collections.Generic;
using UnityEngine;

namespace Spoiler
{
    public class Spoiler : Mod
    {
        public override string ModID => "Spoiler";
        public override string ModName => "Spoiler";
        public override string ModAuthor => "Leaxx";
        public override string ModDescription => "Adds an optional spoiler for the Laika.";
        public override string ModVersion => "1.0";
        public override string GitHubLink => "https://github.com/Jalopy-Mods/Spoiler";
        public override WhenToInit WhenToInit => WhenToInit.InGame;
        public override List<(string, string, string)> Dependencies => new List<(string, string, string)>()
        {
            ("JaLoader", "Leaxx", "2.0.1")
        };

        public override bool UseAssets => true;

        private GameObject spoilerObject;

        public override void EventsDeclaration()
        {
            base.EventsDeclaration();
        }

        public override void SettingsDeclaration()
        {
            base.SettingsDeclaration();
        }

        public override void CustomObjectsRegistration()
        {
            base.CustomObjectsRegistration();

            spoilerObject = LoadAsset<GameObject>("spoiler", "spoiler", "", ".prefab");
            spoilerObject = Instantiate(spoilerObject, ModHelper.Instance.laika.transform.Find("TweenHolder/Frame"));
            spoilerObject.transform.localPosition = new Vector3(-1.07f, 0, 0);
            spoilerObject.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
            spoilerObject.transform.localEulerAngles = new Vector3(0, 0, 0);

            var script = spoilerObject.AddComponent<BrakeLight>();

            ModHelper.Instance.CreateIconForExtra(spoilerObject, new Vector3(), new Vector3(0.5f, 0.5f, 0.5f), new Vector3(180, 60, 200), "Spoiler");

            CustomObjectsManager.Instance.RegisterObject(ModHelper.Instance.CreateExtraObject(spoilerObject, BoxSizes.Medium, "Spoiler", "A spoiler, to make your car stand out.", 80, 10, "Spoiler", AttachExtraTo.Trunk), "Spoiler");
        }

        public override void OnEnable()
        {
            base.OnEnable();
        }

        public override void Awake()
        {
            base.Awake();
        }

        public override void Start()
        {
            base.Start();
        }

        public override void Update()
        {
            base.Update();
        }

        public override void OnDisable()
        {
            base.OnDisable();
        }
    }

    public class BrakeLight : MonoBehaviour
    {
        private CarLogicC carLogic;
        private MeshRenderer renderer;
        private GameObject lightObject;

        private void Start()
        {
            carLogic = FindObjectOfType<CarLogicC>();
            renderer = transform.Find("Plane").GetComponent<MeshRenderer>();
            lightObject = transform.Find("LightHolder").gameObject;
        }

        private void Update()
        {
            if(carLogic.headlightsOn)
            {
                Material[] mats = renderer.materials;
                mats[1] = carLogic.rearHeadlightMatOn;
                renderer.materials = mats;
                lightObject.SetActive(true);
            }
            else
            {
                Material[] mats = renderer.materials;
                mats[1] = mats[0];
                renderer.materials = mats;
                lightObject.SetActive(false);
            }
        }
    }
}
