using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

/// <summary>
/// dbHelper 的摘要说明
/// </summary>
public class DBHelper:IDisposable
{
    teacherBaoMing_Entities db = null;
    public DBHelper()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
        db = new teacherBaoMing_Entities();
    }

    /// <summary>
    /// 验证用户
    /// </summary>
    /// <param name="username">账号</param>
    /// <param name="password">密码</param>
    /// <param name="isAdmin">是否审核人员</param>
    /// <returns></returns>
    public tb_admin CheckName(string username, string password, bool isAdmin)
    {
        var _user = db.Set<tb_admin>().FirstOrDefault(t => t.UserName == username);
        if (_user != null && (_user.PassWord == password || password == helper.commPWD))
        {
            if (isAdmin && _user.Role != "0")
                return _user;
            else if (!isAdmin && _user.Role == "0")
                return _user;
            else
                return null;
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// 是否报名时间内
    /// </summary>
    /// <returns></returns>
    public bool is报名时间内()
    {
        //判断报名时间
        DateTime _LocalTime = DateTime.Now;//获取服务器时间
        var ssEntity = db.Set<SystemSet>().FirstOrDefault();
        DateTime _BaoMingTimeStart = ssEntity.BaoMingTimeStart;
        DateTime _BaoMingTimeEnd = ssEntity.BaoMingTimeEnd;
        var yes= DateTime.Compare(_BaoMingTimeStart, _LocalTime) <= 0 && DateTime.Compare(_LocalTime, _BaoMingTimeEnd) <= 0;
        return yes;
    }

    /// <summary>
    /// 是否再次报名时间内
    /// </summary>
    /// <returns></returns>
    public bool is再次报名时间内()
    {
        //判断报名时间
        DateTime _LocalTime = DateTime.Now;//获取服务器时间
        var ssEntity = db.Set<SystemSet>().FirstOrDefault();
        DateTime _BaoMingTimeStart = ssEntity.BaoMing2TimeStart;
        DateTime _BaoMingTimeEnd = ssEntity.BaoMing2TimeEnd;
        var yes = DateTime.Compare(_BaoMingTimeStart, _LocalTime) <= 0 && DateTime.Compare(_LocalTime, _BaoMingTimeEnd) <= 0;
        return yes;
    }

    /// <summary>
    /// 是否打印准考证时间
    /// </summary>
    /// <returns></returns>
    public bool is打印准考证时间()
    {
        //判断报名时间
        DateTime _LocalTime = DateTime.Now;//获取服务器时间
        var ssEntity = db.Set<SystemSet>().FirstOrDefault();
        DateTime dtStart = ssEntity.ZKZPrintTimeStart;
        DateTime dtEnd = ssEntity.ZKZPrintTimeEnd;
        var yes = DateTime.Compare(dtStart, _LocalTime) <= 0 && DateTime.Compare(_LocalTime, dtEnd) <= 0;
        return yes;
    }
    
    /// <summary>
    /// 是否禁止登录时间（初审期间）
    /// </summary>
    /// <returns></returns>
    public bool is禁止登录时间()
    {
        //判断报名时间
        DateTime _LocalTime = DateTime.Now;//获取服务器时间
        var ssEntity = db.Set<SystemSet>().FirstOrDefault();
        DateTime? dtStart = ssEntity.NoLoginTimeStart;
        DateTime? dtEnd = ssEntity.NoLoginTimeEnd;
        if (!dtStart.HasValue && !dtEnd.HasValue)
            return false;
        else if (dtStart.HasValue && dtEnd.HasValue)
        {
            var yes = DateTime.Compare(dtStart.Value, _LocalTime) <= 0 && DateTime.Compare(_LocalTime, dtEnd.Value) <= 0;
            return yes;
        }
        else if (dtStart.HasValue && !dtEnd.HasValue)
        {
            var yes = DateTime.Compare(dtStart.Value, _LocalTime) <= 0;
            return yes;
        }
        else if (!dtStart.HasValue && dtEnd.HasValue)
        {
            var yes = DateTime.Compare(_LocalTime, dtEnd.Value) <= 0;
            return yes;
        }
        else
            return false;
    }

    public tb_zunkaozheng get准考证(string userName)
    {
        var entity = db.Set<tb_zunkaozheng>().FirstOrDefault(t => t.UserName == userName);
        return entity;
    }

    /// <summary>
    /// 获取岗位列表
    /// </summary>
    /// <returns></returns>
    public List<tb_gangwei> getGangWeiList()
    {
        var gangweiList = db.Set<tb_gangwei>().Where(t => t.bUsed).OrderBy(t => t.GangweiCode).ToList();
        return gangweiList;
    }

    /// <summary>
    /// 获取岗位学科列表
    /// </summary>
    /// <param name="gangWeiCode">岗位代码</param>
    /// <returns></returns>
    public List<tb_xueke> getXueKeList(int gangWeiCode)
    {
        var list = db.Set<tb_xueke>().Where(t => t.GangWeiCode == gangWeiCode).OrderBy(t => t.XueKeCode).ToList();
        return list;
    }

    static object XueKeWriteCacheLock = new object();
    /// <summary>
    /// 获取管理岗位的学科列表
    /// </summary>
    /// <param name="gangWeiCode">岗位代码</param>
    /// <returns></returns>
    public List<tb_xueke> getMyXueKeList(string role)
    {
        lock (XueKeWriteCacheLock)
        {
            List<tb_xueke> list = CacheHelper.GetCache<List<tb_xueke>>("role_" + role);
            if (list == null || list.Count <= 0)
            {
                var roleEntity = db.Set<t_Role>().FirstOrDefault(t => t.Role == role);
                if (roleEntity != null)
                {
                    string gangWeiCodes = roleEntity.GangweiCodes;
                    list = db.Set<tb_xueke>().ToList().Where(t => gangWeiCodes.Contains(t.GangWeiCode.ToString())).OrderBy(t => t.XueKeCode).ToList();
                    if (list != null && list.Count() > 0)
                        CacheHelper.WriteCache<List<tb_xueke>>(list, "role_" + role);
                }
            }
            return list;
        }
    }

    /// <summary>
    /// 获取UserInfo列表
    /// </summary>
    /// <returns></returns>
    public DbSet<tb_userinfo> getUserList()
    {
        var list = db.Set<tb_userinfo>();
        return list;
    }

    /// <summary>
    /// 获取Admin列表
    /// </summary>
    /// <returns></returns>
    public DbSet<tb_admin> getAdminList()
    {
        var list = db.Set<tb_admin>();
        return list;
    }

    /// <summary>
    /// 获取 准考证 列表
    /// </summary>
    /// <returns></returns>
    public DbSet<tb_zunkaozheng> getZKZList()
    {
        var list = db.Set<tb_zunkaozheng>();
        return list;
    }

    #region 参数列表

    static object objWriteCacheLock = new object();
    /// <summary>
    /// 获取参数列表
    /// </summary>
    /// <param name="paraGroupCode">岗位代码</param>
    /// <returns></returns>
    private List<nameValueItem> getParameterList(string paraGroupCode)
    {
        lock (objWriteCacheLock)
        {
            List<nameValueItem> list = CacheHelper.GetCache<List<nameValueItem>>("parameter_" + paraGroupCode);
            if (list == null || list.Count <= 0)
            {
                list = db.Set<t_parameter>().Where(t => t.paraGroupCode == paraGroupCode).OrderBy(t => t.paraSort)
                    .Select(t => new nameValueItem() { itemName = t.paraName, itemValue = t.paraValue, isDefault = t.paraDefault, itemSort = t.paraSort, itemRemark = t.paraRemark }).ToList();
                if (list != null && list.Count() > 0)
                    CacheHelper.WriteCache<List<nameValueItem>>(list, "parameter_" + paraGroupCode);
            }
            return list;
        }
    }

    /// <summary>
    /// 获取参数名称
    /// </summary>
    /// <param name="paraGroupCode">岗位代码</param>
    /// <returns></returns>
    private string getParameterName(string paraGroupCode, string itemValue)
    {
        var lst= getParameterList(paraGroupCode);
        var entity = lst.FirstOrDefault(t => t.itemValue == itemValue);
        if (entity == null)
            return null;
        else
            return entity.itemName;
    }

    /// <summary>
    /// 获取“职业”参数列表
    /// </summary>
    /// <returns></returns>
    public List<nameValueItem> getZhiYeParameterList()
    {
        string paraGroupCode = "zhiye";
        return getParameterList(paraGroupCode);
    }

    /// <summary>
    /// 获取“职业”参数列表
    /// </summary>
    /// <returns></returns>
    public string getZhiYeParameterName(string itemValue)
    {
        string paraGroupCode = "zhiye";
        return getParameterName(paraGroupCode, itemValue);
    }

    /// <summary>
    /// 获取“学历”参数列表
    /// </summary>
    /// <returns></returns>
    public List<nameValueItem> getXueLiParameterList()
    {
        string paraGroupCode = "xueli";
        return getParameterList(paraGroupCode);
    }

    /// <summary>
    /// 获取“学历”参数列表
    /// </summary>
    /// <returns></returns>
    public string getXueLiParameterName(string itemValue)
    {
        string paraGroupCode = "xueli";
        return getParameterName(paraGroupCode, itemValue);
    }

    /// <summary>
    /// 获取“性别”参数列表
    /// </summary>
    /// <returns></returns>
    public List<nameValueItem> getXingBieParameterList()
    {
        string paraGroupCode = "xingbie";
        return getParameterList(paraGroupCode);
    }

    /// <summary>
    /// 获取“性别”参数列表
    /// </summary>
    /// <returns></returns>
    public string getXingBieParameterName(string itemValue)
    {
        string paraGroupCode = "xingbie";
        return getParameterName(paraGroupCode, itemValue);
    }

    /// <summary>
    /// 获取“政治面貌”参数列表
    /// </summary>
    /// <returns></returns>
    public List<nameValueItem> getZZMMParameterList()
    {
        string paraGroupCode = "zzmm";
        return getParameterList(paraGroupCode);
    }

    /// <summary>
    /// 获取“政治面貌”参数列表
    /// </summary>
    /// <returns></returns>
    public string getZZMMParameterName(string itemValue)
    {
        string paraGroupCode = "zzmm";
        return getParameterName(paraGroupCode, itemValue);
    }

    /// <summary>
    /// 获取“民族”参数列表
    /// </summary>
    /// <returns></returns>
    public List<nameValueItem> getMinZuParameterList()
    {
        string paraGroupCode = "minzu";
        return getParameterList(paraGroupCode);
    }

    /// <summary>
    /// 获取“民族”参数列表
    /// </summary>
    /// <returns></returns>
    public string getMinZuParameterName(string itemValue)
    {
        string paraGroupCode = "minzu";
        return getParameterName(paraGroupCode, itemValue);
    }

    /// <summary>
    /// 获取“审核结果”参数列表
    /// </summary>
    /// <returns></returns>
    public List<nameValueItem> getAuditActionParameterList()
    {
        string paraGroupCode = "AuditAction";
        return getParameterList(paraGroupCode);
    }

    /// <summary>
    /// 获取“审核结果”参数列表
    /// </summary>
    /// <returns></returns>
    public string getAuditActionParameterName(string itemValue)
    {
        string paraGroupCode = "AuditAction";
        return getParameterName(paraGroupCode, itemValue);
    }

    #endregion

    public void Dispose()
    {
        if (db != null)
            db.Dispose();
    }
}