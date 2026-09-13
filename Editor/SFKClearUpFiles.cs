using System;
using System.Collections.Generic;
using System.IO;
using Cysharp.Threading.Tasks;
using Nomnom.UnityProjectPatcher.Editor;
using Nomnom.UnityProjectPatcher.Editor.Steps;
using UnityEditor;
using UnityEngine;

namespace Skydorm.SFKProjectPatcher.Editor
{
    /// <summary>
    /// Probably not needed anymore
    /// </summary>
    public readonly struct SFKClearUpFiles : IPatcherStep
    {
        public UniTask<StepResult> Run()
        {
            Debug.Log("[SFK Wrapper] SFKClearUpFiles started.");

            var settings = this.GetSettings();
            string assetsPath = settings.ProjectGameAssetsPath;

            Debug.Log(
                $"[SFK Wrapper] ProjectGameAssetsPath: {assetsPath}"
            );

            ClearUpFiles(assetsPath);

            return UniTask.FromResult(StepResult.Success);
        }

        private static void ClearUpFiles(string assetsPath)
        {
            string projectRoot =
                Directory.GetParent(Application.dataPath).FullName;

            string gamePath = Path.Combine(
                projectRoot,
                assetsPath.Replace('/', Path.DirectorySeparatorChar)
            );

            if (!Directory.Exists(gamePath))
            {
                Debug.LogError(
                    $"[SFK Wrapper] Game path not found: {gamePath}"
                );

                return;
            }

            string scriptsPath =
                Path.Combine(gamePath, "Scripts");

            if (!Directory.Exists(scriptsPath))
            {
                Debug.Log(
                    "[SFK Wrapper] No Scripts folder found."
                );

                return;
            }

            Debug.Log(
                $"[SFK Wrapper] Cleaning Scripts folder: {scriptsPath}"
            );

            foreach (string entry in Directory.GetFileSystemEntries(
                scriptsPath,
                "*",
                SearchOption.TopDirectoryOnly))
            {
                string name = Path.GetFileName(entry);

                if (string.Equals(
                        name,
                        "Assembly-CSharp",
                        StringComparison.OrdinalIgnoreCase))
                    continue;

                if (string.Equals(
                        name,
                        "Assembly-CSharp.meta",
                        StringComparison.OrdinalIgnoreCase))
                    continue;

                DeleteFileSystemEntry(entry);
            }

            AssetDatabase.Refresh();

            Debug.Log(
                "[SFK Wrapper] Scripts cleanup finished."
            );
        }

        private static void DeleteFileSystemEntry(
            string path)
        {
            try
            {
                if (Directory.Exists(path))
                {
                    Debug.Log(
                        $"[SFK Wrapper] Deleting directory: {path}"
                    );

                    Directory.Delete(
                        path,
                        true
                    );
                }
                else if (File.Exists(path))
                {
                    Debug.Log(
                        $"[SFK Wrapper] Deleting file: {path}"
                    );

                    File.Delete(path);
                }
            }
            catch (Exception exception)
            {
                Debug.LogError(
                    $"[SFK Wrapper] Failed to delete: {path}\n{exception}"
                );
            }
        }

        public void OnComplete(bool failed)
        {
        }
    }
}