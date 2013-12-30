﻿/*
 * XCoder v5.1.4992.36291
 * 作者：nnhy/X
 * 时间：2013-09-01 20:13:59
 * 版权：版权所有 (C) 新生命开发团队 2002~2013
*/
﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml.Serialization;
using NewLife.CMX.Config;
using NewLife.CommonEntity;
using NewLife.Log;
using NewLife.Web;
using XCode;
using XCode.Configuration;

namespace NewLife.CMX
{
    /// <summary>频道</summary>
    public partial class Channel : Entity<Channel>
    {
        #region 对象操作﻿
        /// <summary>验证数据，通过抛出异常的方式提示验证失败。</summary>
        /// <param name="isNew"></param>
        public override void Valid(Boolean isNew)
        {
            // 这里验证参数范围，建议抛出参数异常，指定参数名，前端用户界面可以捕获参数异常并聚焦到对应的参数输入框
            //if (String.IsNullOrEmpty(Name)) throw new ArgumentNullException(__.Name, _.Name.DisplayName + "无效！");
            //if (!isNew && ID < 1) throw new ArgumentOutOfRangeException(__.ID, _.ID.DisplayName + "必须大于0！");

            // 建议先调用基类方法，基类方法会对唯一索引的数据进行验证
            base.Valid(isNew);

            // 在新插入数据或者修改了指定字段时进行唯一性验证，CheckExist内部抛出参数异常
            //if (isNew || Dirtys[__.Name]) CheckExist(__.Name);
            //if (isNew || Dirtys[__.Suffix]) CheckExist(__.Suffix);

            if (isNew && !Dirtys[__.CreateUserID]) CreateUserID = Admin.Current.ID;
            if (!Dirtys[__.UpdateUserID]) UpdateUserID = Admin.Current.ID;
            if (isNew && !Dirtys[__.CreateTime]) CreateTime = DateTime.Now;
            if (!Dirtys[__.UpdateTime]) UpdateTime = DateTime.Now;
        }

        /// <summary>首次连接数据库时初始化数据，仅用于实体类重载，用户不应该调用该方法</summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        protected override void InitData()
        {
            base.InitData();

            // InitData一般用于当数据表没有数据时添加一些默认数据，该实体类的任何第一次数据库操作都会触发该方法，默认异步调用
            // Meta.Count是快速取得表记录数
            if (Meta.Count > 0) return;

            // 需要注意的是，如果该方法调用了其它实体类的首次数据库操作，目标实体类的数据初始化将会在同一个线程完成
            if (XTrace.Debug) XTrace.WriteLine("开始初始化{0}[{1}]数据……", typeof(Channel).Name, Meta.Table.DataTable.DisplayName);

            Model.Meta.WaitForInitData(3000);
            foreach (var item in Model.Meta.Cache.Entities)
            {
                var entity = new Channel();
                entity.ModelID = item.ID;

                //switch (item.Name)
                //{
                //    case "文章模型":
                //        entity.FormTemplate = CMXDefaultArticleModelConfig.Current.ContentModelPath;
                //        entity.ListTemplate = CMXDefaultArticleModelConfig.Current.ListModelPath;
                //        entity.Suffix = CMXDefaultArticleModelConfig.Current.Suffix;
                //        break;
                //    case "文本模型":
                //        entity.FormTemplate = CMXDefaultTextModelConfig.Current.ContentModelPath;
                //        entity.ListTemplate = CMXDefaultTextModelConfig.Current.ListModelPath;
                //        entity.Suffix = CMXDefaultTextModelConfig.Current.Suffix;
                //        break;
                //    case "产品模型":
                //        entity.FormTemplate = CMXDefaultProductModelConfig.Current.ContentModelPath;
                //        entity.ListTemplate = CMXDefaultProductModelConfig.Current.ListModelPath;
                //        entity.Suffix = CMXDefaultProductModelConfig.Current.Suffix;
                //        break;
                //    default:
                //        break;
                //}
                entity.ListTemplate = item.Provider.TitleType.Name + "ModelList.html";
                entity.FormTemplate = item.Provider.TitleType.Name + "ModelContent.html";
                entity.Name = "默认" + item.Name.Replace("模型", "频道");
                entity.Enable = true;
                entity.Save();
            }

            if (XTrace.Debug) XTrace.WriteLine("完成初始化{0}[{1}]数据！", typeof(Channel).Name, Meta.Table.DataTable.DisplayName);
        }

