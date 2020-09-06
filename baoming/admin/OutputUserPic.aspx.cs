using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Hosting;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_OutputUserPic : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        using (teacherBaoMing_Entities db = new teacherBaoMing_Entities())
        {
            var userID_Pics = db.tb_userinfo.Select(t => new { t.UserName, t.XingMing, t.IDPhoto }).ToList();
            bool isSucc = true;
            int CountSucc = 0;
            StringBuilder failInfo = new StringBuilder();
            try
            {
                userID_Pics.ForEach(t =>
                {
                    string newFileName = HostingEnvironment.MapPath("~/userPics/" + t.UserName + t.IDPhoto.Substring(t.IDPhoto.Length - 4));
                    bool _isSucc = ImgHelper.MakePic(t.XingMing, t.IDPhoto, newFileName, 150, 200,true, true, ref failInfo);
                    if (_isSucc)
                        CountSucc++;
                    isSucc &= _isSucc;
                });
            }
            catch (Exception ex)
            {
                isSucc = false;
                failInfo.AppendLine(ex.Message);
                if (ex.InnerException != null)
                    failInfo.AppendLine(ex.InnerException.Message);
            }
            string outStr = "图片生成成功的数量为" + CountSucc + "<br/>";
            if (!isSucc)
            {
                outStr += @"图片生成失败的情况如下：<br/>" + failInfo.Replace(Environment.NewLine, "<br/>").ToString();
            }
            litMessage.Text = outStr;
        }
    }

    protected void btnOutput_Click(object sender, EventArgs e)
    {
        var path = HostingEnvironment.MapPath("~/userPics/");
        dlZipDir(path, "userPics" + DateTime.Now.Year);
    }

    ZipOutputStream zos = null;
    String strBaseDir = "";
    protected void dlZipDir(string strPath, string strFileName)
    {
        MemoryStream ms = null;
        Response.ContentType = "application/octet-stream";
        strFileName = HttpUtility.UrlEncode(strFileName);
        Response.AddHeader("Content-Disposition", "attachment;   filename=" + strFileName + ".zip");
        ms = new MemoryStream();
        zos = new ZipOutputStream(ms);
        strBaseDir = strPath + "";
        addZipEntry(strBaseDir);
        zos.Finish();
        zos.Close();
        Response.Clear();
        Response.BinaryWrite(ms.ToArray());
        Response.End();
    }

    protected void addZipEntry(string PathStr)
    {
        DirectoryInfo di = new DirectoryInfo(PathStr);
        foreach (DirectoryInfo item in di.GetDirectories())
        {
            addZipEntry(item.FullName);
        }
        foreach (FileInfo item in di.GetFiles())
        {
            FileStream fs = File.OpenRead(item.FullName);
            byte[] buffer = new byte[fs.Length];
            fs.Read(buffer, 0, buffer.Length);
            string strEntryName = item.FullName.Replace(strBaseDir, "");
            ZipEntry entry = new ZipEntry(strEntryName);
            zos.PutNextEntry(entry);
            zos.Write(buffer, 0, buffer.Length);
            fs.Close();
        }
    }
}