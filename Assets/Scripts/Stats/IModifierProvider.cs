using System.Collections.Generic;

namespace RPG.Stats
{
    public interface IModiferProvider
    {
        IEnumerable<float> GetAdditiveModifer(Stat stat);
    }

}
