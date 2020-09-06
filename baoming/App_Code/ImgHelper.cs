using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.IO;
using System.Web.Hosting;
using System.Text;

/// <summary>
///ImgHelper 的摘要说明
/// </summary>
public class ImgHelper
{
    /// <summary>
    /// 压缩图片（等比例缩小，若小于最大宽度且小于最大高度，则不处理）
    /// </summary>
    /// <param name="imgStream">图片的IO流</param>
    /// <param name="maxWidth">最大宽度(若非正数，不作为条件)</param>
    /// <param name="maxHeight">最大高度(若非正数，不作为条件)</param>
    /// <returns>图片对象</returns>
    public static Image Imp_Zip(Stream imgStream, double maxWidth, double maxHeight)
    {
        Image _img = Image.FromStream(imgStream);
        return Imp_Zip(_img, maxWidth, maxHeight);
    }

    /// <summary>
    /// 压缩图片（等比例缩小，若小于最大宽度且小于最大高度，则不处理）
    /// </summary>
    /// <param name="imgStream">图片的IO流</param>
    /// <param name="maxWidth">最大宽度(若非正数，不作为条件)</param>
    /// <param name="maxHeight">最大高度(若非正数，不作为条件)</param>
    /// <returns>图片对象</returns>
    public static Image Imp_Zip(Image _img, double maxWidth, double maxHeight)
    {
        if (maxWidth <= 0 && maxHeight <= 0)
            throw new Exception("缩放的目标宽高不能都为非正数！");
        double _oldWidth = Convert.ToDouble(_img.Width);
        double _oldHeight = Convert.ToDouble(_img.Height);
        if (_oldWidth <= maxWidth && _oldHeight <= maxHeight)
            return _img;

        //取长宽缩放比的最小值
        double _scale比例因子 = maxWidth / _oldWidth;
        double _tmpScaleHeight = maxHeight / _oldHeight;
        if (maxWidth <= 0)
            _scale比例因子 = _tmpScaleHeight;//按高度缩放
        else if (maxHeight <= 0)
        {
            //按宽度缩放
        }
        else if (_scale比例因子 > _tmpScaleHeight)
            _scale比例因子 = _tmpScaleHeight;

        //计算新的高度和宽度
        int _newHeight = Convert.ToInt32(_oldHeight * _scale比例因子);
        int _newWidth = Convert.ToInt32(_oldWidth * _scale比例因子);
        Bitmap bt = new Bitmap(_img, _newWidth, _newHeight);
        Image rt_img = bt;
        return rt_img;
    }

    /// <summary>
    /// 压缩图片（等比例缩小，若小于最大宽度且小于最大高度，则留白）
    /// </summary>
    /// <param name="imgStream">图片的IO流</param>
    /// <param name="maxWidth">最大宽度(若非正数，不作为条件)</param>
    /// <param name="maxHeight">最大高度(若非正数，不作为条件)</param>
    /// <returns>图片对象</returns>
    public static Image Imp_Zip1(Image originalImage, double maxWidth, double maxHeight)
    {
        if (maxWidth <= 0 && maxHeight <= 0)
            throw new Exception("缩放的目标宽高不能都为非正数！");
        double _oldWidth = Convert.ToDouble(originalImage.Width);
        double _oldHeight = Convert.ToDouble(originalImage.Height);
        //if (_oldWidth <= maxWidth && _oldHeight <= maxHeight)
        //    return originalImage;

        //取长宽缩放比的最小值
        double _scale比例因子 = maxWidth / _oldWidth;
        double _height比例因子 = maxHeight / _oldHeight;
        if (maxWidth <= 0)
            _scale比例因子 = _height比例因子;//按高度缩放
        else if (maxHeight <= 0)
        {
            //按宽度缩放
        }
        else if (_scale比例因子 > _height比例因子)
            _scale比例因子 = _height比例因子;

        //计算新的高度和宽度
        int _newHeight = Convert.ToInt32(_oldHeight * _scale比例因子);
        int _newWidth = Convert.ToInt32(_oldWidth * _scale比例因子);

        Image rt_img = null;
        System.Drawing.Graphics g = null;
        try
        {
            //新建一个bmp图片
            System.Drawing.Image bitmap = new System.Drawing.Bitmap((int)maxWidth, (int)maxHeight);
            //新建一个画板
            g = System.Drawing.Graphics.FromImage(bitmap);
            //设置高质量插值法
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            //设置高质量,低速度呈现平滑程度
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            //清空画布并以透明背景色填充
            g.Clear(System.Drawing.Color.Transparent);
            //在指定位置并且按指定大小绘制原图片的指定部分
            int x = (int)(maxWidth - _newWidth) / 2;
            int y = (int)(maxHeight - _newHeight) / 2;
            g.DrawImage(originalImage, x, y, _newWidth, _newHeight);
            //try
            //{
            //    //以jpg格式保存缩略图
            //    bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
            //}
            //catch (System.Exception e)
            //{
            //    throw e;
            //}
            //finally
            //{
            //    originalImage.Dispose();
            //    bitmap.Dispose();
            //    g.Dispose();
            //}

            //Bitmap bt = new Bitmap(originalImage, _newWidth, _newHeight);
            //Image rt_img = bt;
            rt_img = bitmap;
        }
        catch
        {
            throw;
        }
        finally
        {
            if(originalImage!=null)
                originalImage.Dispose();
            if(g!=null)
                g.Dispose();
        }
        return rt_img;
    }

