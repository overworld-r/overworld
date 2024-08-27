using System;

namespace Overworld.Types
{
    public static class NullOptionExtension
    {
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

        public static void UnwrapOrElse<T>(this T? obj, Action func)
        {
            if (obj is UnityEngine.Object unityObj)
            {
                if (unityObj != null)
                {
                    func();
                }
            }
            else if (obj != null)
            {
                func();
            }
            throw new System.Exception($"Object {obj} of type {typeof(T)} is null");
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