        ///// <summary>已重载。基类先调用Valid(true)验证数据，然后在事务保护内调用OnInsert</summary>
        ///// <returns></returns>
        //public override Int32 Insert()
        //{
        //    return base.Insert();
        //}

        ///// <summary>已重载。在事务保护范围内处理业务，位于Valid之后</summary>
        ///// <returns></returns>
        //protected override Int32 OnInsert()
        //{
        //    return base.OnInsert();
        //}
        #endregion

        #region 扩展属性﻿
        private Model _Model;
        /// <summary>模型</summary>
        public Model Model
        {
            get
            {
                if (_Model == null && ModelID > 0 && !Dirtys.ContainsKey("Model"))
                {
                    _Model = Model.FindByID(ModelID);
                    Dirtys["Model"] = true;
                }
                return _Model;
            }
            set { _Model = value; }
        }

        /// <summary>模型名称</summary>
        public String ModelName { get { return Model != null ? Model.Name : "未命名"; } }

        private Admin _CreateUser;
        /// <summary>创建人</summary>
        public Admin CreateUser
        {
            get
            {
                if (_CreateUser == null && CreateUserID > 0 && !Dirtys.ContainsKey("CreateUser"))
                {
                    _CreateUser = Admin.FindByID(CreateUserID);
                    Dirtys["CreateUser"] = true;
                }
                return _CreateUser;
            }
            set { _CreateUser = value; }
        }

        /// <summary>创建人名称</summary>
        public String CreateUserName { get { return CreateUser != null ? CreateUser.DisplayName : ""; } }

        private Admin _UpdateUser;
        /// <summary>更新人</summary>
        public Admin UpdateUser
        {
            get
            {
                if (_UpdateUser == null && UpdateUserID > 0 && !Dirtys.ContainsKey("UpdateUser"))
                {
                    _UpdateUser = Admin.FindByID(UpdateUserID);
                    Dirtys["UpdateUser"] = true;
                }
                return _UpdateUser;
            }
            set { _UpdateUser = value; }
        }

        /// <summary>更新人名称</summary>
        public String UpdateUserName { get { return UpdateUser != null ? UpdateUser.DisplayName : ""; } }
        #endregion

        #region 扩展查询﻿
        /// <summary>根据ID查询</summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static Channel FindByID(Int32 id)
        {
            if (Meta.Count >= 1000)
                return Find(__.ID, id);
            else
                return Meta.Cache.Entities.Find(__.ID, id);
        }

        /// <summary>根据名称查找</summary>
        /// <param name="name">名称</param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static Channel FindByName(String name)
        {
            if (Meta.Count >= 1000)
                return Find(__.Name, name);
            else // 实体缓存
                return Meta.Cache.Entities.Find(__.Name, name);
            // 单对象缓存
            //return Meta.SingleCache[name];
        }

        /// <summary>
        /// 根据扩展名查找
        /// </summary>
        /// <param name="Suffix"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static Channel FindBySuffix(String Suffix)
        {
            if (Meta.Count > 1000)
                return Find(__.Suffix, Suffix);
            else
                return Meta.Cache.Entities.Find(__.Suffix, Suffix);
        }

