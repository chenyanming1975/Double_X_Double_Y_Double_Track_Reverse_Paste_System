using System.Drawing;

namespace GeneralMachine.Common
{
    public class Variable
    {
        //*******************************************************[路径]*******************************************************
        /// <summary>
        /// 当前用户 职位(角色)
        /// </summary>
        public static string sPermission_CurerentRole = "Hostar";
        /// <summary>
        /// 当前用户名称
        /// </summary>
        public static string sPermission_CurerentUserName = "管理员";
        /// <summary>
        /// 当前用户权限
        /// </summary>
        public static int iPermission_CurerentUser = 0;

        public static bool IsExpand = false;
        public static short Column = 0;
        public static short Row = 0;
        public static PointF ExpandOrg = new PointF();
        public static PointF ExpandX = new PointF();
        public static PointF ExpandY = new PointF();

        ///// <summary>
        ///// 对外扩展方法
        ///// </summary>
        ///// <param name="ORG"></param>
        ///// <returns></returns>
        //public static PointF[] Expand(PointF ORG)
        //{
        //    return MathHelper.Expand2AllPoints(ORG, ExpandOrg, ExpandX, ExpandY, Column, Row);
        //}
    }
}
