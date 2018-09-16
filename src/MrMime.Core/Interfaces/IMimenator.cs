using System;

namespace MrMime.Core.Interfaces
{
    public interface IMimenator
    {
        void Load(string contractsFolder = null);
        T Imitate<T>(T obj, string contractName) where T : class, new();
        T Imitate<T>(T obj, Guid contractId) where T : class, new();
    }
}
