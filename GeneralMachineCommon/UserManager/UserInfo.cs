using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralMachine.UserManager
{
   
    public class UserInfo
    {
        /// <summary>
        /// UserId
        /// </summary>
        private int _userId;

        public int UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }

        /// <summary>
        /// LoginUserName
        /// </summary>
        private string _loginUserName;

        public string LoginUserName
        {
            get { return _loginUserName; }
            set { _loginUserName = value; }
        }

        /// <summary>
        /// Pwd
        /// </summary>
        private string _pwd;

        public string Pwd
        {
            get { return _pwd; }
            set { _pwd = value; }
        }

        /// <summary>
        /// NoteName
        /// </summary>
        private string _noteName;

        public string NoteName
        {
            get { return _noteName; }
            set { _noteName = value; }
        }

        /// <summary>
        /// Role
        /// </summary>
        private string _role;

        public string Role
        {
            get { return _role; }
            set { _role = value; }
        }

        /// <summary>
        /// LastLoginTime
        /// </summary>
        private DateTime _lastLoginTime;

        public DateTime LastLoginTime
        {
            get { return _lastLoginTime; }
            set { _lastLoginTime = value; }
        }

        /// <summary>
        /// Permission
        /// </summary>
        private string _permission;

        public string Permission
        {
            get { return _permission; }
            set { _permission = value; }
        }

    }
}
