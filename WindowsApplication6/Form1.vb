Imports System.Data.SqlClient

Public Class Form1

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Panel1.Visible = False Then
            Panel1.Visible = True

        Else
            Panel1.Visible = False

        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Panel1.Visible = False
    End Sub

    Private Sub ItemsBindingNavigatorSaveItem_Click(sender As Object, e As EventArgs)
        Me.Validate()
        Me.ItemsBindingSource.EndEdit()
        Me.TableAdapterManager.UpdateAll(Me.DbDataSet)

    End Sub
    Dim con As SqlConnection = New SqlConnection("Data Source=EUMA\SQLEXPRESS;Initial Catalog=db;Integrated Security=True")
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'DbDataSet.items' table. You can move, or remove it, as needed.
        Me.ItemsTableAdapter.FillBy1(Me.DbDataSet.items)
        'TODO: This line of code loads data into the 'DbDataSet.items' table. You can move, or remove it, as needed.
        Me.ItemsTableAdapter.FillBy1(Me.DbDataSet.items)

    End Sub

    Private Sub ItemsBindingNavigatorSaveItem_Click_1(sender As Object, e As EventArgs)
        Me.Validate()
        Me.ItemsBindingSource.EndEdit()
        Me.TableAdapterManager.UpdateAll(Me.DbDataSet)

    End Sub

    Private Sub ItemsDataGridView_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles ItemsDataGridView.CellContentClick
        Dim senderGrid = DirectCast(sender, DataGridView)

        If TypeOf senderGrid.Columns(e.ColumnIndex) Is DataGridViewButtonColumn AndAlso
           e.RowIndex >= 0 Then

            If e.ColumnIndex = 4 Then
                ItemsDataGridView.Rows(e.RowIndex).Cells(0).Value.ToString()
                MsgBox(ItemsDataGridView.Rows(e.RowIndex).Cells(1).Value.ToString(), MsgBoxStyle.Exclamation, "Error")
            End If
            If e.ColumnIndex = 5 Then
                'MsgBox(DataGridView1.Rows(e.RowIndex).Cells(2).Value, MsgBoxStyle.Exclamation, "Error")
                con.Open()
                Dim theQuery As String = "DELETE FROM [dbo].[items] WHERE id = @id"
                Dim cmd1 As SqlCommand = New SqlCommand(theQuery, con)
                cmd1.Parameters.AddWithValue("@id", ItemsDataGridView.Rows(e.RowIndex).Cells(0).Value.ToString())

                If cmd1.ExecuteNonQuery > 0 Then
                    MsgBox("Successfully Deleted", MsgBoxStyle.Exclamation, "Process Complete")
                     ItemsBindingSource.DataSource = ItemsTableAdapter.GetData
                    ItemsBindingSource.ResetBindings(False)
                    ItemsDataGridView.Refresh()
                 

                    con.Close()

                Else
                    MsgBox("UY, WA MAN", MsgBoxStyle.Exclamation, "Error")
                    con.Close()


                End If
                con.Close()
            End If




        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        con.Open()

        Dim theQuery As String = "INSERT INTO [dbo].[items] ([name] ,[tagID],[stock]) VALUES(@name, @tagID, @stock )"
        Dim theQuery1 As String = "Select COUNT(*) FROM [dbo].[items] where name =  @item"
        Dim cmd1 As SqlCommand = New SqlCommand(theQuery, con)
        Dim cmd2 As SqlCommand = New SqlCommand(theQuery1, con)
        cmd1.Parameters.AddWithValue("@name", TextBox1.Text)
        cmd2.Parameters.AddWithValue("@item", TextBox1.Text)
        cmd1.Parameters.AddWithValue("@tagID", ComboBox1.SelectedIndex + 1)
        cmd1.Parameters.AddWithValue("@stock", TextBox2.Text)
        If cmd2.ExecuteScalar > 0 Then
            MsgBox("Item is already registered.", MsgBoxStyle.Exclamation, "Error")
            con.Close()

        Else

            If cmd1.ExecuteNonQuery > 0 Then
                MsgBox("Successfully added to database doi", MsgBoxStyle.Exclamation, "Process Complete")
                ItemsBindingSource.DataSource = ItemsTableAdapter.GetData
                ItemsBindingSource.ResetBindings(False)
                ItemsDataGridView.Refresh()
                Panel1.Visible = False
                TextBox1.Text = ""
                TextBox2.Text = ""
                con.Close()

            End If
        End If
    End Sub
End Class
