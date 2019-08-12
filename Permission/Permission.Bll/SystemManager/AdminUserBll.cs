using Permission.Bll.Common;
using Permission.Library;
using Permission.Library.Tools.Text;
using Permission.Model.DbModel.System;
using Permission.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Permission.Library.Extensions;
using Permission.Library.Common;

namespace Permission.Bll.SystemManager
{
    
    public class AdminUserBll : BasePermissionBll<AdminUserDb, AdminUserBll>
    {
        public static readonly string userCookieKey = "userCookieKey";
        /// <summary>
        /// 获取指定用户名的用户
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public AdminUserDb GetModel(string userName)
        {

            return PermissionDal.GetModel(c => c.user_name == userName);

        }
        /// <summary>
        /// 分页方法
        /// </summary>
        /// <param name="m">搜索模型</param>
        /// <returns></returns>
        public PagedList<AdminUserDb> GetPageList(SearchModel m)
        {
            return PermissionDal.Set<AdminUserDb>().Where(m).Pager(m);
        }
    
        /// <summary>
        /// 初始化登录用户
        /// </summary>
        /// <param name="user">登录用户</param>
        /// <returns></returns>
        public LoginUserViewModel IniLogin(AdminUserDb user)
        {
            if (user != null)
            {
                LoginUserViewModel loginUserViewModel = new LoginUserViewModel();
                loginUserViewModel.user_full_name = user.user_full_name;
                loginUserViewModel.user_name = user.user_name;
                List<ModuleDb> moduleDbList = ModuleBll.Instance.GetModuleList(user.user_name).ToList();
                if (moduleDbList != null && moduleDbList.Count > 0)
                {
                    loginUserViewModel.PermissionList = moduleDbList;
                    loginUserViewModel.PermissionListLevel = ModuleBll.Instance.IniLevelModuleList(moduleDbList);
                }
                UpdateUserLogin(user);
                System.Web.HttpContext.Current.Session["LoginUser"] = loginUserViewModel;
                Library.Web.Cookie.CookieManager.SetCookie(userCookieKey, StringDes.DesEncrypt(loginUserViewModel.user_name));
                return loginUserViewModel;
            }
            return new LoginUserViewModel();
        }

