using System;

namespace PersonelTakipSistemi
{
    public static class ApplicationState
    {
        public static readonly string InstanceId = Guid.NewGuid().ToString();
    }
}
