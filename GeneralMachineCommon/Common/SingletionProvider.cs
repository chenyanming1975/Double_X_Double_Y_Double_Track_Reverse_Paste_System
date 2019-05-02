//-----------------------------------------------------------------------
// <copyright file="SingletionProvider.cs" company="鸿仕达智能科技有限公司">
// Copyright (C)2013-2018 鸿仕达智能科技有限公司 . All Rights Reserved.
// </copyright>
// <author>Sunlike</author>
// <summary></summary>
//-----------------------------------------------------------------------
using System;

namespace GeneralMachine.Common
{
    /// <summary>
    /// class SingletionProvider Definition
    /// </summary>
    /// <typeparam name="T">Singleton Class </typeparam>
    [Serializable]
    public class SingletionProvider<T> where T : new()
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SingletionProvider{T}"/> class.
        /// </summary>
        public SingletionProvider()
        {
        }

        /// <summary>
        /// Singleton Class
        /// </summary>
        public static T Instance
        {
            get
            {
                return SingletonCreator.Instance;
            }

            set
            {
                SingletonCreator.Instance = value;
            }
        }

        /// <summary>
        /// 创新创建
        /// </summary>
        public static void ReCreate()
        {
            SingletonCreator.ReCreate();
        }

        /// <summary>
        /// Singleton Class Generator
        /// </summary>
        internal class SingletonCreator
        {
            /// <summary>
            /// T instance
            /// </summary>
            internal static T Instance
            {
                get;
                set;
            }
                = new T();

            /// <summary>
            /// 创新创建
            /// </summary>
            internal static void ReCreate()
            {
                SingletonCreator.Instance = new T();
            }
        }
    }
}