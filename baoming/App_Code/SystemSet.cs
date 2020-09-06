using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
///SystemSet 的摘要说明
/// </summary>
public class SystemSetProvider
{
	public SystemSetProvider()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}

	public int get年份()
	{
		using (var db = new teacherBaoMing_Entities())
		{
			var ssEntity = db.Set<SystemSet>().FirstOrDefault();
			if (ssEntity != null)
				return ssEntity.NianFen;
			else
				throw new Exception("报名年份未设置");
		}
	}

	public DateTime get报名开始时间()
	{
		using (var db = new teacherBaoMing_Entities())
		{
			var ssEntity = db.Set<SystemSet>().FirstOrDefault();
			if (ssEntity != null)
				return ssEntity.BaoMingTimeStart;
			else
				throw new Exception("报名开始时间未设置");
		}
	}

	public DateTime get报名结束时间()
	{
		using (var db = new teacherBaoMing_Entities())
		{
			var ssEntity = db.Set<SystemSet>().FirstOrDefault();
			if (ssEntity != null)
				return ssEntity.BaoMingTimeEnd;
			else
				throw new Exception("报名结束时间未设置");
		}
	}

	public DateTime get准考证打印开始时间()
	{
		using (var db = new teacherBaoMing_Entities())
		{
			var ssEntity = db.Set<SystemSet>().FirstOrDefault();
			if (ssEntity != null)
				return ssEntity.ZKZPrintTimeStart;
			else
				throw new Exception("准考证打印开始时间未设置");
		}
	}

	public DateTime get再次报名开始时间()
	{
		using (var db = new teacherBaoMing_Entities())
		{
			var ssEntity = db.Set<SystemSet>().FirstOrDefault();
			if (ssEntity != null)
				return ssEntity.BaoMing2TimeStart;
			else
				throw new Exception("再次报名开始时间未设置");
		}
	}

	public DateTime get再次报名结束时间()
	{
		using (var db = new teacherBaoMing_Entities())
		{
			var ssEntity = db.Set<SystemSet>().FirstOrDefault();
			if (ssEntity != null)
				return ssEntity.BaoMing2TimeEnd;
			else
				throw new Exception("再次报名结束时间未设置");
		}
	}
}