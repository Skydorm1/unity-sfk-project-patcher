using Nomnom.UnityProjectPatcher.Editor;
using Nomnom.UnityProjectPatcher.Editor.Steps;

namespace Skydorm.SFKProjectPatcher.Editor {
    [UPPatcher("com.skydorm.unity-sfk-project-patcher")]
    public static class SFKWrapper 
    {
        public static void GetSteps(StepPipeline stepPipeline) 
        {
            stepPipeline.SetInputSystem(InputSystemType.Both);

            stepPipeline.InsertLast(new SFKClearUpFiles());
            stepPipeline.InsertLast(new SFKAddressables());
        }
    }
}