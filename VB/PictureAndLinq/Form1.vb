Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraGrid.Views.Grid
Imports System.Collections

Namespace PictureAndLinq
	Partial Public Class Form1
		Inherits Form
		Public Sub New()
			InitializeComponent()
		End Sub

		Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
			repositoryItemPictureEdit1.PictureStoreMode = DevExpress.XtraEditors.Controls.PictureStoreMode.ByteArray

			Dim dc As New AdventureWorksDataContext()
			Dim bs As New BindingSource(dc.ProductPhotos, "")
			gridControl1.DataSource = bs
		End Sub

		Private Sub gridView1_CustomUnboundColumnData(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs) Handles gridView1.CustomUnboundColumnData
			If e.Column.FieldName = "LargePhoto_Unbound" Then
				Dim view As GridView = CType(sender, GridView)
				Dim dataSource As IList = CType(view.DataSource, IList)
				Dim pf As ProductPhoto = CType(dataSource(e.ListSourceRowIndex), ProductPhoto)
				If pf Is Nothing Then
					Return
				End If
				If e.IsGetData Then
					e.Value = pf.LargePhoto.ToArray()
				ElseIf e.IsSetData Then
					Dim data() As Byte = CType(e.Value, Byte())
					pf.LargePhoto = New System.Data.Linq.Binary(data)
				End If
			End If
		End Sub
	End Class
End Namespace