        /// <summary>
        /// 获取登录用户
        /// </summary>
        /// <returns></returns>
        public static LoginUserViewModel GetLoginUser()
        {
            if (System.Web.HttpContext.Current.Session["LoginUser"] != null)
            {
                return (LoginUserViewModel)System.Web.HttpContext.Current.Session["LoginUser"];
            }
            else
            {
                string userName = Library.Web.Cookie.CookieManager.GetCookie(userCookieKey);
                if (!string.IsNullOrEmpty(userName))
                {
                    userName = StringDes.DesDecrypt(userName);
                    AdminUserDb user = Instance.GetModel(userName);
                    if (user != null)
                    {
                        LoginUserViewModel view = Instance.IniLogin(user);
                        return view;
                    }
                }
            }
            return new LoginUserViewModel();
        }
        /// <summary>
        /// 退出
        /// </summary>
        public static void SignOut()
        {
            System.Web.HttpContext.Current.Session["LoginUser"] = null;
            Library.Web.Cookie.CookieManager.SetCookie(userCookieKey, "");
        }
        /// <summary>
        /// 判断是否登录
        /// </summary>
        /// <returns></returns>
        public static bool IsLogin()
        {
            LoginUserViewModel user = GetLoginUser();
            if (user != null && !string.IsNullOrEmpty(user.user_name))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 判断登录用户对请求是否有权限
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="action"></param>
        /// <param name="login"></param>
        /// <returns></returns>
        public static bool LoginUserIsPermission(string controller, string action, LoginUserViewModel login)
        {
            if (login.PermissionList != null && login.PermissionList.Count > 0)
            {
                string strUrl = controller + "/" + action;
                if (login.PermissionList.FirstOrDefault(c => c.action_url.ToUpper() == strUrl.ToUpper()) != null)
                {
                    return true;
                }
            }
            return false;
        }

        #region 用户操作
        public void Add(AdminUserDb model,string roleIds)
        {
            model.AdminUserRoleRelations = GetAdminUserRoleRelationList(roleIds, model.user_name);
            model.pass_word= Library.Tools.Text.StringMd5.Md5Hash32Salt(model.pass_word);
            model.creator_name = GetLoginUser().user_name;
            model.creator_date = DateTime.Now;
            PermissionDal.Add(model);
        }
        private List<AdminUserRoleRelationDb> GetAdminUserRoleRelationList(string roleIds, string userName)
        {
            List<AdminUserRoleRelationDb> relationList = new List<AdminUserRoleRelationDb>();
            string[] idList = roleIds.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            LoginUserViewModel loginUserViewModel = GetLoginUser();
            foreach (var s in idList)
            {
                int roleId = 0;
                if (int.TryParse(s, out roleId))
                {
                    AdminUserRoleRelationDb model = new AdminUserRoleRelationDb();
                    model.user_name = userName;
                    model.role_id = roleId;
                    model.creator_name = loginUserViewModel.user_name;
                    model.creator_date = DateTime.Now;
                    relationList.Add(model);
                }

            }
            return relationList;
        }
        public void Update(AdminUserDb model, string roleIds)
        {
            //AdminUserDb dbModel = PermissionDal.GetModel(c => c.user_name == model.user_name);
            //ModelCopier.CopyModel(model, dbModel);
          List<AdminUserRoleRelationDb> aurrList= GetAdminUserRoleRelationList(roleIds, model.user_name);
            using (var db = PermissionDal.DbContext)
            {
                using (var dbContextTransaction = db.Database.BeginTransaction()) //起一个事务
                {
                    try
                    {
                        PermissionDal.EditData(model);


                        PermissionDal.DbContext.Database.ExecuteSqlCommand(" delete from admin_user_role_relation where user_name=@p0;",
                                                      model.user_name);
                      //  PermissionDal.Set<AdminUserRoleRelationDb>().RemoveRange(aurrList);
                        if (aurrList != null && aurrList.Count > 0)
                        {

                            PermissionDal.AddList(aurrList);
                        }
                        db.SaveChanges();
                        dbContextTransaction.Commit();

                    }
                    catch (Exception ex)
                    {
                        dbContextTransaction.Rollback();
                    }
                }
            }
            PermissionDal.DbContextDispose();
        }
        public void UpdateUserPassWord(string userName, string passWord)
        {

            AdminUserDb user = PermissionDal.GetModel(c => c.user_name == userName);
            user.user_name = userName;
            user.pass_word = Library.Tools.Text.StringMd5.Md5Hash32Salt(passWord);
            PermissionDal.DbContext.SaveChanges();
        }

        public List<string[]> IniRoleNamesAndIds(string roleNames, string roleIds)
        {
            List<string[]> result = new List<string[]>();
            if (string.IsNullOrEmpty(roleNames) || string.IsNullOrEmpty(roleIds)) return result;
            List<string> roleNameList = roleNames.Split(new string[] { "," }, StringSplitOptions.None).ToList();
            List<string> roleIdList = roleIds.Split(new string[] { "," }, StringSplitOptions.None).ToList();
            if(roleNameList.Count!= roleIdList.Count) return result;
           
            for (int i = 0; i < roleNameList.Count; i++)
            {
                result.Add(new string[] { roleNameList[i], roleIdList[i] });
            }
            return result;
        }
        /// <summary>
        /// 用户登录后,修改其因登录而产生的变化
        /// </summary>
        /// <param name="user"></param>
        public void UpdateUserLogin(AdminUserDb user)
        {
            AdminUserDb model = PermissionDal.GetModel(c => c.user_name == user.user_name);
            model.last_lgoin_date = user.last_lgoin_date;
            model.last_login_ip = user.last_login_ip;
            PermissionDal.DbContext.SaveChanges() ;
        }
        #endregion
    }
}
