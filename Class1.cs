using System.Collections.Generic;
using System.Linq;
using BTD_Mod_Helper;
using BTD_Mod_Helper.Api.Display;
using BTD_Mod_Helper.Api.Enums;
using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using MelonLoader;
using SheriffMonkey.Displays.Projectiles;
using UnityEngine;
using System;
using System.Text;
using System.Threading.Tasks;
using TimeTraveler.Displays.Projectiles;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Models.TowerSets;
using Il2CppAssets.Scripts.Unity;
using Il2CppAssets.Scripts.Unity.Display;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Attack;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Emissions;
using Il2CppAssets.Scripts.Simulation.Towers;
using Il2CppAssets.Scripts.Models.Towers.Filters;

[assembly: MelonModInfo(typeof(TemplateMod.Main), "Sheriff Monkey", "2.0.1", "Commander__Cat")]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]

namespace TemplateMod
{
    public class Main : BloonsTD6Mod
    {
        public override void OnApplicationStart()
        {
            MelonLogger.Msg("The Sheriff is in town!");
        }
    }
}
namespace SheriffMonkey.Displays
{
    public class SheriffMonkeyBaseDisplay : ModTowerDisplay<SheriffMonkey>
    {
        // Copy the Boomerang Monkey display
        public override string BaseDisplay => GetDisplay(TowerType.Marine);

        public override bool UseForTower(int[] tiers)
        {
            return tiers.Max() < 5;
        }
        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            SetMeshTexture(node, "SheriffBaseDisplay");
        }
    }
    public class SheriffMonkey005Display : ModTowerDisplay<SheriffMonkey>
    {
        public override string BaseDisplay => GetDisplay(TowerType.Marine);

        public override bool UseForTower(int[] tiers)
        {
            return tiers[2] == 5;
        }

        public override float Scale => 1.1f;

        public override void ModifyDisplayNode(UnityDisplayNode node)
        {


            SetMeshTexture(node, "Sheriff005Display");
        }
    }
    public class SheriffMonkey500Display : ModTowerDisplay<SheriffMonkey>
    {
        public override string BaseDisplay => GetDisplay(TowerType.Marine);

        public override bool UseForTower(int[] tiers)
        {
            return tiers[0] == 5;
        }

        public override float Scale => 1.05f;

        public override void ModifyDisplayNode(UnityDisplayNode node)
        {


            SetMeshTexture(node, "Sheriff500Display");
        }
    }
    public class SheriffMonkey050Display : ModTowerDisplay<SheriffMonkey>
    {
        public override string BaseDisplay => GetDisplay(TowerType.Marine);

        public override bool UseForTower(int[] tiers)
        {
            return tiers[1] == 5;
        }

        public override float Scale => 1.1f;

        public override void ModifyDisplayNode(UnityDisplayNode node)
        {


            SetMeshTexture(node, "Sheriff050Display");
        }
    }
}
namespace SheriffMonkey.Displays.Projectiles
{
    public class BulletDisplay : ModDisplay
    {
        public override string BaseDisplay => Generic2dDisplay;

        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            Set2DTexture(node, "BulletDisplay");
        }
    }
    public class TaserBulletDisplay : ModDisplay
    {
        public override string BaseDisplay => Generic2dDisplay;

        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            Set2DTexture(node, "TaserBulletDisplay");
        }
    }
    public class HandcuffsDisplay : ModDisplay
    {
        public override string BaseDisplay => Generic2dDisplay;

        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            Set2DTexture(node, "HandcuffsDisplay");
        }
    }
    public class GoldenBulletDisplay : ModDisplay
    {
        public override string BaseDisplay => Generic2dDisplay;

        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            Set2DTexture(node, "GoldenBulletDisplay");
        }
    }
}
namespace SheriffMonkey
{
    public class SheriffMonkey : ModTower
    {

        public override TowerSet TowerSet => TowerSet.Military;
        public override string BaseTower => TowerType.EngineerMonkey;
        public override int Cost => 780;

