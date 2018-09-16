using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MrMime.Core.Interfaces;
using MrMime.Core.Models;
using MrMime.Core.ValueGenerators;
using Newtonsoft.Json;

namespace MrMime.Core
{
    public class Mimenator : IMimenator
    {
        private IList<Contract> _contracts;

        private T Imitate<T>(T obj, Contract contract) where T : class, new()
        {
            foreach (var prop in typeof(T).GetProperties())
            {
                var contractField = contract.Fields.FirstOrDefault(cf => cf.Name == prop.Name);
                if (contractField == null) continue;

                var value = ValueGenerator.GetValue(contractField);
                prop.SetValue(obj, value);
            }
            return obj;
        }

        public T Imitate<T>(T obj, string contractName) where T : class, new()
        {
            var contract = _contracts.FirstOrDefault(c => c.Name == contractName);
            return Imitate(obj, contract);
        }

        public T Imitate<T>(T obj, Guid contractId) where T : class, new()
        {
            var contract = _contracts.FirstOrDefault(c => c.ContractId == contractId);
            return Imitate(obj, contract);
        }

        public void Load(string contractsFolder = null)
        {
            _contracts = new List<Contract>();
            var files = Directory.GetFiles(contractsFolder ?? Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"Contracts"), "*.contract.json");
            Parallel.ForEach(files, (file) =>
            {
                using (var fileReader = new StreamReader(file))
                {
                    var contractJson = fileReader.ReadToEnd();
                    _contracts.Add(JsonConvert.DeserializeObject<Contract>(contractJson));
                }
            });
        }
    }
}
