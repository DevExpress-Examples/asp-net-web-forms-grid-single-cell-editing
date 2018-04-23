using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using System.Drawing;
using DevExpress.Web.ASPxRoundPanel;
using DevExpress.Web.Data;
using DevExpress.Web.ASPxClasses.Internal;

public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e) {
        if(ASPxGridView1.IsEditing && Session["column"] != null) {
            string fieldName = Convert.ToString(Session["column"]);
            for(int i = 0;i < ASPxGridView1.Columns.Count;i++) {
                GridViewDataColumn column = (GridViewDataColumn)ASPxGridView1.Columns[i];
                if(column != null && column.FieldName != fieldName)
                    column.EditItemTemplate = new MyTemplate();
            }
        }
    }
    protected void ASPxGridView1_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e) {
        e.Cell.Attributes.Add("onclick", "onCellClick(" + e.VisibleIndex + ", '" + e.DataColumn.FieldName + "')");
    }
    protected void ASPxGridView1_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e) {
        ASPxGridView gridView = (ASPxGridView)sender;
        gridView.UpdateEdit();
        string[] data = e.Parameters.Split(new char[] { '|' });
        gridView.FocusedRowIndex = Convert.ToInt32(data[0]);
        for(int i = 0;i < gridView.Columns.Count;i++) {
            GridViewDataColumn column = (GridViewDataColumn)gridView.Columns[i];
            if(column != null)
                if(column.FieldName != data[1])
                    column.EditItemTemplate = new MyTemplate();
                else {
                    Session["column"] = column.FieldName;
                    column.EditItemTemplate = null;
                }
        }
        gridView.StartEdit(Convert.ToInt32(data[0]));
    }
    protected void ASPxGridView1_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e) {
        if(e.RowType == GridViewRowType.InlineEdit) {
            string fieldName = Session["column"] == null ? "" : Session["column"].ToString();
            for(int i = 0;i < e.Row.Cells.Count;i++) {
                GridViewDataColumn column = ((GridViewDataColumn)((ASPxGridView)sender).VisibleColumns[i]);
                if(column.FieldName != fieldName)
                    e.Row.Cells[i].Attributes.Add("onclick", "onCellClick(" + e.VisibleIndex + ", '" + column.FieldName + "')");
            }
        }
    }
    protected void ASPxGridView1_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e) {
        e.Editor.Enabled = !e.Column.ReadOnly;
        if(e.Editor.Enabled) {
            ((ASPxTextBox)e.Editor).ClientSideEvents.KeyPress = "function(s,e) {OnEditorKeyPress(s, e);}";
        }
    }
    protected void ASPxGridView1_RowUpdated(object sender, ASPxDataUpdatedEventArgs e) {
        ASPxGridView gridView = (ASPxGridView)sender;
        for(int i = 0;i < gridView.Columns.Count;i++)
            if(gridView.Columns[i] is GridViewDataColumn) {
                ((GridViewDataColumn)gridView.Columns[i]).EditItemTemplate = null;
                Session["column"] = null;
            }
    }
}

