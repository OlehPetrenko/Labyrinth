﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Classes
{
    /// <summary>
    /// Extends the default <see cref="JsonUtility"/>.
    /// Allows to work with a <see cref="List{T}"/>.
    /// </summary>
    public static class JsonHelper
    {
        public static List<T> FromJson<T>(string json)
        {
            var wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
            return wrapper.Items;
        }

        public static string ToJson<T>(List<T> array)
        {
            var wrapper = new Wrapper<T> { Items = array };
            return JsonUtility.ToJson(wrapper);
        }

        public static string ToJson<T>(List<T> array, bool prettyPrint)
        {
            var wrapper = new Wrapper<T> { Items = array };
            return JsonUtility.ToJson(wrapper, prettyPrint);
        }


        [Serializable]
        private class Wrapper<T>
        {
            public List<T> Items;
        }
    }
}