        public override int TopPathUpgrades => 5;
        public override int MiddlePathUpgrades => 5;
        public override int BottomPathUpgrades => 5;
        public override string Description => "The Sheriff is in town, and shoots bloons with his gun";
        public override ParagonMode ParagonMode => ParagonMode.Base555;
        public override void ModifyBaseTowerModel(TowerModel towerModel)
        {
            towerModel.range = 50;
            var attackModel = towerModel.GetAttackModel();
            attackModel.range = 50;


            var projectile = attackModel.weapons[0].projectile;
            //projectile.ApplyDisplay<RedCardDisplay>();
            projectile.pierce = 1;
            projectile.ApplyDisplay<BulletDisplay>();
        }
        public override int GetTowerIndex(List<TowerDetailsModel> towerSet)
        {
            return towerSet.First(model => model.towerId == TowerType.SniperMonkey).towerIndex + 1;
        }
    }
}
namespace SheriffMonkey.Upgrades.MiddlePath
{
    public class FastShooting : ModUpgrade<SheriffMonkey>
    {

        public override int Path => MIDDLE;
        public override int Tier => 1;
        public override int Cost => 360;
        public override string Description => "The sheriffs gun shoots faster";

        public override void ApplyUpgrade(TowerModel tower)
        {

            foreach (var towerModel in tower.GetWeapons())
            {
                foreach (var attackModel in tower.GetWeapons())
                {
                    attackModel.Rate = .45f;

                }
            }
        }
    }
    public class FasterShooting : ModUpgrade<SheriffMonkey>
    {

        public override int Path => MIDDLE;
        public override int Tier => 2;
        public override int Cost => 780;
        public override string Description => "The sheriffs gun shoots even faster";

        public override void ApplyUpgrade(TowerModel tower)
        {

            foreach (var towerModel in tower.GetWeapons())
            {
                foreach (var attackModel in tower.GetWeapons())
                {
                    attackModel.Rate = .30f;

                }
            }
            foreach (var projectile in tower.GetWeapons().Select(weaponModel => weaponModel.projectile))
            {
                projectile.GetDamageModel().damage += 1;

            }
        }
    }
    public class Taser : ModUpgrade<SheriffMonkey>
    {

        public override int Path => MIDDLE;
        public override int Tier => 3;
        public override int Cost => 1740;
        public override string Description => "Bullets now shock bloons";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var attackModel = towerModel.GetAttackModel();
            attackModel.weapons[0].Rate = .30f;
            attackModel.weapons[0].projectile = Game.instance.model.GetTowerFromId("DartlingGunner-200").GetAttackModel().weapons[0].projectile.Duplicate();
            attackModel.range = 50f;
            attackModel.weapons[0].projectile.GetDamageModel().damage = 4f;
            attackModel.weapons[0].projectile.ApplyDisplay<TaserBulletDisplay>();
            attackModel.weapons[0].projectile.pierce = 2;
        }
    }
    public class ImprovedTaser : ModUpgrade<SheriffMonkey>
    {

        public override int Path => MIDDLE;
        public override int Tier => 4;
        public override int Cost => 12740;
        public override string Description => "Taser can now make bloons stumble back towards the exit";

        public override void ApplyUpgrade(TowerModel tower)
        {
            var attackModel = tower.GetAttackModel();
            attackModel.weapons[0].projectile.GetDamageModel().damage += 4;
            var windModel = Game.instance.model.GetTowerFromId("NinjaMonkey-010").GetWeapon().projectile.GetBehavior<WindModel>().Duplicate();
            windModel.chance = 0.33f;
            windModel.distanceMin = 100f;
            attackModel.weapons[0].projectile.AddBehavior(windModel);
            attackModel.weapons[0].Rate = .20f;
            tower.range += 10;
            attackModel.range += 10;
        }
    }
    public class DeathlyTaser : ModUpgrade<SheriffMonkey>
    {

        public override int Path => MIDDLE;
        public override int Tier => 5;
        public override int Cost => 133740;
        public override string Description => "Firepower. Need more firepower.";

        public override void ApplyUpgrade(TowerModel tower)
        {
            var attackModel = tower.GetAttackModel();
            attackModel.weapons[0].projectile.GetDamageModel().damage += 22;
            var windModel = Game.instance.model.GetTowerFromId("NinjaMonkey-010").GetWeapon().projectile.GetBehavior<WindModel>().Duplicate();
            windModel.chance = .50f;
            windModel.distanceMin = 200f;
            attackModel.weapons[0].Rate = .4f;
            attackModel.weapons[0].projectile.pierce += 2;
            attackModel.weapons[0].projectile.GetBehavior<WindModel>().affectMoab = true;
        }
    }
}
namespace SheriffMonkey.Upgrades.TopPath
{
    public class PiercingBullets : ModUpgrade<SheriffMonkey>
    {

