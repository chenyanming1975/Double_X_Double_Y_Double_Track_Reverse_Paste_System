using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralMachine.Common
{
    /// <summary>
    /// 复合字典
    /// </summary>
    public class MultiDictionary<Key1, Key2, Value>
    {
        /// <summary>
        /// 复合字典
        /// </summary>
        private Dictionary<Key1, Dictionary<Key2, Value>> mDict1 = new Dictionary<Key1, Dictionary<Key2, Value>>();

        #region 对外方法
        /// <summary>
        /// 是否存在该元素
        /// </summary>
        /// <param name="key1"></param>
        /// <param name="key2"></param>
        /// <returns></returns>
        public bool Contains(Key1 key1, Key2 key2)
        {
            if (mDict1.ContainsKey(key1))
                if (mDict1[key1].ContainsKey(key2))
                    return true;
            return false;
        }

        /// <summary>
        /// 添加键值对
        /// </summary>
        /// <param name="key1"></param>
        /// <param name="key2"></param>
        /// <param name="val"></param>
        public void Add(Key1 key1, Key2 key2, Value val)
        {
            if(mDict1.ContainsKey(key1))
            {
                if (mDict1[key1].ContainsKey(key2))
                    mDict1[key1][key2] = val;
                else
                    mDict1[key1].Add(key2, val);
            }
            else
            {
                mDict1.Add(key1, new Dictionary<Key2, Value>());
                mDict1[key1].Add(key2, val);
            }
        }

        #region 删除操作
        public void Remove(Key1 key1, Key2 key2)
        {
            if (mDict1.ContainsKey(key1))
            {
                mDict1[key1].Remove(key2);
            }
        }

        public void Remove(Key1 key1)
        {
            mDict1.Remove(key1);
        }

        public void Clear()
        {
            mDict1.Clear();
        }
        #endregion

        /// <summary>
        /// 声明索引器
        /// </summary>
        /// <param name="key1"></param>
        /// <param name="key2"></param>
        /// <returns></returns>
        public Value this[Key1 key1, Key2 key2]
        {
            get
            {
                return this.Get(key1, key2);
            }

            set
            {
                this.Set(key1, key2, value);
            }
        }

        #endregion

        #region 私有方法

        /// <summary>    
        /// 赋值    
        /// </summary>
        private void Set(Key1 key1, Key2 key2, Value value)
        {
            if (mDict1.ContainsKey(key1))
            {
                var dict2 = mDict1[key1];
                if (dict2.ContainsKey(key2))
                    dict2[key2] = value;
                else
                    dict2.Add(key2, value);
            }
            else
            {
                var dict2 = new Dictionary<Key2, Value>();
                dict2.Add(key2, value);
                mDict1.Add(key1, dict2);
            }
        }

        /// <summary>
        /// 取值
        /// </summary>
        private Value Get(Key1 key1, Key2 key2, Value defaultValue = default(Value))
        {
            if (mDict1.ContainsKey(key1))
            {
                var dict2 = mDict1[key1];
                if (dict2.ContainsKey(key2))
                    return dict2[key2];
            }
            return defaultValue;
        }

        #endregion
    }
}
