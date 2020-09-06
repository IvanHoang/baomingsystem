using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// controlHelper 的摘要说明
/// </summary>
public class controlHelper
{
    public controlHelper()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }

    /// <summary>
    /// 下拉列表控件 绑定数据
    /// </summary>
    /// <param name="ddl">下拉控件</param>
    /// <param name="datas">数据列表</param>
    /// <param name="selectValue">选中值，给null则默认选中第1项</param>
    
    public static void DropDownList_DataBind(DropDownList ddl, List<nameValueItem> datas,string selectValue)
    {
        ddl.DataSource = datas;
        ddl.DataTextField = "itemName";
        ddl.DataValueField = "itemValue";
        if (selectValue==null)
        {
            var defaultItem = datas.FirstOrDefault(t => t.isDefault == true);
            if (defaultItem == null)
                ddl.SelectedIndex = 0;
            else
                ddl.SelectedValue = defaultItem.itemValue;
        }
        else
            ddl.SelectedValue = selectValue;

        ddl.DataBind();
    }
}