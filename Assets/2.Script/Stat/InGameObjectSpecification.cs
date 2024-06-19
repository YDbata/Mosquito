using System.Collections.Generic;
using UnityEngine;

namespace Mosquito.Stat
{
    [CreateAssetMenu(fileName = "StartStatData", menuName = "Stat", order = 0)]
    public class InGameObjectSpecification : ScriptableObject
    {
        [field: SerializeField] public int id { get; private set; }

        public float this[StatType statDataModel] => _statTable[statDataModel];
        public IEnumerable<StatDataModel> statTable => _statPairs;


        [SerializeField] List<StatDataModel> _statPairs;
        Dictionary<StatType, float> _statTable;
        

        protected virtual void OnEnable()
        {
            _statTable = new Dictionary<StatType, float>();
            foreach (var spec in _statPairs)
            {
                _statTable.Add(spec.type, spec.value);
            }
        }
    }
}