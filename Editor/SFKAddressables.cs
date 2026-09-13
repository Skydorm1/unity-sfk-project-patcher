using System.IO;
using System;
using System.Reflection;
using System.Text.RegularExpressions;
using Cysharp.Threading.Tasks;
using Nomnom.UnityProjectPatcher.Editor;
using Nomnom.UnityProjectPatcher.Editor.Steps;
using UnityEngine;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;
using UnityEditor;

namespace Skydorm.SFKProjectPatcher.Editor
{
    public readonly struct SFKAddressables : IPatcherStep
    {
        public UniTask<StepResult> Run()
        {
            Debug.Log("[SFK Wrapper] SFKAddressables started.");
            var settings = this.GetSettings();
            string assetsPath = settings.ProjectGameAssetsPath;

            Debug.Log($"[SFK Wrapper] ProjectGameAssetsPath: {assetsPath} - should be Assets/SuperFantasyKingdom/Game");
            
            AddBundlesToAddressables();
            return UniTask.FromResult(StepResult.Success);
        }

        private static void AddBundlesToAddressables()
        {
            Debug.Log(
                "[SFK Wrapper] Start Legacy AssetBundle → Addressables Converting..."
            );
            string[] bundles = AssetDatabase.GetAllAssetBundleNames();

            Debug.Log(
                $"[SFK Wrapper] Found Legacy AssetBundles: {bundles.Length}"
            );
            AddressableAssetSettings settings =
                AddressableAssetSettingsDefaultObject.Settings;

            if (settings == null)
            {
                settings = AddressableAssetSettings.Create(
                    AddressableAssetSettingsDefaultObject.kDefaultConfigFolder,
                    AddressableAssetSettingsDefaultObject.kDefaultConfigAssetName,
                    true,
                    true
                );

                AddressableAssetSettingsDefaultObject.Settings = settings;

                Debug.Log(
                    "[SFK Wrapper] Addressables Settings wurden erstellt."
                );
            }
            else
            {
                Debug.Log(
                    "[SFK Wrapper] Addressables Settings existieren bereits."
                );
            }

            Assembly addressablesAssembly =
                typeof(AddressableAssetSettings).Assembly;

            System.Type utilityType =
                addressablesAssembly.GetType(
                    "UnityEditor.AddressableAssets.Settings.AddressableAssetUtility"
                );

            if (utilityType == null)
            {
                Debug.LogError(
                    "[SFK Wrapper] AddressableAssetUtility konnte nicht gefunden werden."
                );

                return;
            }

            MethodInfo convertMethod =
                utilityType.GetMethod(
                    "ConvertAssetBundlesToAddressables",
                    BindingFlags.Static | BindingFlags.NonPublic
                );

            if (convertMethod == null)
            {
                Debug.LogError(
                    "[SFK Wrapper] ConvertAssetBundlesToAddressables() couldn't be found."
                );

                return;
            }

            Debug.Log(
                "[SFK Wrapper] Call ConvertAssetBundlesToAddressables() ..."
            );

            try
            {
                convertMethod.Invoke(null, null);

                Debug.Log(
                    "[SFK Wrapper] Legacy AssetBundles successfully converted."
                );
            }
            catch (TargetInvocationException exception)
            {
                Debug.LogException(
                    exception.InnerException ?? exception
                );
            }
            catch (System.Exception exception)
            {
                Debug.LogException(exception);
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            Debug.Log(
                "[SFK Wrapper] Legacy AssetBundle → Addressables finished."
            );
        }

        public void OnComplete(bool failed)
        {
        }
    }
}