    /// <summary>
    /// 制作显示用的图片，如果目标图片存在，则不处理；若不存在，则在相同目录下面生成目标图片文件
    /// 新文件格式：P_Width_Height_原文件名
    /// </summary>
    /// <param name="orgPicFilename">原图片文件(可以绝对路径或相对路径)，如：/Upload/120/TouXiang/fpic.jpg</param>
    /// <param name="width">目标图片的宽(若非正数，不作为条件)</param>
    /// <param name="height">目标图片的高(若非正数，不作为条件)</param>
    private static bool MakePicForDisplay(string orgPicFilename, int width, int height, ref string ReasonORFilename,bool rocateToUP)
    {
        string _orgPicFilename = orgPicFilename;
        if (_orgPicFilename.StartsWith("~/"))
            _orgPicFilename = HostingEnvironment.MapPath(_orgPicFilename);
        FileInfo fi = new FileInfo(_orgPicFilename);
        if (fi.Exists)
        {
            string newPicFileName = string.Format("P_{0}_{1}_{2}", width, height, fi.Name);
            string _newPathFile = Path.Combine(fi.DirectoryName, newPicFileName);
            if (!File.Exists(_newPathFile))
            {
                try
                {
                    Image _org = Image.FromFile(_orgPicFilename);
                    if (rocateToUP)
                    {
                        //读取exif信息，判断旋转

                        ExifManager em = new ExifManager(_org);
                        _org=em.rotatingToUP();
                    }

                    Image _img = Imp_Zip(_org, width, height);
                    _img.Save(_newPathFile);
                }
                catch (Exception ex)
                {
                    if (!string.IsNullOrEmpty(ReasonORFilename))
                        ReasonORFilename += Environment.NewLine;
                    ReasonORFilename += string.Format("文件{0}转变成缩略图时异常！{1}", orgPicFilename, ex.Message);
                    return false;
                }
            }
            ReasonORFilename = Path.GetDirectoryName(orgPicFilename) + "/" + newPicFileName;
            return true;
        }
        else
        {
            if (!string.IsNullOrEmpty(ReasonORFilename))
                ReasonORFilename += Environment.NewLine;
            ReasonORFilename += string.Format("文件{0}不存在！", orgPicFilename);
            return false;
        }
    }
    public static bool MakePic(string realName, string orgPicFilename, string _newPathFile, int width, int height, bool overRide, bool rocateToUP, ref StringBuilder failInfo)
    {
        string _orgPicFilename = orgPicFilename;
        if (_orgPicFilename.StartsWith("~/"))
            _orgPicFilename = HostingEnvironment.MapPath(_orgPicFilename);
        FileInfo fi = new FileInfo(_orgPicFilename);
        if (fi.Exists)
        {
            //string _newPathFile = Path.Combine(fi.DirectoryName, newPicFileName);
            if (overRide || !File.Exists(_newPathFile))
            {
                try
                {
                    Image _org = Image.FromFile(_orgPicFilename);
                    if (rocateToUP)
                    {
                        //读取exif信息，判断旋转

                        ExifManager em = new ExifManager(_org);
                        _org = em.rotatingToUP();
                    }

                    Image _img = Imp_Zip1(_org, width, height);
                    if (_img != null)
                    {
                        _img.Save(_newPathFile);
                        _img.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    failInfo.AppendFormat("{0}的照片文件{1}转变成缩略图时异常！{2}", realName, orgPicFilename, ex.Message);
                    failInfo.AppendLine();
                    return false;
                }
            }
            return true;
        }
        else
        {
            failInfo.AppendFormat("{0}的照片文件{1}不存在！", realName, orgPicFilename);
            failInfo.AppendLine();
            return false;
        }
    }

    /// <summary>
    /// 加载图片
    /// </summary>
    /// <param name="width">目标图片的宽(若非正数，不作为条件)</param>
    /// <param name="height">目标图片的高(若非正数，不作为条件)</param>
    /// <param name="picName">默认图片名称，相对根路径</param>
    /// <returns></returns>
    public static string LoadImage(int width, int height,string picName = null,bool rocateToUP=true)
    {
        string _picName = "~/images/default.gif";
        if (!string.IsNullOrWhiteSpace(picName))
            _picName = picName;
        string ReasonORFilename = string.Empty;
        bool bSuccess = MakePicForDisplay(_picName, width, height, ref ReasonORFilename, true);
        return ReasonORFilename.Replace('\\', '/');
    }
}
