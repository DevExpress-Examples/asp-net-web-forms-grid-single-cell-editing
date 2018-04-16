Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Reflection
Imports System.Web

Public Class GridDataItem
	Public Property ID() As Integer
	Public Property C1() As Integer
	Public Property C2() As Double
	Public Property C3() As String
	Public Property C4() As Boolean
	Public Property C5() As Date


	Public Shared Function CastPropertyValue(ByVal [property] As PropertyInfo, ByVal value As String) As Object
		If [property] Is Nothing OrElse String.IsNullOrEmpty(value) Then
			Return Nothing
		End If
		If [property].PropertyType.IsEnum Then
			Dim enumType As Type = [property].PropertyType
			If System.Enum.IsDefined(enumType, value) Then
				Return System.Enum.Parse(enumType, value)
			End If
		End If
		If [property].PropertyType Is GetType(Boolean) Then
			Return value = "1" OrElse value = "true" OrElse value = "on" OrElse value = "checked"
		ElseIf [property].PropertyType Is GetType(Uri) Then
			Return New Uri(Convert.ToString(value))
		Else
			Return Convert.ChangeType(value, [property].PropertyType)
		End If
	End Function

	Public Shared Function GetData() As List(Of GridDataItem)
		Dim key = "34FAA431-CF79-4869-9488-93F6AAE81263"
		If HttpContext.Current.Session(key) Is Nothing Then
			HttpContext.Current.Session(key) = Enumerable.Range(0, 100).Select(Function(i) New GridDataItem With {.ID = i, .C1 = i Mod 2, .C2 = i * 0.5 Mod 3, .C3 = "C3 " & i, .C4 = i Mod 2 = 0, .C5 = New Date(2013 + i, 12, 16)}).ToList()
		End If
		Return DirectCast(HttpContext.Current.Session(key), List(Of GridDataItem))
	End Function

	Public Shared Sub UpdateData(ByVal model As GridDataItem)
		Dim item As GridDataItem = GetData().Find(Function(i) i.ID = model.ID)
		item.C1 = model.C1
		item.C2 = model.C2
		item.C3 = model.C3
		item.C4 = model.C4
		item.C5 = model.C5
	End Sub
End Class