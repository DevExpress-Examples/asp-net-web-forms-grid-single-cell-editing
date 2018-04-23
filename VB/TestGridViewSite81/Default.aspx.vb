Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Configuration
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports DevExpress.Web.ASPxEditors
Imports DevExpress.Web.ASPxGridView
Imports System.Drawing
Imports DevExpress.Web.ASPxRoundPanel
Imports DevExpress.Web.Data
Imports DevExpress.Web.ASPxClasses.Internal

Partial Public Class _Default
	Inherits System.Web.UI.Page
	Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
		If ASPxGridView1.IsEditing AndAlso Session("column") IsNot Nothing Then
			Dim fieldName As String = Convert.ToString(Session("column"))
			For i As Integer = 0 To ASPxGridView1.Columns.Count - 1
				Dim column As GridViewDataColumn = CType(ASPxGridView1.Columns(i), GridViewDataColumn)
				If column IsNot Nothing AndAlso column.FieldName <> fieldName Then
					column.EditItemTemplate = New MyTemplate()
				End If
			Next i
		End If
	End Sub
	Protected Sub ASPxGridView1_HtmlDataCellPrepared(ByVal sender As Object, ByVal e As ASPxGridViewTableDataCellEventArgs)
		e.Cell.Attributes.Add("onclick", "onCellClick(" & e.VisibleIndex & ", '" & e.DataColumn.FieldName & "')")
	End Sub
	Protected Sub ASPxGridView1_CustomCallback(ByVal sender As Object, ByVal e As ASPxGridViewCustomCallbackEventArgs)
		Dim gridView As ASPxGridView = CType(sender, ASPxGridView)
		gridView.UpdateEdit()
		Dim data() As String = e.Parameters.Split(New Char() { "|"c })
		gridView.FocusedRowIndex = Convert.ToInt32(data(0))
		For i As Integer = 0 To gridView.Columns.Count - 1
			Dim column As GridViewDataColumn = CType(gridView.Columns(i), GridViewDataColumn)
			If column IsNot Nothing Then
				If column.FieldName <> data(1) Then
					column.EditItemTemplate = New MyTemplate()
				Else
					Session("column") = column.FieldName
					column.EditItemTemplate = Nothing
				End If
			End If
		Next i
		gridView.StartEdit(Convert.ToInt32(data(0)))
	End Sub
	Protected Sub ASPxGridView1_HtmlRowCreated(ByVal sender As Object, ByVal e As ASPxGridViewTableRowEventArgs)
		If e.RowType = GridViewRowType.InlineEdit Then
			Dim fieldName As String
			If Session("column") Is Nothing Then
				fieldName = ""
			Else
				fieldName = Session("column").ToString()
			End If
			For i As Integer = 0 To e.Row.Cells.Count - 1
				Dim column As GridViewDataColumn = (CType((CType(sender, ASPxGridView)).VisibleColumns(i), GridViewDataColumn))
				If column.FieldName <> fieldName Then
					e.Row.Cells(i).Attributes.Add("onclick", "onCellClick(" & e.VisibleIndex & ", '" & column.FieldName & "')")
				End If
			Next i
		End If
	End Sub
	Protected Sub ASPxGridView1_CellEditorInitialize(ByVal sender As Object, ByVal e As ASPxGridViewEditorEventArgs)
		e.Editor.Enabled = Not e.Column.ReadOnly
		If e.Editor.Enabled Then
			CType(e.Editor, ASPxTextBox).ClientSideEvents.KeyPress = "function(s,e) {OnEditorKeyPress(s, e);}"
		End If
	End Sub
	Protected Sub ASPxGridView1_RowUpdated(ByVal sender As Object, ByVal e As ASPxDataUpdatedEventArgs)
		Dim gridView As ASPxGridView = CType(sender, ASPxGridView)
		For i As Integer = 0 To gridView.Columns.Count - 1
			If TypeOf gridView.Columns(i) Is GridViewDataColumn Then
				CType(gridView.Columns(i), GridViewDataColumn).EditItemTemplate = Nothing
				Session("column") = Nothing
			End If
		Next i
	End Sub
End Class