        public override int Path => TOP;
        public override int Tier => 1;
        public override int Cost => 270;
        public override string Description => "Bullets gain more pierce allowing them to hit more bloons";

        public override void ApplyUpgrade(TowerModel tower)
        {
            var attackModel = tower.GetAttackModel();
            attackModel.weapons[0].projectile.pierce += 1;
        }
    }
    public class ExtraPiercingBullets : ModUpgrade<SheriffMonkey>
    {

        public override int Path => TOP;
        public override int Tier => 2;
        public override int Cost => 430;
        public override string Description => "Bullets gain even more pierce";

        public override void ApplyUpgrade(TowerModel tower)
        {
            var attackModel = tower.GetAttackModel();
            attackModel.weapons[0].projectile.pierce += 1;
        }
    }
    public class Handcuffs : ModUpgrade<SheriffMonkey>
    {

        public override int Path => TOP;
        public override int Tier => 3;
        public override int Cost => 1240;
        public override string Description => "Sherrif throws handcuffs freezing bloons in place";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var attackModel = towerModel.GetAttackModel();
            towerModel.AddBehavior(Game.instance.model.GetTowerFromId("IceMonkey-003").GetAttackModel().Duplicate());
            towerModel.GetAttackModel(1).range = attackModel.range = 60f;
            towerModel.GetAttackModel(1).weapons[0].Rate = 1.4f;
            towerModel.GetAttackModel(1).weapons[0].projectile.pierce = 3f;
            towerModel.GetAttackModel(1).weapons[0].projectile.ApplyDisplay<HandcuffsDisplay>();
            towerModel.GetAttackModel(0).weapons[0].projectile.GetDamageModel().damage += 1;
        }
    }
    public class PlatinumHandcuffs : ModUpgrade<SheriffMonkey>
    {

        public override int Path => TOP;
        public override int Tier => 4;
        public override int Cost => 2560;
        public override string Description => "Reinforced handcuffs, are better in many ways";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var attackModel = towerModel.GetAttackModel();
            towerModel.GetAttackModel(1).range = attackModel.range = 60f;
            towerModel.GetAttackModel(1).weapons[0].Rate = 0.9f;
            towerModel.GetAttackModel(1).weapons[0].projectile.pierce = 10f;
            attackModel.weapons[0].projectile.pierce += 2;
            towerModel.GetAttackModel(0).weapons[0].projectile.GetDamageModel().damage += 1;

        }
    }
    public class ExtraLargeHandcuffs : ModUpgrade<SheriffMonkey>
    {

        public override int Path => TOP;
        public override int Tier => 5;
        public override int Cost => 24560;
        public override string Description => "Extra large handcuffs, lock up even the biggest of bloons";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var attackModel = towerModel.GetAttackModel();
            towerModel.AddBehavior(Game.instance.model.GetTowerFromId("IceMonkey-005").GetAttackModel().Duplicate());
            towerModel.GetAttackModel(1).range = attackModel.range = 80f;
            towerModel.GetAttackModel(1).weapons[0].Rate = 0.7f;
            towerModel.GetAttackModel(1).weapons[0].projectile.pierce = 10f;
            attackModel.weapons[0].projectile.pierce += 10;
            towerModel.GetAttackModel(1).weapons[0].projectile.ApplyDisplay<HandcuffsDisplay>();
            towerModel.GetAttackModel(0).weapons[0].projectile.GetDamageModel().damage += 37;
            towerModel.GetAttackModel(1).weapons[0].projectile.scale += .5f;
        }
    }
}
namespace SheriffMonkey.Upgrades.TopPath
{
    public class HigherVantagePoint : ModUpgrade<SheriffMonkey>
    {

        public override int Path => BOTTOM;
        public override int Tier => 1;
        public override int Cost => 340;
        public override string Description => "Range is extended";

        public override void ApplyUpgrade(TowerModel tower)
        {
            var attackModel = tower.GetAttackModel();
            tower.range += 10;
            attackModel.range += 10;
        }
    }
    public class AdvancedGoggles : ModUpgrade<SheriffMonkey>
    {

        public override int Path => BOTTOM;
        public override int Tier => 2;
        public override int Cost => 650;
        public override string Description => "Night Vision goggle allow the sherrif to see camo bloons";

