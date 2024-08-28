using System;

namespace Overworld.Types
{
    public static class NullOptionExtension
    {
        public static void OptGetComponent<T>(
            this UnityEngine.GameObject gameObject,
            Action<T> some = null!,
            Action none = null!
        )
        {
            if (gameObject.TryGetComponent<T>(out var component))
            {
                if (some != null)
                    some(component);
            }
            else
            {
                if (none != null)
                    none();
            }
        }

        public static void Match<T>(this T? obj, Action<T> some = null!, Action none = null!)
        {
            if (obj is UnityEngine.Object unityObj)
            {
                if (unityObj != null)
                {
                    if (some != null)
                    {
                        some(obj);
                    }
                }
            }
            else if (obj != null)
            {
                if (some != null)
                {
                    some(obj);
                }
            }

            if (none != null)
                none();
        }

        public static T Match<T>(this T? obj, Func<T, T> some, Func<T> none)
        {
            if (obj is UnityEngine.Object unityObj)
            {
                if (unityObj != null)
                {
                    if (some != null)
                    {
                        return some(obj);
                    }
                }
            }
            else if (obj != null)
            {
                if (some != null)
                {
                    return some(obj);
                }
            }

            return none();
        }

        public static T Unwrap<T>(this T? obj)
        {
            if (obj is UnityEngine.Object unityObj)
            {
                if (unityObj == null)
                {
                    throw new System.Exception($"Unity object {obj} of type {typeof(T)} is null");
                }
            }
            else if (obj == null)
            {
                throw new System.Exception($"Object {obj} of type {typeof(T)} is null");
            }
            return obj;
        }

        public static T Except<T>(this T? obj, string message)
        {
            if (obj is UnityEngine.Object unityObj)
            {
                if (unityObj == null)
                {
                    throw new System.Exception(message);
                }
            }
            else if (obj == null)
            {
                throw new System.Exception(message);
            }
            return obj;
        }
    }
}