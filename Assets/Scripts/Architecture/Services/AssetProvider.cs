
using System.Collections.Generic;
using UnityEngine;

namespace Architecture.Services
{
    public class AssetProvider
    {
        private Dictionary<string, GameObject> _cash = new Dictionary<string, GameObject>();

        public GameObject LoadObject(string path)
        {
            if (_cash.ContainsKey(path))
                return _cash[path];
            
            return Resources.Load(path) as GameObject;
        }
    }
}