        public override void ApplyUpgrade(TowerModel tower)
        {
            var attackModel = tower.GetAttackModel();
            tower.range += 5;
            attackModel.range += 5;
            tower.GetDescendants<FilterInvisibleModel>().ForEach(model => model.isActive = false);
        }
    }
    public class GoldenBullets : ModUpgrade<SheriffMonkey>
    {

        public override int Path => BOTTOM;
        public override int Tier => 3;
        public override int Cost => 1750;
        public override string Description => "Golden bullets generate cash on hit";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var attackModel = towerModel.GetAttackModel(0);
            var bananaFarmCash = Game.instance.model.GetTowerFromId("BananaFarm-003").GetWeapon().projectile.GetBehavior<CashModel>().Duplicate<CashModel>();
            var bananaFarmText = Game.instance.model.GetTowerFromId("BananaFarm-003").GetWeapon().projectile.GetBehavior<CreateTextEffectModel>().Duplicate<CreateTextEffectModel>();
            bananaFarmCash.minimum = 2f;
            bananaFarmCash.maximum = 2f;
            attackModel.weapons[0].projectile.AddBehavior(bananaFarmCash);
            attackModel.weapons[0].projectile.AddBehavior(bananaFarmText);
            attackModel.weapons[0].projectile.ApplyDisplay<GoldenBulletDisplay>();
            attackModel.weapons[0].projectile.pierce += 1;
            attackModel.weapons[0].projectile.GetDamageModel().damage += 1;
        }
    }
    public class IncreasedFunding : ModUpgrade<SheriffMonkey>
    {

        public override int Path => BOTTOM;
        public override int Tier => 4;
        public override int Cost => 4750;
        public override string Description => "Somehow the sherrif got more funding for the monkey defence force";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var attackModel = towerModel.GetAttackModel(0);
            var bananaFarmCash = Game.instance.model.GetTowerFromId("BananaFarm-003").GetWeapon().projectile.GetBehavior<CashModel>().Duplicate<CashModel>();
            var bananaFarmText = Game.instance.model.GetTowerFromId("BananaFarm-003").GetWeapon().projectile.GetBehavior<CreateTextEffectModel>().Duplicate<CreateTextEffectModel>();
            bananaFarmCash.minimum = 3f;
            bananaFarmCash.maximum = 3f;
            attackModel.weapons[0].projectile.AddBehavior(bananaFarmCash);
            attackModel.weapons[0].projectile.AddBehavior(bananaFarmText);
            towerModel.AddBehavior(new PerRoundCashBonusTowerModel("merchantmen_pirate_crew", 200.0f, 0.0f, 1.0f, CreatePrefabReference("80178409df24b3b479342ed73cffb63d"), false));
            attackModel.weapons[0].projectile.pierce += 2;
            attackModel.weapons[0].projectile.GetDamageModel().damage += 1;
        }
    }
    public class MonkeyMoneyMountain : ModUpgrade<SheriffMonkey>
    {

        public override int Path => BOTTOM;
        public override int Tier => 5;
        public override int Cost => 105000;
        public override string Description => "Thats a lot of money";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var attackModel = towerModel.GetAttackModel(0);
            var bananaFarmCash = Game.instance.model.GetTowerFromId("BananaFarm-003").GetWeapon().projectile.GetBehavior<CashModel>().Duplicate<CashModel>();
            var bananaFarmText = Game.instance.model.GetTowerFromId("BananaFarm-003").GetWeapon().projectile.GetBehavior<CreateTextEffectModel>().Duplicate<CreateTextEffectModel>();
            bananaFarmCash.minimum = 5f;
            bananaFarmCash.maximum = 5f;
            attackModel.weapons[0].projectile.AddBehavior(bananaFarmCash);
            attackModel.weapons[0].projectile.AddBehavior(bananaFarmText);
            towerModel.AddBehavior(new PerRoundCashBonusTowerModel("merchantmen_pirate_crew", 3000.0f, 0.0f, 1.0f, CreatePrefabReference("80178409df24b3b479342ed73cffb63d"), false));
            attackModel.weapons[0].projectile.pierce += 11;
            attackModel.weapons[0].projectile.GetDamageModel().damage += 3;
            //CommanderCat Mod
        }
    }
}
