using System;

namespace BL
{
    /// <summary>
    /// This is a BLobject Factory singleton class.
    /// It is created in order to decouple the construction of the BLOBject from the BLOBject itself.
    /// Uses lazy initialization and background worker threads.
    /// </summary>
    public class BLFactory
    {
        private static readonly Lazy<BLOBject> lazy = new Lazy<BLOBject>(() => new BLOBject());

        public static IBL.IBLInterface GetBL() => lazy.Value;
        
    }
}
