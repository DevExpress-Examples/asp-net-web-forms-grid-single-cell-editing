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
Imports DevExpress.Web.ASPxGridView

Public Class MyTemplate
	Implements ITemplate

	#Region "ITemplate Members"

	Public Sub InstantiateIn(ByVal container As Control) Implements ITemplate.InstantiateIn
		Dim lc As New LiteralControl()
		lc.ID = "label"
		Dim templateContainer As GridViewEditItemTemplateContainer = TryCast(container, GridViewEditItemTemplateContainer)
		lc.Text = templateContainer.Text
		container.Controls.Add(lc)
	End Sub
	#End Region
End Class