﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.18444
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace ASP
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Web;
    using System.Web.Helpers;
    using System.Web.Mvc;
    using System.Web.Mvc.Ajax;
    using System.Web.Mvc.Html;
    using System.Web.Optimization;
    using System.Web.Routing;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.WebPages;
    using NewLife;
    
    #line 2 "..\..\Areas\Admin\Views\Index\_Left_Category.cshtml"
    using NewLife.CMX;
    
    #line default
    #line hidden
    using NewLife.Cube;
    
    #line 1 "..\..\Areas\Admin\Views\Index\_Left_Category.cshtml"
    using NewLife.Model;
    
    #line default
    #line hidden
    using NewLife.Reflection;
    using NewLife.Web;
    using XCode;
    using XCode.Membership;
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Areas/Admin/Views/Index/_Left_Category.cshtml")]
    public partial class _Areas_Admin_Views_Index__Left_Category_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Areas_Admin_Views_Index__Left_Category_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 3 "..\..\Areas\Admin\Views\Index\_Left_Category.cshtml"
  
    var cat = Model as IEntityCategory;
    var chn = cat.Channel;

            
            #line default
            #line hidden
WriteLiteral("\r\n<li>\r\n");

            
            #line 8 "..\..\Areas\Admin\Views\Index\_Left_Category.cshtml"
    
            
            #line default
            #line hidden
            
            #line 8 "..\..\Areas\Admin\Views\Index\_Left_Category.cshtml"
     if (cat.Childs.Count == 0)
    {

            
            #line default
            #line hidden
WriteLiteral("        <a");

WriteAttribute("href", Tuple.Create(" href=\"", 176), Tuple.Create("\"", 224)
, Tuple.Create(Tuple.Create("", 183), Tuple.Create<System.Object, System.Int32>(Href("~/CMS/")
, 183), false)
            
            #line 10 "..\..\Areas\Admin\Views\Index\_Left_Category.cshtml"
, Tuple.Create(Tuple.Create("", 189), Tuple.Create<System.Object, System.Int32>(chn.Model.Name
            
            #line default
            #line hidden
, 189), false)
, Tuple.Create(Tuple.Create("", 206), Tuple.Create("/", 206), true)
            
            #line 10 "..\..\Areas\Admin\Views\Index\_Left_Category.cshtml"
, Tuple.Create(Tuple.Create("", 207), Tuple.Create<System.Object, System.Int32>(chn.ID
            
            #line default
            #line hidden
, 207), false)
, Tuple.Create(Tuple.Create("", 216), Tuple.Create("_", 216), true)
            
            #line 10 "..\..\Areas\Admin\Views\Index\_Left_Category.cshtml"
, Tuple.Create(Tuple.Create("", 217), Tuple.Create<System.Object, System.Int32>(cat.ID
            
            #line default
            #line hidden
, 217), false)
);

WriteLiteral(" target=\"main\"");

WriteLiteral(">\r\n            <i");

WriteLiteral(" class=\"menu-icon fa fa-caret-right\"");

WriteLiteral("></i>\r\n");

WriteLiteral("            ");

            
            #line 12 "..\..\Areas\Admin\Views\Index\_Left_Category.cshtml"
       Write(cat.Name);

            
            #line default
            #line hidden
WriteLiteral("\r\n        </a>\r\n");

            
            #line 14 "..\..\Areas\Admin\Views\Index\_Left_Category.cshtml"
    }
    else
    {

            
            #line default
            #line hidden
WriteLiteral("        <a");

WriteLiteral(" href=\"#\"");

WriteLiteral(" class=\"dropdown-toggle\"");

WriteLiteral(">\r\n            <i");

WriteLiteral(" class=\"menu-icon fa fa-caret-right\"");

WriteLiteral("></i>\r\n");

WriteLiteral("            ");

            
            #line 19 "..\..\Areas\Admin\Views\Index\_Left_Category.cshtml"
       Write(cat.Name);

            
            #line default
            #line hidden
WriteLiteral("\r\n        </a>\r\n");

WriteLiteral("        <b");

WriteLiteral(" class=\"arrow\"");

WriteLiteral("></b>\r\n");

WriteLiteral("        <ul");

WriteLiteral(" class=\"submenu\"");

WriteLiteral(">\r\n");

            
            #line 23 "..\..\Areas\Admin\Views\Index\_Left_Category.cshtml"
            
            
            #line default
            #line hidden
            
            #line 23 "..\..\Areas\Admin\Views\Index\_Left_Category.cshtml"
             foreach (IEntityCategory cat2 in cat.Childs)
            {
                
            
            #line default
            #line hidden
            
            #line 25 "..\..\Areas\Admin\Views\Index\_Left_Category.cshtml"
           Write(Html.Partial("_Left_Category", cat2));

            
            #line default
            #line hidden
            
            #line 25 "..\..\Areas\Admin\Views\Index\_Left_Category.cshtml"
                                                     ;
            }

            
            #line default
            #line hidden
WriteLiteral("        </ul>\r\n");

            
            #line 28 "..\..\Areas\Admin\Views\Index\_Left_Category.cshtml"
    }

            
            #line default
            #line hidden
WriteLiteral("</li>");

        }
    }
}
#pragma warning restore 1591
