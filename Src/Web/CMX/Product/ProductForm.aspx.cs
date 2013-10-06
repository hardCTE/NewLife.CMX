﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NewLife.Log;
using NewLife.Web;
using NewLife.CMX;

public partial class CMX_ProductForm : MyModelEntityForm<Product>
{
    public String ContentTxt { get { return Entity.ConentTxt; } }

    protected override void OnInitComplete(EventArgs e)
    {
        base.OnInitComplete(e);

        Entity.CategoryName = Entity.CategoryName ?? Request["Name"];
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        ManagerPage.SetFormScript(true);

        EntityForm.OnGetForm += EntityForm_OnGetForm;
    }

    void EntityForm_OnGetForm(object sender, NewLife.CommonEntity.EntityFormEventArgs e)
    {
        Entity.ConentTxt = Request["MyContent"];

    }
}