using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using GeneralMachine.Common;

namespace GeneralMachine.UserManager
{
    public class UserInfoHelper
    {
        /// <summary>
        /// 查询所有的用户信息
        /// </summary>
        /// <returns></returns>
        public static List<UserInfo> GetAllUsers()
        {
            string sql = "select * from UserInfo";
            DataTable dt = SqliteHelper.ExecuteTable(sql);
            List<UserInfo> user = new List<UserInfo>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    user.Add(RowToUserInfo(dr));
                }
            }
            return user;
        }

        /// <summary>
        /// 登录信息
        /// </summary>
        /// <param name="loginName"></param>
        /// <param name="pwd"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static bool IsLoginByLoginName(string loginName, string pwd, out string msg, out string ur, out int permission)
        {
            bool flag = false;
            permission = 0;
            ur = "";
            UserInfo user = IsLoginByLoginName(loginName);//获取对象
            if (user != null)
            {
                if (pwd == user.Pwd)
                {
                    ur = user.Role;
                    permission = int.Parse(user.Permission);
                    flag = true;
                    msg = "登录成功";//,上次登录时间:"+ user.LastLoginTime.ToString();
                }
                else
                {
                    msg = "密码错误";
                }
            }
            else
            {
                msg = "帐号不存在";
            }
            return flag;
        }

        /// <summary>
        /// 该方法是根据帐号去数据库查询 ,返回的是对象
        /// </summary>
        /// <param name="loginName">登录的帐号</param>
        /// <returns>userInfo对象</returns>
        public static UserInfo IsLoginByLoginName(string loginName)
        {
            string sql = "select * from UserInfo where LoginUserName=@LoginUserName";
            DataTable dt = SqliteHelper.ExecuteTable(sql, new SQLiteParameter("@LoginUserName", loginName));
            UserInfo user = null;
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    user = RowToUserInfo(dr);
                }
            }
            return user;
        }

        /// <summary>
        /// 关系转对象
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private static UserInfo RowToUserInfo(DataRow dr)
        {
            UserInfo user = new UserInfo();
            user.UserId = Convert.ToInt32(dr["UserId"]);
            user.Role = dr["Role"].ToString();
            user.LoginUserName = dr["LoginUserName"].ToString();
            user.Pwd = dr["Pwd"].ToString();
            user.LastLoginTime = Convert.ToDateTime(dr["LastLoginTime"]);
            user.Permission = dr["Permission"].ToString();
            user.NoteName = dr["NoteName"].ToString();
            return user;
        }

        /// <summary>
        ///  删除
        /// </summary>
        /// <param name="pro"></param>
        /// <returns></returns>
        public static int DeleteUserInfo(UserInfo ur)
        {
            string sql = "delete from UserInfo where UserId=" + ur.UserId;
            return SqliteHelper.ExecuteNonQuery(sql);
        }

        /// <summary>
        ///  新增
        /// </summary>
        /// <param name="pro"></param>
        /// <returns></returns>
        public static int AddUserInfo(UserInfo ur)
        {
            //string sql = "insert into UserInfo(UserId,LoginUserName,NoteName,Pwd,Role,LastLoginTime,Permission) values" +
                                             //"(@UserId,@LoginUserName,@NoteName,@Pwd,@Role,@LastLoginTime,@Permission)";
            string sql = "insert into UserInfo(LoginUserName,NoteName,Pwd,Role,LastLoginTime,Permission) values"+
                                             "(@LoginUserName,@NoteName,@Pwd,@Role,@LastLoginTime,@Permission)";
            return AddAndUpdate(ur, sql, 1);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="ur"></param>
        /// <returns></returns>
        public static int UpdateUserInfo(UserInfo ur)
        {
            //UserId=@UserId,
            string sql = "update UserInfo set LoginUserName=@LoginUserName,NoteName=@NoteName,Pwd=@Pwd,Role=@Role,LastLoginTime=@LastLoginTime,Permission=@Permission where UserId=@UserId";
            return AddAndUpdate(ur, sql, 2);
        }

        /// <summary>
        /// 增加和更新
        /// </summary>
        /// <param name="ur"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        private static int AddAndUpdate(UserInfo ur, string sql, int temp)
        {
            SQLiteParameter[] ps = {
                 //new SQLiteParameter("@UserId",ur.UserId),
                  new SQLiteParameter("@LoginUserName",ur.LoginUserName),
                   new SQLiteParameter("@NoteName",ur.NoteName),
                    new SQLiteParameter("@Pwd",ur.Pwd),
                     new SQLiteParameter("@Role",ur.Role),
                      new SQLiteParameter("@LastLoginTime",ur.LastLoginTime),
                       new SQLiteParameter("@Permission",ur.Permission)
                                   };
            List<SQLiteParameter> list = new List<SQLiteParameter>();
            list.AddRange(ps);
            if (temp == 1)//增加
            {
            }
            else if (temp == 2)//修改
            {
                list.Add(new SQLiteParameter("@UserId", ur.UserId));
            }
            return SqliteHelper.ExecuteNonQuery(sql, list.ToArray());
        }
    }
}
