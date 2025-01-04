using frameworks.services;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace frameworks.ioc
{
    public static class InjectBindings
    {
        public static void Inject(object obj)
        {
#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                return;
            }
#endif

            Type type = obj.GetType();
            List<FieldInfo> fieldInfo = GetFields(type);
            foreach (FieldInfo field in fieldInfo)
            {
                object[] attributes = field.GetCustomAttributes(true);
                if (attributes == null)
                {
                    Debug.Log("NULL Attributes array");
                }

                foreach (Attribute attribute in attributes)
                {
                    if (attribute is InjectService)
                    {
                        if (ServiceRegistry.ServiceMap.ContainsKey(field.FieldType))
                        {
                            field.SetValue(obj, ServiceRegistry.ServiceMap[field.FieldType]);
                        }
                    }
                }
            }
        }

        private static List<FieldInfo> GetFields(Type type)
        {
            List<FieldInfo> fieldInfo = new List<FieldInfo>();

            while (type != null)
            {
                fieldInfo.AddRange(type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance));
                type = type.BaseType;
            }

            return fieldInfo;
        }
    }

}