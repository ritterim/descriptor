using System;

namespace RimDev.Descriptor.Helpers
{
    public static class EnvironmentHelper
    {
        /// <summary>
        /// Checks whether runtime is using Mono or MS-version.
        /// Pulled/modified from http://www.mono-project.com/docs/gui/winforms/porting-winforms-applications/.
        /// </summary>
        public static Lazy<bool> IsRunningOnMono = new Lazy<bool>(() =>
        {
            return Type.GetType("Mono.Runtime") != null;
        });
    }
}
