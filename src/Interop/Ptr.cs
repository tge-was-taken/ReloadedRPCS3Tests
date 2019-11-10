using ReloadedRPCS3TestsNoConfig.Interop.CPP;
using ReloadedRPCS3TestsNoConfig.RPCS3.Types;
using System;
using System.Runtime.InteropServices;

namespace ReloadedRPCS3TestsNoConfig.Interop
{
    /// <summary>
    /// Pointer wrapper for generic type arguments.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public unsafe struct Ptr<T> where T : unmanaged
    {
        public T* Value;

        public static implicit operator Ptr<T>( T* value ) => new Ptr<T>() { Value = value };
        public static implicit operator T*( Ptr<T> value ) => value.Value;
    }

    /// <summary>
    /// Pointer wrapper for generic type arguments with 2 levels of indirection.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public unsafe struct Ptr2<T> where T : unmanaged
    {
        public T** Value;

        public static implicit operator Ptr2<T>( T** value ) => new Ptr2<T>() { Value = value };
        public static implicit operator T**( Ptr2<T> value ) => value.Value;
    }

    /// <summary>
    /// Pointer wrapper for generic type arguments with 3 levels of indirection.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public unsafe struct Ptr3<T> where T : unmanaged
    {
        public T*** Value;

        public static implicit operator Ptr3<T>( T*** value ) => new Ptr3<T>() { Value = value };
        public static implicit operator T***( Ptr3<T> value ) => value.Value;
    }

    /// <summary>
    /// Pointer wrapper for generic type arguments with 4 levels of indirection.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public unsafe struct Ptr4<T> where T : unmanaged
    {
        public T**** Value;

        public static implicit operator Ptr4<T>( T**** value ) => new Ptr4<T>() { Value = value };
        public static implicit operator T****( Ptr4<T> value ) => value.Value;
    }

    /// <summary>
    /// Pointer wrapper for generic type arguments with 5 levels of indirection.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public unsafe struct Ptr5<T> where T : unmanaged
    {
        public T***** Value;

        public static implicit operator Ptr5<T>( T***** value ) => new Ptr5<T>() { Value = value };
        public static implicit operator T*****( Ptr5<T> value ) => value.Value;
    }

    /// <summary>
    /// Pointer wrapper for generic type arguments with 6 levels of indirection.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public unsafe struct Ptr6<T> where T : unmanaged
    {
        public T****** Value;

        public static implicit operator Ptr6<T>( T****** value ) => new Ptr6<T>() { Value = value };
        public static implicit operator T******( Ptr6<T> value ) => value.Value;
    }

    /// <summary>
    /// Pointer wrapper for generic type arguments with 7 levels of indirection.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public unsafe struct Ptr7<T> where T : unmanaged
    {
        public T******* Value;

        public static implicit operator Ptr7<T>( T******* value ) => new Ptr7<T>() { Value = value };
        public static implicit operator T*******( Ptr7<T> value ) => value.Value;
    }

    /// <summary>
    /// Pointer wrapper for generic type arguments with 8 levels of indirection.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public unsafe struct Ptr8<T> where T : unmanaged
    {
        public T******** Value;

        public static implicit operator Ptr8<T>( T******** value ) => new Ptr8<T>() { Value = value };
        public static implicit operator T********( Ptr8<T> value ) => value.Value;
    }
}
