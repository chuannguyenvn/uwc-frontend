using System;
using System.Collections;
using System.Collections.Generic;
using Commons.Communications.Mcps;
using Commons.Types;
using Newtonsoft.Json;
using Requests.DataStores.Mcps;
using UnityEngine;

namespace Requests.DataStores
{
    public class DataStoreManager : Singleton<DataStoreManager>
    {
        public static class Mcps
        {
            public static ListView ListView = new();
        }
    }
}