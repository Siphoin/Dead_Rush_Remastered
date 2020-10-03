
    static   class AssemblyLog
    {
        [UnityEngine.RuntimeInitializeOnLoadMethod(UnityEngine.RuntimeInitializeLoadType.BeforeSplashScreen)]
        private static void LogAssembly()
        {
#if UNITY_EDITOR
            UnityEngine.Debug.Log("Assembly info ini: .NET version: " + typeof(string).Assembly.ImageRuntimeVersion);

#endif
        }
    }