        /// <summary>
        /// 优先使用频道扩展名查询，在没有频道扩展名的前提下再使用模型编号查询
        /// 注意如果频道扩展名为空的情况下，只使用模型编号查询，返回的对象为所有使用该模型的频道中ID最后的一个
        /// </summary>
        /// <param name="Suffix"></param>
        /// <param name="ModelID"></param>
        /// <returns></returns>
        public static Channel FindBySuffixOrModel(String Suffix, Int32 ModelID = 0)
        {
            if (String.IsNullOrEmpty(Suffix) && ModelID == 0) return new Channel();

            if (!String.IsNullOrEmpty(Suffix) && ModelID > 0) return FindBySuffixAndModel(Suffix, ModelID);

            if (!String.IsNullOrEmpty(Suffix)) return FindBySuffix(Suffix);

            if (Meta.Count > 1000)
                return Find(__.ModelID, ModelID);
            else
                return Meta.Cache.Entities.Find(__.ModelID, ModelID);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Suffix"></param>
        /// <param name="ModelID"></param>
        /// <returns></returns>
        public static Channel FindBySuffixAndModel(String Suffix, Int32 ModelID)
        {
            if (String.IsNullOrEmpty(Suffix) || ModelID < 1) return new Channel();

            if (Meta.Count > 1000)
                return Find(new String[] { __.Suffix, __.ModelID }, new Object[] { Suffix, ModelID });
            else
                return Meta.Cache.Entities.Find(e => (e.Suffix == Suffix && e.ModelID == ModelID));
        }
        #endregion

        #region 高级查询
        // 以下为自定义高级查询的例子

        ///// <summary>
        ///// 查询满足条件的记录集，分页、排序
        ///// </summary>
        ///// <param name="key">关键字</param>
        ///// <param name="orderClause">排序，不带Order By</param>
        ///// <param name="startRowIndex">开始行，0表示第一行</param>
        ///// <param name="maximumRows">最大返回行数，0表示所有行</param>
        ///// <returns>实体集</returns>
        //[DataObjectMethod(DataObjectMethodType.Select, true)]
        //public static EntityList<Channel> Search(String key, String orderClause, Int32 startRowIndex, Int32 maximumRows)
        //{
        //    return FindAll(SearchWhere(key), orderClause, null, startRowIndex, maximumRows);
        //}

        ///// <summary>
        ///// 查询满足条件的记录总数，分页和排序无效，带参数是因为ObjectDataSource要求它跟Search统一
        ///// </summary>
        ///// <param name="key">关键字</param>
        ///// <param name="orderClause">排序，不带Order By</param>
        ///// <param name="startRowIndex">开始行，0表示第一行</param>
        ///// <param name="maximumRows">最大返回行数，0表示所有行</param>
        ///// <returns>记录数</returns>
        //public static Int32 SearchCount(String key, String orderClause, Int32 startRowIndex, Int32 maximumRows)
        //{
        //    return FindCount(SearchWhere(key), null, null, 0, 0);
        //}

        /// <summary>构造搜索条件</summary>
        /// <param name="key">关键字</param>
        /// <returns></returns>
        private static String SearchWhere(String key)
        {
            // WhereExpression重载&和|运算符，作为And和Or的替代
            // SearchWhereByKeys系列方法用于构建针对字符串字段的模糊搜索
            var exp = SearchWhereByKeys(key, null);

            // 以下仅为演示，Field（继承自FieldItem）重载了==、!=、>、<、>=、<=等运算符（第4行）
            //if (userid > 0) exp &= _.OperatorID == userid;
            //if (isSign != null) exp &= _.IsSign == isSign.Value;
            //if (start > DateTime.MinValue) exp &= _.OccurTime >= start;
            //if (end > DateTime.MinValue) exp &= _.OccurTime < end.AddDays(1).Date;

            return exp;
        }
        #endregion

        #region 扩展操作
        #endregion

        #region 业务
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj">可以是ID也可以SuffixStr</param>
        /// <returns></returns>
        //public static String GetListUrl(Object obj)
        //{
        //    if (obj == null) return null;

        //    Channel c = GetModel(obj);

        //    return c == null ? "" : c.Model.CategoryTemplatePath;
        //}

        //public static Channel GetModel(Object obj)
        //{
        //    if (obj == null) return null;
        //    Int32 i;
        //    if (Int32.TryParse(obj.ToString(), out i))
        //        return FindByID(i);
        //    else
        //        return FindBySuffix(obj.ToString());
        //}
        #endregion
    }
}